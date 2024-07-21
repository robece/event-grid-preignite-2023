using Azure;
using Azure.Core.Serialization;
using Azure.Messaging;
using Azure.Messaging.EventGrid.Namespaces;
using Azure.Storage;
using Azure.Storage.Blobs;
using Microsoft.Azure.Relay;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Namespace.PushPull
{
    public partial class frmPushPullWindow : Form
    {
        private Settings? _settings = null;
        private int _idxPublished = 0;
        private int _lstViewFontSize = 17;
        private List<KeyValuePair<string, double>> elements = new List<KeyValuePair<string, double>>
            {
                new KeyValuePair<string, double>("Acknowledged", 0.5),
                new KeyValuePair<string, double>("Released", 0.3),
                new KeyValuePair<string, double>("Rejected", 0.2),
            };
        private static readonly HttpClient client = new HttpClient();
        private HybridConnectionListener? listener = null;
        private HybridConnectionStream? stream = null;

        public frmPushPullWindow()
        {
            InitializeComponent();
            _settings = Utils.GetSettings();
            lblVersion.Text = $"Version: {_settings!.version}";
        }

        private async void frmPushPullWindow_Load(object sender, EventArgs e)
        {
            InitTable();
            await RunRelayAsync();
        }

        private void InitTable()
        {
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, 80);

            var dataSize = (rdbCustomEvents.Checked ? 1200 : 3500);

            lstViewWebhook.Clear();
            lstViewPull.Clear();

            // Webhook

            lstViewWebhook.View = View.Details;
            lstViewWebhook.GridLines = true;
            lstViewWebhook.FullRowSelect = true;
            lstViewWebhook.Scrollable = true;
            lstViewWebhook.Columns.Add("Event State", 400, HorizontalAlignment.Left);
            lstViewWebhook.Columns.Add("Time", 650, HorizontalAlignment.Left);
            lstViewWebhook.Columns.Add("Data", dataSize, HorizontalAlignment.Left);
            lstViewWebhook.SmallImageList = imgList;

            // Event Grid

            lstViewPull.View = View.Details;
            lstViewPull.GridLines = true;
            lstViewPull.FullRowSelect = true;
            lstViewPull.Scrollable = true;
            lstViewPull.Columns.Add("Event State", 400, HorizontalAlignment.Left);
            lstViewPull.Columns.Add("Time", 650, HorizontalAlignment.Left);
            lstViewPull.Columns.Add("Data", dataSize, HorizontalAlignment.Left);
            lstViewPull.SmallImageList = imgList;
        }

        private async Task RunRelayAsync()
        {
            if (_settings == null)
                return;

            var tokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(_settings.relayKeyName, _settings.relayKey);
            listener = new HybridConnectionListener(new Uri(string.Format("sb://{0}/{1}", _settings.relayNamespace, _settings.relayConnectionName)), tokenProvider);

            // Subscribe to the status events.
            listener.Connecting += (o, e) =>
            {
                Console.WriteLine("Connecting");
                lblListener.Text = "Listener connecting!";
            };

            listener.Offline += (o, e) =>
            {
                Console.WriteLine("Offline");
                lblListener.Text = "Listener offline!";
            };

            listener.Online += (o, e) =>
            {
                Console.WriteLine("Online");
                lblListener.Text = "Listener online!";
            };

            // Provide an HTTP request handler
            listener.RequestHandler = async (context) =>
            {
                if (context.Request.HttpMethod == "OPTIONS" && context.Request.Url.PathAndQuery == @"/hybridconn02/api/webhook")
                {
                    var callback = context.Request.Headers["WebHook-Request-Callback"];
                    using HttpResponseMessage response = await client.GetAsync(callback);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);

                    context.Response.StatusCode = HttpStatusCode.OK;
                    context.Response.StatusDescription = "OK";

                    var origin = context.Request.Headers["Webhook-Request-Origin"];
                    context.Response.Headers.Add("Webhook-Allowed-Origin", origin);
                    using (var sw = new StreamWriter(context.Response.OutputStream))
                    {
                        sw.WriteLine("OK");
                    }

                    context.Response.Close();
                }

                if (context.Request.HttpMethod == "POST" && context.Request.Url.PathAndQuery == @"/hybridconn02/api/webhook")
                {
                    using (StreamReader reader = new StreamReader(context.Request.InputStream))
                    {
                        string jsonData = await reader.ReadToEndAsync();
                        var jsonBin = BinaryData.FromString(jsonData);
                        CloudEvent @event = CloudEvent.Parse(jsonBin)!;

                        string strData = string.Empty;
                        if (rdbCustomEvents.Checked)
                        {
                            strData = @event.Data!.ToString();
                            dynamic data = JObject.Parse(strData);
                            strData = $"NotificationType: {data.notificationType}";
                        }
                        else
                        {
                            strData = @event.Data!.ToString();
                            dynamic data = JObject.Parse(strData);
                            strData = $"EventType: {@event.Type}, Url: {data.url}";
                        }

                        LstViewWebhookAddItemSafe("Delivered", @event.Time.ToString(), strData);
                    }

                    context.Response.StatusCode = HttpStatusCode.OK;
                    context.Response.StatusDescription = "OK";
                    context.Response.Close();
                }
            };

            // Opening the listener establishes the control channel to
            // the Azure Relay service. The control channel is continuously 
            // maintained, and is reestablished when connectivity is disrupted.
            await listener.OpenAsync();

            stream = await listener.AcceptConnectionAsync();
        }

        private void timerPublish_Tick(object sender, EventArgs e)
        {
            if (rdbCustomEvents.Checked)
            {
                SendCustomEventsAsync();
            }
            else
            {
                SendBlobFilesAsync();
            }
        }

        private void btnPublishStart_Click(object sender, EventArgs e)
        {
            timerPublish.Start();
            btnStartPublish.Enabled = false;
            btnStopPublish.Enabled = true;
            rdbCustomEvents.Enabled = false;
            rdbSystemEvents.Enabled = false;
            progressBarPublish.Style = ProgressBarStyle.Marquee;
        }

        private void btnPublishStop_Click(object sender, EventArgs e)
        {
            timerPublish.Stop();
            btnStartPublish.Enabled = true;
            btnStopPublish.Enabled = false;
            rdbCustomEvents.Enabled = true;
            rdbSystemEvents.Enabled = true;
            progressBarPublish.Style = ProgressBarStyle.Blocks;
        }

        private void btnPublishClear_Click(object sender, EventArgs e)
        {
            _idxPublished = 0;
            LblPublishedEventsUpdateTextSafe($"{_idxPublished}");
        }

        private void LblPublishedEventsUpdateTextSafe(string? text)
        {
            if (lblPublishedEvents.InvokeRequired)
                lblPublishedEvents.Invoke(new Action(() => lblPublishedEvents.Text = text));
            else
                lblPublishedEvents.Text = text;
        }

        private void LstViewWebhookAddItemSafe(string? eventState, string? time, string? dataPayload)
        {
            if (lstViewWebhook.InvokeRequired)
            {
                lstViewWebhook.Invoke(new Action(() =>
                {
                    ListViewItem entryListItem = lstViewWebhook.Items.Insert(0, eventState);
                    entryListItem.UseItemStyleForSubItems = false;
                    entryListItem.ForeColor = SelectForeColor(eventState);
                    entryListItem.Font = new Font(entryListItem.Font.FontFamily, _lstViewFontSize, FontStyle.Bold);

                    ListViewItem.ListViewSubItem subItem01 = entryListItem.SubItems.Add(time);
                    subItem01.ForeColor = SelectForeColor(eventState);
                    subItem01.Font = new Font(subItem01.Font.FontFamily, _lstViewFontSize, FontStyle.Regular);

                    ListViewItem.ListViewSubItem subItem02 = entryListItem.SubItems.Add(dataPayload);
                    subItem02.ForeColor = SelectForeColor(eventState);
                    subItem02.Font = new Font(subItem02.Font.FontFamily, _lstViewFontSize, FontStyle.Regular);
                }));
            }
            else
            {
                ListViewItem entryListItem = lstViewWebhook.Items.Insert(0, eventState);
                entryListItem.UseItemStyleForSubItems = false;
                entryListItem.ForeColor = SelectForeColor(eventState);
                entryListItem.Font = new Font(entryListItem.Font.FontFamily, _lstViewFontSize, FontStyle.Bold);

                ListViewItem.ListViewSubItem subItem01 = entryListItem.SubItems.Add(time);
                subItem01.ForeColor = SelectForeColor(eventState);
                subItem01.Font = new Font(subItem01.Font.FontFamily, _lstViewFontSize, FontStyle.Regular);

                ListViewItem.ListViewSubItem subItem02 = entryListItem.SubItems.Add(dataPayload);
                subItem02.ForeColor = SelectForeColor(eventState);
                subItem02.Font = new Font(subItem02.Font.FontFamily, _lstViewFontSize, FontStyle.Regular);
            }
        }

        private void LstViewPullAddItemSafe(string? eventState, string? time, string? dataPayload)
        {
            if (lstViewPull.InvokeRequired)
            {
                lstViewPull.Invoke(new Action(() =>
                {
                    ListViewItem entryListItem = lstViewPull.Items.Insert(0, eventState);
                    entryListItem.UseItemStyleForSubItems = false;
                    entryListItem.ForeColor = SelectForeColor(eventState);
                    entryListItem.Font = new Font(entryListItem.Font.FontFamily, _lstViewFontSize, FontStyle.Bold);

                    ListViewItem.ListViewSubItem subItem01 = entryListItem.SubItems.Add(time);
                    subItem01.ForeColor = SelectForeColor(eventState);
                    subItem01.Font = new Font(subItem01.Font.FontFamily, _lstViewFontSize, FontStyle.Regular);

                    ListViewItem.ListViewSubItem subItem02 = entryListItem.SubItems.Add(dataPayload);
                    subItem02.ForeColor = SelectForeColor(eventState);
                    subItem02.Font = new Font(subItem02.Font.FontFamily, _lstViewFontSize, FontStyle.Regular);
                }));
            }
            else
            {
                ListViewItem entryListItem = lstViewPull.Items.Insert(0, eventState);
                entryListItem.UseItemStyleForSubItems = false;
                entryListItem.ForeColor = SelectForeColor(eventState);
                entryListItem.Font = new Font(entryListItem.Font.FontFamily, _lstViewFontSize, FontStyle.Bold);

                ListViewItem.ListViewSubItem subItem01 = entryListItem.SubItems.Add(time);
                subItem01.ForeColor = SelectForeColor(eventState);
                subItem01.Font = new Font(subItem01.Font.FontFamily, _lstViewFontSize, FontStyle.Regular);

                ListViewItem.ListViewSubItem subItem02 = entryListItem.SubItems.Add(dataPayload);
                subItem02.ForeColor = SelectForeColor(eventState);
                subItem02.Font = new Font(subItem02.Font.FontFamily, _lstViewFontSize, FontStyle.Regular);
            }
        }

        private Color SelectForeColor(string? eventState)
        {
            switch (eventState)
            {
                case "Received":
                    return Color.Black;
                case "Delivered":
                    return Color.DarkGoldenrod;
                case "Acknowledged":
                    return Color.Green;
                case "Released":
                    return Color.Blue;
                case "Rejected":
                    return Color.Red;
                default:
                    return Color.Black;
            }
        }

        private async void SendCustomEventsAsync()
        {
            await Task.Factory.StartNew(async () =>
            {
                var myCustomDataSerializer = new JsonObjectSerializer(new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                string topicEndpoint = _settings!.namespaceTopicEndpoint;
                string topicAccessKey = _settings!.namespaceTopicEndpointAccessKey;

                var keyCredential = new AzureKeyCredential(topicAccessKey);
                var senderClient = new EventGridSenderClient(new Uri(topicEndpoint), _settings!.namespaceTopicName, keyCredential);

                Random rnd = new Random();

                string[] prefixValues = { "Vehicle", "User", "Record", "File", "Order" };
                int prefixValuesIndex = rnd.Next(prefixValues.Length);
                string strPrefix = prefixValues[prefixValuesIndex].ToString();

                string[] suffixValues = { "Created", "Automated", "Initiated", "Updated", "Saved", "Deleted" };
                int suffixValuesIndex = rnd.Next(prefixValues.Length);
                string strSuffix = suffixValues[suffixValuesIndex].ToString();

                CloudEvent cloudEvent = new CloudEvent("/source",
                    $"{strPrefix}{strSuffix}",
                    myCustomDataSerializer.Serialize(new CustomModel() { NotificationType = $"{strPrefix}{strSuffix}" }), "application/json", CloudEventDataFormat.Json);

                await senderClient.SendAsync(cloudEvent);
                _idxPublished++;
                LblPublishedEventsUpdateTextSafe($"{_idxPublished}");
            });
        }

        private async void SendBlobFilesAsync()
        {
            await Task.Factory.StartNew(async () =>
            {
                var blobServiceClient = new BlobServiceClient(new Uri(_settings!.storageAccountUri), new StorageSharedKeyCredential(_settings!.storageAccountName, _settings!.storageAccountKey));
                string containerName = _settings!.storageAccountContainerName;

                var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

                bool isExist = containerClient.Exists();
                if (!isExist)
                    containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);

                string localPath = "data";
                Directory.CreateDirectory(localPath);
                string fileName = "quickstart" + Guid.NewGuid().ToString() + ".txt";
                string localFilePath = Path.Combine(localPath, fileName);
                await File.WriteAllTextAsync(localFilePath, "Hello, World!");

                BlobClient blobClient = containerClient.GetBlobClient(fileName);
                await blobClient.UploadAsync(localFilePath, true);
                _idxPublished++;
                LblPublishedEventsUpdateTextSafe($"{_idxPublished}");

                await blobClient.DeleteAsync();
                File.Delete(localFilePath);
                _idxPublished++;
                LblPublishedEventsUpdateTextSafe($"{_idxPublished}");
            });
        }

        private class CustomModel
        {
            public string? NotificationType { get; set; }
        }

        private void btnClearEventHub_Click(object sender, EventArgs e)
        {
            lstViewWebhook.Items.Clear();
        }

        private async void timerPull_Tick(object sender, EventArgs e)
        {
            await Task.Factory.StartNew(async () =>
            {
                string topicEndpoint = _settings!.namespaceTopicEndpoint;
                string topicAccessKey = _settings!.namespaceTopicEndpointAccessKey;

                var keyCredential = new AzureKeyCredential(topicAccessKey);
                var receiverClient = new EventGridReceiverClient(new Uri(topicEndpoint), _settings!.namespaceTopicName, _settings!.namespaceTopicSubscriptionName, keyCredential);

                ReceiveResult receiveResult = await receiverClient.ReceiveAsync(_settings!.namespaceTopicSubscriptionMaxEvents);

                foreach (ReceiveDetails detail in receiveResult.Details)
                {
                    CloudEvent @event = detail.Event;
                    BrokerProperties brokerProperties = detail.BrokerProperties;

                    string strData = string.Empty;
                    if (rdbCustomEvents.Checked)
                    {
                        strData = @event.Data!.ToString();
                        dynamic data = JObject.Parse(strData);
                        strData = $"NotificationType: {data.notificationType}";
                    }
                    else
                    {
                        strData = @event.Data!.ToString();
                        dynamic data = JObject.Parse(strData);
                        strData = $"EventType: {@event.Type}, Url: {data.url}";
                    }

                    LstViewPullAddItemSafe("Received", $"{@event.Time}", $"{strData}");

                    string eventState = RandomizeEventState();

                    var toRelease = new List<string>();
                    var toAcknowledge = new List<string>();
                    var toReject = new List<string>();

                    switch (eventState)
                    {
                        case "Acknowledged":

                            toAcknowledge.Add(brokerProperties.LockToken);
                            AcknowledgeResult acknowledgeResult = await receiverClient.AcknowledgeAsync(toAcknowledge);

                            if (acknowledgeResult.SucceededLockTokens.Count > 0)
                                LstViewPullAddItemSafe("Acknowledged", $"{@event.Time}", $"{strData}");

                            break;

                        case "Released":

                            toRelease.Add(brokerProperties.LockToken);
                            ReleaseResult releaseResult = await receiverClient.ReleaseAsync(toRelease);

                            if (releaseResult.SucceededLockTokens.Count > 0)
                                LstViewPullAddItemSafe("Released", $"{@event.Time}", $"{strData}");

                            break;

                        case "Rejected":

                            toReject.Add(brokerProperties.LockToken);
                            RejectResult rejectResult = await receiverClient.RejectAsync(toReject);

                            if (rejectResult.SucceededLockTokens.Count > 0)
                                LstViewPullAddItemSafe("Rejected", $"{@event.Time}", $"{strData}");

                            break;
                    }
                }
            });
        }

        private string RandomizeEventState()
        {
            Random r = new Random();
            double randomNumber = r.NextDouble();
            double cumulative = 0.0;
            for (int i = 0; i < elements.Count; i++)
            {
                cumulative += elements[i].Value;
                if (randomNumber < cumulative)
                {
                    return elements[i].Key;
                }
            }
            return string.Empty;
        }

        private void btnStartPull_Click(object sender, EventArgs e)
        {
            timerPull.Start();
            btnStartPull.Enabled = false;
            btnStopPull.Enabled = true;
        }

        private void btnStopPull_Click(object sender, EventArgs e)
        {
            timerPull.Stop();
            btnStartPull.Enabled = true;
            btnStopPull.Enabled = false;
        }

        private void btnClearPull_Click(object sender, EventArgs e)
        {
            lstViewPull.Items.Clear();
        }

        private void btnArchitecture_Click(object sender, EventArgs e)
        {
            if (rdbCustomEvents.Checked)
            {
                frmCustomEventsDiagram frmDiagram = new frmCustomEventsDiagram();

                DialogResult result = frmDiagram.ShowDialog();
                if (frmDiagram != null)
                {
                    frmDiagram.Dispose();
                }
            }
            else
            {
                frmSystemEventsDiagram frmDiagram = new frmSystemEventsDiagram();

                DialogResult result = frmDiagram.ShowDialog();
                if (frmDiagram != null)
                {
                    frmDiagram.Dispose();
                }
            }
        }

        private async void frmPushPullWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            //await stream.CloseAsync(new CancellationToken());
            await listener.CloseAsync();
        }

        private void rdbCustomEvents_CheckedChanged(object sender, EventArgs e)
        {
            lblPublish.Text = "Publish custom events to Azure Event Grid Namespace";
            pbCustomEvents.Visible = true;
            pbStorageEvents.Visible = false;

            InitTable();
        }

        private void rdbSystemEvents_CheckedChanged(object sender, EventArgs e)
        {
            lblPublish.Text = "Publish Azure system events to Azure Event Grid Namespace";
            pbCustomEvents.Visible = false;
            pbStorageEvents.Visible = true;

            InitTable();
        }
    }
}

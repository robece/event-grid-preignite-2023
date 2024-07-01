using Azure;
using Azure.Core.Serialization;
using Azure.Messaging;
using Azure.Messaging.EventGrid.Namespaces;
using Microsoft.Azure.Relay;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace PushPull
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
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, 80);

            // Webhook

            lstViewWebhook.View = View.Details;
            lstViewWebhook.GridLines = true;
            lstViewWebhook.FullRowSelect = true;
            lstViewWebhook.Scrollable = true;
            lstViewWebhook.Columns.Add("Event State", 400, HorizontalAlignment.Left);
            lstViewWebhook.Columns.Add("Time", 650, HorizontalAlignment.Left);
            lstViewWebhook.Columns.Add("Data", 900, HorizontalAlignment.Left);
            lstViewWebhook.SmallImageList = imgList;

            // Event Grid

            lstViewPull.View = View.Details;
            lstViewPull.GridLines = true;
            lstViewPull.FullRowSelect = true;
            lstViewPull.Scrollable = true;
            lstViewPull.Columns.Add("Event State", 400, HorizontalAlignment.Left);
            lstViewPull.Columns.Add("Time", 650, HorizontalAlignment.Left);
            lstViewPull.Columns.Add("Data", 900, HorizontalAlignment.Left);
            lstViewPull.SmallImageList = imgList;

            await RunRelayAsync();
        }

        private async Task RunRelayAsync()
        {
            if (_settings == null)
                return;

            var tokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(_settings.relayKeyName, _settings.relayKey);
            listener = new HybridConnectionListener(new Uri(string.Format("sb://{0}/{1}", _settings.relayNamespace, _settings.relayConnectionName)), tokenProvider);

            // Subscribe to the status events.
            listener.Connecting += (o, e) => { 
                Console.WriteLine("Connecting"); 
            };
            
            listener.Offline += (o, e) => { 
                Console.WriteLine("Offline"); 
            };
            
            listener.Online += (o, e) => { 
                Console.WriteLine("Online");
                MessageBox.Show("Listener online!");
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
                        string data = await reader.ReadToEndAsync();
                        var jsonBin = BinaryData.FromString(data);
                        CloudEvent cloudEvent = CloudEvent.Parse(jsonBin)!;

                        LstViewWebhookAddItemSafe("Delivered", cloudEvent.Time.ToString(), cloudEvent.Data!.ToString());
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
            SendAsync();
        }

        private void btnPublishStart_Click(object sender, EventArgs e)
        {
            timerPublish.Start();
            btnStartPublish.Enabled = false;
            btnStopPublish.Enabled = true;
            progressBarPublish.Style = ProgressBarStyle.Marquee;
        }

        private void btnPublishStop_Click(object sender, EventArgs e)
        {
            timerPublish.Stop();
            btnStartPublish.Enabled = true;
            btnStopPublish.Enabled = false;
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

        private async void SendAsync()
        {
            await Task.Factory.StartNew(async () =>
            {
                var myCustomDataSerializer = new JsonObjectSerializer(new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                string topicEndpoint = _settings!.namespaceTopicEndpoint;
                string topicAccessKey = _settings!.namespaceTopicEndpointAccessKey;

                var keyCredential = new AzureKeyCredential(topicAccessKey);
                EventGridClient client = new EventGridClient(new Uri(topicEndpoint), keyCredential);

                Random rnd = new Random();

                string[] prefixValues = { "Vehicle", "User", "Record", "File", "Order" };
                int prefixValuesIndex = rnd.Next(prefixValues.Length);
                string strPrefix = prefixValues[prefixValuesIndex].ToString();

                string[] suffixValues = { "Created", "Automated", "Initiated", "Updated", "Saved", "Deleted" };
                int suffixValuesIndex = rnd.Next(prefixValues.Length);
                string strSuffix = suffixValues[suffixValuesIndex].ToString();

                CloudEvent cloudEvent = new CloudEvent("/source",
                    $"{strPrefix}{strSuffix}",
                    myCustomDataSerializer.Serialize(new CustomModel() { Notification = $"{strPrefix}{strSuffix}" }), "application/json", CloudEventDataFormat.Json);

                await client.PublishCloudEventAsync("topic01", cloudEvent);
                _idxPublished++;
                LblPublishedEventsUpdateTextSafe($"{_idxPublished}");
            });
        }

        private class CustomModel
        {
            public string? Notification { get; set; }
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
                EventGridClient client = new EventGridClient(new Uri(topicEndpoint), keyCredential);

                ReceiveResult receiveResult = await client.ReceiveCloudEventsAsync(_settings!.namespaceTopicName, _settings!.namespaceTopicSubscriptionName, _settings!.namespaceTopicSubscriptionMaxEvents);
                var list = receiveResult.Value.ToList();

                foreach (var item in list)
                {
                    string strData = item.Event.Data!.ToString();
                    LstViewPullAddItemSafe("Received", $"{item.Event.Time}", $"{strData}");

                    List<string> lockTokens = new List<string>();
                    lockTokens.Add(item.BrokerProperties.LockToken);

                    string eventState = RandomizeEventState();

                    AcknowledgeOptions acknowledgeOptions = new AcknowledgeOptions(lockTokens);
                    ReleaseOptions releaseOptions = new ReleaseOptions(lockTokens);
                    RejectOptions rejectOptions = new RejectOptions(lockTokens);

                    switch (eventState)
                    {
                        case "Acknowledged":

                            AcknowledgeResult acknowledgeResult = await client.AcknowledgeCloudEventsAsync(_settings!.namespaceTopicName, _settings!.namespaceTopicSubscriptionName, acknowledgeOptions);

                            if (acknowledgeResult.SucceededLockTokens.Count > 0)
                                LstViewPullAddItemSafe("Acknowledged", $"{item.Event.Time}", $"{strData}");

                            break;

                        case "Released":

                            ReleaseResult releaseResult = await client.ReleaseCloudEventsAsync(_settings!.namespaceTopicName, _settings!.namespaceTopicSubscriptionName, releaseOptions);

                            if (releaseResult.SucceededLockTokens.Count > 0)
                                LstViewPullAddItemSafe("Released", $"{item.Event.Time}", $"{strData}");

                            break;

                        case "Rejected":

                            RejectResult rejectResult = await client.RejectCloudEventsAsync(_settings!.namespaceTopicName, _settings!.namespaceTopicSubscriptionName, rejectOptions);

                            if (rejectResult.SucceededLockTokens.Count > 0)
                                LstViewPullAddItemSafe("Rejected", $"{item.Event.Time}", $"{strData}");

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
            frmArchitecture frmArchitecture = new frmArchitecture();

            DialogResult result = frmArchitecture.ShowDialog();
            if (frmArchitecture != null)
            {
                frmArchitecture.Dispose();
            }
        }

        private async void frmPushPullWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            //await stream.CloseAsync(new CancellationToken());
            await listener.CloseAsync();
        }
    }
}

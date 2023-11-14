using Azure;
using Azure.Core.Serialization;
using Azure.Messaging;
using Azure.Messaging.EventGrid.Namespaces;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using Azure.Storage.Blobs;
using System.Text;
using System.Text.Json;

namespace PushPull
{
    public partial class frmPushPullWindow : Form
    {
        private Settings? _settings = null;
        private int _idxPublished = 0;
        private EventProcessorClient? _eventProcessorClient;
        private int _lstViewFontSize = 17;

        public frmPushPullWindow()
        {
            InitializeComponent();
            _settings = Utils.GetSettings();
            lblVersion.Text = $"Version: {_settings!.version}";
        }

        private void frmPushPullWindow_Load(object sender, EventArgs e)
        {
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, 80);

            // Event Hub 

            lstViewEventHub.View = View.Details;
            lstViewEventHub.GridLines = true;
            lstViewEventHub.FullRowSelect = true;
            lstViewEventHub.Scrollable = true;
            lstViewEventHub.Columns.Add("Event Data", 2500, HorizontalAlignment.Left);
            lstViewEventHub.SmallImageList = imgList;

            string storageConnectionString = _settings!.processorStorageConnectionString;
            BlobContainerClient storageClient = new BlobContainerClient(storageConnectionString, _settings!.processorStorageContainer);
            _eventProcessorClient = new EventProcessorClient(storageClient, EventHubConsumerClient.DefaultConsumerGroupName, _settings!.processorConnectionString, _settings!.processorHub);
            _eventProcessorClient.ProcessEventAsync += ProcessEventHandler;
            _eventProcessorClient.ProcessErrorAsync += ProcessErrorHandler;

            // Event Grid

            lstViewPull.View = View.Details;
            lstViewPull.GridLines = true;
            lstViewPull.FullRowSelect = true;
            lstViewPull.Scrollable = true;
            lstViewPull.Columns.Add("Event State", 200, HorizontalAlignment.Left);
            lstViewPull.Columns.Add("Time", 350, HorizontalAlignment.Left);
            lstViewPull.Columns.Add("Data", 500, HorizontalAlignment.Left);
            lstViewPull.SmallImageList = imgList;

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

        private void LstViewEventHubAddItemSafe(string? dataPayload)
        {
            if (lstViewEventHub.InvokeRequired)
            {
                lstViewEventHub.Invoke(new Action(() =>
                {
                    ListViewItem entryListItem = lstViewEventHub.Items.Insert(0, dataPayload);
                    entryListItem.UseItemStyleForSubItems = false;
                    entryListItem.Font = new Font(entryListItem.Font.FontFamily, _lstViewFontSize, FontStyle.Regular);
                }));
            }
            else
            {
                ListViewItem entryListItem = lstViewEventHub.Items.Insert(0, dataPayload);
                entryListItem.UseItemStyleForSubItems = false;
                entryListItem.Font = new Font(entryListItem.Font.FontFamily, _lstViewFontSize, FontStyle.Regular);
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
                    myCustomDataSerializer.Serialize(new CustomModel() { Notification = $"{strPrefix}{strSuffix}" }), "application/json");

                await client.PublishCloudEventAsync("topic01", cloudEvent);
                _idxPublished++;
                LblPublishedEventsUpdateTextSafe($"{_idxPublished}");
            });
        }

        private class CustomModel
        {
            public string? Notification { get; set; }
        }

        private async void btnStartEventHub_Click(object sender, EventArgs e)
        {
            await _eventProcessorClient!.StartProcessingAsync();
            btnStartEventHub.Enabled = false;
            btnStopEventHub.Enabled = true;
        }

        private async void btnStopEventHub_Click(object sender, EventArgs e)
        {
            await _eventProcessorClient!.StopProcessingAsync();
            btnStartEventHub.Enabled = true;
            btnStopEventHub.Enabled = false;
        }

        private void btnClearEventHub_Click(object sender, EventArgs e)
        {
            lstViewEventHub.Items.Clear();
        }

        private Task ProcessEventHandler(ProcessEventArgs eventArgs)
        {
            string json = Encoding.UTF8.GetString(eventArgs.Data.Body.ToArray());

            if (!json.ToLower().Contains("locktoken"))
                LstViewEventHubAddItemSafe(json);

            return Task.CompletedTask;
        }

        private Task ProcessErrorHandler(ProcessErrorEventArgs eventArgs)
        {
            Console.WriteLine($"\tPartition '{eventArgs.PartitionId}': an unhandled exception was encountered. This was not expected to happen.");
            Console.WriteLine(eventArgs.Exception.Message);
            Console.ReadLine();
            return Task.CompletedTask;
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

                    switch (eventState)
                    {
                        case "Acknowledged":

                            AcknowledgeResult acknowledgeResult = await client.AcknowledgeCloudEventsAsync(_settings!.namespaceTopicName, _settings!.namespaceTopicSubscriptionName, lockTokens);

                            if (acknowledgeResult.SucceededLockTokens.Count > 0)
                                LstViewPullAddItemSafe("Acknowledged", $"{item.Event.Time}", $"{strData}");

                            break;

                        case "Released":

                            ReleaseResult releaseResult = await client.ReleaseCloudEventsAsync(_settings!.namespaceTopicName, _settings!.namespaceTopicSubscriptionName, lockTokens);

                            if (releaseResult.SucceededLockTokens.Count > 0)
                                LstViewPullAddItemSafe("Released", $"{item.Event.Time}", $"{strData}");

                            break;

                        case "Rejected":

                            RejectResult rejectResult = await client.RejectCloudEventsAsync(_settings!.namespaceTopicName, _settings!.namespaceTopicSubscriptionName, lockTokens);

                            if (rejectResult.SucceededLockTokens.Count > 0)
                                LstViewPullAddItemSafe("Rejected", $"{item.Event.Time}", $"{strData}");

                            break;
                    }
                }
            });
        }

        private string RandomizeEventState()
        {
            List<KeyValuePair<string, double>> elements = new List<KeyValuePair<string, double>>
            {
                new KeyValuePair<string, double>("Acknowledged", 0.5),
                new KeyValuePair<string, double>("Released", 0.3),
                new KeyValuePair<string, double>("Rejected", 0.2),
            };

            Random r = new Random();
            double diceRoll = r.NextDouble();

            double cumulative = 0.0;
            for (int i = 0; i < elements.Count; i++)
            {
                cumulative += elements[i].Value;
                if (diceRoll < cumulative)
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
    }
}

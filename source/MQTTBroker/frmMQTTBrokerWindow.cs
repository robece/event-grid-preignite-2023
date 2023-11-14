using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Formatter;
using System.Security.Cryptography.X509Certificates;

namespace MQTTBroker
{
    public partial class frmMQTTBrokerWindow : Form
    {
        private Settings? _settings = null;
        private int _idxPublished = 0;
        private int _lstViewFontSize = 17;
        private MqttFactory? _mqttFactory = null;
        private IMqttClient? _mqttClient = null;
        private MqttClientOptions? _mqttClientOptions = null;

        public frmMQTTBrokerWindow()
        {
            InitializeComponent();
            _settings = Utils.GetSettings();
            lblVersion.Text = $"Version: {_settings!.version}";
        }

        [Obsolete]
        private void frmPushPullWindow_Load(object sender, EventArgs e)
        {
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, 80);

            // Event Grid - MQTT Broker

            lstViewMQTTBroker.View = View.Details;
            lstViewMQTTBroker.GridLines = true;
            lstViewMQTTBroker.FullRowSelect = true;
            lstViewMQTTBroker.Scrollable = true;
            lstViewMQTTBroker.Columns.Add("Message State", 200, HorizontalAlignment.Left);
            lstViewMQTTBroker.Columns.Add("Time", 350, HorizontalAlignment.Left);
            lstViewMQTTBroker.Columns.Add("Message", 1500, HorizontalAlignment.Left);
            lstViewMQTTBroker.SmallImageList = imgList;

            _mqttFactory = new MqttFactory();
            _mqttClient = _mqttFactory.CreateMqttClient();
            var caCert = X509Certificate.CreateFromCertFile(_settings!.rootCertificateFileName);
            var clientCertTemp = X509Certificate2.CreateFromPemFile(_settings.clientCertificateFileName, _settings.clientCertificateKeyFileName);

            var clientCert = new X509Certificate2(clientCertTemp.Export(X509ContentType.Pkcs12));

            var certificates = new List<X509Certificate>
                            {
                                caCert,
                                clientCert
                            };

            var mqttTlsOptions = new MqttClientOptionsBuilderTlsParameters()
            {
                AllowUntrustedCertificates = true,
                IgnoreCertificateChainErrors = true,
                IgnoreCertificateRevocationErrors = true,
                UseTls = true,
                Certificates = certificates,
            };

            string clientId = _settings.clientId;
            _mqttClientOptions = new MqttClientOptionsBuilder()
                .WithClientId($"{clientId}")
                .WithTcpServer(_settings.mqttEndpoint, _settings.mqttPort)
                .WithCredentials($"{clientId}", "")
                .WithTls(mqttTlsOptions)
                .WithCleanSession()
                .WithProtocolVersion(MqttProtocolVersion.V500)
                .WithKeepAlivePeriod(TimeSpan.FromSeconds(60))
                .Build();
        }

        private void timerPublish_Tick(object sender, EventArgs e)
        {
            SendAsync();
        }

        private async void btnPublishStart_Click(object sender, EventArgs e)
        {
            // Event Grid - MQTT Broker

            var cancellationToken = new CancellationTokenSource(20000);
            try
            {
                var response = await _mqttClient!.ConnectAsync(_mqttClientOptions, cancellationToken.Token);
                LstViewMQTTBrokerAddItemSafe(response.ResultCode.ToString(), DateTime.Now.ToString("yyyy/MM/dd/ hh:mm:ss"), $"Client connected: {_mqttClient.IsConnected}");
            }
            catch (Exception ex)
            {
                LstViewMQTTBrokerAddItemSafe("Exception", DateTime.Now.ToString("yyyy/MM/dd/ hh:mm:ss"), $"{ex.Message}");
            }

            timerPublish.Start();
            btnStartPublish.Enabled = false;
            btnStopPublish.Enabled = true;
            progressBarPublish.Style = ProgressBarStyle.Marquee;
        }

        private async void btnPublishStop_Click(object sender, EventArgs e)
        {
            // Event Grid - MQTT Broker

            if (_mqttClient!.IsConnected)
                await _mqttClient.DisconnectAsync();

            timerPublish.Stop();
            btnStartPublish.Enabled = true;
            btnStopPublish.Enabled = false;
            progressBarPublish.Style = ProgressBarStyle.Blocks;
        }

        private void btnPublishClear_Click(object sender, EventArgs e)
        {
            _idxPublished = 0;
            LblPublishedEventsUpdateTextSafe($"{_idxPublished}");

            lstViewMQTTBroker.Items.Clear();
        }

        private void LblPublishedEventsUpdateTextSafe(string? text)
        {
            if (lblPublishedEvents.InvokeRequired)
                lblPublishedEvents.Invoke(new Action(() => lblPublishedEvents.Text = text));
            else
                lblPublishedEvents.Text = text;
        }

        private void LstViewMQTTBrokerAddItemSafe(string? eventState, string? time, string? dataPayload)
        {
            if (lstViewMQTTBroker.InvokeRequired)
            {
                lstViewMQTTBroker.Invoke(new Action(() =>
                {
                    ListViewItem entryListItem = lstViewMQTTBroker.Items.Insert(0, eventState);
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
                ListViewItem entryListItem = lstViewMQTTBroker.Items.Insert(0, eventState);
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
                case "Success":
                    return Color.Green;
                case "Exception":
                    return Color.Red;
                default:
                    return Color.Black;
            }
        }

        private void SendAsync()
        {
            Task.Run(async () =>
            {
                try
                {
                    var topic = $"{_settings!.mqttBrokertopic}";

                    Random rnd = new Random();
                    double res = 0f;
                    res = rnd.Next(65, 80);
                    var value = res.ToString();

                    var applicationMessage = new MqttApplicationMessageBuilder()
                    .WithTopic(topic)
                    .WithPayload(value)
                    .WithPayloadFormatIndicator(MQTTnet.Protocol.MqttPayloadFormatIndicator.CharacterData)
                    .Build();

                    if (!_mqttClient!.IsConnected)
                        await _mqttClient!.ReconnectAsync();

                    await _mqttClient!.PublishAsync(applicationMessage, CancellationToken.None).ContinueWith(
                        (b) =>
                        {
                            LstViewMQTTBrokerAddItemSafe(b.Result.ReasonCode.ToString(), DateTime.Now.ToString("yyyy/MM/dd/ hh:mm:ss"), $"Data sent: {value}, topic destination: {topic}.");
                            _idxPublished++;
                            LblPublishedEventsUpdateTextSafe($"{_idxPublished}");
                        });
                }
                catch (Exception ex)
                {
                    LstViewMQTTBrokerAddItemSafe("Exception", DateTime.Now.ToString("yyyy/MM/dd/ hh:mm:ss"), $"{ex.Message}");
                }
            });
        }

        private async void frmMQTTBrokerWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_mqttClient!.IsConnected)
            {
                await _mqttClient.DisconnectAsync();
                _mqttClient = null;
                _mqttFactory = null;
            }
        }
    }
}

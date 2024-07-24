using Azure;
using Azure.Core.Serialization;
using Azure.Messaging;
using Azure.Messaging.EventGrid.Namespaces;
using System.Text.Json;
using WatsonWebserver;
using WatsonWebserver.Core;

namespace Benchmark.StorageQueueAndPull.Sender
{
    internal class Program
    {
        private static Settings? _settings = null;
        private static Webserver? _webServer = null;
        private static bool _enable = false;
        private static int _idxPublished = 0;

        private static void InitWebServer()
        {
            WebserverSettings settings = new WebserverSettings();
            settings.Hostname = "localhost";
            settings.Port = 8000;

            _webServer = new Webserver(settings, async (HttpContextBase ctx) =>
            {
                string resp = "Hello from Benchmark.StorageQueueAndPull.Sender app!";
                ctx.Response.StatusCode = 200;
                ctx.Response.ContentLength = resp.Length;
                ctx.Response.ContentType = "text/plain";
                await ctx.Response.Send(resp);
                return;
            });

            _webServer.Routes.PreAuthentication.Static.Add(WatsonWebserver.Core.HttpMethod.POST, _settings!.webServerProcessStartPath, async (HttpContextBase ctx) =>
            {
                _enable = true;
                Console.WriteLine("Task enabled");
                string response = "OK";
                ctx.Response.ContentType = "text/plain";
                ctx.Response.ContentLength = response.Length;
                await ctx.Response.Send(response);
                return;
            });

            _webServer.Routes.PreAuthentication.Static.Add(WatsonWebserver.Core.HttpMethod.POST, _settings!.webServerProcessStopPath, async (HttpContextBase ctx) =>
            {
                _enable = false;
                Console.WriteLine("Task disabled");
                string response = "OK";
                ctx.Response.ContentType = "text/plain";
                ctx.Response.ContentLength = response.Length;
                await ctx.Response.Send(response);
                return;
            });

            _webServer.Routes.PreAuthentication.Static.Add(WatsonWebserver.Core.HttpMethod.POST, _settings!.webServerProcessInfoPath, async (HttpContextBase ctx) =>
            {
                Console.WriteLine("Task info");
                string response = $"Total processed: {_idxPublished}";
                ctx.Response.ContentType = "text/plain";
                ctx.Response.ContentLength = response.Length;
                await ctx.Response.Send(response);
                return;
            });

            _webServer.Start();
        }

        static void Main(string[] args)
        {
            _settings = Utils.GetSettings();
            InitWebServer();

            var task = Task.Factory.StartNew(async () =>
            {
                Console.WriteLine("Task started");
                while (true)
                {
                    if (_enable)
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
                            myCustomDataSerializer.Serialize(new CustomNotificationModel() { NotificationType = $"{strPrefix}{strSuffix}" }), "application/json", CloudEventDataFormat.Json);

                        try
                        {
                            senderClient.SendAsync(cloudEvent).GetAwaiter();
                            _idxPublished++;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            });

            try
            {
                task.Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

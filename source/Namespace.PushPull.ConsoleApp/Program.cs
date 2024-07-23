using System.Text;
using System.Net;

namespace Namespace.PushPull.ConsoleApp
{
    internal class Program
    {
        public static HttpListener? listener;
        public static string url = "http://localhost:8000/";
        public static int requestCount = 0;
        
        private static readonly HttpClient httpClient = new HttpClient();

        public static async Task HandleIncomingConnections()
        {
            bool runServer = true;

            while (runServer)
            {
                HttpListenerContext ctx = await listener!.GetContextAsync();
                HttpListenerRequest httpListenerRequest = ctx.Request;
                HttpListenerResponse httpListenerResponse = ctx.Response;

                Console.WriteLine("Request #: {0}", requestCount++);
                Console.WriteLine(httpListenerRequest.Url.ToString());
                Console.WriteLine(httpListenerRequest.HttpMethod);
                Console.WriteLine(httpListenerRequest.UserHostName);
                Console.WriteLine(httpListenerRequest.UserAgent);
                Console.WriteLine();

                if ((httpListenerRequest.HttpMethod == "OPTIONS") && (httpListenerRequest.Url.AbsolutePath == "/hybridconn02/console/webhook"))
                {
                    var callback = httpListenerRequest.Headers["WebHook-Request-Callback"];
                    using HttpResponseMessage response = await httpClient.GetAsync(callback);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    
                    var origin = httpListenerRequest.Headers["Webhook-Request-Origin"];
                    httpListenerResponse.Headers.Add("Webhook-Allowed-Origin", origin);

                    httpListenerResponse.StatusCode = (int)HttpStatusCode.OK;
                    httpListenerResponse.StatusDescription = "OK";
                    httpListenerResponse.Close();

                    Console.WriteLine("Cloud Events handshake requested");                    
                }

                if ((httpListenerRequest.HttpMethod == "POST") && (httpListenerRequest.Url.AbsolutePath == "/hybridconn02/console/webhook"))
                {
                    httpListenerResponse.StatusCode = (int)HttpStatusCode.OK;
                    httpListenerResponse.StatusDescription = "OK";
                    httpListenerResponse.Close();
                }
            }
        }

        public static void Main(string[] args)
        {            
            listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();
            Console.WriteLine("Listening for connections on {0}", url);

            Task listenTask = HandleIncomingConnections();
            listenTask.GetAwaiter().GetResult();
            
            listener.Close();
        }
    }
}
namespace TelnetServer.ConsoleApp
{
    using Azure.Messaging;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;
    using WatsonWebserver;
    using WatsonWebserver.Core;

    internal enum EClientState
    {
        NotLogged = 0,
        Logging = 1,
        LoggedIn = 2,
        Subscribe = 3
    }

    internal class Client
    {
        public IPEndPoint remoteEndPoint;
        public DateTime connectedAt;
        public EClientState clientState;
        public string commandIssued = string.Empty;

        public Client(IPEndPoint _remoteEndPoint, DateTime _connectedAt, EClientState _clientState)
        {
            this.remoteEndPoint = _remoteEndPoint;
            this.connectedAt = _connectedAt;
            this.clientState = _clientState;
        }
    }

    internal class Program
    {
        private static Socket serverSocket;
        private static byte[] data = new byte[dataSize];
        private static bool newClients = true;
        private const int dataSize = 1024;
        private static Dictionary<Socket, Client> clientList = new Dictionary<Socket, Client>();
        private static Webserver? _webServer = null;
        private static int _idxPushed = 0;
        private static Stack<string> stack = new Stack<string>(5);

        static void Main(string[] args)
        {
            InitTelnetServer();
            InitWebServer();
        }

        #region initialize

        private static void InitTelnetServer()
        {
            Console.WriteLine("Starting Telnet Server...");
            new Thread(new ThreadStart(backgroundThread)) { IsBackground = false }.Start();
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 23);
            serverSocket.Bind(endPoint);
            serverSocket.Listen(0);
            serverSocket.BeginAccept(new AsyncCallback(AcceptConnection), serverSocket);
            Console.WriteLine("Telnet Server socket listening to upcoming connections.");
        }

        private static void InitWebServer()
        {
            WebserverSettings settings = new WebserverSettings();
            settings.Hostname = "localhost";
            settings.Port = 8000;

            _webServer = new Webserver(settings, async (HttpContextBase ctx) =>
            {
                string resp = "Hello from TelnetServer.ConsoleApp app!";
                ctx.Response.StatusCode = 200;
                ctx.Response.ContentLength = resp.Length;
                ctx.Response.ContentType = "text/plain";
                await ctx.Response.Send(resp);
                return;
            });

            _webServer.Routes.PreAuthentication.Static.Add(WatsonWebserver.Core.HttpMethod.OPTIONS, "/api/webhook", async (HttpContextBase ctx) =>
            {
                ctx.Response.StatusCode = 200;
                var origin = ctx.Request.Headers["Webhook-Request-Origin"];
                ctx.Response.Headers.Add("Webhook-Allowed-Origin", origin);

                string response = "OK";
                ctx.Response.ContentType = "text/plain";
                ctx.Response.ContentLength = response.Length;
                await ctx.Response.Send(response);
                return;
            });

            _webServer.Routes.PreAuthentication.Static.Add(WatsonWebserver.Core.HttpMethod.POST, "/api/webhook", async (HttpContextBase ctx) =>
            {
                using (StreamReader reader = new StreamReader(ctx.Request.Data))
                {
                    string jsonData = await reader.ReadToEndAsync();
                    var jsonBin = BinaryData.FromString(jsonData);
                    CloudEvent @event = CloudEvent.Parse(jsonBin)!;

                    string strData = string.Empty;
                    strData = @event.Data!.ToString();
                    dynamic data = JObject.Parse(strData);
                    strData = $"Event Id: {@event.Id}\n\rTime: {@event.Time}\n\rEventType: {@event.Type}\n\r";
                    _idxPushed++;

                    int clientNumber = 0;
                    foreach (KeyValuePair<Socket, Client> client in clientList)
                    {
                        Client currentClient = client.Value;
                        clientNumber++;
                        Console.WriteLine(string.Format("Client #{0} (From: {1}:{2}, ECurrentState: {3}, Connection time: {4})", clientNumber,
                            currentClient.remoteEndPoint.Address.ToString(), currentClient.remoteEndPoint.Port, currentClient.clientState, currentClient.connectedAt));

                        Socket socket = client.Key;
                        Client clientSocket = client.Value;

                        if (clientSocket.clientState == EClientState.Subscribe)
                        {
                            AddItemToStack(strData, stack);
                            string table = PrintStack(stack);

                            byte[] message = Encoding.ASCII.GetBytes("\u001B[1J\u001B[H" + table);
                            socket.BeginSend(message, 0, message.Length, SocketFlags.None, new AsyncCallback(SendData), clientSocket);
                            serverSocket.BeginAccept(new AsyncCallback(AcceptConnection), serverSocket);
                        }
                    }
                }

                string response = "OK";
                ctx.Response.ContentType = "text/plain";
                ctx.Response.ContentLength = response.Length;
                await ctx.Response.Send(response);
                return;
            });

            _webServer.Start();
        }

        private static void AddItemToStack(string item, Stack<string> stack)
        {
            if (!string.IsNullOrEmpty(item))
                stack.Push(item);

            if (stack.Count > 10)
            {
                string[] items = stack.ToArray();
                Array.Resize(ref items, items.Length - 1);
                Array.Reverse(items);
                stack.Clear();

                foreach (string s in items)
                    stack.Push(s);
            }
        }

        private static string PrintStack(Stack<string> stack)
        {
            string result = string.Empty;
            List<string> list = new List<string>(stack.ToArray());
            //list.Reverse();
            foreach (string item in list)
            {
                result += item;
            }

            return result;
        }

        #endregion

        private static void backgroundThread()
        {
            while (true)
            {
                string Input = Console.ReadLine();

                if (Input == "clients")
                {
                    if (clientList.Count == 0) continue;
                    int clientNumber = 0;
                    foreach (KeyValuePair<Socket, Client> client in clientList)
                    {
                        Client currentClient = client.Value;
                        clientNumber++;
                        Console.WriteLine(string.Format("Client #{0} (From: {1}:{2}, ECurrentState: {3}, Connection time: {4})", clientNumber,
                            currentClient.remoteEndPoint.Address.ToString(), currentClient.remoteEndPoint.Port, currentClient.clientState, currentClient.connectedAt));
                    }
                }

                if (Input.StartsWith("kill"))
                {
                    string[] _Input = Input.Split(' ');
                    int clientID = 0;
                    try
                    {
                        if (Int32.TryParse(_Input[1], out clientID) && clientID >= clientList.Keys.Count)
                        {
                            int currentClient = 0;
                            foreach (Socket currentSocket in clientList.Keys.ToArray())
                            {
                                currentClient++;
                                if (currentClient == clientID)
                                {
                                    currentSocket.Shutdown(SocketShutdown.Both);
                                    currentSocket.Close();
                                    clientList.Remove(currentSocket);
                                    Console.WriteLine("Client has been disconnected and cleared up.");
                                }
                            }
                        }
                        else { Console.WriteLine("Could not kick client: invalid client number specified."); }
                    }
                    catch { Console.WriteLine("Could not kick client: invalid client number specified."); }
                }

                if (Input == "killall")
                {
                    int deletedClients = 0;
                    foreach (Socket currentSocket in clientList.Keys.ToArray())
                    {
                        currentSocket.Shutdown(SocketShutdown.Both);
                        currentSocket.Close();
                        clientList.Remove(currentSocket);
                        deletedClients++;
                    }

                    Console.WriteLine("{0} clients have been disconnected and cleared up.", deletedClients);
                }

                if (Input == "lock") { newClients = false; Console.WriteLine("Refusing new connections."); }
                if (Input == "unlock") { newClients = true; Console.WriteLine("Accepting new connections."); }
            }
        }

        private static void AcceptConnection(IAsyncResult result)
        {
            if (!newClients) return;
            Socket oldSocket = (Socket)result.AsyncState;
            Socket newSocket = oldSocket.EndAccept(result);
            Client client = new Client((IPEndPoint)newSocket.RemoteEndPoint, DateTime.Now, EClientState.NotLogged);
            clientList.Add(newSocket, client);
            Console.WriteLine("Client connected. (From: " + string.Format("{0}:{1}", client.remoteEndPoint.Address.ToString(), client.remoteEndPoint.Port) + ")");
            string output = "** Azure Event Grid Telnet Server **\n\r\n\r";
            output += "Please input your password:\n\r";
            client.clientState = EClientState.Logging;
            byte[] message = Encoding.ASCII.GetBytes(output);
            newSocket.BeginSend(message, 0, message.Length, SocketFlags.None, new AsyncCallback(SendData), newSocket);
            serverSocket.BeginAccept(new AsyncCallback(AcceptConnection), serverSocket);
        }

        private static void SendData(IAsyncResult result)
        {
            try
            {
                Socket clientSocket = (Socket)result.AsyncState;
                clientSocket.EndSend(result);
                clientSocket.BeginReceive(data, 0, dataSize, SocketFlags.None, new AsyncCallback(ReceiveData), clientSocket);
            }
            catch { }
        }

        private static void ReceiveData(IAsyncResult result)
        {
            try
            {
                Socket clientSocket = (Socket)result.AsyncState;
                Client client;
                clientList.TryGetValue(clientSocket, out client);
                int received = clientSocket.EndReceive(result);
                if (received == 0)
                {
                    clientSocket.Close();
                    clientList.Remove(clientSocket);
                    serverSocket.BeginAccept(new AsyncCallback(AcceptConnection), serverSocket);
                    Console.WriteLine("Client disconnected. (From: " + string.Format("{0}:{1}", client.remoteEndPoint.Address.ToString(), client.remoteEndPoint.Port) + ")");
                    return;
                }

                Console.WriteLine("Received '{0}' (From: {1}:{2})", BitConverter.ToString(data, 0, received), client.remoteEndPoint.Address.ToString(), client.remoteEndPoint.Port);

                // 0x2E & 0X0D => return/intro
                if (data[0] == 0x2E && data[1] == 0x0D && client.commandIssued.Length == 0)
                {
                    string currentCommand = client.commandIssued;
                    Console.WriteLine(string.Format("Received '{0}' while EClientStatus '{1}' (From: {2}:{3})", currentCommand, client.clientState.ToString(), client.remoteEndPoint.Address.ToString(), client.remoteEndPoint.Port));
                    client.commandIssued = "";
                    byte[] message = Encoding.ASCII.GetBytes("\u001B[1J\u001B[H" + HandleCommand(clientSocket, currentCommand));
                    clientSocket.BeginSend(message, 0, message.Length, SocketFlags.None, new AsyncCallback(SendData), clientSocket);
                }

                else if (data[0] == 0x0D && data[1] == 0x0A)
                {
                    string currentCommand = client.commandIssued;
                    Console.WriteLine(string.Format("Received '{0}' (From: {1}:{2}", currentCommand, client.remoteEndPoint.Address.ToString(), client.remoteEndPoint.Port));
                    client.commandIssued = "";
                    byte[] message = Encoding.ASCII.GetBytes("\u001B[1J\u001B[H" + HandleCommand(clientSocket, currentCommand));
                    clientSocket.BeginSend(message, 0, message.Length, SocketFlags.None, new AsyncCallback(SendData), clientSocket);
                }

                else
                {
                    // 0x08 => remove character
                    if (data[0] == 0x08)
                    {
                        if (client.commandIssued.Length > 0)
                        {
                            client.commandIssued = client.commandIssued.Substring(0, client.commandIssued.Length - 1);
                            byte[] message = Encoding.ASCII.GetBytes("\u0020\u0008");
                            clientSocket.BeginSend(message, 0, message.Length, SocketFlags.None, new AsyncCallback(SendData), clientSocket);
                        }
                        else
                        {
                            clientSocket.BeginReceive(data, 0, dataSize, SocketFlags.None, new AsyncCallback(ReceiveData), clientSocket);
                        }
                    }
                    // 0x7F => delete character
                    else if (data[0] == 0x7F)
                    {
                        clientSocket.BeginReceive(data, 0, dataSize, SocketFlags.None, new AsyncCallback(ReceiveData), clientSocket);
                    }
                    else
                    {
                        string currentCommand = client.commandIssued;
                        client.commandIssued += Encoding.ASCII.GetString(data, 0, received);
                        clientSocket.BeginReceive(data, 0, dataSize, SocketFlags.None, new AsyncCallback(ReceiveData), clientSocket);
                    }
                }
            }
            catch { }
        }

        private static string HandleCommand(Socket clientSocket, string Input)
        {
            string Output = "** Azure Event Grid Telnet Server **\n\r\n\r";
            byte[] dataInput = Encoding.ASCII.GetBytes(Input);
            Client client;
            clientList.TryGetValue(clientSocket, out client);

            if (client.clientState == EClientState.Logging)
            {
                if (Input == "123")
                {
                    Console.WriteLine("Client has logged in (correct password), marking as logged...");
                    client.clientState = EClientState.LoggedIn;
                    Output += "Logged successfully.\n\r";
                    Output += "Please enter a valid command:\n\r";
                }
                else
                {
                    Console.WriteLine("Client login failed (incorrect password).");
                    Output += "Incorrect password. Please input your password: ";
                }
            }
            if (client.clientState == EClientState.LoggedIn)
            {
                if (Input == "subscribe")
                {
                    client.clientState = EClientState.Subscribe;
                    Output += "Waiting events...\n\r";
                }
            }

            if (client.clientState == EClientState.Subscribe)
            {
                if (Input == "s")
                {
                    client.clientState = EClientState.LoggedIn;
                    Output += "Please enter a valid command:\n\r";
                }
            }

            return Output;
        }
    }
}
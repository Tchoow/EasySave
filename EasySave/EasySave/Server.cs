using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace EasySave
{
    public class Server
    {
        private const string serverAdr = "127.0.0.1";
        private const int serverPort = 20167;
        private Socket _serverSocket;


        public Socket serverSocket
        {
            get { return _serverSocket; }
            set { _serverSocket = value; }
        }


        public Server()
        {

        }
        public Socket Initialize()
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress iPAddress = IPAddress.Parse(serverAdr);

            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, serverPort);
            socket.Bind(iPEndPoint);

            socket.Listen(1);
            Console.WriteLine($"Serveur en écoute sur {iPEndPoint}");

            return socket;
        }
        public Socket AcceptConnexion(Socket socketServ)
        {
            Socket socketClient = socketServ.Accept();
            Trace.WriteLine($"Nouvelle connexion : {socketClient.RemoteEndPoint.ToString()}");
            
            return socketClient;
        }
        public void SendJobs(string name, string state, int progression)
        {
            byte[] bufferSend = Encoding.ASCII.GetBytes("update====" + name + "====" + state + "====" + progression);
            if(_serverSocket != null) { _serverSocket.Send(bufferSend); }
        }
        public (string result, List<Job> jobs) ListenNetwork(List<Job> jobs)
        {
            
                byte[] buffer = new byte[8192];
                int bytesRead;
                string msgtosend = "";
                try
                {
                bytesRead = this._serverSocket.Receive(buffer);
                
                    if (bytesRead > 0)
                    {
                        string messageRead = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    string[] result = messageRead.Split("====");
                    switch (result[0])
                    {
                        case "getjoblist":
                            msgtosend = JsonConvert.SerializeObject(jobs, Formatting.Indented);
                            break;
                        case "playjob":
                            return ("play",JsonConvert.DeserializeObject<List<Job>>(result[1]));
                            break;
                        //run job index
                        case "pausejob":
                            return ("pause", JsonConvert.DeserializeObject<List<Job>>(result[1]));
                            break;
                        case "stopjob":
                            return ("stop", JsonConvert.DeserializeObject<List<Job>>(result[1]));
                            break;
                                //pausejob
                            default:
                            return (null,null);
                                break;
                        }
                    msgtosend = JsonConvert.SerializeObject(jobs, Formatting.Indented);
                    byte[] bufferReponse = Encoding.ASCII.GetBytes(msgtosend);
                    this.serverSocket.Send(bufferReponse);
                    return (null, null);
                    }
                return (null, null);
                }
                catch (Exception)
                {
                    return (null,null);
                }            
        }
        public void CloseSocket()
        {
            this.serverSocket.Shutdown(SocketShutdown.Both);
            this.serverSocket.Close();
        }
    }
}

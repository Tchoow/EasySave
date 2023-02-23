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
        public void ListenNetwork(List<Job> jobs)
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
                    
                        switch (messageRead)
                        {
                            case "getjoblist":
                                msgtosend = JsonConvert.SerializeObject(jobs, Formatting.Indented);
                                break;
                            case "playjob":
                                break;
                            //run job index
                            case "pausejob":
                                break;
                            case "stopjob":
                                break;
                                //pausejob
                            default:
                                msgtosend = JsonConvert.SerializeObject(jobs, Formatting.Indented);
                                break;
                        }
                        byte[] bufferReponse = Encoding.ASCII.GetBytes(msgtosend);
                        Trace.WriteLine("send");
                        this.serverSocket.Send(bufferReponse);
                    }
                }
                catch (Exception)
                {
                    return;
                }            
        }
        public void CloseSocket()
        {
            this.serverSocket.Shutdown(SocketShutdown.Both);
            this.serverSocket.Close();
        }
    }
}

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
        public void ListenNetwork(Socket serv, List<Job> jobs)
        {
            byte[] buffer = new byte[8192];
            int bytesRead;
                try
                {
                    bytesRead = serv.Receive(buffer);
                    if (bytesRead > 0)
                    {
                        string messageRead = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                        Trace.WriteLine("Client : " + messageRead);
                        string msgtosend = JsonConvert.SerializeObject(jobs, Formatting.Indented);
                        byte[] bufferReponse = Encoding.ASCII.GetBytes(msgtosend);
                        serv.Send(bufferReponse);
                    }
                }
                catch (Exception)
                {
                    return;
                }
        }
        public void CloseSocket(Socket serv)
        {
            serv.Shutdown(SocketShutdown.Both);
            serv.Close();
        }
    }
}

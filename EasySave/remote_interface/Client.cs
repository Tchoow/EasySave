using EasySave;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace remote_interface
{
    class Client
    {
        private IPAddress ipadr;
        private int servport;
        public Client(IPAddress ip, int serverport)
        {
            this.ipadr = ip;
            this.servport = serverport;
        }
        public Socket SeConnecter()
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint serverEndPoint = new IPEndPoint(this.ipadr, this.servport);

            socket.Connect(serverEndPoint);

            return socket;
        }
        public List<Job> ListenNetwork(Socket client)
        {
            string messageReponse = "les jobs en cours";
            byte[] bufferReponse = Encoding.ASCII.GetBytes(messageReponse);
            Trace.WriteLine(bufferReponse);
            client.Send(bufferReponse);
            int bytesRead;
            try
            {
                byte[] buffer = new byte[8192]; ;
                bytesRead = client.Receive(buffer);
                if (bytesRead > 0)
                {
                    string messageRead = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    List<Job> receivedJob = JsonConvert.DeserializeObject<List<Job>>(messageRead);// désérialisez la chaîne en un objet de la classe Job                  
                    return receivedJob;
                }
            }
            catch (Exception)
            {
               
            }
            return new List<Job>();
            
        }
        public void CloseSocket(Socket client)
        {
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }
    }
}

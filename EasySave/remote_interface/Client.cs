using EasySave;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace Remote_interface
{
    class Client
    {
        private IPAddress ipadr;
        private int servport;
        private Socket _connectedSocket { get; set; }

        public Socket connectedSocket
        {
            get { return _connectedSocket; }
            set { _connectedSocket = value; }
        }

        public Client(IPAddress ip, int serverport)
        {
            this.ipadr = ip;
            this.servport = serverport;
            _connectedSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public void SeConnecter()
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint serverEndPoint = new IPEndPoint(this.ipadr, this.servport);

            socket.Connect(serverEndPoint);

            this.connectedSocket = socket;
        }
        public dynamic ListenNetwork()
        {
            int bytesRead;
            try
            {
                byte[] buffer = new byte[8192]; ;
                bytesRead = connectedSocket.Receive(buffer);
                if (bytesRead > 0)
                {
                    string messageRead = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    if (messageRead.StartsWith("update"))
                    {
                        string[] message = messageRead.Split("====");
                        Trace.WriteLine(message);
                        Trace.WriteLine(message.GetType());
                        return message;
                    }
                    else
                    {
                        List<Job> receivedJob = JsonConvert.DeserializeObject<List<Job>>(messageRead);// désérialisez la chaîne en un objet de la classe Job                  
                        return receivedJob;
                    }
                }
            }
            catch (Exception)
            {
               return null;
            }
            return null;
            
        }
        public void CloseSocket()
        {
            _connectedSocket.Shutdown(SocketShutdown.Both);
            _connectedSocket.Close();
        }
        public void Request(string message, List<Job> jobs)
        {
            byte[] bufferSend = Encoding.ASCII.GetBytes(message + "====" + JsonConvert.SerializeObject(jobs, Formatting.Indented));
            _connectedSocket.Send(bufferSend);
        }
    }
}

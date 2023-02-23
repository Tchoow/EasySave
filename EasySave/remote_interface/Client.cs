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
        public List<Job> ListenNetwork()
        {
            int bytesRead;
            try
            {
                byte[] buffer = new byte[8192]; ;
                bytesRead = connectedSocket.Receive(buffer);
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

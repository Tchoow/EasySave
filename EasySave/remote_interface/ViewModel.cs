using EasySave;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace Remote_interface
{
    class ViewModel
    {
        private Client client;


        public void SetClient(IPAddress ip, int port){ client = new Client(ip, port); }
        public void connectClient() { client.SeConnecter(); }
        public Socket getClientConnectedSocket() { return client.connectedSocket; }
        public dynamic ListenNetwork() { return client.ListenNetwork(); }
        public void CloseSocket() { client.CloseSocket(); }
        public void SendMessage(string message, List<Job> jobs) { client.Request(message, jobs); }
        public bool IsConnected() { 
            if(client != null && client.connectedSocket != null)
            {
                return true;
            }
            return false;
        }
    }
}

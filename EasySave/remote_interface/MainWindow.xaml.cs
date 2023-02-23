using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Sockets;
using System.Text;
using System.Net;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;

namespace Remote_interface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel viewModel;
        private Thread receive_thread;
        private Thread send_thread;
        private List<EasySave.Job> jobs;

        public MainWindow()
        {
            InitializeComponent();
            Closing += MainWindow_Closing;
            viewModel = new ViewModel();
        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //client.CloseSocket(clientSocket);
            if(viewModel.IsConnected())
            {
                viewModel.CloseSocket();
            }
        }

        private void Button_Click_connect(object sender, RoutedEventArgs e)
        {
            
            IPAddress ipadr;
            int port;
            bool isValidIP = IPAddress.TryParse(this.ipinput.Text, out ipadr);
            bool isValidPort = int.TryParse(this.portinput.Text, out port);
            Trace.WriteLine(isValidIP);
            Trace.WriteLine(isValidPort);
            if (isValidIP && isValidPort) 
            {
                ipadr = IPAddress.Parse(this.ipinput.Text);
            }
            else
            {
                MessageBox.Show("Adresse ip ou port incorrect, veuillez vérifier vos les champs", "Error", MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            //client = new Client(ipadr,Convert.ToInt32(this.portinput.Text));
            viewModel.SetClient(ipadr, port);
            try
            {
                viewModel.connectClient();
            }
            catch (Exception)
            {
                MessageBox.Show("Problème lors de la connexion au serveur","Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show("Connexion réussi", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            receive_thread = new Thread(() =>
            {
                while (true)
                {
                    List<EasySave.Job> jobs = new List<EasySave.Job>(viewModel.ListenNetwork());

                    // Mettre à jour la DataGrid sur le thread de l'interface utilisateur
                    Dispatcher.Invoke(() => {
                        jobdatagrid.ItemsSource = new List<EasySave.Job>(jobs);
                    });
                }
            });

            receive_thread.IsBackground = true;
            receive_thread.Start();

            viewModel.SendMessage("joblist");
        }


        private void Button_Click_play(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_pause(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_stop(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_refresh(object sender, RoutedEventArgs e)
        {
            //client.Request(clientSocket, "getjoblist");
        }
    }
}

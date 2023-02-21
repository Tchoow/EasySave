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
using System.Diagnostics;
using EasySave;
using System.Threading;
using System.Net.Sockets;

namespace EasySaveGUI
{

    public partial class MainWindow : Window
    {
        private Frame ContentFrame;
        private ViewModel viewModel;
        private Server serv;
        Thread serverThread;


        public MainWindow()
        {
            Mutex myMutex;
            bool aIsNewInstance = false;
            myMutex = new Mutex(true, "MyWPFApplication", out aIsNewInstance);
            if (!aIsNewInstance)
            {
                MessageBox.Show("Already an instance is running...");
                App.Current.Shutdown();
            }
            viewModel = new ViewModel(this);
            InitializeComponent();
            this.ContentFrame = (Frame)FindName("CFrame");
            this.ContentFrame.Content = new PageHome();
            this.viewModel = new ViewModel(this);
            serv = new Server();

            this.serverThread = new Thread(() => { 
                while (true)
                {
                    Socket servsocket = serv.Initialize();
                    Socket accepted = serv.AcceptConnexion(servsocket);
                    serv.ListenNetwork(accepted,viewModel.getJobsList());
                    serv.CloseSocket(servsocket);
                    serv.CloseSocket(accepted);
                }
                
            });
            serverThread.IsBackground = true;
            serverThread.Start();
        }

        private void btnJob(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Content = new PageJob(viewModel);
        }

        private void btnHome(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Content = new PageHome();
        }

        private void btnLang(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Content = new PageLang();
        }

        private void btnAbout(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Content = new PageAbout();
        }

        private void btnHelp(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Content = new PageHelp();
        }

        private void btnLogs(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Content = new PageLogs(this.viewModel);
        }

        private void btnExec(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Content = new PageExec(viewModel);
        }
    }
}

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
using System.IO;
using System.Net.Sockets;

namespace EasySaveGUI
{

    public partial class MainWindow : Window
    {
        private Frame ContentFrame;
        private ViewModel viewModel;
        Thread serverThread;
        private string frameName;
        private PageExec pageExec;


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
            viewModel.setLangueIndex(comboLanguage.SelectedIndex);
            this.UpdateTrad();

            this.serverThread = new Thread(() => {
                viewModel.RunServer();
                while (true)
                {
                    viewModel.ServerListen();
                    /*Socket servsocket = serv.Initialize();
                    Socket accepted = serv.AcceptConnexion(servsocket);
                    serv.ListenNetwork(accepted, viewModel.getJobsList());
                    serv.CloseSocket(servsocket);
                    serv.CloseSocket(accepted);*/
                }

            });
            serverThread.IsBackground = true;
            serverThread.Start();
        }
        private void UpdateTrad()
        {
            JobBtn.Content   = viewModel.getTraduction("JobMainWindow");
            ExecBtn.Content  = viewModel.getTraduction("ExecutionMainWindow");
            LogBtn.Content   = viewModel.getTraduction("LogsMainWindow");
            SettBtn.Content  = viewModel.getTraduction("SettingsMainWindow");
            HelpBtn.Content  = viewModel.getTraduction("HelpMainWindow");
            Aboutbtn.Content = viewModel.getTraduction("AboutMainWindow");
        }


        public void acceptObserver(string name, string state, int progression)
        {
            this.pageExec.updateInfos(name, state, progression);   
        }



        private void btnJob(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Content = new PageJob(viewModel);
            frameName = "Jobs";
        }

        private void btnHome(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Content = new PageHome();
        }

        private void btnSett(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Content = new PageSett(this.viewModel);
            frameName = "Sett";
        }

        private void btnAbout(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Content = new PageAbout(this.viewModel);
            frameName = "About";
        }

        private void btnHelp(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Content = new PageHelp(this.viewModel);
            frameName = "Help";
        }

        private void btnLogs(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Content = new PageLogs(this.viewModel);
            frameName = "Logs";
        }

        private void btnExec(object sender, RoutedEventArgs e)
        {
            this.pageExec = new PageExec(viewModel);
            this.ContentFrame.Content = this.pageExec;
            frameName = "Exec";
        }

        private void comboLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.setLangueIndex(comboLanguage.SelectedIndex);
            UpdateTrad();
            if (this.ContentFrame != null)
            {
                switch (frameName)
                {
                    case "Jobs":
                        this.ContentFrame.Content = new PageJob(viewModel);
                        break;
                    case "Sett":
                        this.ContentFrame.Content = new PageSett(this.viewModel);
                        break;
                    case "About":
                        this.ContentFrame.Content = new PageAbout(this.viewModel);
                        break;
                    case "Help":
                        this.ContentFrame.Content = new PageHelp(this.viewModel);
                        break;
                    case "Logs":
                        this.ContentFrame.Content = new PageLogs(this.viewModel);
                        break;
                    case "Exec":
                        this.pageExec = new PageExec(viewModel);
                        this.ContentFrame.Content = this.pageExec;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

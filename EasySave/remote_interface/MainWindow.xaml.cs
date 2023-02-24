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
        private List<EasySave.Job> jobSelected;

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
                    var jobs = viewModel.ListenNetwork();
                if (jobs is List<EasySave.Job>)
                {
                    Dispatcher.Invoke(() => {
                        jobdatagrid.ItemsSource = new List<EasySave.Job>(jobs);
                    });
                }
                else if (jobs is string[] && jobs.Length <= 4)
                {
                        Dispatcher.Invoke(() =>
                        {
                            // Get jobs with item source
                            List<EasySave.Job> tempJobs = (List<EasySave.Job>)jobdatagrid.ItemsSource;

                            // Update list with new datas
                            int index = tempJobs.FindIndex((job) => job.Name == jobs[1]);
                            tempJobs[index].State = jobs[2];
                            tempJobs[index].Progression = Convert.ToInt32(jobs[3]);

                            // Set the List to Item Source
                            jobdatagrid.ItemsSource = tempJobs;
                            jobdatagrid.Items.Refresh();
                        });
                    }
                      
                    
                    
                    // Mettre à jour la DataGrid sur le thread de l'interface utilisateur
                }
            });

            receive_thread.IsBackground = true;
            receive_thread.Start();

            viewModel.SendMessage("getjoblist", null);
        }


        private void Button_Click_play(object sender, RoutedEventArgs e)
        {
            if(jobSelected != null)
            {
                viewModel.SendMessage("playjob", jobSelected);
            }
            else
            {
                MessageBox.Show("Select job");
            }
        }

        private void Button_Click_pause(object sender, RoutedEventArgs e)
        {
            if (jobSelected.Count > 0)
            {
                viewModel.SendMessage("pausejob", jobSelected);
            }
            else
            {
                MessageBox.Show("Select job");
            }
        }

        private void Button_Click_stop(object sender, RoutedEventArgs e)
        {
            if (jobSelected.Count > 0)
            {
                viewModel.SendMessage("stopjob", jobSelected);
            }
            else
            {
                MessageBox.Show("Select job");
            }
        }

        private void jobdatagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            jobSelected = new List<EasySave.Job>();
            foreach (EasySave.Job item in jobdatagrid.SelectedItems)
            {
                jobSelected.Add(item);
            }
        }
    }
}

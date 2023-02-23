using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
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
using EasySave;

namespace EasySaveGUI
{
    public partial class PageExec : Page
    {
        private List<Job> jobs;
        private List<Job> jobSelected;
        private ViewModel viewModel;
        bool wantCrypt;

        public PageExec(ViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            jobs = viewModel.getJobsList();
            Trace.WriteLine(jobs);
            jobdatagrid.ItemsSource = viewModel.getJobsList();
            UpdateTrad();

        }
        private void UpdateTrad()
        {
            execalljob.Content = viewModel.getTraduction("execalljob");
            execselectedjob.Content = viewModel.getTraduction("execselectedjob");
            cryptbtn.Text = viewModel.getTraduction("cryptactive");
            extensionslabel.Text = viewModel.getTraduction("extensionscrypt");
            name.Header = viewModel.getTraduction("name");
            srcfile.Header = viewModel.getTraduction("fromdir");
            destfile.Header = viewModel.getTraduction("todir");
            state.Header = viewModel.getTraduction("state");
            ttlfilecop.Header = viewModel.getTraduction("ttlfilcop");
            Progr.Header = viewModel.getTraduction("progr");
            created.Header = viewModel.getTraduction("creadate");
            stopselectedjob.Content = viewModel.getTraduction("stopjob");
        }
        private void runJobs(List<Job> jobs)
        {
            string[] extensions;
            if (jobs != null)
            {
                if (wantCrypt == true)
                {
                    if (extension_text.Text != "")
                    {
                        Trace.WriteLine("chiffre extens");
                        extensions = extension_text.Text.Split(",");
                        this.viewModel.executeJobs(jobs, extensions);
                    }
                    else
                    {
                        Trace.WriteLine("chiffre tout");
                        this.viewModel.executeJobs(jobs, new string[] { "" });
                    }

                }
                else
                {
                    Trace.WriteLine("no chiff");
                    extensions = new string[] { ".psdfg" };
                    viewModel.executeJobs(jobs, extensions);
                }
            }
        }

        private void pauseJobs(List<Job> jobs)
        {
            this.viewModel.pauseJobs(jobs);
        }

        public void updateInfos(string name, string state, int progression)
        {
            Dispatcher.Invoke(() =>
            {
                // Get jobs with item source
                List<Job> tempJobs = (List<Job>)jobdatagrid.ItemsSource;

                // Update list with new datas
                int index = tempJobs.FindIndex((job) => job.Name == name);
                tempJobs[index].State = state;
                tempJobs[index].Progression = progression;

                // Set the List to Item Source
                jobdatagrid.ItemsSource = tempJobs;
                jobdatagrid.Items.Refresh();
            });
        }


        private void stopJobs(List<Job> jobs)
        {
            this.viewModel.stopJobs(jobs);
        }

        private void exec_selectedJobs_btn(object sender, RoutedEventArgs e)
        {
            runJobs(jobSelected);

            
        }

        private void pause_sekectedJobs_btn(object sender, RoutedEventArgs e)
        {
            pauseJobs(jobSelected);
        }

        private void stop_sekectedJobs_btn(object sender, RoutedEventArgs e)
        {
            stopJobs(jobSelected);
        }



        private void chiffrement_Checked(object sender, RoutedEventArgs e)
        {
            if(chiff_check.IsChecked == true)
            {
                wantCrypt = true;
                extension_text.IsEnabled = true;
            }
            else
            {
                wantCrypt = false;
                extension_text.IsEnabled = false;
                this.extension_text.Text = "";
            }
            
        }

        private void execAll_btn(object sender, RoutedEventArgs e)
        {
            runJobs(jobs);
        }

        private void jobdatagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            jobSelected = new List<Job>();
            foreach (Job item in jobdatagrid.SelectedItems)
            {
                jobSelected.Add(item);
            }
        }
    }
}
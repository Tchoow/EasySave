using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using EasySave;

namespace EasySaveGUI
{
    /// <summary>
    /// Logique d'interaction pour PageExec.xaml
    /// </summary>
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
            jobdatagrid.ItemsSource = viewModel.getJobsList();

        }

        private void exec_selectedJobs_btn(object sender, RoutedEventArgs e)
        {
            jobSelected = new List<Job>();
            foreach (Job item in jobdatagrid.Items)
            {
                if (item.IsSelect == true)
                {
                    item.IsSelect = false;
                    jobSelected.Add(item);
                }
            }
            if(wantCrypt == true)
            {
                string[] extensions = extension_text.Text.Split(",");
                Trace.WriteLine(extensions);
               viewModel.executeJobs(jobSelected, extensions);
            }
            viewModel.executeJobs(jobSelected, new string[] {" "});

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
            viewModel.executeJobs(jobs, new string[] {" "});
        }
    }
}
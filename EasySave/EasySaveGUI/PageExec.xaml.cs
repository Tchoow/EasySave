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
            UpdateTrad();

        }
        private void UpdateTrad()
        {
            execalljob.Content = viewModel.getTraduction("execalljob");
            execselectedjob.Content = viewModel.getTraduction("execselectedjob");
            cryptbtn.Text = viewModel.getTraduction("cryptactive");
            extensionslabel.Text = viewModel.getTraduction("extensionscrypt");
            statesave.Text = viewModel.getTraduction("statesave");
            name.Header = viewModel.getTraduction("name");
            srcfile.Header = viewModel.getTraduction("fromdir");
            destfile.Header = viewModel.getTraduction("todir");
            savetype.Header = viewModel.getTraduction("savetype");
            state.Header = viewModel.getTraduction("state");
            ttlfilecop.Header = viewModel.getTraduction("ttlfilcop");
            ttlfilesiz.Header = viewModel.getTraduction("ttlfilsiz");
            filesleft.Header = viewModel.getTraduction("filleft");
            Progr.Header = viewModel.getTraduction("progr");
            created.Header = viewModel.getTraduction("creadate");
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
                        viewModel.executeJobs(jobs, extensions);
                    }
                    else
                    {
                        Trace.WriteLine("chiffre tout");
                        viewModel.executeJobs(jobs, new string[] { "" });
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
        private void exec_selectedJobs_btn(object sender, RoutedEventArgs e)
        {
            runJobs(jobSelected);
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
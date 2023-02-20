using EasySave;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace EasySaveGUI
{
    public partial class PageJob : Page
    {
        List<Job> jobs;
        ViewModel viewModel;
        
        int index = 0;
        
        public PageJob(ViewModel viewModel)
        {
            this.viewModel = viewModel;
            InitializeComponent();
            reloadGrid();
            UpdateTrad();
        }
        public void UpdateTrad()
        {
            clearInputs.Content = viewModel.getTraduction("clearinputsjob");
            updateJobButton.Content = viewModel.getTraduction("savejob");
            deleteButton.Content = viewModel.getTraduction("deletejob");
            newJobButton.Content = viewModel.getTraduction("newjob");
            openDest.Content = viewModel.getTraduction("open");
            openSrc.Content = viewModel.getTraduction("open");
            full.Content = viewModel.getTraduction("full");
            diff.Content = viewModel.getTraduction("diff");
            job.Text = viewModel.getTraduction("JobMainWindow");
            lblName.Text = viewModel.getTraduction("name");
            SrcPath.Text = viewModel.getTraduction("fromdir");
            DestPath.Text = viewModel.getTraduction("todir");
            SavTyp.Text = viewModel.getTraduction("savetype");

        }
        public void reloadGrid()
        {
            this.jobs = viewModel.getJobsList();
            myDataGrid.ItemsSource = jobs;
            index = -1;
        }

        public void clearGrid()
        {
            NameTB.Text = "";
            DestinationPathTB.Text = "";
            SourcePathTB.Text = "";
            SaveTypeTB.SelectedIndex = -1;
        }

        private void myDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(myDataGrid.SelectedIndex != -1)
            {
                int index = myDataGrid.SelectedIndex;
                this.index = index;
                NameTB.Text = jobs[index].Name;
                SourcePathTB.Text = jobs[index].SourceFilePath;
                DestinationPathTB.Text = jobs[index].DestinationFilePath;
                SaveTypeTB.SelectedIndex = jobs[index].SaveType - 1;
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (myDataGrid.SelectedIndex != - 1)
            {
                this.viewModel.deleteJobWithIndex(index + 1);
                reloadGrid();
                clearGrid();
            }
        }

        private void clearInputsButton_Click(object sender, RoutedEventArgs e)
        {
            this.index = -1;
            SaveTypeTB.SelectedIndex = -1;
            NameTB.Text = "";
            SourcePathTB.Text = "";
            DestinationPathTB.Text = "";
        }

        private void newJobButton_Click(object sender, RoutedEventArgs e)
        {
            Job job = new Job(NameTB.Text, SourcePathTB.Text, DestinationPathTB.Text, SaveTypeTB.SelectedIndex + 1, "Paused");
            this.viewModel.addNewJob(job);
            reloadGrid();
        }

        private void updateJobButton_Click(object sender, RoutedEventArgs e)
        {
            if(index == -1) return;
            Job job = new Job(NameTB.Text, SourcePathTB.Text, DestinationPathTB.Text, SaveTypeTB.SelectedIndex + 1, "Paused");
            this.viewModel.updateJob(job, index);
            this.jobs = this.viewModel.getJobsList();
            reloadGrid();
        }

        private void OpenFileDialog_Source(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = SourcePathTB.Text != "" ? SourcePathTB.Text : "C:\\";
            bool? result = openFileDialog.ShowDialog();
            if(result == true)
            {
                SourcePathTB.Text = System.IO.Path.GetDirectoryName(openFileDialog.FileName);
            }
        }

        private void OpenFileDialog_Dest(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = DestinationPathTB.Text != "" ? DestinationPathTB.Text : "C:\\";
            bool? result = openFileDialog.ShowDialog();
            if (result == true)
            {
                DestinationPathTB.Text = System.IO.Path.GetDirectoryName(openFileDialog.FileName);
            }
        }
    }
}

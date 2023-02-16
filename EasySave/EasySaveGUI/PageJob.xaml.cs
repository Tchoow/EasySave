using EasySave;
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
        }

        public void reloadGrid()
        {
            this.jobs = viewModel.getJobsList();
            myDataGrid.ItemsSource = jobs;
            index = -1;
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
            if(myDataGrid.SelectedIndex != - 1)
            {
                this.viewModel.deleteJobWithIndex(index + 1);
                reloadGrid();
            }
        }

        private void newJobButton_Click(object sender, RoutedEventArgs e)
        {
            Job job = new Job(NameTB.Text, SourcePathTB.Text, DestinationPathTB.Text, SaveTypeTB.SelectedIndex + 1, "Paused");
            this.viewModel.addNewJob(job);
            reloadGrid();
        }

        private void updateJobButton_Click(object sender, RoutedEventArgs e)
        {
            if(index == -1]) return;
            Job job = new Job(NameTB.Text, SourcePathTB.Text, DestinationPathTB.Text, SaveTypeTB.SelectedIndex + 1, "Paused");
            this.viewModel.updateJob(job, index);
            this.jobs = this.viewModel.getJobsList();
            reloadGrid();
        }
    }
}

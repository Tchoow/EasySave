using EasySave;
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
            this.jobs = viewModel.getJobsList();
            myDataGrid.ItemsSource = jobs;

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
                this.viewModel.deleteJobWithIndex(index);
            }
        }

        private void newJobButton_Click(object sender, RoutedEventArgs e)
        {
            Job job = new Job(NameTB.Text, SourcePathTB.Text, DestinationPathTB.Text, SaveTypeTB.SelectedIndex + 1, "PAUSED");
            viewModel.addNewJob(job);
        }

        private void updateJobButton_Click(object sender, RoutedEventArgs e)
        {
            Job job = new Job(NameTB.Text, SourcePathTB.Text, DestinationPathTB.Text, SaveTypeTB.SelectedIndex + 1, "PAUSED");
            viewModel.updateJob(job, index);
        }
    }
}

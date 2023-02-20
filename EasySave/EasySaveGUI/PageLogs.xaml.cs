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
using System.IO;

namespace EasySaveGUI
{
    public partial class PageLogs : Page
    {
        private ViewModel viewModel;
        private int logIndex;
        private List<Log> lstLogs;
        FileInfo[] logsFileInfos;

        public PageLogs(ViewModel viewModel)
        {
            InitializeComponent();
            JSON_XML_TB.Visibility = Visibility.Collapsed;
            this.viewModel = viewModel;
            this.logIndex  = 0;


            List<LogsDataFiles> datas   = new List<LogsDataFiles>();
            this.logsFileInfos = this.viewModel.getLogsFiles();

            for(int i = 0; i < this.logsFileInfos.Length; i++)
            {
                datas.Add(new LogsDataFiles { Id = i, Names = this.logsFileInfos[i].Name, Date = this.logsFileInfos[i].CreationTime.ToString() });
            }
            LogsGridFiles.ItemsSource = datas;
        }
 
        public void btnPreview(object sender, RoutedEventArgs e)
        {
            JSON_XML_TB.Visibility = Visibility.Collapsed;
            LogsGridContent.Visibility = Visibility.Visible;

            // Get log file info with log file in parameter
            this.lstLogs = this.viewModel.getLogsLst(this.logsFileInfos[this.logIndex].Name);
            List<LogsDataContent> datas = new List<LogsDataContent>();
            for (int i = 0; i < this.lstLogs.Count; i++)
            {
                datas.Add(new LogsDataContent { Id = i, Names = this.lstLogs[i].Name, SourcePath = this.lstLogs[i].FileSource, DesPath = this.lstLogs[i].DestPath, Date = this.lstLogs[i].Time.ToString() }) ;
            }

            LogsGridContent.ItemsSource = datas;
        }

        public void btnXML(object sender, RoutedEventArgs e)
        {
            JSON_XML_TB.Visibility = Visibility.Visible;
            LogsGridContent.Visibility = Visibility.Collapsed;
            this.lstLogs = this.viewModel.getLogsLst(this.logsFileInfos[this.logIndex].Name);
            string filePath = logsFileInfos[logIndex].FullName;
            string fileName = System.IO.Path.GetFileNameWithoutExtension(filePath);
            string dName = System.IO.Path.GetDirectoryName(filePath);
            string formattedXML = this.viewModel.getLogsXML(this.lstLogs[logIndex], dName+"\\"+fileName+".xml");
            JSON_XML_TB.Text = formattedXML;
        }
        public void btnJSON(object sender, RoutedEventArgs e)
        {
            JSON_XML_TB.Visibility = Visibility.Visible;
            LogsGridContent.Visibility = Visibility.Collapsed;
            this.lstLogs = this.viewModel.getLogsLst(this.logsFileInfos[this.logIndex].Name);
            string formattedJSON = this.viewModel.getLogsJSON(this.lstLogs[logIndex], logsFileInfos[logIndex].FullName);
            Trace.WriteLine(this.logsFileInfos[this.logIndex].Name);
            JSON_XML_TB.Text = formattedJSON;
        }

        private void LogsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LogsGridFiles.SelectedItem != null)
            {
                int index         = LogsGridFiles.SelectedIndex;
                this.logIndex     = index;
            }
        }
    }
    class LogsDataContent
    {
        public int Id { get; set; }
        public string Names { get; set; }
        public string SourcePath { get; set; }
        public string DesPath { get; set; }
        public string Date { get; set; }
    }

    class LogsDataFiles
    {
        public int Id { get; set; }
        public string Names { get; set; }
        public string Date { get; set; }
    }
}
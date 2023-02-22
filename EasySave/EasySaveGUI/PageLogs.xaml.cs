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
            UpdateTrad();

            this.logsFileInfos = this.viewModel.getLogsFiles();
            LogsGridFiles.ItemsSource = this.logsFileInfos;
        }

        public void UpdateTrad()
        {
            prev.Content = viewModel.getTraduction("preview");
            log.Text = viewModel.getTraduction("LogsMainWindow");
            name.Header = viewModel.getTraduction("name");
            lastmod.Header = viewModel.getTraduction("lastmod");
            dir.Header = viewModel.getTraduction("dir");
            id.Header = viewModel.getTraduction("ID");
            nameh.Header = viewModel.getTraduction("name");
            srcpath.Header = viewModel.getTraduction("fromdir");
            destpath.Header = viewModel.getTraduction("todir");
            date.Header = viewModel.getTraduction("creadate");
        }

        public void btnPreview(object sender, RoutedEventArgs e)
        {
            JSON_XML_TB.Visibility = Visibility.Collapsed;
            LogsGridContent.Visibility = Visibility.Visible;

            // Get log file info with log file in parameter
            this.lstLogs = this.viewModel.getLogsLst(this.logsFileInfos[this.logIndex].Name);
            List<LogsDataContent> datas = new List<LogsDataContent>();
            if (lstLogs == null || lstLogs.Count == 0) return;
            for (int i = 0; i < this.lstLogs.Count; i++)
            {
                datas.Add(new LogsDataContent { Id = i, Names = this.lstLogs[i].Name, SourcePath = this.lstLogs[i].FileSource, DesPath = this.lstLogs[i].DestPath, Date = this.lstLogs[i].Time.ToString() }) ;
            }

            LogsGridContent.ItemsSource = datas;
        }

        public void btnXML(object sender, RoutedEventArgs e)
        {
            this.lstLogs = this.viewModel.getLogsLst(this.logsFileInfos[this.logIndex].Name);
            if (lstLogs == null || lstLogs.Count == 0) return;
            JSON_XML_TB.Visibility = Visibility.Visible;
            LogsGridContent.Visibility = Visibility.Collapsed;
            string filePath = logsFileInfos[logIndex].FullName;
            string fileName = System.IO.Path.GetFileNameWithoutExtension(filePath);
            string dName = System.IO.Path.GetDirectoryName(filePath);
            string formattedXML = this.viewModel.getLogsXML(this.lstLogs[logIndex], dName+"\\"+fileName+".xml");
            JSON_XML_TB.Text = formattedXML;
        }
        public void btnJSON(object sender, RoutedEventArgs e)
        {
            this.lstLogs = this.viewModel.getLogsLst(this.logsFileInfos[this.logIndex].Name);
            if (lstLogs == null || lstLogs.Count == 0) return;
            JSON_XML_TB.Visibility = Visibility.Visible;
            LogsGridContent.Visibility = Visibility.Collapsed;
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

        private void JSON_XML_TB_TextChanged(object sender, TextChangedEventArgs e)
        {

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

}
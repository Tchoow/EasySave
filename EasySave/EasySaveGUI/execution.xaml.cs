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
    /// Logique d'interaction pour execution.xaml
    /// </summary>
    public partial class execution : Page
    {
        public View view = new View();
        public ViewModel viewModel;
        public Model model;
        List<MyData> datas = new List<MyData>
            {
                new MyData { Id = 1, Name = "John", SourcePath = "C:\\Documents\\source1", DesPath = "C:\\Documents\\destination1", Type = "OK", IsSelect= false },
                new MyData { Id = 2, Name = "Jane", SourcePath = "C:\\Documents\\source2", DesPath = "C:\\Documents\\destination2", Type = "Error", IsSelect = false },
                new MyData { Id = 3, Name = "Bob", SourcePath = "C:\\Documents\\source3", DesPath = "C:\\Documents\\destination3", Type = "OK", IsSelect = false },
                new MyData { Id = 4, Name = "Alice", SourcePath = "C:\\Documents\\source4", DesPath = "C:\\Documents\\destination4", Type = "Error", IsSelect = false }
            };
        public execution()
        {
            InitializeComponent();
            jobdatagrid.ItemsSource = datas;

        }

        private void exec_selectedJobs_btn(object sender, RoutedEventArgs e)
        {
            foreach (var item in datas)
            {
                if(item.IsSelect == true)
                {
                    Trace.WriteLine(item.Name);
                }
                
            }
        }
    }
    public class MyData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SourcePath { get; set; }
        public string DesPath { get; set; }
        public string Type { get; set; }
        public bool IsSelect { get; set; }
    }
}


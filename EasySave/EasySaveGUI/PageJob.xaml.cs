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
        public PageJob()
        {
            InitializeComponent();

            List<MyData> datas = new List<MyData>
            {
                new MyData { Id = 1, Names = "John", SourcePath = "C:\\Documents\\source1", DesPath = "C:\\Documents\\destination1", State = "OK" },
                new MyData { Id = 2, Names = "Jane", SourcePath = "C:\\Documents\\source2", DesPath = "C:\\Documents\\destination2", State = "Error" },
                new MyData { Id = 3, Names = "Bob", SourcePath = "C:\\Documents\\source3", DesPath = "C:\\Documents\\destination3", State = "OK" },
                new MyData { Id = 4, Names = "Alice", SourcePath = "C:\\Documents\\source4", DesPath = "C:\\Documents\\destination4", State = "Error" }
            };


            myDataGrid.ItemsSource = datas;

        }
    }



    public class MyData
    {
        public int Id { get; set; }
        public string Names { get; set; }
        public string SourcePath { get; set; }
        public string DesPath { get; set; }
        public string State { get; set; }
    }
}

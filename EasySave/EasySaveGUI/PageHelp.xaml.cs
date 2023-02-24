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

    public partial class PageHelp : Page
    {
        ViewModel viewModel;


        public PageHelp(ViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            UpdateTrad();
        }

        public void UpdateTrad()
        {
            doc.Text = viewModel.getTraduction("docu");
            help.Text = viewModel.getTraduction("help");
            opendoc.Content = viewModel.getTraduction("opendoc");
            helpdesc.Text = viewModel.getTraduction("helpdesc");
            docudesc.Text = viewModel.getTraduction("docudesc");
        }
        public void btnPreview()
        {

        }

        public void btnJSON()
        {

        }

        public void btnXML()
        {

        }

        private void opendoc_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://pdfhost.io/v/OrWoLh~kZ_Microsoft_Word_EasySave_User_Manual_GUIdocx",
                UseShellExecute = true
            });
        }
    }
}

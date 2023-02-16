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
using System.Diagnostics;
using EasySave;

using EasySave;

namespace EasySaveGUI
{

    public partial class MainWindow : Window
    {
        private Frame ContentFrame;
        private ViewModel viewModel;

        public MainWindow()
        {
            viewModel = new ViewModel(this);
            InitializeComponent();
            this.ContentFrame = (Frame)FindName("CFrame");
            this.ContentFrame.Content = new PageHome();
            this.viewModel = new ViewModel(this);
        }

        private void btnJob(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Content = new PageJob(viewModel);
        }

        private void btnLang(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Content = new PageLang();
        }

        private void btnAbout(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Content = new PageAbout();
        }

        private void btnHelp(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Content = new PageHelp();
        }

        private void btnLogs(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Content = new PageLogs(this.viewModel);
        }

        private void btnExec(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Content = new PageExec(viewModel);
        }
    }
}

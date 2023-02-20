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
using System.Threading;

namespace EasySaveGUI
{

    public partial class MainWindow : Window
    {
        private Frame ContentFrame;
        private ViewModel viewModel;
        private string frameName;

        public MainWindow()
        {
            Mutex myMutex;
            bool aIsNewInstance = false;
            myMutex = new Mutex(true, "MyWPFApplication", out aIsNewInstance);
            if (!aIsNewInstance)
            {
                MessageBox.Show("Already an instance is running...");
                App.Current.Shutdown();
            }
            viewModel = new ViewModel(this);
            InitializeComponent();
            this.ContentFrame = (Frame)FindName("CFrame");
            this.ContentFrame.Content = new PageHome();
            this.viewModel = new ViewModel(this);
            viewModel.setLangueIndex(comboLanguage.SelectedIndex);
            this.UpdateTrad();
            
        }
        private void UpdateTrad()
        {
            JobBtn.Content = viewModel.getTraduction("JobMainWindow");
            ExecBtn.Content = viewModel.getTraduction("ExecutionMainWindow");
            LogBtn.Content = viewModel.getTraduction("LogsMainWindow");
            LangBtn.Content = viewModel.getTraduction("LanguagesMainWindow");
            HelpBtn.Content = viewModel.getTraduction("HelpMainWindow");
            Aboutbtn.Content = viewModel.getTraduction("AboutMainWindow");
        }
        private void btnJob(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Content = new PageJob(viewModel);
            frameName = "Job";
        }

        private void btnHome(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Content = new PageHome();
        }

        private void btnLang(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Content = new PageLang();
            frameName = "Lang";
        }

        private void btnAbout(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Content = new PageAbout(this.viewModel);
            frameName = "about";
        }

        private void btnHelp(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Content = new PageHelp(this.viewModel);
            frameName = "help";
        }

        private void btnLogs(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Content = new PageLogs(this.viewModel);
            frameName = "log";
        }

        private void btnExec(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Content = new PageExec(viewModel);
            frameName = "exec";
        }

        private void comboLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.setLangueIndex(comboLanguage.SelectedIndex);
            UpdateTrad();
            if (this.ContentFrame != null)
            {
                switch (frameName)
                {
                    case "Job":
                        this.ContentFrame.Content = new PageJob(viewModel);
                        break;
                    case "Lang":
                        this.ContentFrame.Content = new PageLang();
                        break;
                    case "about":
                        this.ContentFrame.Content = new PageAbout(this.viewModel);
                        break;
                    case "help":
                        this.ContentFrame.Content = new PageHelp(this.viewModel);
                        break;
                    case "log":
                        this.ContentFrame.Content = new PageLogs(this.viewModel);
                        break;
                    case "exec":
                        this.ContentFrame.Content = new PageExec(viewModel);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

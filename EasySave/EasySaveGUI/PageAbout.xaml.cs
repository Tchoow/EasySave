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

    /// <summary>
    /// Logique d'interaction pour PageAbout.xaml
    /// </summary>
    public partial class PageAbout : Page
    {
        
        ViewModel viewModel;

        public PageAbout(ViewModel viewModel)
        {
            InitializeComponent();

            this.viewModel = viewModel;
            UpdateTrad();

        }
        public void UpdateTrad()
        {
            about.Text = viewModel.getTraduction("about");
            aboutusdesc.Text = viewModel.getTraduction("aboutdesc");
        }
    }
}

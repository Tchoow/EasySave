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
    public partial class PageSett : Page
    {
        ViewModel viewModel;


        public PageSett(ViewModel viewModel)
        {
            this.viewModel = viewModel;
            InitializeComponent();
            UpdateTrad();
        }
        public void UpdateTrad()
        {
            
            Sett.Text = viewModel.getTraduction("SettingsMainWindow");
            opensrc.Content = viewModel.getTraduction("open");
            save.Content = viewModel.getTraduction("save");
            add.Content = viewModel.getTraduction("add");
            delete.Content = viewModel.getTraduction("delete");
            priority.Text = viewModel.getTraduction("prio");
            busisoft.Text = viewModel.getTraduction("busisoft");
            maxfilesize.Text = viewModel.getTraduction("maxfilsiz");
            cryptopath.Text = viewModel.getTraduction("cryptopath");//
        }
    }
}

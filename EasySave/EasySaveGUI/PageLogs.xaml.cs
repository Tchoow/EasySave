﻿using EasySave;
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

namespace EasySaveGUI
{
    /// <summary>
    /// Logique d'interaction pour PageLogs.xaml
    /// </summary>
    public partial class PageLogs : Page
    {
        public PageLogs(ViewModel viewmodel)
        {
            InitializeComponent();
            List<Log> datas = viewmodel.getLogsList();
            Trace.WriteLine(datas);
            this.logdatagrid.ItemsSource = datas;
        }
    }
}

﻿using System;
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
using ChartCPNTs.Views;
using ChartCPNTs.Models;
using ChartCPNTs.ViewModels;

namespace Tester.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public ColorBarModel model { get; set; } = new ChartCPNTs.Models.ColorBarModel(10, -5);

        public MainWindow()
        {
            InitializeComponent();
            //legend.Model = model;
            //DataContext = this;
        }
    }
}

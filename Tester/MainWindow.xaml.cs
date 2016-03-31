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
using ChartCPNTs.Views;
using ChartCPNTs.Models;
using ChartCPNTs.ViewModels;

namespace Tester
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MeshViewModel viewModel { get; set; }
        public LegendModel legendModel { get; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            legendModel = new LegendModel(5, -1);
        }
    }
}

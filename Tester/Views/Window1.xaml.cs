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
using System.Windows.Shapes;
using ChartCPNTs.Models;

namespace Tester.Views
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            var data = new List<List<double>>()
            {
                new List<double>() { 1,2,3,4,5 },
                new List<double>() { 2,3,4,5,6 }
            };
            var model = new MeshModel(data);
            mesh.Model = model;
        }
    }
}

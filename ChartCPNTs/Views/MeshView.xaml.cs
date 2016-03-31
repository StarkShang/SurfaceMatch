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
using ChartCPNTs.Models;
using ChartCPNTs.ViewModels;

namespace ChartCPNTs.Views
{
    /// <summary>
    /// MeshView.xaml 的交互逻辑
    /// </summary>
    public partial class MeshView : UserControl
    {
        public MeshModel model
        {
            set
            {
                viewModel.RowNumber = value.Data.Count;
                viewModel.ColumnNumber = value.Data[0].Count;
            }
        }
        public MeshViewModel viewModel { get; set; } = new MeshViewModel();

        private double hLineLength;
        private double vLineLength;

        public MeshView()
        {
            InitializeComponent();

            hLineLength = canvas.Width / (viewModel.ColumnNumber = 1);
            vLineLength = canvas.Height / (viewModel.RowNumber - 1);

            for (int i = 0; i < viewModel.RowNumber; i++)
            {
                for (int j = 0; j < viewModel.ColumnNumber; j++)
                {
                    var pt = new Ellipse()
                    {
                        Width = viewModel.PointSize,
                        Height = viewModel.PointSize,
                        Stroke = Brushes.Black,
                        Fill = Brushes.Black
                    };
                    Canvas.SetBottom(pt, i * vLineLength);
                    Canvas.SetLeft(pt, j * hLineLength);
                }
            }

        }
    }
}

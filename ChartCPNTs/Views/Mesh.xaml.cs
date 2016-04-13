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
    public partial class Mesh : UserControl
    {
        public MeshModel Model
        {
            set
            {
                ViewModel.RowNumber = value.Data.Count;
                ViewModel.ColumnNumber = value.Data[0].Count;
                DrawMesh();
            }
        }

        public Mesh()
        {
            InitializeComponent();

            

        }

        void DrawMesh()
        {
            for (int i = 0; i < ViewModel.RowNumber; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < ViewModel.ColumnNumber; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            grid.ShowGridLines = true;
            for (int i = 0; i < ViewModel.RowNumber; i++)
            {
                for (int j = 0; j < ViewModel.ColumnNumber; j++)
                {
                    var lPt = new Ellipse() { Width = ViewModel.PointSize, Height = ViewModel.PointSize, VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Left, Stroke = Brushes.Black, Fill = Brushes.Black };
                    var rPt = new Ellipse() { Width = ViewModel.PointSize, Height = ViewModel.PointSize, VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Right, Stroke = Brushes.Black, Fill = Brushes.Black };
                    var line = new Line() { X1 = ViewModel.PointSize, Y1 = 0, X2 = ActualWidth - ViewModel.PointSize, Y2 = 0, VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center, Stroke = Brushes.Black, Fill = Brushes.Black };
                    Grid.SetRow(lPt, i);
                    Grid.SetRow(rPt, i);
                    Grid.SetRow(line, i);
                    Grid.SetColumn(lPt, j);
                    Grid.SetColumn(rPt, j);
                    Grid.SetColumn(line, j);
                    grid.Children.Add(lPt);
                    grid.Children.Add(rPt);
                    grid.Children.Add(line);
                }
            }
        }

        private void sizeChangedEvent(object sender, SizeChangedEventArgs e)
        {
            
        }
    }
}

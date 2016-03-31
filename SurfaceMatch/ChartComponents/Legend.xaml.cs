using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace SurfaceMatch.ChartComponents
{
    /// <summary>
    /// Legend.xaml 的交互逻辑
    /// </summary>
    public partial class Legend : UserControl
    {
        public LegendViewModel viewModel { get; set; } = new LegendViewModel(max: 5, min: -1);
        public Legend()
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void OnLoadedEvent(object sender, RoutedEventArgs e)
        {
            Canvas.SetTop(MidValueLable, (viewModel.MidPosition * canvas.ActualHeight)-0.5*MidValueLable.ActualHeight);
        }
    }
    public class LegendViewModel
    {
        public string MinValue { get; set; }
        public string MidValue { get; set; }
        public string MaxValue { get; set; }
        public Color MinValueColor { get; set; } = Colors.Green;
        public Color MidValueColor { get; set; } = Colors.Blue;
        public Color MaxValueColor { get; set; } = Colors.Red;
        public Brush MinValueBrush { get; set; } = Brushes.Green;
        public Brush MidValueBrush { get; set; } = Brushes.Blue;
        public Brush MaxValueBrush { get; set; } = Brushes.Red;

        public double MidPosition { get; set; }


        public LegendViewModel(double max, double min)
        {
            if (max*min>=0)
            {
                MaxValue = string.Format("{0:F2}", max);
                MidValue = string.Format("{0:F2}", (max + min) / 2);
                MinValue = string.Format("{0:F2}", min);
                MidPosition = 0.5;
                MidValueColor = (MaxValueColor + MinValueColor)*(float)0.5;
            }
            else
            {
                MaxValue = string.Format("{0:F2}", max);
                MidValue = "0";
                MinValue = string.Format("{0:F2}", min);
                MidPosition = max / (max - min);
            }
        }
    }

    public class MutiplyConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value * double.Parse(parameter as string);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

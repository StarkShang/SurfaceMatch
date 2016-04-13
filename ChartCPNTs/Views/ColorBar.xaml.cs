using System.Windows.Controls;
using ChartCPNTs.ViewModels;
using ChartCPNTs.Models;
using System.Windows;

namespace ChartCPNTs.Views
{
    /// <summary>
    /// ColorBar.xaml 的交互逻辑
    /// </summary>
    public partial class ColorBar : UserControl
    {
        private Models.ColorBarModel model;
        public ColorBarModel Model
        {
            get
            {
                return model;
            }
            set
            {
                model = value;
                ViewModel.Model = value;
            }
        }
        public static readonly DependencyProperty ModelProperty =
            DependencyProperty.Register("Model", typeof(Models.ColorBarModel), typeof(ColorBar));


        public ColorBar()
        {
            InitializeComponent();
        }
    }
}

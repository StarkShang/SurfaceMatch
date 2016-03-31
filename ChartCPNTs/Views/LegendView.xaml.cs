using System.Windows.Controls;
using ChartCPNTs.ViewModels;
using ChartCPNTs.Models;
using System.Windows;

namespace ChartCPNTs.Views
{
    /// <summary>
    /// LegendView.xaml 的交互逻辑
    /// </summary>
    public partial class LegendView : UserControl
    {
        public static readonly DependencyProperty ModelProperty = DependencyProperty.Register("Model", typeof(LegendModel), typeof(LegendView));
        public LegendModel Model
        {
            get { return (LegendModel)GetValue(ModelProperty); }
            set
            {
                ViewModel.Model = value;
                SetValue(ModelProperty, value);
            }
        }

        public LegendView()
        {
            InitializeComponent();
        }
    }
}

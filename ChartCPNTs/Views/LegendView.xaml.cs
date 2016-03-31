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
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register("ViewModel", typeof(LegendViewModel),typeof(LegendView));
        public LegendViewModel ViewModel
        {
            get { return (LegendViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ModelProperty = DependencyProperty.Register("Model", typeof(LegendModel), typeof(LegendView));
        public LegendModel Model
        {
            get { return (LegendModel)GetValue(ModelProperty); }
            set { SetValue(ModelProperty, value); }
        }
        public LegendView()
        {
            InitializeComponent();

            Model = new LegendModel();
            ViewModel = new LegendViewModel(Model);
            DataContext = ViewModel;
        }
    }
}

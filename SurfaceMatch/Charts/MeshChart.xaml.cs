using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using SurfaceMatch.ChartComponents;

namespace SurfaceMatch.Charts
{
    /// <summary>
    /// MeshChart.xaml 的交互逻辑
    /// </summary>
    public partial class MeshChart : Page
    {
        public LegendModel LegendModel { get; set; }

        public MeshChart()
        {
            InitializeComponent();
        }

        public MeshChart(List<List<double>> data)
        {
            InitializeComponent();

            var max = data.Select(item => item.Max()).Max();
            var min = data.Select(item => item.Min()).Min();
            LegendModel = new LegendModel(max,min);
            DataContext = this;
        }

        void findMinMax()
        {

        }

    }

    
}

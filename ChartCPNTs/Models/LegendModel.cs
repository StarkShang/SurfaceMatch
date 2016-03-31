using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartCPNTs.Models
{
    public class LegendModel : ObservableObject
    {
        /// <summary>
        /// 最大值
        /// </summary>
        /// 
        private double minValue;
        public double MinValue
        {
            get { return minValue; }
            set
            {
                minValue = value;
                RaisePropertyChanged("MinValue");
            }
        }
        private double midValue;
        public double MidValue
        {
            get { return midValue; }
            set
            {
                midValue = value;
                RaisePropertyChanged("MidValue");
            }

        }
        private double maxValue;
        public double MaxValue
        {
            get { return maxValue; }
            set
            {
                maxValue = value;
                RaisePropertyChanged("MaxValue");
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="max">最大值</param>
        /// <param name="min">最小值</param>
        public LegendModel(double max, double min)
        {
            maxValue = max;
            minValue = min;
            midValue = max * min < 0 ? 0 : (max + min) / 2;
        }
        public LegendModel() { }
    }
}

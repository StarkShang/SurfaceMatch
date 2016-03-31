using GalaSoft.MvvmLight;
using System.Windows.Media;
using ChartCPNTs.Models;

namespace ChartCPNTs.ViewModels
{
    public class LegendViewModel : ViewModelBase
    {
        public LegendModel Model { get; set; }

        public string MinValue
        {
            get { return string.Format("0:F2", Model.MinValue); }
            set { RaisePropertyChanged("MinValue"); }
        }
        public string MidValue
        { get; set; }
        public string MaxValue { get; set; }
        private Color minValueColor = Colors.Green;
        public Color MinValueColor { get { return minValueColor; } }
        private Color midValueColor = Colors.Blue;
        public Color MidValueColor { get { return midValueColor; } }
        private Color maxValueColor = Colors.Red;
        public Color MaxValueColor { get { return maxValueColor; } }
        private Brush minValueBrush = Brushes.Green;
        public Brush MinValueBrush { get { return minValueBrush; } }
        private Brush midValueBrush = Brushes.Blue;
        public Brush MidValueBrush { get { return midValueBrush; } }
        private Brush maxValueBrush = Brushes.Red;
        public Brush MaxValueBrush { get { return maxValueBrush; } }
        public Color MinColor { set { minValueColor = value; minValueBrush = new SolidColorBrush(value); } }
        public Color MidColor { set { midValueColor = value; midValueBrush = new SolidColorBrush(value); } }
        public Color MaxColor { set { maxValueColor = value; maxValueBrush = new SolidColorBrush(value); } }
        public double MidPosition { get; set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public LegendViewModel(LegendModel model)
        {
            if (IsInDesignMode)
            {
                MinValue = "-0.5";
                MidValue = "0";
                MaxValue = "5.12";
                MidPosition = 0.5;
            }
            else
            {
                Model = model;
            }
        }

        public void SetColor(Color minColor, Color midColor, Color maxColor)
        {
            minValueColor = minColor;
            minValueBrush = new SolidColorBrush(minColor);
            midValueColor = midColor;
            midValueBrush = new SolidColorBrush(midColor);
            maxValueColor = maxColor;
            maxValueBrush = new SolidColorBrush(maxColor);
        }
    }
}

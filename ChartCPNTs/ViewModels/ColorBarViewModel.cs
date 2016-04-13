using GalaSoft.MvvmLight;
using System.Windows.Media;
using ChartCPNTs.Models;

namespace ChartCPNTs.ViewModels
{
    public class ColorBarViewModel : ViewModelBase
    {
        private Models.ColorBarModel model;
        public ColorBarModel Model
        {
            get { return model; }
            set
            {
                model = value;
                MinValue = string.Format("{0:F2}", model.MinValue);
                MidValue = string.Format("{0:F2}", model.MidValue);
                MaxValue = string.Format("{0:F2}", model.MaxValue);
                MidPosition = (model.MaxValue - model.MidValue) / (model.MaxValue - model.MinValue);
                FirstPartHeight = string.Format("{0}*", (model.MaxValue - model.MidValue) / (model.MaxValue - model.MinValue));
                SecondPartHeight = string.Format("{0}*", (model.MidValue - model.MinValue) / (model.MaxValue - model.MinValue));
                RaisePropertyChanged("Model");
            }
        }
        private string minValue;
        public string MinValue
        {
            get { return minValue; }
            set
            {
                minValue = value;
                RaisePropertyChanged("MinValue");
            }
        }
        private string midValue;
        public string MidValue
        {
            get { return midValue; }
            set
            {
                midValue = value;
                RaisePropertyChanged("MidValue");
            }
        }
        private string maxValue;
        public string MaxValue
        {
            get { return maxValue; }
            set
            {
                maxValue = value;
                RaisePropertyChanged("MaxValue");
            }
        }
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
        private double midPosition;
        public double MidPosition
        {
            get { return midPosition; }
            set
            {
                midPosition = value;
                RaisePropertyChanged("MidPosition");
            }
        }
        private string firstPartHeight;
        public string FirstPartHeight
        {
            get { return firstPartHeight; }
            set
            {
                firstPartHeight = value;
                RaisePropertyChanged("FirstPartHeight");
            }
        }
        private string secondPartHeight;
        public string SecondPartHeight
        {
            get { return secondPartHeight; }
            set
            {
                secondPartHeight = value;
                RaisePropertyChanged("SecondPartHeight");
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public ColorBarViewModel()
        {
            if (IsInDesignMode)
            {
                Model = new Models.ColorBarModel(5, -1);
            }
            else
            {
                //model = new LegendModel();
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

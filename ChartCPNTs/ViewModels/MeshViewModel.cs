using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using ChartCPNTs.Models;
using System.Windows.Media;

namespace ChartCPNTs.ViewModels
{
    public class MeshViewModel : ViewModelBase
    {
        private MeshModel model;
        public MeshModel Model
        {
            get { return model; }
            set
            {
                model = value;
                RowNumber = model.Data.Count;
                ColumnNumber = model.Data[0].Count;
                foreach (var item in model.Data)
                {
                    foreach (var num in item)
                    {

                    }
                }
                RaisePropertyChanged("Model");
            }
        }
        private int rowNumber;
        public int RowNumber
        {
            get { return rowNumber; }
            set
            {
                rowNumber = value;
                RaisePropertyChanged("RowNumber");
            }
        }
        private int columnNumber;
        public int ColumnNumber
        {
            get { return columnNumber; }
            set
            {
                columnNumber = value;
                RaisePropertyChanged("ColumnNumber");
            }
        }
        private Brush maxValueColor = Brushes.Red;
        public Brush MaxValueColor
        {
            get { return maxValueColor; }
            set
            {
                maxValueColor = value;
                RaisePropertyChanged("MaxValueColor");
            }
        }
        private Brush minValueColor = Brushes.Green;
        public Brush MinValueColor
        {
            get { return maxValueColor; }
            set
            {
                minValueColor = value;
                RaisePropertyChanged("MinValueColor");
            }
        }

        private List<List<Brush>> colorMaps;
        public List<List<Brush>> ColorMaps
        {
            get { return colorMaps; }
            set
            {
                colorMaps = value;
                RaisePropertyChanged("ColorMaps");
            }
        }

        public double PointSize { get; set; } = 3;

        public MeshViewModel()
        {
            if (IsInDesignMode)
            {
                var data  = new List<List<double>>()
                {
                    new List<double>() { 1,2,3,4,5 },
                    new List<double>() { 2,3,4,5,6 }
                };
                Model = new MeshModel(data);
            }
            else
            {
                // Code runs "for real"
            }
        }
    }
}

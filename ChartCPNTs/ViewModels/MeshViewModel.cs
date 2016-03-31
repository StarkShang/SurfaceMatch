using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace ChartCPNTs.ViewModels
{
    public class MeshViewModel : ViewModelBase
    {
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }

        public double PointSize { get; set; } = 3;

        public MeshViewModel()
        {
            if (IsInDesignMode)
            {
                RowNumber = 5;
                ColumnNumber = 3;
                PointSize = 3;
            }
            else
            {
                // Code runs "for real"
            }
        }
    }
}

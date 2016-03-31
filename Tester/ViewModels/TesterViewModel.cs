using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using ChartCPNTs.Models;

namespace Tester.ViewModels
{
    class TesterViewModel : ViewModelBase
    {
        LegendModel model = new LegendModel(5,-1);
    }
}

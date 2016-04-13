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
        public ColorBarModel model { get; set; } = new ColorBarModel(10, -5);
    }
}

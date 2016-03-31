using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartCPNTs.Models
{
    public class MeshModel
    {
        public List<List<double>> Data { get; set; }
        public MeshModel(List<List<double>> data)
        {
            var max = data.Select(item => item.Max()).Max();
            var min = data.Select(item => item.Min()).Min();
        }
    }
}

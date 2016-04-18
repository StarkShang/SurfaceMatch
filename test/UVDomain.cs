using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class UVDomain
    {
        public int numDiscreteU { get; set; }
        public int numDiscreteV { get; set; }
        /// <summary>
        /// 离散的U值
        /// </summary>
        private List<double> discreteU;
        public List<double> DiscreteU
        {
            get { return discreteU; }
            set { discreteU = value; }
        }
        /// <summary>
        /// 离散的V值
        /// </summary>
        private List<double> discreteV;
        public List<double> DiscreteV
        {
            get { return discreteV; }
            set { discreteV = value; }
        }
        /// <summary>
        /// 离散点
        /// </summary>
        private List<UVParam> discretePoints;
        public List<UVParam> DiscretePoints
        {
            get { return discretePoints; }
            set { discretePoints = value; }
        }

        /// <summary>
        /// 离散UV参数域
        /// </summary>
        /// <param name="uDNum"></param>
        /// <param name="vDNum"></param>
        /// <returns></returns>
        public List<UVParam> discretizeUVDomain(int uDNum, int vDNum)
        {
            var discreteU = new List<double>();
            var discreteV = new List<double>();
            var discretePoints = new List<UVParam>();
            // 离散U值
            for (int i = 0; i < uDNum; i++)
            {
                var uValue = (uDNum - i) / (double)(uDNum - 1) * minU + i / (double)(uDNum - 1) * maxU;
                discreteU.Add(uValue);
            }
            // 离散V值
            for (int i = 0; i < vDNum; i++)
            {
                var vValue = (vDNum - i) / (double)(vDNum - 1) * minV + i / (double)(vDNum - 1) * maxV;
                discreteV.Add(vValue);
            }
            // 获得离散点
            foreach (var uValue in discreteU)
            {
                foreach (var vValue in discreteV)
                {
                    discretePoints.Add(new UVParam(uValue, vValue));
                }
            }
            // 赋值
            numDiscreteU = uDNum;
            numDiscreteV = vDNum;
            this.discreteU = discreteU;
            this.discreteV = discreteV;
            this.discretePoints = discretePoints;
            return discretePoints;
        }

        public UVDomain(double[] uvMinMax)
        {
            try
            {
                minU = uvMinMax[0];
                maxU = uvMinMax[1];
                minV = uvMinMax[2];
                maxV = uvMinMax[3];
            }
            catch (Exception)
            {
                throw;
            }
        }

        private double minU;
        private double maxU;
        private double minV;
        private double maxV;
    }

    struct UVParam
    {
        public double U { get; set; }
        public double V { get; set; }

        public UVParam(double u, double v)
        {
            U = u;
            V = v;
        }
    }
}

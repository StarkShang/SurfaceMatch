using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MatchProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            // 创建二进制读取器
            BinaryReader dBReader;
            BinaryReader rBReader;
            try
            {
                dBReader = new BinaryReader(new FileStream("designPoints", FileMode.Open));
            }
            catch  (IOException e)
            {
                Console.WriteLine(e.Message + "Cannot open file.");
                return;
            }
            try
            {
                rBReader = new BinaryReader(new FileStream("realPoints", FileMode.Open));
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "Cannot open file.");
                return;
            }
            // 读取点数据
            const int discreteNum = 501;
            var dMesh = new Types.Mesh(discreteNum, discreteNum);
            var rMesh = new Types.Mesh(discreteNum, discreteNum);
            for (int i = 0; i < discreteNum; i++)
            {
                for (int j = 0; j < discreteNum; j++)
                {
                    dMesh.setNode(
                        new Types.Node(
                            dBReader.ReadDouble(),
                            dBReader.ReadDouble(),
                            dBReader.ReadDouble()),
                        i, j);
                    rMesh.setNode(
                        new Types.Node(
                            rBReader.ReadDouble(),
                            rBReader.ReadDouble(),
                            rBReader.ReadDouble()),
                        i, j);
                }
            }
            dBReader.Close();
            rBReader.Close();
            // Step 3.2 将曲面mesh转换成图
            var dGraph = dMesh.generateGraph();
            var rGraph = rMesh.generateGraph();
            // Step 3.3 确定离散映射点
            var mapNum = 9;
            var step = discreteNum / (mapNum + 1);
            var dMapPoint = new List<Tuple<int, int>>();
            var rMapPoint = new List<Tuple<int, int>>();
            for (int i = 0; i < mapNum; i++)
            {
                for (int j = 0; j < mapNum; j++)
                {
                    dMapPoint.Add(new Tuple<int, int>((i + 1) * step, (j + 1) * step));
                    rMapPoint.Add(new Tuple<int, int>((i + 1) * step, (j + 1) * step));
                }
            }
            // Step 3.4 计算理想曲面上各个边界点到映射点的距离
            var dDist = new List<Tuple<double, double, double>>();
            foreach (var item in dMapPoint)
            {
                var dist = dGraph.getGeodesics(item.Item1, item.Item2, step);
                dDist.Add(dist);
            }
            // Step 3.5 生成映射点
            for (int i = 0; i < dDist.Count; i++)
            {
                while (true)
                {
                    var dist0 = rGraph.getGeodesics(rMapPoint[i].Item1, rMapPoint[i].Item2, step);
                    var dist1 = rGraph.getGeodesics(rMapPoint[i].Item1 - 1, rMapPoint[i].Item2, step);
                    var dist2 = rGraph.getGeodesics(rMapPoint[i].Item1, rMapPoint[i].Item2 + 1, step);
                    var dist3 = rGraph.getGeodesics(rMapPoint[i].Item1 + 1, rMapPoint[i].Item2, step);
                    var dist4 = rGraph.getGeodesics(rMapPoint[i].Item1, rMapPoint[i].Item2 - 1, step);

                    var target0 = Math.Sqrt(
                        (dist0.Item1 - dDist[i].Item1) * (dist0.Item1 - dDist[i].Item1)
                        + (dist0.Item2 - dDist[i].Item2) * (dist0.Item2 - dDist[i].Item2)
                        + (dist0.Item3 - dDist[i].Item3) * (dist0.Item3 - dDist[i].Item3));
                    var target1 = Math.Sqrt(
                        (dist1.Item1 - dDist[i].Item1) * (dist1.Item1 - dDist[i].Item1)
                        + (dist1.Item2 - dDist[i].Item2) * (dist1.Item2 - dDist[i].Item2)
                        + (dist1.Item3 - dDist[i].Item3) * (dist1.Item3 - dDist[i].Item3));
                    var target2 = Math.Sqrt(
                        (dist2.Item1 - dDist[i].Item1) * (dist2.Item1 - dDist[i].Item1)
                        + (dist2.Item2 - dDist[i].Item2) * (dist2.Item2 - dDist[i].Item2)
                        + (dist2.Item3 - dDist[i].Item3) * (dist2.Item3 - dDist[i].Item3));
                    var target3 = Math.Sqrt(
                        (dist3.Item1 - dDist[i].Item1) * (dist3.Item1 - dDist[i].Item1)
                        + (dist3.Item2 - dDist[i].Item2) * (dist3.Item2 - dDist[i].Item2)
                        + (dist3.Item3 - dDist[i].Item3) * (dist3.Item3 - dDist[i].Item3));
                    var target4 = Math.Sqrt(
                        (dist4.Item1 - dDist[i].Item1) * (dist4.Item1 - dDist[i].Item1)
                        + (dist4.Item2 - dDist[i].Item2) * (dist4.Item2 - dDist[i].Item2)
                        + (dist4.Item3 - dDist[i].Item3) * (dist4.Item3 - dDist[i].Item3));

                    var minDist = new double[] { target0, target1, target2, target3, target4 }.Min();
                    if (minDist == target0)
                    {
                        break;
                    }
                    else if (minDist == target1)
                    {
                        rMapPoint[i] = new Tuple<int, int>(rMapPoint[i].Item1 - 1, rMapPoint[i].Item2);
                    }
                    else if (minDist == target2)
                    {
                        rMapPoint[i] = new Tuple<int, int>(rMapPoint[i].Item1, rMapPoint[i].Item2 + 1);
                    }
                    else if (minDist == target3)
                    {
                        rMapPoint[i] = new Tuple<int, int>(rMapPoint[i].Item1 + 1, rMapPoint[i].Item2);
                    }
                    else if (minDist == target4)
                    {
                        rMapPoint[i] = new Tuple<int, int>(rMapPoint[i].Item1, rMapPoint[i].Item2 - 1);
                    }
                }
            }
            Console.ReadKey();

        }
    }
}

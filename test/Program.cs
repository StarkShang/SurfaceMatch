using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            var graph = new Geodesic.Graph(2, 3);
            graph.AddVertex(
                        new Tuple<Geodesic.Vertex, Tuple<int, int>>(
                            new Geodesic.Vertex(0, 0, 5, 1),
                            new Tuple<int, int>(0, 0)));
            graph.AddVertex(
                        new Tuple<Geodesic.Vertex, Tuple<int, int>>(
                            new Geodesic.Vertex(5, 0, 3, 1),
                            new Tuple<int, int>(0, 1)));
            graph.AddVertex(
                        new Tuple<Geodesic.Vertex, Tuple<int, int>>(
                            new Geodesic.Vertex(3, 0, 0, 3),
                            new Tuple<int, int>(0, 2)));
            graph.AddVertex(
                        new Tuple<Geodesic.Vertex, Tuple<int, int>>(
                            new Geodesic.Vertex(0, 1, 2, 0),
                            new Tuple<int, int>(1, 0)));
            graph.AddVertex(
                        new Tuple<Geodesic.Vertex, Tuple<int, int>>(
                            new Geodesic.Vertex(2, 1, 5, 0),
                            new Tuple<int, int>(1, 1)));
            graph.AddVertex(
                        new Tuple<Geodesic.Vertex, Tuple<int, int>>(
                            new Geodesic.Vertex(5, 3, 0, 0),
                            new Tuple<int, int>(1, 2)));
            Geodesic.GetShortestPath(graph, 0, 0, 1, 1);

            Console.ReadKey();
        }
    }
}

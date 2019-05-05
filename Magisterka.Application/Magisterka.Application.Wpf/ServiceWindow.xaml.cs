

using Magisterka.Infrastructure.Shared.Enum;

namespace Magisterka.Application.Wpf
{
    using Magisterka.Infrastructure.Shared.Structure;
    using QuickGraph.Algorithms.ShortestPath;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;
    using QuickGraph.Algorithms;
    using QuickGraph.Algorithms.Observers;
    using QuickGraph;
    /// <summary>
    /// Interaction logic for ServiceWindow.xaml
    /// </summary>
    public partial class ServiceWindow : Window
    {
        private int graphItemsCount = 1693;

        public BusGraph BusGraph { get; set; }

        public ServiceWindow(BusGraph busGraph)
        {
            InitializeComponent();

            this.BusGraph = busGraph;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var tTable = new int[graphItemsCount,graphItemsCount];

            Func<GraphEdge, double> edgeCost = cost => 1; // constant cost
            // We want to use Dijkstra on this graph
            //var dijkstra = new DijkstraShortestPathAlgorithm<GraphNode, GraphEdge>(BusGraph, edgeCost);


            

            for (int i = 1; i < graphItemsCount - 1; i++)
            {
                var source = BusGraph.Vertices.FirstOrDefault(x => x.BusStop.ArrayNumber == i);

                TryFunc<GraphNode, IEnumerable<GraphEdge>> tryGetPaths = BusGraph.ShortestPathsDijkstra(edgeCost, source);

                for (int j= 1; j < graphItemsCount - 1; j++)
                {
                    if (i != j)
                    {
                        var target = BusGraph.Vertices.FirstOrDefault(x => x.BusStop.ArrayNumber == j);

                        IEnumerable<GraphEdge> path;

                        if (tryGetPaths(target, out path))
                        {
                            // tutaj pozbyć sie podwojncyh linków pieszych
                            var prev = path.FirstOrDefault();

                            if (path.Count() > 1)
                            {
                                foreach (var graphEdge in path)
                                {
                                    if (prev == graphEdge)
                                    {
                                        continue;
                                    }

                                    if (prev.ConnectionType == ConnectionType.Walk
                                        && graphEdge.ConnectionType == ConnectionType.Walk)
                                    {
                                        continue;
                                    }
                                    tTable[i, j] = 1;
                                }
                            }
                            else
                            {
                                tTable[i, j] = 1;
                            }

                            
                        }
                        else
                        {
                            tTable[i, j] = 0;
                        }
                    }
                }
            }





            // creating the observer
            // var vis = new VertexPredecessorRecorderObserver<GraphNode,GraphEdge>();

            // compute and record shortest paths
            //using(ObserverScope.Create(dijkstra, vis))
            //   dijkstra.Compute(sourceCity);

            // vis can create all the shortest path in the graph
            //foreach(var e in vis.Path(targetCity))
            //   Console.WriteLine(e);      



            //  }

            //BusGraph.CondensateStronglyConnected<>()
            //var firstNode = 





        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Anotar.Log4Net;
using Magisterka.Infrastructure.Shared.Collections;
using Magisterka.Infrastructure.Shared.Consts;
using Magisterka.Infrastructure.Shared.Enum;
using Magisterka.Infrastructure.Shared.Generic;
using Magisterka.Infrastructure.Shared.Structure;
using MoreLinq;
using QuickGraph;
using QuickGraph.Algorithms;
using QuickGraph.Algorithms.ConnectedComponents;
using QuickGraph.Algorithms.Observers;
using QuickGraph.Algorithms.Search;

namespace Magisterka.Modules.Main.Matrix
{
    public class DistanceMatrixGenerator : BaseMatrixGenerator
    {
        ///  private BusGraph BusGraph { get; set; }
        private BusGraph BusGraph { get; set; }


        private IDictionary<int, int> parents;
        private IDictionary<int, int> distances;
        private int currentDistance;

      ///  private int[,] DistanceMatrix { get; set; }


        private GraphNode StartGraphNode { get; set; }

        // private int[,] transferMatrix = null;

        public DistanceMatrixGenerator(BusLinesList busLinesList, BusStopList busStopList, BusGraph  busGraph) 
            : base(busLinesList, busStopList)
        {
            BusGraph = busGraph;
          
        }

       

        public int[,] GenerateDistanceMatrix()
        {
            var matrix = new int[BusStopList.Count, BusStopList.Count];


            //var cartesianBusStops = BusStopList.Cartesian(BusStopList, (first, second) => new GenericPair<BusStop>(first, second));

            var totalCount = BusStopList.Count();
            var counter = 0;

            foreach (var busStopToCalculate in BusStopList)
            {
                counter++;
                LogTo.Debug("Wykonano {0}%", (counter * 100 /totalCount ));


                var searchAlgorithm = new BreadthFirstSearchAlgorithm<GraphNode, GraphEdge>(BusGraph);
                var distanceRecorderObserver = new VertexDistanceRecorderObserver<GraphNode, GraphEdge>(edge => 1 );

              
                using (distanceRecorderObserver.Attach(searchAlgorithm))
                {
                    StartGraphNode = BusGraph.GetGraphNode(busStopToCalculate);

                  
                    searchAlgorithm.Compute(StartGraphNode);


                  
                }

                

                foreach (var verticle in BusGraph.Vertices)
                {
                    if (busStopToCalculate.ArrayNumber == verticle.BusStop.ArrayNumber)
                    {
                        matrix[busStopToCalculate.ArrayNumber, verticle.BusStop.ArrayNumber] = 0;
                    }
                    else if (distanceRecorderObserver.Distances.ContainsKey(verticle))
                    {
                        var edgeCount = distanceRecorderObserver.Distances[verticle];

                        matrix[busStopToCalculate.ArrayNumber, verticle.BusStop.ArrayNumber] = (int)edgeCount;
                    }
                    else
                    {
                        matrix[busStopToCalculate.ArrayNumber, verticle.BusStop.ArrayNumber] = GraphConsts.MaxValue;
                    }

                }
            }

            return matrix;
        }

    }
}
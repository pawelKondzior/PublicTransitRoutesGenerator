using Anotar.Log4Net;
using Magisterka.Infrastructure.Shared.Enum;
using Magisterka.Infrastructure.Shared.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magisterka.Modules.Main.Algoritms.Matrix
{
    public abstract class BaseMatrixGenerator : BaseAlgorithm
    {
        //   private Stopwatch timer = new Stopwatch();

        //  protected bool TimeWarning = false;
        protected int[,] Matrix { get; set; }

        protected int TableGeneratedPopulatonCount = 0;

        protected int MaxGenerationCount { get; set; }

        protected BusStop Start { get; set; }
        protected BusStop End { get; set; }

        public BaseMatrixGenerator(AlgorithmParameters baseAlgorithmParameters) // :this()
            : base(baseAlgorithmParameters)
        {
            Start = AlgorithmParameters.Start;
            End = AlgorithmParameters.End;

            //Matrix = AlgorithmParameters.D;

            //MaxGenerationCount = AlgorithmParameters.MaxTableGenerationPopulationCountMDG;
        }

        public virtual List<List<BusStop>> Generate()
        {
            var topNodeVertex = GetTopNodeVertex();

            if (topNodeVertex != null)
            {
                var allPaths = topNodeVertex.TraverseAllPathList();

                var listOfBusPaths = GenerateBusStopListFromGraphNodes(allPaths);

                LogTo.Info("sciezki");

                listOfBusPaths.ForEach(path => LogTo.Info(string.Join(" ", path.Select(x => x.Id).ToArray())));

                return listOfBusPaths;
            }

            return new List<List<BusStop>>();
        }


        public GraphNode GetTopNodeVertex()
        {
            var minimalPath = Matrix[Start.ArrayNumber, End.ArrayNumber];

            if (minimalPath == 0 || minimalPath == 999)
            {
                LogTo.Warn("Brak połczenia między przystankami");
                return null;
            }

            var topNode = AlgorithmParameters.BusStopList.FirstOrDefault(x => x.Id == Start.Id);

            var topNodeVertex = new GraphNode(topNode);

            AddChildsToTopNode(topNodeVertex, minimalPath);

            return topNodeVertex;
        }

        protected virtual void AddChildsToTopNode(GraphNode topNode, int minimalPath)
        {
            if (TableGeneratedPopulatonCount >= MaxGenerationCount)
            {
                LogTo.Info("TableGeneratedPopulatonCount >= MaxGenerationCount");
                return;
            }

            var result = new List<GraphNode>();


            if (topNode.BusStop == null)
            {
                return;
            }

            var childNodes = GetChildNodesForTopNode(topNode, minimalPath);

            ///  LogTo.Info("child nodes {0} ", string.Join(" ", childNodes.Select(x => x.Id.ToString())));


            foreach (var nextBusStop in childNodes)
            {
                var target = new GraphNode(nextBusStop);

                result.Add(target);

                topNode.GraphEdges.Add(new GraphEdge(target, topNode, ConnectionType.Unknown));

            }

            minimalPath = minimalPath - 1;

            if (minimalPath == 0)
            {
                var target = new GraphNode(End);

                topNode.GraphEdges.Add(new GraphEdge(target, topNode, ConnectionType.Unknown));

                TableGeneratedPopulatonCount++;
            }
            else
            {
                foreach (var nextLevelItem in result.Where(x => x.BusStop.Id != AlgorithmParameters.End.Id))
                {
                    AddChildsToTopNode(nextLevelItem, minimalPath);
                }
            }
        }

        private IEnumerable<BusStop> GetChildNodesForTopNode(GraphNode topNode, int minimalPath)
        {
            var items = new List<int>();

            var nextStopPathDistance = minimalPath - 1;

            foreach (var busStop in AlgorithmParameters.BusStopList)
            {
                
                if (topNode.BusStop.ArrayNumber == busStop.ArrayNumber)
                {
                    continue;
                }

                if (minimalPath != 0 && End.ArrayNumber == busStop.ArrayNumber)
                {
                    continue;
                }

                var currentToEnd = Matrix[busStop.ArrayNumber, End.ArrayNumber];
                var currentSum = Matrix[busStop.ArrayNumber, End.ArrayNumber] + Matrix[topNode.BusStop.ArrayNumber, busStop.ArrayNumber];

                if (currentSum == minimalPath && currentToEnd == nextStopPathDistance)
                {
                    items.Add(busStop.ArrayNumber);
                }
            }

            var childNodes = AlgorithmParameters.BusStopList.Where(x => items.Contains(x.ArrayNumber));

            return childNodes;
        }

        private List<List<BusStop>> GenerateBusStopListFromGraphNodes(List<List<GraphNode>> graphNodesList)
        {
            var result = new List<List<BusStop>>();
            graphNodesList.ForEach(x => result.Add(x.Select(s => s.BusStop).ToList()));

            


            return result.Where(x => x != null).Where(x => x.LastOrDefault()?.Id == End.Id).ToList();
        }
    }
}

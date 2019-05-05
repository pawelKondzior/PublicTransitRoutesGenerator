// -----------------------------------------------------------------------
//  <copyright file="BusNode.cs" company="DevCore .NET">
//      Copyright (c) DevCore.NET All rights reserved.
//  </copyright>
//  <author>Paweł Kondzior</author>
// -----------------------------------------------------------------------

using Magisterka.Infrastructure.Shared.Enum;
using Magisterka.Infrastructure.Shared.Extensions;

namespace Magisterka.Infrastructure.Shared.Structure
{
    using System;
    using System.Collections.Generic;
    using Magisterka.Infrastructure.Shared.IoDto;
    using System.Linq;
    using Magisterka.Infrastructure.Shared.Collections;
    using System.Diagnostics;
    using QuickGraph;
    using Anotar.Log4Net;

    public class GraphNode
    {
        #region Properties
        
        public List<GraphEdge> GraphEdges { get; set; }

        public BusStop BusStop { get; set; }

        public List<BusLineIoDto> BusLines { get; set; }

        #endregion

        #region Constructors

        public GraphNode(BusStop busStop)
        {
            this.GraphEdges = new List<GraphEdge>();
            this.BusStop = busStop;
        }


        #endregion

        #region Methods

        /// <summary>
        /// Rekurencyjnei przeszukuje drog z jednogo punktu do drugiego
        /// </summary>
        public void GetLineCombinationContainingBusStops(int endBusStopId, IEnumerable<BusStop> busStops, List<BusStop> combinationList, ref List<List<BusStop>> aggregateResults)
        {
            var busStopsIds = busStops.Select(x => x.Id).Distinct();

            GetLineCombinationContainingBusStops(endBusStopId, busStopsIds, combinationList, ref aggregateResults);
        }

        /// <summary>
        /// Rekurencyjnei przeszukuje drog z jednogo punktu do drugiego
        /// </summary>
        /// <param name="endBusStopId"></param>
        /// <param name="busStopsIds"></param>
        /// <param name="combinationList"></param>
        /// <param name="aggregateResults"></param>
        public void GetLineCombinationContainingBusStops(int endBusStopId, IEnumerable<int> busStopsIds, List<BusStop> combinationList, ref List<List<BusStop>> aggregateResults)
        {
            if (!combinationList.AddIfNotExists(BusStop))
            {
                return;
            }

            if (this.BusStop.Id == endBusStopId)
            {
                aggregateResults.Add(combinationList);
                return;
            }

            var newNodes = GraphEdges.Where(x => busStopsIds.Contains(x.Target.BusStop.Id)).Select(x => x.Target);

            if (newNodes.Count() == 0)
            {
                // aggregateResults.Add(combinationList);
                return;
            }

            foreach (var graphNode in newNodes)
            {
                graphNode.GetLineCombinationContainingBusStops(endBusStopId, busStopsIds, combinationList.ToList(), ref aggregateResults);
            }

           // aggregateResults.Add(combinationList);
        }

        /// <summary>
        /// Uzupełnia cały graf
        /// </summary>
        /// <param name="busStopList"></param>
        /// <param name="busLines"></param>
        /// <param name="existingGraphNodes"></param>
        /// <param name="walkLinkTime"></param>
        public void CompleteConnections(BusStopList busStopList, BusLinesList busLines, BusGraph busGrpah, int walkLinkTime)
        {
            var myBusLines = busLines.GetBusLinesForBusStop(this.BusStop);

            foreach (var myBusLine in myBusLines)
            {
                var targetId = myBusLine.GetNextBusStopId(this.BusStop);

                ///   var target = busStopList.FirstOrDefault(x => x.Id == targetId);
                if (targetId.HasValue)
                {
                    var targetBusStop = busStopList.FirstOrDefault(x => x.Id == targetId.Value);

                    if (targetBusStop == null)
                    {
                        LogTo.Warn("Brak busstop o ID {0}", targetId.Value);
                            
                        targetBusStop = new BusStop
                        {
                            ///ArrayNumber = id
                        };
                    }
                    else
                    {
                        var target = busGrpah.GetGraphNodeForBusStop(targetBusStop);
                        GraphEdges.Add(new GraphEdge(target, this, ConnectionType.Bus, myBusLine));
                    }
                }
                //else
                //{
                //    LogTo.Warn("nie znaezion busstop w grafie");
                //}
            }


            //var ids = busLines.GetNextBusStopsIds(BusStop).Distinct();

            //var nextBusStops = busStopList.Where(x => ids.Contains(x.Id));

            //if (this.BusLines == null)
            //{
            //    this.BusLines = busLines.GetBusLinesForBusStop(this.BusStop);
            //}

            //foreach (var nextBusStop in nextBusStops)
            //{
            //    var target = busGrpah.GetGraphNodeForBusStop(nextBusStop);

            //    GraphEdges.Add(new GraphEdge(target, this, ConnectionType.Bus));
            //}

            foreach (var item in busStopList)
            {
                if (item == BusStop)
                {
                    continue;
                }

                var time = BusStop.GetWalkTime(item);

                if (time <= walkLinkTime)
                {
                    var target = busGrpah.GetGraphNodeForBusStop(item);

                    GraphEdges.Add(new GraphEdge(target, this, ConnectionType.Walk, time));
                }
            }
        }

        private GraphEdge FindFirstGraphEdge(ref BusLineIoDto busLineIoDto)
        {
            foreach (var graphEdge in this.GraphEdges)
            {
                foreach (var busLine in graphEdge.Target.BusLines)
                {
                    if (busLine.NameAndVariant == busLineIoDto.NameAndVariant)
                    {
                        return graphEdge;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Pobiera wszyskie przystanki az do końcowego leżące na danej lini
        /// Problem tu jest taki że źle lata, trzeba przekazać jaki typ, bo inaczej cofa się do najbliższego
        /// sąsiada po linku pieszym
        /// </summary>
        /// <param name="endBusStop"></param>
        /// <param name="busLineIoDto"></param>
        /// <param name="actualList"></param>
        /// <returns></returns>
        public List<BusStop> GetAllBusStopsOnBusLineToSelectedBusStop(ref BusStop endBusStop, ref BusLineIoDto busLineIoDto, ref List<BusStop> actualList)
        {
            // var firstOrDefault = this.GraphEdges.FirstOrDefault(x => x.GraphNode.BusLines.Contains(busLineIoDto));

            // System.StackOverflowException was unhandled
            var firstOrDefault = this.FindFirstGraphEdge(ref busLineIoDto);

            if (firstOrDefault == null)
            {
                return actualList;
            }

            var nextNode = firstOrDefault.Target;

            if (nextNode.BusStop == endBusStop)
            {
                return actualList;
            }

            actualList.Add(nextNode.BusStop);

            if (actualList.Count > 500)
            {
                return null;
                // Debug.WriteLine(actualList.Count);
            }

            return nextNode.GetAllBusStopsOnBusLineToSelectedBusStop(ref endBusStop, ref busLineIoDto, ref actualList);
        }

        public void ValidateGraphEdges()
        {
            var nodesList = new List<GraphEdge>();

            foreach (var item in this.GraphEdges)
            {
                if (!nodesList.Any(x => x.Target.BusStop == item.Target.BusStop
                                     && x.ConnectionType == item.ConnectionType))
                {
                    nodesList.Add(item);
                }
                else
                {
                    throw new Exception("Nowdy sie dubluja");
                }
            }
        }


        //public  List<GraphNode> GetAllTreChildList()
        //{
        //    List<GraphNode> result = new List<GraphNode>();
        //    result.Add(this);

        //    foreach (var child in this.GraphEdges)
        //    {
        //        result.AddRange(child.Target.GetAllTreChildList());
        //    }
        //    return result;
        //}



        public void TraverseAction(Action<GraphNode> action)
        {
            action(this);

            this.GraphEdges.Select(x => x.Target).ToList().ForEach(x => x.TraverseAction(action));
        }


        public List<List<GraphNode>> TraverseAllPathList()
        {
            var resultList = new List<List<GraphNode>>();

            var pathArray = new GraphNode[] { this };

            TraverseAllPathList(ref resultList, pathArray);

            return resultList;
        }




        public void TraverseAllPathList(ref List<List<GraphNode>> resultList, GraphNode[] pathArray)
        {
            pathArray = pathArray.Union(new[] {this}).ToArray();

            if (this.GraphEdges.Any())
            {
                foreach (var child in this.GraphEdges)
                {
                    child.Target.TraverseAllPathList(ref resultList, pathArray);
                }
            }
            else
            {
                resultList.Add(pathArray.ToList());
            }
        }

       


        #endregion
    }
}
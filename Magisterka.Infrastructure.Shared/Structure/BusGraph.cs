// -----------------------------------------------------------------------
//  <copyright file="BusGraph.cs" company="DevCore .NET">
//      Copyright (c) DevCore.NET All rights reserved.
//  </copyright>
//  <author>Paweł Kondzior</author>
// -----------------------------------------------------------------------



namespace Magisterka.Infrastructure.Shared.Structure
{
    using System.Collections.Generic;
    using Magisterka.Infrastructure.Shared.Collections;
    using Magisterka.Infrastructure.Shared.IoDto;
    using System.Linq;
    using System;
    using QuickGraph;
    using Enum;

    public class BusGraph : AdjacencyGraph<GraphNode, GraphEdge>
    {
       /// GraphNodeList GraphNodes { get; set; }

        public BusStopList BusStopList { get; set; }

        private BusLinesList BusLines { get; set; }

        private int WalkLinkTime { get; set; }



        public BusGraph()
        {
            
        }

        public BusGraph(BusStopList busStopList, BusLinesList busLines, int walkLinkTime) 
           // BusGraphCreationModeEnum  busGraphCreationModeEnum = BusGraphCreationModeEnum.CreateGraphNodesFromData)
        {
            

            this.BusStopList = busStopList;
            this.BusLines = busLines;
            this.WalkLinkTime = walkLinkTime;

           //if (busGraphCreationModeEnum == BusGraphCreationModeEnum.CreateGraphNodesFromData)

           // {
                CreateNodes();
           // }
        }

        private void CreateNodes()
        {
            
            foreach (var busStop  in BusStopList)
            {
                var graphNode = GetGraphNodeForBusStop(busStop); //.new GraphNode(busStop);
                graphNode.CompleteConnections(BusStopList, BusLines, this, WalkLinkTime);
            }

            foreach (var verticle in Vertices)
            {
                foreach (var edge in verticle.GraphEdges)
                {
                    this.AddEdge(edge);
                }
            }
        }

        //public GraphNode GetGraphNodeForBusStopId(int id)
        //{
        //    var graphNode = this.Vertices.FirstOrDefault(x => x.BusStop.Id == id);

        //    if (graphNode == null)
        //    {
        //        graphNode = new GraphNode(busStop);
        //        AddVertex(graphNode);
        //    }

        //    return graphNode;
        //}


        public GraphNode GetGraphNodeForBusStop(BusStop busStop)
        {
            var graphNode = this.Vertices.FirstOrDefault(x => x.BusStop.Id == busStop.Id);

            if (graphNode == null)
            {
                graphNode = new GraphNode(busStop);
                AddVertex(graphNode);
            }

            return graphNode;
        }

        public GraphNode GetGraphNode(BusStop busStop)
        {
            return GetGraphNodeForBusStop(busStop);
        }

       
        public WalkLineChanges GetWalkLines(int startBusStopId, int endBusStopId, BusStopList busStopList)
        {
            //var start = busStopList.FirstOrDefault(x => x.Id == startBusStopId);
           // var end = busStopList.FirstOrDefault(x => x.Id == endBusStopId);

            var start = busStopList.FirstOrDefault(x => x.ArrayNumber == startBusStopId);
            var end = busStopList.FirstOrDefault(x => x.ArrayNumber == endBusStopId);

            return GetWalkLines(start, end);
        }

        public WalkLineChanges GetWalkLines(BusStop start, BusStop end)
        {
            var startNode = GetGraphNodeForBusStop(start);

            var item = startNode.GraphEdges.Select(x => x.Target).Select(x => x.BusStop).FirstOrDefault(x => x.Id == end.Id);

            if (item != null)
            {
                return new WalkLineChanges(start, end);
            }

            return null;
        }

        //public List<String> getConnectionList(int busStopId, String destinationBusStop)
        //{
        //    List<String> lines = new List<string>();

        //    // Node node = nodes[nodeId];
        //    GraphNode graphNode = GraphNodes.FirstOrDefault(x => x.BusStop.Id == busStopId);


        //    foreach (String line in graphNode.GraphEdges.BusLinks.Keys)
        //    {
        //        BusLine busLine = busList[line];

        //        if (busLine.ifConnectionExists(nodeId, destinationBusStop))
        //            lines.Add(busLine.Name);
        //    }

        //    if (node.Links.ContainsKey(destinationBusStop))
        //        if (node.Links[destinationBusStop] <= link)
        //            lines.Add("link" + node.Links[destinationBusStop]);

        //    return lines;
        //}

        public List<BusStop> GetMiddleBusStopsIds(PartOfBusLine partOfBusLine)
        {
            List<BusStop> list = new List<BusStop>();

            var startNode = GetGraphNode(partOfBusLine.Start);
            //var endNode = GetGraphNode(partOfBusLine.End);

            var endBusStop = partOfBusLine.End;
            var busLine = partOfBusLine.BusLineIoDto;
            var actualList = new List<BusStop>();

            return startNode.GetAllBusStopsOnBusLineToSelectedBusStop
                (ref endBusStop, ref busLine, ref actualList);
        }


        public List<PartOfBusLine> GetDirectlyConnectedBusLines(BusStop start, BusStop end, bool allowWalkLinks, int maxWalkTime)
        {
            var startNode = GetGraphNodeForBusStop(start);
            var endNode = GetGraphNodeForBusStop(end);

            var list = new List<PartOfBusLine>();
            var collectionOfEdges = startNode.GraphEdges.Where(x => x.Target.BusStop.Id == endNode.BusStop.Id);
            
            if (!allowWalkLinks)
            {
                collectionOfEdges = collectionOfEdges.Where(x => x.ConnectionType == ConnectionType.Bus);
            }
            
            
            

            foreach (var edge in collectionOfEdges.Where(x => x.BusLine != null))
            {
                list.Add(new PartOfBusLine(start, end, edge.BusLine, ConnectionType.Bus));   
            }

            if (allowWalkLinks && start.GetWalkTime(end) <= maxWalkTime)
            {
                list.Add(new PartOfBusLine(start, end, ConnectionType.Walk));
            }

            return list;
        }

    }
}
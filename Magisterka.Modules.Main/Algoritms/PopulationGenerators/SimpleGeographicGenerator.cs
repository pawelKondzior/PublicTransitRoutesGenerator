// -----------------------------------------------------------------------
//  <copyright file="SimpleGeographicGenerator.cs" company="DevCore .NET">
//      Copyright (c) DevCore.NET All rights reserved.
//  </copyright>
//  <author>Paweł Kondzior</author>
// -----------------------------------------------------------------------


using System.Collections.Generic;

namespace Magisterka.Modules.Main.Algoritms.PopulationGenerators
{
    using Magisterka.Infrastructure.Shared.Structure;
    using System.Linq;

    public class SimpleGeographicGenerator : BasePopulationGenerator
    {
        #region Construcotrs

        //public SimpleGeographicGenerator()
        //{  
        //}

        public SimpleGeographicGenerator(AlgorithmParameters baseAlgorithmParameters)
            :base(baseAlgorithmParameters)
        {}

        #endregion  


        public override Population Generate()
        {
            var distance = AlgorithmParameters.Start.GetSimpleDistance(AlgorithmParameters.End);

            var list = AlgorithmParameters.BusStopList
                .Where(x => x.GetSimpleDistance(AlgorithmParameters.Start) <= distance
                         && x.GetSimpleDistance(AlgorithmParameters.End) <= distance);

            var startMax = list.Max(x => x.GetSimpleDistance(AlgorithmParameters.Start));
            var endMax = list.Max(x => x.GetSimpleDistance(AlgorithmParameters.End));

            var contains = list.Contains(AlgorithmParameters.End);

            var endBusStopId = AlgorithmParameters.End.Id;

            var startNode = AlgorithmParameters.BusGraph.GetGraphNode(AlgorithmParameters.Start);

            var aggregateResults = new List<List<BusStop>>();

            startNode.GetLineCombinationContainingBusStops(endBusStopId, list, new List<BusStop>(), ref aggregateResults);

            return new Population(aggregateResults, AlgorithmParameters.BusGraph, AlgorithmParameters.BusLines);
        }
    }
}
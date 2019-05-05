// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BusLinesList.cs" company="">
//   
// </copyright>
//  <author>Paweł Kondzior</author>
// <summary>
//   The bus lines list.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Magisterka.Infrastructure.Shared.Structure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Magisterka.Infrastructure.Shared.Collections;
    using Magisterka.Infrastructure.Shared.Enum;
    using Magisterka.Infrastructure.Shared.IoDto;
    using Magisterka.Infrastructure.Shared.Comparers;

    /// <summary>
    /// The bus lines list.
    /// </summary>
    [Serializable]
    public class BusLinesList : List<BusLineIoDto>
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get bus lines for bus stop.
        /// </summary>
        /// <param name="busStop">
        /// The bus stop.
        /// </param>
        /// <returns>
        /// The System.Collections.Generic.List`1[T -&gt; Magisterka.Infrastructure.Shared.IoDto.BusLineIoDto].
        /// </returns>
        public List<BusLineIoDto> GetBusLinesForBusStop(BusStop busStop)
        {
            var list = new List<BusLineIoDto>();

            foreach (BusLineIoDto busLine in this)
            {
                if (busLine.ContainsBusStop(busStop))
                {
                    list.Add(busLine);
                }
            }

            return list;
        }

        /// <summary>
        /// Pobiera liste linie w którzych znajdują sie przystanki
        /// </summary>
        /// <param name="startBusStopId">
        /// The start Bus Stop Id.
        /// </param>
        /// <param name="endBusStopId">
        /// The end Bus Stop Id.
        /// </param>
        /// <param name="busStopList">
        /// The bus Stop List.
        /// </param>
        /// <returns>
        /// The System.Collections.Generic.List`1[T -&gt; Magisterka.Infrastructure.Shared.Structure.PartOfBusLine].
        /// </returns>
        public List<PartOfBusLine> GetConnectedBusLines(int startBusStopId, int endBusStopId, BusStopList busStopList, int maxWalkTime)
        {
            BusStop start = busStopList.FirstOrDefault(x => x.ArrayNumber == startBusStopId);
            BusStop end = busStopList.FirstOrDefault(x => x.ArrayNumber == endBusStopId);

            return GetConnectedBusLines(start, end, maxWalkTime);
        }

        /// <summary>
        /// Pobiera liste linie w którzych znajdują sie przystanki
        /// </summary>
        /// <param name="start">
        /// Przystanek początkowy 
        /// </param>
        /// <param name="end">
        /// Przystanek końcowy
        /// </param>
        /// <param name="maxWalkTime">
        /// maksymalny określony czas na przjście pieszo 
        /// </param>
        /// <returns>
        /// The System.Collections.Generic.List`1[T -&gt; Magisterka.Infrastructure.Shared.Structure.PartOfBusLine].
        /// </returns>
        public List<PartOfBusLine> GetConnectedBusLines(BusStop start, BusStop end, int maxWalkTime)
        {
            var list = new List<PartOfBusLine>();

            foreach (BusLineIoDto item in this)
            {
                if (item.BusStopsAreConnected(start, end))
                {
                    list.Add(new PartOfBusLine(start, end, item, ConnectionType.Bus));
                }
            }

            if (start.GetWalkTime(end) <= maxWalkTime)
            {
                list.Add(new PartOfBusLine(start, end, ConnectionType.Walk));
            }

            return list;
        }


        public bool BusStopsAreConnected(BusStop start, BusStop end)
        {
            foreach (BusLineIoDto item in this)
            {
                if (item.BusStopsAreConnected(start, end))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Pobiera liste przysanków do jktórych mozna dojechać z obecnego
        /// </summary>
        /// <param name="busStop">
        /// </param>
        /// <returns>
        /// The System.Collections.Generic.List`1[T -&gt; System.Int32].
        /// </returns>
        public List<int> GetNextBusStopsIds(BusStop busStop)
        {
            var list = new List<int>();

            foreach (BusLineIoDto busLine in this)
            {
                int? item = busLine.GetNextBusStopId(busStop);

                if (item.HasValue)
                {
                    list.Add(item.Value);
                }
            }

            return list;
        }


        public IEnumerable<BusStopLineIoDto> GetAllBusStopsDistinct()
        {
            var query = this.SelectMany(busLine => busLine.Select(busStop => busStop));

            var distinctList = query.Distinct(new BusStopLineIoDtoComparer());

            return distinctList;
        }


        public List<BusLineIoDto> GetLinesContainingBusStops(List<BusStop> busStopList)
        {
            var result = new List<BusLineIoDto>();

            var busStopsIds = busStopList.Select(x => x.Id);

            this.ForEach(line =>
            {
                var lineBusStops = line.Select(x => x.BusStopId);

                if (lineBusStops.Intersect(busStopsIds).Any())
                {
                    result.Add(line);
                }
            });

            return result;
        }

        #endregion

        // <summary>
        /// Pobiera liste linei autobusowych ktorymi mozna przejechać tą trase, wybiera najdluzsze line (pod wzgledem przystankow)
        /// </summary>
        /// <param name="busStops"></param>
        /// <returns></returns>
        // public List<PartOfBusLine> GetLongestBusLines(List<BusStop> busStops)
        // {
        // var result = new List<PartOfBusLine>();

        // foreach (var busStop in busStops)
        // {

        // }
        // }
    }
}
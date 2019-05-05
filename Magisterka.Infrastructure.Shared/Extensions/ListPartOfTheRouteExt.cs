
// // -----------------------------------------------------------------------
// //  <copyright company="DevCore .NET">
// //      Copyright (c) DevCore.NET All rights reserved.
// //  </copyright>
// //  <author> Paweł Kondzior</author>
// // -----------------------------------------------------------------------

namespace Magisterka.Infrastructure.Shared.Extensions
{
    using Magisterka.Infrastructure.Shared.Structure;
    using System.Collections.Generic;
    using System.Linq;

    public static class ListPartOfTheRouteExt
    {
        /// <summary>
        /// Pobiera poprzeni i nastepny bus stop id dla bus Stop Id 
        /// </summary>
        public static PrevAndNextBusStopsIds GetPrevAndNexBusStopId(this List<PartOfTheRoute> partoOfTheRoute, int busStopId)
        {
            var result = new PrevAndNextBusStopsIds(busStopId);

            var currentbusStop = partoOfTheRoute.FirstOrDefault(x => x.BusStop.Id == busStopId);
            var currentIndex = partoOfTheRoute.IndexOf(currentbusStop);

            if (currentIndex > 0)
            {
                var prevIndex = currentIndex - 1;
                result.PrevBusStopId = partoOfTheRoute[prevIndex].BusStop.Id;     
            }

            if (currentIndex + 1 < partoOfTheRoute.Count)
            {
                var nextIndex = currentIndex + 1;
                result.NextBusStopId = partoOfTheRoute[nextIndex].BusStop.Id;
            }

            return result;
        }

        
    }
}
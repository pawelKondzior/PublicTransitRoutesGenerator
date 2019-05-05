// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PartOfTheRoute.cs" company="">
//   
// </copyright>
//  <author>Paweł Kondzior</author>
// <summary>
//   tak jak RouteElement
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Magisterka.Infrastructure.Shared.Structure
{
    using Magisterka.Infrastructure.Shared.Enum;
    using Magisterka.Infrastructure.Shared.IoDto;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// tak jak RouteElement
    /// </summary>
    public class PartoOfTheRouteDistances
    {


        public PartoOfTheRouteDistances(PartOfTheRoute partOfTheRoute, List<PartOfTheRoute> partOfTheRoutes, int stopsMaxDistance)
        {
            BusStopDistancses = new List<BusStopDistancs>();
            StartPartOfTheRoute = partOfTheRoute;
            BusStop = partOfTheRoute.BusStop;

            int counter = 1;
            double routeDistance = 0.0;

            foreach (var part in partOfTheRoutes)
            {
                var distance = StartPartOfTheRoute.BusStop.GetDistance(part.BusStop);
                routeDistance += distance;

                BusStopDistancses.Add(new BusStopDistancs(StartPartOfTheRoute, part, counter, distance, routeDistance));
                counter++;

                if (counter == stopsMaxDistance)
                {
                    break;
                }
            }

            ItemWithBestRatio = BusStopDistancses.OrderByDescending(x => x.DisanceToStopsCountRatio).FirstOrDefault();
        }

        public PartOfTheRoute StartPartOfTheRoute { get; private set; }

        /// <summary>
        /// Gets or sets the bus stop.
        /// </summary>
        public BusStop BusStop { get; private set; }


        public List<BusStopDistancs> BusStopDistancses { get; private set; }

        public BusStopDistancs ItemWithBestRatio { get; private set; }

    }
}
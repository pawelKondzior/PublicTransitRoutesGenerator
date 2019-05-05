// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BusStopDistancs.cs" company="">
//   
// </copyright>
// //  <author>Paweł Kondzior</author>
// <summary>
//   The bus stop distancs.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Magisterka.Infrastructure.Shared.Structure
{
    /// <summary>
    /// The bus stop distancs.
    /// </summary>
    public class BusStopDistancs
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BusStopDistancs"/> class.
        /// </summary>
        /// <param name="busStop">
        /// The bus stop.
        /// </param>
        /// <param name="distance">
        /// The distance.
        /// </param>
        public BusStopDistancs(PartOfTheRoute startPartOfTheRoute, PartOfTheRoute endPartOfTheRoute, 
            int nuberOfBusStopsFromStart, double distance, double routeDistance)
        {
            this.StartPartOfTheRoute = startPartOfTheRoute;
            this.EndPartOfTheRoute = endPartOfTheRoute;

            this.Distance = distance;
            this.RouteDistance = routeDistance;
            // this.Distance = startPartOfTheRoute.BusStop.GetDistance(EndPartOfTheRoute.BusStop);
            this.NuberOfBusStopsFromStart = nuberOfBusStopsFromStart;

            if (Distance == 0.0)
            {
                this.DisanceToStopsCountRatio = 0.0;
            }
            else
            {
                this.DisanceToStopsCountRatio = RouteDistance / Distance;
            }
        }

        #endregion

        #region Public Properties

        public PartOfTheRoute StartPartOfTheRoute { get; set; }

        public PartOfTheRoute EndPartOfTheRoute { get; set; }

        /// <summary>
        /// Gets or sets the distance.
        /// </summary>
        public double Distance { get; private set; }

        public double RouteDistance { get; private set; }

        /// <summary>
        /// Ile przystanków od początkowego przystanków
        /// </summary>
        public int NuberOfBusStopsFromStart { get; private set; }


        public double DisanceToStopsCountRatio { get; private set; }
        #endregion
    }
}
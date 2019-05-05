// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PartOfBusLine.cs" company="">
//   
// </copyright>
//  <author>Paweł Kondzior</author>
// <summary>
//   Trasa z jednogo punktu do drugiego wraz z przystankami poczatkowym i koncowym
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Magisterka.Infrastructure.Shared.Structure
{
    using System.Collections.Generic;

    using Magisterka.Infrastructure.Shared.IoDto;
    using Magisterka.Infrastructure.Shared.Enum;
    using Magisterka.Infrastructure.Shared.Collections;

    /// <summary>
    /// Trasa z jednogo punktu do drugiego wraz z przystankami poczatkowym i koncowym
    /// </summary>
    public class PartOfBusLine
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PartOfBusLine"/> class.
        /// </summary>
        /// <param name="start">
        /// The start.
        /// </param>
        /// <param name="end">
        /// The end.
        /// </param>
        /// <param name="busLineIoDto">
        /// The bus line io dto.
        /// </param>
        /// <param name="connectionType">
        /// The connection Type.
        /// </param>
        public PartOfBusLine(BusStop start, BusStop end, BusLineIoDto busLineIoDto, ConnectionType connectionType)
        {
            this.Start = start;
            this.End = end;
            this.BusLineIoDto = busLineIoDto;
            this.ConnectionType = connectionType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PartOfBusLine"/> class.
        /// </summary>
        /// <param name="start">
        /// The start.
        /// </param>
        /// <param name="end">
        /// The end.
        /// </param>
        /// <param name="connectionType">
        /// The connection type.
        /// </param>
        public PartOfBusLine(BusStop start, BusStop end, ConnectionType connectionType)
        {
            this.Start = start;
            this.End = end;
            this.ConnectionType = connectionType;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the bus line io dto.
        /// </summary>
        public BusLineIoDto BusLineIoDto { get; set; }

        /// <summary>
        /// Gets or sets the end.
        /// </summary>
        public BusStop End { get; set; }

        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        public BusStop Start { get; set; }

        /// <summary>
        /// Gets or sets the connection type.
        /// </summary>
        public ConnectionType ConnectionType { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The are lines connected with walk line.
        /// </summary>
        /// <param name="partOfBusLine">
        /// The part of bus line.
        /// </param>
        /// <param name="walkLineChanges">
        /// The walk line changes.
        /// </param>
        /// <returns>
        /// The System.Boolean.
        /// </returns>
        public bool AreLinesConnectedWithWalkLine(PartOfBusLine partOfBusLine, WalkLineChanges walkLineChanges)
        {
            if (this.End == walkLineChanges.Start && partOfBusLine.Start == walkLineChanges.End)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// The get connecting walk line.
        /// </summary>
        /// <param name="partOfBusLine">
        /// The part of bus line.
        /// </param>
        /// <param name="lineChangeses">
        /// The line changeses.
        /// </param>
        /// <returns>
        /// The Magisterka.Infrastructure.Shared.Structure.WalkLineChanges.
        /// </returns>
        public WalkLineChanges GetConnectingWalkLine(PartOfBusLine partOfBusLine, List<WalkLineChanges> lineChangeses)
        {
            foreach (WalkLineChanges item in lineChangeses)
            {
                if (this.AreLinesConnectedWithWalkLine(partOfBusLine, item))
                {
                    return item;
                }
            }

            return null;
        }

        public List<BusStop> GetMiddleBusStops(ref BusLinesList busLinesList, ref BusStopList busStopList)
        {
            //return BusLineIoDto.GetMiddleBusStops(this.Start, this.End, ref  busStopList);
            return BusLineIoDto.GetMiddleBusStops(this.Start, this.End, this.BusLineIoDto, ref busStopList);
        }

        #endregion
    }
}
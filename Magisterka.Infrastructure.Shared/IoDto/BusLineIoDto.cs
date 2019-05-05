// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BusLineIoDto.cs" company="">
//   
// </copyright>
// <summary>
//   The bus line io dto.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Diagnostics;
using Anotar.Log4Net;

namespace Magisterka.Infrastructure.Shared.IoDto
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Magisterka.Infrastructure.Shared.Enum;
    using Magisterka.Infrastructure.Shared.Structure;
    using Magisterka.Infrastructure.Shared.Extensions;
using Magisterka.Infrastructure.Shared.Collections;


    /// <summary>
    /// The bus line io dto.
    /// </summary>
    [Serializable]
    public class BusLineIoDto : List<BusStopLineIoDto>
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the name and variant.
        /// </summary>
        public string NameAndVariant
        {
            get
            {
                return this.Name + " " + this.Variant.ToString();
            }
        }

        /// <summary>
        /// Gets or sets the variant.
        /// </summary>
        public int Variant { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Sprawdza czy przystanek koncowy oi początkwiy istnieja w trasie
        /// </summary>
        /// <param name="start">
        /// </param>
        /// <param name="end">
        /// </param>
        /// <returns>
        /// The System.Boolean.
        /// </returns>
        public bool BusStopsAreConnected(BusStop start, BusStop end)
        {
            int startItemIndex = this.FindIndex(x => x.BusStopId == start.Id);
            int endItemIndex = this.FindIndex(x => x.BusStopId == end.Id);

            

            if (startItemIndex >= 0 && endItemIndex >= 0 && startItemIndex < endItemIndex)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// The contains bus stop.
        /// </summary>
        /// <param name="busStop">
        /// The bus stop.
        /// </param>
        /// <returns>
        /// The System.Boolean.
        /// </returns>
        public bool ContainsBusStop(BusStop busStop)
        {
            return this.Any(x => x.BusStopId == busStop.Id);
        }

        /// <summary>
        /// The get first time for bus stop.
        /// </summary>
        /// <param name="busStop">
        /// The bus stop.
        /// </param>
        /// <param name="startTime">
        /// The start time.
        /// </param>
        /// <param name="dayTypeEnum">
        /// The day type enum.
        /// </param>
        /// <returns>
        /// The Magisterka.Infrastructure.Shared.Structure.Time.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public Time GetFirstTimeForBusStop(BusStop busStop, Time startTime, DayTypeEnum dayTypeEnum)
        {
            startTime.FixTime();
            BusStopLineIoDto busLineStop = this.FirstOrDefault(x => x.BusStopId == busStop.Id);

            List<Time> queryCollection = null;
            switch (dayTypeEnum)
            {
                case DayTypeEnum.WorkingDay:
                    {
                        queryCollection = busLineStop.BusinessDaySchedule;
                        break;
                    }

                case DayTypeEnum.Saturday:
                    {
                        queryCollection = busLineStop.SaturdaySchedule;
                        break;
                    }

                case DayTypeEnum.Sunday:
                    {
                        queryCollection = busLineStop.SundaySchedule;
                        break;
                    }

                default:
                    throw new ArgumentOutOfRangeException("dayTypeEnum");
            }

            Time time = null;
            if (queryCollection.Any() && startTime != null)
            {
                time = queryCollection.OrderBy(x => x.MinutesSum).FirstOrDefault(x => x.MinutesSum >= startTime.MinutesSum );

                if (time == null)
                {
                    LogTo.Warn("Time == null   - biore nastpny dzien");


                    time = queryCollection.OrderBy(x => x.MinutesSum).FirstOrDefault();
                }
            }
            else
            {
                LogTo.Warn("Brak czasu dojazdu w xml");
            }

            // .Where(x => x.MinutesSum >= startTime.MinutesSum).Min(x =>);`
            return time;
        }

        /// <summary>
        /// The get middle bus stops ids.
        /// </summary>
        /// <param name="start">
        /// The start.
        /// </param>
        /// <param name="end">
        /// The end.
        /// </param>
        /// <returns>
        /// The System.Collections.Generic.IEnumerable`1[T -&gt; System.Int32].
        /// </returns>
        public IEnumerable<int> GetMiddleBusStopsIds(BusStop start, BusStop end)
        {
            int startItemIndex = this.FindIndex(x => x.BusStopId == start.Id);
            int endItemIndex = this.FindIndex(x => x.BusStopId == end.Id);

            int count = endItemIndex - startItemIndex;

            return this.GetRange(startItemIndex + 1, count).Select(x => x.BusStopId);
        }

        /// <summary>
        /// TODO: to można bardzo zoptymyalizawoać mając już wcześniejszą liste BusStopów
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="busLineIoDto"></param>
        /// <param name="busStopList"></param>
        /// <returns></returns>
        public List<BusStop> GetMiddleBusStops(BusStop start, BusStop end, BusLineIoDto busLineIoDto, ref BusStopList busStopList)
        // public List<BusStop> GetMiddleBusStops(BusStop start, BusStop end, )
        {
            int startItemIndex = this.FindIndex(x => x.BusStopId == start.Id);
            int endItemIndex = this.FindIndex(x => x.BusStopId == end.Id);

            var bettwenBusStops = this.BetweenTwoIndexes(startItemIndex, endItemIndex);//.Select(x => x.BusStopId);

            var result = new List<BusStop>();

            foreach (var tempStop in bettwenBusStops)
            {
                var item = busStopList.FirstOrDefault(x => x.Id == tempStop.BusStopId);

                if (item == null) /// brak w liscie od otwarty wrocław
                {

                    LogTo.Warn("Brakujacy przystanek Id {0}", tempStop.BusStopId);

                    item = new BusStop();
                    item.Id = tempStop.BusStopId;
                    item.NotExistingInBusList = true;
                }

                result.Add(item);
            }

            return result;
        }

        /// <summary>
        /// Zwraca id kolekjeego przystanku autobusowego, jeśli ten przystanek znajduje się w tej trasie
        /// </summary>
        /// <param name="busStop">
        /// </param>
        /// <returns>
        /// The System.Nullable`1[T -&gt; System.Int32].
        /// </returns>
        public int? GetNextBusStopId(BusStop busStop)
        {
            BusStopLineIoDto item = this.FirstOrDefault(x => x.BusStopId == busStop.Id);

            if (item != null)
            {
                int index = this.IndexOf(item);

                if (index < this.Count - 1)
                {
                    return this[index + 1].BusStopId;
                }
            }

            return null;
        }

        //public BusStop GetNextBusStop(BusStop busStop)
        //{
        //    BusStopLineIoDto item = this.FirstOrDefault(x => x.BusStopId == busStop.Id);

        //    if (item != null)
        //    {
        //        int index = this.IndexOf(item);

        //        if (index < this.Count - 1)
        //        {
        //            return this[index + 1];
        //        }
        //    }

        //    return null;
        //}

        public string DisplayValue
        {
            get
            {
                return string.Format("{0}", this.NameAndVariant);
            }
        }

        #endregion
    }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntireRoute.cs" company="">
//   
// </copyright>
//  <author>Paweł Kondzior</author>
// <summary>
//   Takie coś jak Candidate
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Magisterka.Infrastructure.Shared.Structure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Magisterka.Infrastructure.Shared.Enum;
    using Magisterka.Infrastructure.Shared.Interfaces;
    using Magisterka.Infrastructure.Shared.IoDto;
    using Magisterka.Infrastructure.Shared.Extensions;
    using Anotar.Log4Net;
    using MoreLinq;

    /// <summary>
    ///     Takie coś jak Candidate
    /// </summary>
    public class EntireRoute : ISubject
    {
        #region Fields

        /// <summary>
        ///     The _route time in minutes.
        /// </summary>
        private int? _routeTimeInMinutes;


        public int BusConnectionsCount { get; private set; }


        public int WalkConnectionsCount { get; private set; }

        public int LinesCount { get; private set; }


        public string LinesNames { get; private set; }


        /// <summary>
        /// Lista przystankow z jakich trasa została tworzona
        /// </summary>
        public List<BusStop> CreationInputBusLines { get; set; }

        #endregion

        #region Constructors and Destructors



        public EntireRoute(List<BusStop> creationInputBusLines)
            : this()
        {
            this.CreationInputBusLines = creationInputBusLines;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EntireRoute" /> class.
        /// </summary>
        public EntireRoute()
        {
            this.PartOfTheRoute = new List<PartOfTheRoute>();
            this.BusChengesList = new List<BusStop>();
            this.BusMutatedList = new List<BusStop>();
            this.LinesNames = string.Empty;

        }

        public EntireRoute(List<PartOfTheRoute> routes)
           : this()
        {
            routes.ForEach(x => PartOfTheRoute.Add(x));
            this.LinesNames = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntireRoute"/> class.
        /// </summary>
        /// <param name="lineCombination">
        /// The line combination.
        /// </param>
        public EntireRoute(LineCombination lineCombination)
            : this()
        {
            foreach (PartOfTheRoute partOfTheRoute in this.PartOfTheRoute)
            {
                this.PartOfTheRoute.Add(partOfTheRoute);
            }
            this.LinesNames = string.Empty;
        }



        /// <summary>
        /// Initializes a new instance of the <see cref="EntireRoute"/> class.
        /// </summary>
        /// <param name="busStops">
        /// The bus stops.
        /// </param>
        /// <param name="busGraph">
        /// The bus graph.
        /// </param>
        public EntireRoute(List<BusStop> busStops, BusGraph busGraph)
            : this()
        {
            // BusStopList busStopList = new BusStopList();
            foreach (BusStop busStop in busStops)
            {
                var partOfTheRoute = new PartOfTheRoute { BusStop = busStop };

                this.PartOfTheRoute.Add(partOfTheRoute);
            }
            this.LinesNames = string.Empty;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the adaptation function result.
        /// </summary>
        public int AdaptationFunctionResult { get; set; }

        /// <summary>
        ///     Gets or sets the evaluation function result.
        /// </summary>
        public double EvaluationFunctionResult { get; set; }

        /// <summary>
        ///     Ile tazy ten elemnt został wybrany do mutacji
        /// </summary>
        public int NumberOfSelectingForMutation { get; set; }


        public List<PartOfTheRoute> PartOfTheRoute { get; set; }

        /// <summary>
        /// Gets or sets lista przesiadek
        /// </summary>
        public List<BusStop> BusChengesList { get; set; }

        public int TransferCount { get; set; }


        /// <summary>
        /// Get or sets mutowane przystanki
        /// </summary>
        public List<BusStop> BusMutatedList { get; set; }

        /// <summary>
        /// Określa czy powstalo za pomocą mutacji/hybrydyzacji czy może jest z populacji początkowej
        /// </summary>
        public bool IsGeneretedByEaAlgotithm
        {
            get
            {
                if (IsFromHybrydyzaion || IsFromMutation)
                {
                    return true;
                }
                return false;
            }
        }

        public int Generation { get; set; }

        public bool IsFromMutation { get; set; }

        public bool IsFromHybrydyzaion { get; set; }


        public bool TimesCorrect { get; set; }

        /// <summary>
        
        ///     Gets the route time.
        /// </summary>
        public int RouteTime
        {
            get
            {
                if (this._routeTimeInMinutes.HasValue)
                {
                    return this._routeTimeInMinutes.Value;
                }

                this.CalculateRouteTime();

                if (this._routeTimeInMinutes.HasValue)
                {
                    return this._routeTimeInMinutes.Value;
                }

                return 0;
            }
        }

        /// <summary>
        ///     Gets or sets the time from arrive.
        /// </summary>
        public int TimeFromArrive { get; set; }





        #endregion

        #region Public Methods and Operators


        public PartOfTheRoute GetNextPartOfTheRouteFor(PartOfTheRoute current)
        {
            return this.PartOfTheRoute.GetNext(current);
        }


        public PartOfTheRoute GetPreviousPartOfTheRouteFor(PartOfTheRoute current)
        {
            return this.PartOfTheRoute.GetPrevious(current);
        }



        public BusStop GetNextBusStopFor(BusStop currentBusStop)
        {
            return this.PartOfTheRoute.Select(x => x.BusStop).GetNext(currentBusStop);
        }


        public BusStop GetPreviousBusStopFor(BusStop currentBusStop)
        {
            return this.PartOfTheRoute.Select(x => x.BusStop).GetPrevious(currentBusStop);
        }


        /// </param>
        public void AddToRoute(PartOfBusLine partOfBusLine)
        {
            var lastItem = PartOfTheRoute.LastOrDefault();

            if (lastItem == null)
            {
                var newItem = new PartOfTheRoute();
                newItem.BusStop = partOfBusLine.Start;
                newItem.InputLine = null;
                this.PartOfTheRoute.Add(newItem);
                lastItem = newItem;
            }

            lastItem.OutputLine = partOfBusLine.BusLineIoDto;
            lastItem.NextBusStopConnection = partOfBusLine.ConnectionType;

            var nextItem = new PartOfTheRoute();
            nextItem.BusStop = partOfBusLine.End;
            nextItem.InputLine = partOfBusLine.BusLineIoDto;
            nextItem.OutputLine = null;

            ChackForTransferChange(lastItem, nextItem);

            this.PartOfTheRoute.Add(nextItem);
        }

        private void ChackForTransferChange(PartOfTheRoute lastItem, PartOfTheRoute nextItem)
        {
            if (lastItem.InputLine != null && lastItem.InputLine != lastItem.OutputLine)
            {
                TransferCount++;
            }

            if (lastItem.NextBusStopConnection == ConnectionType.Walk)
            {
                TransferCount++;
            }


            //else if (lastItem.NextBusStopConnection != nextItem.NextBusStopConnection )
            //{
            //    TransferCount++;
            //}
        }

        public void CalculateBusStopConnections()
        {
            BusConnectionsCount = this.PartOfTheRoute.Count(x => x.NextBusStopConnection == ConnectionType.Bus);
            WalkConnectionsCount = this.PartOfTheRoute.Count(x => x.NextBusStopConnection == ConnectionType.Walk);
            LinesCount = this.PartOfTheRoute.Where(x => x.OutputLine != null).Select(x => x.OutputLine.Name).Distinct().Count();

            if (LinesCount > 0)
            {
                this.LinesNames = string.Join(" ", this.PartOfTheRoute.Where(x => x.OutputLine != null).Select(x => x.OutputLine.Name).Distinct());
            }



            var prev = this.PartOfTheRoute.FirstOrDefault();

            foreach (var item in PartOfTheRoute.Skip(1))
            {
                if (item.NextBusStopConnection != prev.NextBusStopConnection)
                {
                    BusChengesList.Add(prev.BusStop);
                }
                else if (prev.NextBusStopConnection != null && item.NextBusStopConnection != null 
                    && prev.NextBusStopConnection != item.NextBusStopConnection)
                {
                    BusChengesList.Add(prev.BusStop);
                }


            }
        }


        /// <summary>
        /// Nie testowane
        /// </summary>
        /// <param name="busStops">
        /// </param>
        /// <param name="busLineIoDto">
        /// </param>
        public void AddToRoute(IEnumerable<BusStop> busStops, BusLineIoDto busLineIoDto)
        {
            BusStop firstItem = busStops.FirstOrDefault();
            BusStop lastItem = busStops.LastOrDefault();

            foreach (BusStop busStop in busStops)
            {
                var partOfTheRoute = new PartOfTheRoute();
                partOfTheRoute.NextBusStopConnection = ConnectionType.Bus;
                partOfTheRoute.BusStop = busStop;

                if (firstItem == busStop)
                {
                    partOfTheRoute.OutputLine = busLineIoDto;
                }
                else if (lastItem == busStop)
                {
                    partOfTheRoute.OutputLine = null;
                    partOfTheRoute.InputLine = busLineIoDto;
                }
                else
                {
                    partOfTheRoute.OutputLine = busLineIoDto;
                    partOfTheRoute.InputLine = busLineIoDto;
                }

                this.PartOfTheRoute.Add(partOfTheRoute);
            }
        }

        /// <summary>
        /// Nie testowane
        /// </summary>
        /// <param name="partOfBusLine">
        /// The part Of Bus Line.
        /// </param>
        /// <param name="busStops">
        /// </param>
        public void AddToRoute(PartOfBusLine partOfBusLine, List<BusStop> busStops)
        {
            var currentLastPartOfThRoute = this.PartOfTheRoute.LastOrDefault();

            var firstPartOfTheRoute = new PartOfTheRoute();
            firstPartOfTheRoute.BusStop = partOfBusLine.Start;
            firstPartOfTheRoute.OutputLine = partOfBusLine.BusLineIoDto;
            firstPartOfTheRoute.NextBusStopConnection = partOfBusLine.ConnectionType;

            if (currentLastPartOfThRoute == null)
            {
                this.PartOfTheRoute.Add(firstPartOfTheRoute);
            }
            else
            {
                currentLastPartOfThRoute.OutputLine = partOfBusLine.BusLineIoDto;
                currentLastPartOfThRoute.NextBusStopConnection = partOfBusLine.ConnectionType;


                /// NIE WIEM W JAKIM CELU TO BYŁO
                /// DODAWAŁO DUPLIKATY
                //if (currentLastPartOfThRoute.BusStop.Id != firstPartOfTheRoute.BusStop.Id
                //  || currentLastPartOfThRoute.NextBusStopConnection != firstPartOfTheRoute.NextBusStopConnection)
                //{
                //    this.PartOfTheRoute.Add(firstPartOfTheRoute);
                //}
            }



            if (busStops != null)
            {
                foreach (BusStop busStop in busStops)
                {
                    var partOfTheRoute = new PartOfTheRoute();
                    partOfTheRoute.BusStop = busStop;
                    partOfTheRoute.InputLine = partOfBusLine.BusLineIoDto;
                    partOfTheRoute.OutputLine = partOfBusLine.BusLineIoDto;
                    partOfTheRoute.NextBusStopConnection = partOfBusLine.ConnectionType;

                    this.PartOfTheRoute.Add(partOfTheRoute);
                }
            }

            var lastPartOfTheRoute = new PartOfTheRoute();

            // lastPartOfTheRoute.BusStop = partOfBusLine.Start;
            lastPartOfTheRoute.BusStop = partOfBusLine.End;
            lastPartOfTheRoute.InputLine = partOfBusLine.BusLineIoDto;
            lastPartOfTheRoute.NextBusStopConnection = partOfBusLine.ConnectionType;
            this.PartOfTheRoute.Add(lastPartOfTheRoute);
        }

        /// <summary>
        /// Wyliza funnckje przystosowana
        /// </summary>
        /// <param name="functionTypeEnum">
        /// </param>
        public void CalculateAdaptationFunction(AdaptationFunctionTypeEnum functionTypeEnum)
        {
            switch (functionTypeEnum)
            {
                case AdaptationFunctionTypeEnum.ByTime:
                    {
                        this.CalculateAdaptationFunctionByTime();
                        break;
                    }

                case AdaptationFunctionTypeEnum.CalculateAdaptationFunctionByBusStops:
                    {
                        this.CalculateAdaptationFunctionByBusStops();
                        break;
                    }

                case AdaptationFunctionTypeEnum.ByFewestBusChanges:
                    {
                        this.CalculateAdaptationFunctionByFewestBusChanges();
                        break;
                    }

                default:
                    throw new ArgumentOutOfRangeException("functionTypeEnum");
            }
        }

        /// <summary>
        ///     The calculate route time.
        /// </summary>
        public void CalculateRouteTime()
        {
            if (this.PartOfTheRoute != null && this.PartOfTheRoute.Count > 0)
            {
                PartOfTheRoute startElement = this.PartOfTheRoute.FirstOrDefault();
                PartOfTheRoute lastElement =
                    this.PartOfTheRoute.LastOrDefault(x => x.InputTime != null && x.OutputTime != null);

                if (startElement != null && lastElement != null)
                {
                    Time startElementTime = null;
                    Time lastElementtime = null;

                    if (startElement.InputTime != null)
                    {
                        startElementTime = startElement.InputTime;
                    }
                    else if (startElement.OutputTime != null)
                    {
                        startElementTime = startElement.OutputTime;
                    }

                    if (lastElement.InputTime != null)
                    {
                        lastElementtime = lastElement.InputTime;
                    }
                    else if (lastElement.OutputTime != null)
                    {
                        lastElementtime = lastElement.OutputTime;
                    }

                    if (lastElementtime == null || startElementTime == null)
                    {
                        this._routeTimeInMinutes = 0;
                    }
                    else
                    {
                        this._routeTimeInMinutes = lastElementtime.MinutesSum - startElementTime.MinutesSum;
                    }
                }
            }
        }

        /// <summary>
        /// Oblicza czas trasy od momentu rozpoczecia (to ci wpisal uzytkownik) do ostatniego przystanu
        /// </summary>
        /// <param name="arriveTime">
        /// </param>
        public void CalculateTimeFromArrive(Time arriveTime)
        {
            if (this.PartOfTheRoute != null && this.PartOfTheRoute.Count > 0)
            {
                PartOfTheRoute lastElement = this.PartOfTheRoute.LastOrDefault();

                if (lastElement == null || lastElement.InputTime == null)
                {
                    lastElement = this.PartOfTheRoute.LastOrDefault(x => x.InputTime != null);
                }

                if (lastElement != null && lastElement.InputTime != null)
                {
                    this.TimeFromArrive = lastElement.InputTime.MinutesSum - arriveTime.MinutesSum;
                }
            }
        }

        /// <summary>
        ///     The clone.
        /// </summary>
        /// <returns>
        ///     The System.Object.
        /// </returns>
        public EntireRoute Clone()
        {
            var returnObj = new EntireRoute();
            returnObj.PartOfTheRoute = new List<Structure.PartOfTheRoute>();

            this.PartOfTheRoute.ForEach(x => returnObj.PartOfTheRoute.Add(x.Clone()));
            returnObj.TransferCount = this.TransferCount;


            returnObj.CreationInputBusLines = this.CreationInputBusLines;

            return returnObj;
        }

        /// <summary>
        /// The complete with times.
        /// </summary>
        /// <param name="busGraph">
        /// The bus graph.
        /// </param>
        /// <param name="arriveTime">
        /// The arrive Time.
        /// </param>
        /// <param name="dayTypeEnum">
        /// The day type enum.
        /// </param>
        public bool CompleteWithTimes(Time arriveTime, DayTypeEnum dayTypeEnum)
        {
            var startTime = new Time(arriveTime);

            PartOfTheRoute prev = null;/// this.PartOfTheRoute.FirstOrDefault();
            PartOfTheRoute current = null;
            PartOfTheRoute next = null;
            ///prev.CompleteWithTimes(null, startTime, dayTypeEnum);


            for(int i = 0; i < this.PartOfTheRoute.Count; i++)
            {
                current = this.PartOfTheRoute[i];

                if (i >= 1)
                {
                    prev = this.PartOfTheRoute[i - 1];
                }

                if (i + 1 < this.PartOfTheRoute.Count)
                {
                    next =  this.PartOfTheRoute[i + 1];
                }

                var completeResult = current.CompleteWithTimes(prev, next, ref startTime, dayTypeEnum);

                if (completeResult == false)
                {
                    return false;
                }

                next = null;
            }

            //foreach (PartOfTheRoute item in this.PartOfTheRoute)
            //{
            //    var completeResult = item.CompleteWithTimes(prev, ref startTime, dayTypeEnum);

            //    if (completeResult == false)
            //    {
            //        return false;
            //    }

            //    prev = item;
            //}

            return true;

            //return prev.CompleteWithTimes(null,  ref startTime, dayTypeEnum);
            //return true;

          //  return prev.CompleteWithTimes(null, busGraph, ref startTime, dayTypeEnum);
        }

        /// <summary>
        /// The insert route beetwen.
        /// </summary>
        /// <param name="startPart">
        /// The start part.
        /// </param>
        /// <param name="endPart">
        /// The end part.
        /// </param>
        /// <param name="middleRoute">
        /// The entire route.
        /// </param>
        public void InsertRouteBeetwen(PartOfTheRoute startPart, PartOfTheRoute endPart, EntireRoute middleRoute)
        {
            int startItemIndex = this.PartOfTheRoute.IndexOf(startPart);
            int endItemIndex = this.PartOfTheRoute.IndexOf(endPart);


            var newRoute = new List<PartOfTheRoute>();

            var firstFromMiddle = middleRoute.PartOfTheRoute.FirstOrDefault();
            var lastFromNewRoute = middleRoute.PartOfTheRoute.LastOrDefault();



            var temp = middleRoute.PartOfTheRoute.ToList();
            temp.Remove(firstFromMiddle);
            temp.Remove(lastFromNewRoute);

            var middleToAdd = temp;

            foreach (PartOfTheRoute item in this.PartOfTheRoute)
            {
                int index = this.PartOfTheRoute.IndexOf(item);

                if (index < startItemIndex)
                {
                    newRoute.Add(item);
                }
                else if (index == startItemIndex)
                {
                    newRoute.Add(item);



                    item.NextBusStopConnection = firstFromMiddle.NextBusStopConnection;

                    newRoute.AddRange(middleToAdd);

                }

                else if (index >= endItemIndex)
                {
                    newRoute.Add(item);
                }
            }

            this.PartOfTheRoute = newRoute;

        }

        /// <summary>
        /// The remove bus stops beetwen.
        /// </summary>
        /// <param name="startPart">
        /// The start part.
        /// </param>
        /// <param name="endPart">
        /// The end part.
        /// </param>
        public void RemoveBusStopsBeetwen(PartOfTheRoute startPart, PartOfTheRoute endPart)
        {
            List<PartOfTheRoute> temp = this.PartOfTheRoute.ToList();

            bool startDelete = false;

            foreach (PartOfTheRoute partOfTheRoute in temp)
            {
                if (partOfTheRoute == endPart)
                {
                    return;
                }

                if (startDelete)
                {
                    this.PartOfTheRoute.Remove(partOfTheRoute);
                }

                if (partOfTheRoute == startPart)
                {
                    startDelete = true;
                }
            }
        }

        public List<int> GetHybridizationAvibleBusStopsIds(EntireRoute second)
        {
            var result = new List<int>();

            var first = this;

            var firstBusStopsIds = first.PartOfTheRoute.Select(x => x.BusStop.Id);
            var secondBusStopsIds = second.PartOfTheRoute.Select(x => x.BusStop.Id);

            var intersectedItemsBusStopsIds = firstBusStopsIds.Intersect(secondBusStopsIds);

            foreach (var busStopId in intersectedItemsBusStopsIds)
            {
                var firstItem = first.PartOfTheRoute.GetPrevAndNexBusStopId(busStopId);

                var secondItem = second.PartOfTheRoute.GetPrevAndNexBusStopId(busStopId);

                if (firstItem.HybridizationAvible(secondItem))
                {
                    result.Add(busStopId);
                }
            }

            return result;
        }
        

        public bool CheckIfHaveDuplicateBusStopsOneAfterAnother()
        {
            var duplicateExist = false;// PartOfTheRoute.GroupBy(x => x.BusStop.Id).Any(g => g.Count() > 1);

            var lastOne = PartOfTheRoute.FirstOrDefault();

            if (lastOne == null)
            {
                LogTo.Warn("Brak pierwszego elementu");
                return duplicateExist;
            }

            foreach(var part in PartOfTheRoute.Skip(1))
            {
                if (part.BusStop.Id == lastOne.BusStop.Id)
                {
                    duplicateExist = true;
                    break;
                }

                lastOne = part;
            }


            if (duplicateExist)
            {
                LogTo.Warn("Istinie duplikat w trasie");
            }


            return duplicateExist;
        }

        public bool CheckIfTimesAreCorrectlyIncrementing()
        {
            var correct = true;
            var previus = PartOfTheRoute.FirstOrDefault();

            foreach (var part in PartOfTheRoute.Skip(1))
            {
                if (!previus.CheckIfTimesAreCorrectlyIncrementing()
                    || !part.CheckIfTimesAreCorrectlyIncrementing())
                {
                    correct = false;
                    break;
                }

                var previusTime = previus.GetAnyCorrectTime();
                var currentTime = part.GetAnyCorrectTime();

                if (previusTime == null || currentTime == null)
                {
                    LogTo.Error("Brak czasow w jednym elemencie");
                    correct = false;
                    break;
                }
                else if (previusTime > currentTime )
                {
                    LogTo.Error("Bledne czasy {0} {1}", previus.GetAnyCorrectTime().ToString(), part.GetAnyCorrectTime().ToString());

                    correct = false;
                    break;
                }

                previus  = part;
            }

            TimesCorrect = correct;
            return correct;
        }



        public bool EndsWithThisBusLine(BusLineIoDto busLineIo)
        {
            var lastItem = PartOfTheRoute.LastOrDefault();

            if (lastItem == null)
            {
                return false;
            }

            if (lastItem.InputLine != null && lastItem.InputLine.Name == busLineIo.Name)
            {
                return true;
            }

            return false;
        }


        public BusLineIoDto GetLastInputBusLine()
        {
            var lastItem = PartOfTheRoute.LastOrDefault();

            if (lastItem == null)
            {
                return null;
            }

            return lastItem.InputLine;
        }


        public List<PartOfTheRoute> GetEverythingBeetwenStartEndEndStop()
        {
            var first = this.PartOfTheRoute.FirstOrDefault();
            var last = this.PartOfTheRoute.LastOrDefault();

            var result = this.PartOfTheRoute.ToList();
            result.Remove(first);
            result.Remove(last);

            return result;
        }
        #endregion

        #region Methods

        /// <summary>
        ///     Oblicza funckje przystosowania dla najmnejszej liczy przystanków
        /// </summary>
        private void CalculateAdaptationFunctionByBusStops()
        {
            this.AdaptationFunctionResult = this.PartOfTheRoute.Count;
        }

        /// <summary>
        ///     Oblicza funkcje przystosowania dla najmenjsszej liczby przesiadek
        /// </summary>
        private void CalculateAdaptationFunctionByFewestBusChanges()
        {
            throw new NotImplementedException("Napisac trzeba ....");
        }

        /// <summary>
        ///     Oblicza funckje przystosowania dla najktótszego czasu przejazdy
        /// </summary>
        private void CalculateAdaptationFunctionByTime()
        {
            // jesli jest juz obliczony czas od przyjazdu, ale przewaznie jest
            this.AdaptationFunctionResult = this.TimeFromArrive;
        }




        #endregion
    }
}
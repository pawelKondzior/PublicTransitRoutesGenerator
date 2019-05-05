// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PartOfTheRoute.cs" company="">
//   
// </copyright>
//  <author>Paweł Kondzior</author>
// <summary>
//   tak jak RouteElement
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Anotar.Log4Net;
using Magisterka.Infrastructure.Shared.Extensions;

namespace Magisterka.Infrastructure.Shared.Structure
{
    using Magisterka.Infrastructure.Shared.Enum;
    using Magisterka.Infrastructure.Shared.IoDto;
    using System;

    /// <summary>
    /// tak jak RouteElement
    /// </summary>
    public class PartOfTheRoute
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the bus stop.
        /// </summary>
        public BusStop BusStop { get; set; }

        /// <summary>
        /// Gets or sets the input line.
        /// </summary>
        public BusLineIoDto InputLine { get; set; }

        /// <summary>
        /// Gets or sets the input time.
        /// </summary>
        public Time InputTime { get; set; }

        /// <summary>
        /// Gets or sets the output time.
        /// </summary>
        public Time OutputTime { get; set; }


        

        /// <summary>
        /// Gets or sets the next bus stop connection.
        /// </summary>
        public ConnectionType NextBusStopConnection { get; set; }

        /// <summary>
        /// Gets or sets the output line.
        /// </summary>
        public BusLineIoDto OutputLine { get; set; }

      

        public int NumberOfSelectingForMutation { get; set; }

        #endregion

        public PartOfTheRoute()
        {
            NextBusStopConnection = ConnectionType.Unknown;
        }

        public PartOfTheRoute(PartOfTheRoute theRoute)
        {
            BusStop = theRoute.BusStop;
           

            if (theRoute.InputTime != null)
            {
                InputTime = theRoute.InputTime.Clone();
            }

            if (theRoute.OutputTime != null)
            { 
                OutputTime = theRoute.OutputTime.Clone();
            } 
            
            NextBusStopConnection = theRoute.NextBusStopConnection;

            InputLine = theRoute.InputLine;
            OutputLine = theRoute.OutputLine;
            NumberOfSelectingForMutation = 0;
        }




        #region Public Methods and Operators

        
        public void CompleteWithWalkTimes(PartOfTheRoute nextItem, ref Time startTime, DayTypeEnum dayTypeEnum)
        {
            /// LogTo.Info("Walk");

            //this.InputTime = new Time(startTime);

            // quick fix
            if (this.InputTime != null && this.InputTime > startTime)
            {
                startTime = new Time(this.InputTime);
            }




            this.OutputTime = new Time(startTime);
            

            var minutes = nextItem.BusStop.GetWalkTime(this.BusStop);
            startTime.AddMinutes(minutes);

            nextItem.InputTime = new Time(startTime);
         ///   nextItem.InputTime()
        }

            
        public bool CompleteWithBusTimes(ref Time startTime, DayTypeEnum dayTypeEnum)
        {
            // LogTo.tr("Bus");

            Time inputTime = null;
            Time outputTime = null;
           // Time nextInputTime = null;

            if (this.InputLine != null)
            {
                inputTime = this.InputLine.GetFirstTimeForBusStop(this.BusStop, new Time(startTime), dayTypeEnum);
            }

            if (inputTime != null)
            {
                startTime = new Time(inputTime);
            }

            if (this.OutputLine != null)
            {
                outputTime = this.OutputLine.GetFirstTimeForBusStop(this.BusStop, new Time(startTime), dayTypeEnum);
            }

            //if (nextItem != null && nextItem.InputLine != null && outputTime != null)
            //{
            //    nextInputTime = nextItem.InputLine.GetFirstTimeForBusStop(this.BusStop, new Time(outputTime) , dayTypeEnum);
            //}


            if (inputTime == null && outputTime == null)
            {
                //LogTo.Warn("Brak czasu trasa powinna nie zostać uwzgledniona");
                return false;
            }


            if (inputTime != null && outputTime != null)
            {
                this.OutputTime = new Time(outputTime);
                this.InputTime = new Time(inputTime);

                startTime = new Time(outputTime);
            }
            else if (inputTime != null && outputTime == null)
            {
                this.InputTime = new Time(inputTime);
                startTime = new Time(inputTime);
            }
            else if (inputTime == null && outputTime != null)
            {
           //     this.InputTime = new Time(startTime);
                this.OutputTime = new Time(outputTime);
                startTime = new Time(outputTime);
            }



            return true;

            //if (nextInputTime != null)
            //{
            //    nextItem.InputTime = new Time(nextInputTime);
            //    startTime = new Time(nextInputTime);
            //}
            //else if (this.InputLine == null && this.OutputLine == null)
            //{
            //    this.OutputTime = startTime;
            //}
        }



        public bool CompleteWithTimes(PartOfTheRoute prevItem, PartOfTheRoute nextItem, ref Time startTime, DayTypeEnum dayTypeEnum)
        {
            //if (prevItem != null && prevItem.NextBusStopConnection == ConnectionType.Walk)
            //{

            //    prevItem.CompleteWithWalkTimes(this, ref startTime, dayTypeEnum);
                
            //}


            //if (NextBusStopConnection == ConnectionType.Bus)
         //   {
                CompleteWithBusTimes(ref startTime, dayTypeEnum);  
          //  }
            if( NextBusStopConnection == ConnectionType.Walk)
            {
                CompleteWithWalkTimes(nextItem, ref startTime, dayTypeEnum);
            }
            
            /// dla ostatniego elemntu
            if (this.NextBusStopConnection == ConnectionType.Unknown)
            {
                Time inputTime = null;

                if (this.InputLine != null)
                {
                    inputTime = this.InputLine.GetFirstTimeForBusStop(this.BusStop, startTime, dayTypeEnum);
                }

                if (inputTime == null)
                {

                    var minutes = prevItem.BusStop.GetWalkTime(this.BusStop);
                    startTime.AddMinutes(minutes);
                    this.InputTime = new Time(startTime);
                }
                else
                {
                    this.InputTime = inputTime;
                    startTime = new Time(inputTime);
                }
                
            }

            return true;
            
        }

        public PartOfTheRoute Clone()
        {
            return new PartOfTheRoute(this);
        }

        public bool CheckIfTimesAreCorrectlyIncrementing()
        {
            if (InputTime != null && OutputTime != null)
            {
                
                if (OutputTime >= InputTime)
                {
                    return true;
                }
                return false;
            }

            return true;            
        }

        public Time GetAnyCorrectTime()
        {
            if (InputTime != null)
            {
                return InputTime;
            }

            if (OutputTime != null)
            {
                return OutputTime;
            }

            return null;
        }

        /// <summary>
        /// The calculate time.
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
        //public Time CalculateTime(Time startTime, DayTypeEnum dayTypeEnum, PartOfTheRoute nextPart)
        //{

        //}

        #endregion
    }
}
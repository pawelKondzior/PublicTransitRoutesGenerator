// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Time.cs" company="">
//   
// </copyright>
//  <author>Paweł Kondzior</author>
// <summary>
//   The time.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Magisterka.Infrastructure.Shared.Structure
{
    using System;

    /// <summary>
    /// The time.
    /// </summary>
    [Serializable]
    public class Time 
    {
        #region Fields

        /// <summary>
        /// The _minutes sum.
        /// </summary>
        public int? _minutesSum = null;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Time"/> class.
        /// </summary>
        public Time()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Time"/> class.
        /// </summary>
        /// <param name="time">
        /// The time.
        /// </param>
        public Time(Time time)
        {
            this.Hour = time.Hour;
            this.Minute = time.Minute;

            FixTime();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Time"/> class.
        /// </summary>
        /// <param name="minutes">
        /// The minutes.
        /// </param>
        public Time(int minutes)
        {
            this.Hour = minutes / 60;

            this.Minute = minutes % 60;

            FixTime();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Time"/> class.
        /// </summary>
        /// <param name="hour">
        /// The hour.
        /// </param>
        /// <param name="minutes">
        /// The minutes.
        /// </param>
        public Time(int hour, int minutes)
        {
            this.Hour = hour;

            this.Minute = minutes;

            FixTime();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the hour.
        /// </summary>
        /// <value>The hour.</value>
        /// <remarks></remarks>
        public int Hour { get; set; }

        /// <summary>
        /// Gets or sets the minute.
        /// </summary>
        /// <value>The minute.</value>
        /// <remarks></remarks>
        public int Minute { get; set; }


        public int Day { get; set; }

        /// <summary>
        /// Gets the minutes sum.
        /// </summary>
        public int MinutesSum
        {
            get
            {
                if (!this._minutesSum.HasValue)
                {
                    this._minutesSum = this.GetMinutesSum();
                }

                return this._minutesSum.Value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The ++.
        /// </summary>
        /// <param name="time">
        /// The time.
        /// </param>
        /// <returns>
        /// </returns>
        public static Time operator ++(Time time)
        {
            if (time.Minute < 59)
            {
                time.Minute++;
            }
            else
            {
                time.Hour++;
                time.Minute = 0;
                time._minutesSum = time.GetMinutesSum();
            }

            return time;
        }

        public static bool operator >=(Time t1, Time t2)
        {
            if (t1.MinutesSum >= t2.MinutesSum)
            {
                return true;
            }

            return false;
        }

        public static bool operator <=(Time t1, Time t2)
        {
            if (t1.MinutesSum <= t2.MinutesSum)
            {
                return true;
            }

            return false;
        }

        public static bool operator >(Time t1, Time t2)
        {
            if (t1.MinutesSum > t2.MinutesSum)
            {
                return true;
            }

            return false;
        }

        public static bool operator <(Time t1, Time t2)
        {
            if (t1.MinutesSum < t2.MinutesSum)
            {
                return true;
            }

            return false;
        }



        public void FixTime()
        {
            _minutesSum = null;
            AddMinutes(0);
        }

        /// <summary>
        /// The add minutes.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public void AddMinutes(int value)
        {
            this.Minute += value;

            var hourToAdd = this.Minute / 60;
            this.Hour += hourToAdd;
            this.Minute = this.Minute % 60;

            var daysToAdd = this.Hour / 24;
            this.Day += daysToAdd;
            this.Hour = this.Hour % 24;



        }

        /// <summary>
        /// The get minutes sum.
        /// </summary>
        /// <returns>
        /// The System.Int32.
        /// </returns>
        public int GetMinutesSum()
        {
            return this.Hour * 60 + this.Minute;
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The System.String.
        /// </returns>
        public override string ToString()
        {
            return this.Hour + " " + this.Minute;
        }

      

        public Time Clone()
        {

            return new Time(this);
        }

        #endregion
    }
}
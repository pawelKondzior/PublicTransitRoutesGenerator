// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SinglePoint.cs" company="">
//   
// </copyright>
// <summary>
//   The single point.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Magisterka.Infrastructure.Shared.Basic
{
    using log4net;
    using Magisterka.Infrastructure.Shared.Interfaces;
    using System;
    using System.Device.Location;

    /// <summary>
    /// The single point.
    /// </summary>
    public class SinglePoint : ILogItem
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePoint"/> class.
        /// </summary>
        public SinglePoint()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePoint"/> class.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="y">
        /// The y.
        /// </param>
        public SinglePoint(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the X. longtitude
        /// </summary>
        /// <value>The X.</value>
        /// <remarks></remarks>
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the Y.
        /// </summary>
        /// <value>The Y.</value>
        /// <remarks></remarks>
        public double Y { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get distance.
        /// </summary>
        /// <param name="secondPoint">
        /// The second point.
        /// </param>
        /// <returns>
        /// The System.Double.
        /// </returns>
        /// <remarks> 
        /// latitude is actually a Y coordinate. 
        /// Latitude refers to a point relative to the equator
        /// Longitude is actually an X coordinate
        /// </remarks>
        public virtual  double GetDistance(SinglePoint secondPoint)
        {
            var firstCoord = new GeoCoordinate(this.Y, this.X);
            var secondCoord = new GeoCoordinate(secondPoint.Y, secondPoint.X);

            return firstCoord.GetDistanceTo(secondCoord);
            // return this.GetEuclideanDistance(secondPoint) * 111196.672;
        }

        /// <summary>
        /// Bez tego mnożenia itp 
        /// </summary>
        /// <param name="secondPoint">
        /// </param>
        /// <returns>
        /// The System.Double.
        /// </returns>
        public double GetSimpleDistance(SinglePoint secondPoint)
        {
            return this.GetEuclideanDistance(secondPoint);
        }

        /// <summary>
        /// Czes przebycia drogi do punktu na piechotę w minutach
        /// </summary>
        /// <param name="secondPoint">
        /// The second Point.
        /// </param>
        /// <returns>
        /// The System.Int32.
        /// </returns>
        public virtual int GetWalkTime(SinglePoint secondPoint)
        {
            var seconds = this.GetDistance(secondPoint) * 0.75;
            double minutes = seconds / 60.0d;
            return (int)minutes;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get euclidean distance.
        /// </summary>
        /// <param name="secondPoint">
        /// The second point.
        /// </param>
        /// <returns>
        /// The System.Double.
        /// </returns>
        protected double GetEuclideanDistance(SinglePoint secondPoint)
        {
            return Math.Sqrt(Math.Pow(secondPoint.X - this.X, 2) + Math.Pow(secondPoint.Y - this.Y, 2));
        }

        #endregion

        public virtual void LogItem(ILog log)
        {
            log.Info(string.Format("SinglePoint X {0) Y {1} ", this.X, this.Y));
        }

        public bool HasGeometryPoints()
        {
            if (X != 0 && Y != 0)
            {
                return true;
            }

            return false;
        }


    }
}
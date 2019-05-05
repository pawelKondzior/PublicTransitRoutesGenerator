// -----------------------------------------------------------------------
//  <copyright file="LabeledPoint.cs" company="DevCore .NET">
//      Copyright (c) DevCore.NET All rights reserved.
//  </copyright>
//  <author>Paweł Kondzior</author>
// -----------------------------------------------------------------------
using Magisterka.Infrastructure.Shared.Structure;
using System.Windows.Forms.DataVisualization.Charting;
namespace Magisterka.Infrastructure.Shared.Basic
{
    /// <summary>
    /// Pamiętajacy swoją nazwe albo swoje id, potrzbne np do zaznaczania
    /// </summary>
    public class LabeledPoint : SinglePoint
    {
        public LabeledPoint()
        {
        }

        public LabeledPoint(double x, double y)
             :base(x, y)
        {
        }

        public LabeledPoint(double x, double y, string strId)
            : base(x, y)
        {
            this.Id = int.Parse(strId);
        }

        public LabeledPoint(double x, double y, int  id)
            : base(x, y)
        {
            this.Id = id;
        }

        public LabeledPoint(DataPoint dataPoint)
            : base(dataPoint.XValue, dataPoint.YValues[0])
        {
            this.Id = int.Parse(dataPoint.LegendText);
        }


        public LabeledPoint(BusStop busStop)
              : base(busStop.X, busStop.Y)
        {
            this.Id = busStop.Id;
        }


        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        /// <remarks></remarks>
        public int Id{ get; set; }
    }
}
// // -----------------------------------------------------------------------
// //  <copyright file="MainWindow.Varibles.cs" company="DevCore .NET">
// //      Copyright DevCore.NET All rights reserved.
// //  </copyright>
// //  <author>Paweł Kondzior</author>
// // -----------------------------------------------------------------------
namespace Magisterka.Application.Wpf
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Forms;
    using System.Windows.Forms.DataVisualization.Charting;
    using System.Windows.Input;
    using Magisterka.Infrastructure.Shared.Basic;
    using Magisterka.Infrastructure.Shared.ChartExt;
    using Magisterka.Infrastructure.Shared.Collections;
    using Magisterka.Infrastructure.Shared.Consts;
    using Magisterka.Infrastructure.Shared.Enum;
    using Magisterka.Infrastructure.Shared.Extensions;
    using Magisterka.Infrastructure.Shared.Structure;
    using Magisterka.Modules.Main.Algoritms.PopulationGenerators;
    using Magisterka.Modules.Main.Init;
    using Magisterka.Modules.Main.Services;
    using MessageBox = System.Windows.MessageBox;
    using MouseEventArgs = System.Windows.Forms.MouseEventArgs;
    using Magisterka.Modules.Main.Algoritms.EA;
    using log4net;
   // using Magisterka.Modules.Main.Aspects;
    using System.Collections.ObjectModel;
    using GMap.NET.WindowsPresentation;
    using GMap.NET.MapProviders;
    using GMap.NET;
    using System.Windows.Controls.Primitives;
    using Magisterka.Application.Wpf.Markers;
    using Magisterka.Data.Access.PP;

    public partial class MainWindow
    {
        public void InitializeMap(BusStopList busStopList)
        {
            MainMap.InitializeMap(this, busStopList);
        }

        /// <summary>
        ///     The register chart events.
        /// </summary>
        private void RegisterMapEvents()
        {

        }

        private void BGetLastBusStops(LastPoint lastPoints)
        {
            if (lastPoints == null)
            {
                MessageBox.Show("Brak zapisanych ostatnich przystanków");
                return;
            }

            var startPoint = MainMap.BusMarkerList.FirstOrDefault(x => x.BusStop.Id == lastPoints.StartPointId);
            var endPoint = MainMap.BusMarkerList.FirstOrDefault(x => x.BusStop.Id == lastPoints.EndPointId);


            if (startPoint == null || endPoint == null)
            {
                MessageBox.Show("Nie znaleziono punktów");
                return;
            }

            this.MainMap.NormalAll();


            startPoint.MarkSelected();
            endPoint.MarkSelected();

            StartPoint = new LabeledPoint(startPoint.BusStop);
            EndPoint = new LabeledPoint(endPoint.BusStop);

            UpdateChartWithSelectedPoints();
        }


       
        // zones list
        /// List<GMapMarker> Circles = new List<GMapMarker>();

        // zoom changed
        private void sliderZoom_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // updates circles on map
            //foreach (var c in Circles)
            //{
            //    UpdateCircle(c.Shape as Circle);
            //}
        }

        // zoom up
        private void czuZoomUp_Click(object sender, RoutedEventArgs e)
        {
            MainMap.Zoom = ((int)MainMap.Zoom) + 1;
        }

        // zoom down
        private void czuZoomDown_Click(object sender, RoutedEventArgs e)
        {
            MainMap.Zoom = ((int)(MainMap.Zoom + 0.99)) - 1;
        }

        // calculates circle radius
        //void UpdateCircle(Circle c)
        //{
        //    var pxCenter = MainMap.FromLatLngToLocal(c.Center);
        //    var pxBounds = MainMap.FromLatLngToLocal(c.Bound);

        //    double a = (double)(pxBounds.X - pxCenter.X);
        //    double b = (double)(pxBounds.Y - pxCenter.Y);
        //    var pxCircleRadius = Math.Sqrt(a * a + b * b);

        //    c.Width = 55 + pxCircleRadius * 2;
        //    c.Height = 55 + pxCircleRadius * 2;
        //    (c.Tag as GMapMarker).Offset = new System.Windows.Point(-c.Width / 2, -c.Height / 2);
        //}
    }
}
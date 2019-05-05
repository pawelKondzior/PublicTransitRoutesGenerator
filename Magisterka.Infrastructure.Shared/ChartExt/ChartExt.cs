

namespace Magisterka.Infrastructure.Shared.ChartExt
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms.DataVisualization.Charting;
    using Magisterka.Infrastructure.Shared.Basic;
    using System.Drawing;
    using System.Linq;
    using Magisterka.Infrastructure.Shared.Structure;
    using System.Threading;
    using System.Globalization;
    using Magisterka.Infrastructure.Shared.Collections;
    using Magisterka.Infrastructure.Shared.Settings;

    public static class ChartExt
    {
        public static void SetPointToOneColor(this Chart windowsFormsChart, Color color)
        {
            if (windowsFormsChart.Series["Punkty"] != null)
            {
                foreach (var item in windowsFormsChart.Series["Punkty"].Points)
                {
                    item.MarkerColor = color;

                }
            }
        }


        #region Drow

        public static void DrowBusStop(this Chart windowsFormsChart, BusStop busStop, Color color)
        {
            var dataPooint = new DataPoint(busStop.X, busStop.Y);
            dataPooint.LegendText = busStop.Id.ToString();


            dataPooint.ToolTip = busStop.Name ?? string.Empty;
            dataPooint.MarkerColor = color;

            windowsFormsChart.Series["Punkty"].Points.Add(dataPooint);
        }

        private static void ChangeBusStopColor(this Chart windowsFormsChart, BusStop busStop, Color color)
        {
            var point = windowsFormsChart.Series["Punkty"].Points
                //.FirstOrDefault(x => x.XValue == busStop.X && x.YValues[0] == busStop.Y);
                .FirstOrDefault(x => x.LegendText == busStop.Id.ToString());

            if (point != null)
            {
                point.MarkerColor = color;
                //   point.BorderWidth = 5;
            }
            // windowsFormsChart.Series["Punkty"].Points.Add(dataPooint);
        }

        private static void AddBusStopChart(this Chart windowsFormsChart)
        {
            windowsFormsChart.Series.Add("Punkty");
            var series = windowsFormsChart.Series["Punkty"];

            series.ChartType = SeriesChartType.Point;
            series.MarkerSize = 5;
            series.MarkerStyle = MarkerStyle.Circle;
            series.Color = Color.FromArgb(128, Color.Yellow);



        }

        public static void DrowBusStopList(this Chart windowsFormsChart, List<BusStop> busStopList, Color? color)
        {
            if (color == null)
            {
                color = Color.Orange;
            }

            if (!windowsFormsChart.Series.Any(x => x.Name == "Punkty"))
            {
                windowsFormsChart.AddBusStopChart();
            }

            foreach (var item in busStopList)
            {
                DrowBusStop(windowsFormsChart, item, color.Value);
            }
        }


        public static void DrowEntireRoute(this Chart windowsFormsChart, EntireRoute entireRoute,
            Color? mainColor = null, Color? changeColor = null, Color? mutationColor = null)
        {
            mainColor = mainColor ?? Color.Black;
            changeColor = changeColor ?? Color.Brown;
            mutationColor = mutationColor ?? Color.Red;

            if (!windowsFormsChart.Series.Any(x => x.Name == "Punkty"))
            {
                windowsFormsChart.AddBusStopChart();
            }

            if (entireRoute == null)
            {
                return;
            }

            foreach (var item in entireRoute.PartOfTheRoute)
            {
                if (entireRoute.BusChengesList.Contains(item.BusStop))
                {
                    DrowBusStop(windowsFormsChart, item.BusStop, changeColor.Value);
                }
                else if (entireRoute.BusMutatedList.Contains(item.BusStop))
                {
                    DrowBusStop(windowsFormsChart, item.BusStop, mutationColor.Value);
                }
                else
                {
                    DrowBusStop(windowsFormsChart, item.BusStop, mainColor.Value);
                }
            }

        }

        public static void ChangeBusStopListColor(this Chart windowsFormsChart, List<BusStop> busStopList, Color color)
        {
            if (!windowsFormsChart.Series.Any(x => x.Name == "Punkty"))
            {
                return;
            }

            foreach (var item in busStopList)
            {
                ChangeBusStopColor(windowsFormsChart, item, color);
            }
        }

        public static void SetChartScale(this Chart windowsFormsChart, BusStopList busStopList)
        {
            if (!windowsFormsChart.Series.Any(x => x.Name == "Punkty"))
            {
                windowsFormsChart.AddBusStopChart();
            }

            windowsFormsChart.ChartAreas["Punkty"].AxisX.Maximum = busStopList.MaxLongitude;
            windowsFormsChart.ChartAreas["Punkty"].AxisX.Minimum = busStopList.MinLongitude;

            windowsFormsChart.ChartAreas["Punkty"].AxisY.Maximum = busStopList.MaxLatitude;
            windowsFormsChart.ChartAreas["Punkty"].AxisY.Minimum = busStopList.MinLatitude;

            
        }

        //public static void DrowLineSegment(this Chart windowsFormsChart, double x1, double y1, double x2, double y2)
        //{
        //    if (!ConfigurationHelper.DrowLines)
        //    {
        //        return;
        //    }
        //    DrowLineSegment(windowsFormsChart, x1, y1, x2, y2, Color.Green);
        //}

        //public static void DrowLineSegment(this Chart windowsFormsChart, double x1, double y1, double x2, double y2, Color color)
        //{
        //    var name = Guid.NewGuid().ToString();

        //    windowsFormsChart.Series.Add(name);
        //    windowsFormsChart.Series[name].ChartArea = "Punkty";
        //    windowsFormsChart.Series[name].ChartType = SeriesChartType.Line;
        //    windowsFormsChart.Series[name]["LabelStyle"] = "Center";
        //    windowsFormsChart.Series[name].Color = color;

        //    windowsFormsChart.Series[name].Points.Add(new DataPoint(x1, y1));
        //    windowsFormsChart.Series[name].Points.Add(new DataPoint(x2, y2));
        //}

        //public static void DrowLineSegment(this Chart windowsFormsChart, LineSegment lineSegment)
        //{
        //    if (!ConfigurationHelper.DrowLines)
        //    {
        //        return;
        //    }

        //    DrowLineSegment(windowsFormsChart, lineSegment.FirstPoint.X, lineSegment.FirstPoint.Y,
        //                          lineSegment.LastPoint.X, lineSegment.LastPoint.Y);
        //}

        //public static void DrowLineSegment(this Chart windowsFormsChart, LineSegment lineSegment, Color color)
        //{
        //    if (!ConfigurationHelper.DrowLines)
        //    {
        //        return;
        //    }

        //    DrowLineSegment(windowsFormsChart, lineSegment.FirstPoint.X, lineSegment.FirstPoint.Y,
        //                          lineSegment.LastPoint.X, lineSegment.LastPoint.Y, color);
        //}

        public static void DrowSquare(this Chart windowsFormsChart, Square square)
        {
            if (!ConfigurationHelper.DrowLines)
            {
                return;
            }

          //  square.LineSegments.ForEach(x => windowsFormsChart.DrowLineSegment(x, square.Color));
        }

        public static void DrowSquares(this Chart windowsFormsChart, List<Square> squareList)
        {
            if (!ConfigurationHelper.DrowLines)
            {
                return;
            }
            foreach (var square in squareList)
            {
                windowsFormsChart.DrowSquare(square);
            }
        }

        public static void ClearSquares(this Chart windowsFormsChart)
        {
            var lineSeries = windowsFormsChart.Series.Where(x => x.ChartType == SeriesChartType.Line).ToList();

            foreach (var lineSerie in lineSeries)
            {
                windowsFormsChart.Series.Remove(lineSerie);
            }
        }


        public static void DrowBackgroubnd(this Chart windowsFormsChart)
        {
            //var backImage = new NamedImage("bgImg", Magisterka.Infrastructure.Shared.Res.Resources.map);
            //windowsFormsChart.Images.Add(backImage);
            //windowsFormsChart.BackImage = "bgImg";
          //  windowsFormsChart.BackImageTransparentColor = Color.Transparent;
        }

        #endregion

        #region Remove

        public static void RemoveAllLineCharts(this Chart windowsFormsChart)
        {
            var collection = windowsFormsChart.Series.Where(x => x.ChartType == SeriesChartType.Line).ToList();

            foreach (var item in collection)
            {
                windowsFormsChart.Series.Remove(item);
            }
        }

        public static void RemoveAllPointCharts(this Chart windowsFormsChart)
        {
            var collection = windowsFormsChart.Series.Where(x => x.ChartType == SeriesChartType.Point).ToList();

            foreach (var item in collection)
            {
                windowsFormsChart.Series.Remove(item);
            }
        }



        #endregion


    }
}
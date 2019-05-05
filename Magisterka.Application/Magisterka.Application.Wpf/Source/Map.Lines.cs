namespace Magisterka.Application.Wpf
{
    using System.Windows.Controls;
    using System.Windows.Media;
    using GMap.NET.WindowsPresentation;
    using System.Globalization;
    using System.Windows.Forms;
    using System.Windows;
    using System;
    using Magisterka.Infrastructure.Shared.Collections;
    using GMap.NET.MapProviders;
    using GMap.NET;
    using Magisterka.Application.Wpf.Markers;
    using System.Collections.Generic;
    using Magisterka.Data.Access.PP;
    using Magisterka.Infrastructure.Shared.Structure;
    using System.Linq;
    using Magisterka.Infrastructure.Shared.Enum;
    using Magisterka.Infrastructure.Shared.IoDto;
    using System.Windows.Shapes;
    using Magisterka.Infrastructure.Shared.Basic;
    using Magisterka.Infrastructure.Shared.Settings;

    /// <summary>
    /// the custom map f GMapControl 
    /// </summary>
    public partial class Map
    {
        public List<GMapPolygon> LinesList = new List<GMapPolygon>();

        public void DrowLineSegment(double x1, double y1, double x2, double y2)
        {
            if (!ConfigurationHelper.DrowLines)
            {
                return;
            }
            DrowLineSegment(x1, y1, x2, y2, Brushes.Green);
        }

        public void DrowLineSegment(double x1, double y1, double x2, double y2, Brush color)
        {
            var name = Guid.NewGuid().ToString();

            List<PointLatLng> pointlatlang = new List<PointLatLng>();
            pointlatlang.Add(new PointLatLng(y1, x1));
            pointlatlang.Add(new PointLatLng(y2, x2));

            //Declare polygon in gmap
            GMapPolygon polygon = new GMapPolygon(pointlatlang);
            polygon.RegenerateShape(this);
            //setting line style
            (polygon.Shape as Path).Stroke = color;
            (polygon.Shape as Path).StrokeThickness = 1.5;
            (polygon.Shape as Path).Effect = null;
            //To add polygon in gmap
            this.Markers.Add(polygon);

            LinesList.Add(polygon);
        }

        public void DrowLineSegment(Infrastructure.Shared.Basic.LineSegment lineSegment, Brush color)
        {
            if (!ConfigurationHelper.DrowLines)
            {
                return;
            }

            DrowLineSegment(lineSegment.FirstPoint.X, lineSegment.FirstPoint.Y, lineSegment.LastPoint.X, lineSegment.LastPoint.Y, color);
        }

        public void DrowLineSegment(Infrastructure.Shared.Basic.LineSegment lineSegment)
        {
            if (!ConfigurationHelper.DrowLines)
            {
                return;
            }

            DrowLineSegment(lineSegment.FirstPoint.X, lineSegment.FirstPoint.Y,
                                  lineSegment.LastPoint.X, lineSegment.LastPoint.Y);
        }



        public void DrowSquare(Square square)
        {
            if (!ConfigurationHelper.DrowLines)
            {
                return;
            }

            square.LineSegments.ForEach(x => DrowLineSegment(x, square.Color));
        }


        public void DrowSquares(List<Square> squareList)
        {
            if (!ConfigurationHelper.DrowLines)
            {
                return;
            }

            squareList.ForEach(DrowSquare);

        }

        public void ClearSquares()
        {
            LinesList.ForEach(line => this.Markers.Remove(line));

            LinesList.Clear();


            //var lineSeries = windowsFormsChart.Series.Where(x => x.ChartType == SeriesChartType.Line).ToList();

            //foreach (var lineSerie in lineSeries)
            //{
            //    windowsFormsChart.Series.Remove(lineSerie);
            //}
        }


    }

}
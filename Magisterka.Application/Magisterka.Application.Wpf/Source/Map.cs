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
    public partial class Map : GMapControl
    {


        public System.Windows.Media.Brush[] LinesColors = new Brush[]
        {
            Brushes.Red,
            Brushes.Green,
            Brushes.Blue,
            Brushes.Yellow,
            Brushes.Orange,
            Brushes.Gray,
            Brushes.Aqua,
            Brushes.Beige,
            Brushes.Brown,
            Brushes.Coral,
            Brushes.DarkCyan,
            Brushes.DarkKhaki

        };

        public List<BusMarker> BusMarkerList = new List<BusMarker>();

        public long ElapsedMilliseconds;

#if DEBUG
        DateTime start;
        DateTime end;
        int delta;

        private int counter;
        readonly Typeface tf = new Typeface("GenericSansSerif");
        readonly System.Windows.FlowDirection fd = new System.Windows.FlowDirection();

        MainWindow MainWindow { get; set; }

        /// <summary>
        /// any custom drawing here
        /// </summary>
        /// <param name="drawingContext"></param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            start = DateTime.Now;

            base.OnRender(drawingContext);

            end = DateTime.Now;
            delta = (int)(end - start).TotalMilliseconds;

            //     FormattedText text = new FormattedText(string.Format(CultureInfo.InvariantCulture, "{0:0.0}", Zoom) + "z, " + MapProvider + ", refresh: " + counter++ + ", load: " + ElapsedMilliseconds + "ms, render: " + delta + "ms", CultureInfo.InvariantCulture, fd, tf, 20, Brushes.Blue);
            //     drawingContext.DrawText(text, new Point(text.Height, text.Height));
            //    text = null;
        }

#endif

        public void ShowMap()
        {
            this.MapProvider = GMapProviders.BingMap;
        }

        public void HideMap()
        {
            this.MapProvider = GMapProviders.EmptyProvider;
        }


        public void InitializeMap(MainWindow mainWindow, BusStopList busStopList)
        {
            MainWindow = mainWindow;
            this.MapProvider = GMapProviders.BingMap;
            this.Position = new PointLatLng(51.1259, 16.9945);


           

            SetMapScale(busStopList);
            AddBusMarkers(busStopList);

            
        }


        public void SetFocusTo()
        {

        }


        public void SetMapScale(BusStopList busStopList)
        {
            var sizeLong = (busStopList.MaxLongitude - busStopList.MinLongitude);///2;
            var sizeLat = (busStopList.MaxLatitude - busStopList.MinLatitude);///2;

            var centerLong = (busStopList.MaxLongitude + busStopList.MinLongitude) / 2;
            var centerLat = (busStopList.MaxLatitude + busStopList.MinLatitude) / 2;

            var ract = new RectLatLng(busStopList.MaxLatitude, busStopList.MinLongitude, sizeLong, sizeLat);


            this.SetZoomToFitRect(ract);
        }

        private void AddBusMarkers(BusStopList busStopList)
        {
            busStopList.ForEach(busStop =>
            {
                var busStopPosition = new PointLatLng(busStop.Y, busStop.X);

                GMapMarker busStopMarker = new GMapMarker(busStopPosition);
                {
                    string ToolTipText = busStop.Name;


                    var busMarkerShape = new BusMarker(MainWindow, busStopMarker, busStop);
                    BusMarkerList.Add(busMarkerShape);

                    busStopMarker.Shape = busMarkerShape;

                    busMarkerShape.MarkNormal();
                }

                this.Markers.Add(busStopMarker);
            });
        }


        public void HideAll()
        {
            BusMarkerList.ForEach(x => x.Hide());
        }

        public void NormalAll()
        {
            BusMarkerList.ForEach(x => x.MarkNormal());
        }



        public void MakeBusStopBigger(BusStop busStop)
        {
            BusMarkerList.ForEach(x => x.SizeNormalSelected());


            var marker = this.BusMarkerList.FirstOrDefault(x => x.BusStop.Id == busStop.Id);

            marker.SizeBiggerSelected();
        }


        public void DrowEntireRoute(EntireRoute entireRoute)
        {
            if (entireRoute == null)
            {
                return;
            }

            var brushStack = new System.Collections.Generic.Stack<Brush>(this.LinesColors);

            Brush brushColor = Brushes.Black;
            PartOfTheRoute previus = null;

            foreach (var item in entireRoute.PartOfTheRoute)
            {
                if (item.NextBusStopConnection == ConnectionType.Walk)
                {
                    brushColor = Brushes.Black;
                }
                else if (previus != null && previus.NextBusStopConnection == ConnectionType.Walk && item.NextBusStopConnection == ConnectionType.Bus)
                {
                    brushColor = brushStack.Pop();
                    brushColor = brushStack.Peek();
                }
                else if (item.InputLine != item.OutputLine) //item.InputLine == null && 
                {
                    brushColor = brushStack.Pop();
                    brushColor = brushStack.Peek();
                }

                var marker = this.BusMarkerList.FirstOrDefault(x => x.BusStop.Id == item.BusStop.Id);

                if (entireRoute.BusChengesList.Contains(item.BusStop))
                {
                    marker.MarkSelected(brushColor);
                }
                else if (entireRoute.BusMutatedList.Contains(item.BusStop))
                {
                    marker.MarkSelected(brushColor);
                }
                else
                {
                    marker.MarkSelected(brushColor);
                }


                previus = item;
            }

        }

        public void DrowBusStops(IEnumerable<BusStop> list)
        {
            if (list == null)
            {
                return;
            }

            this.HideAll();

            var brushColor = Brushes.Black;

            foreach (var item in list)
            {
                var marker = this.BusMarkerList.FirstOrDefault(x => x.BusStop.Id == item.Id);
                marker.MarkSelected(brushColor);
            }
        }

        public void DrowEntireLine(BusLineIoDto busLine)
        {
            if (busLine == null)
            {
                return;
            }

            this.HideAll();

            var brushColor = Brushes.Black;

            foreach (var item in busLine)
            {
                var marker = this.BusMarkerList.FirstOrDefault(x => x.BusStop.Id == item.BusStopId);
                marker.MarkSelected(brushColor);
            }
        }

    }
}
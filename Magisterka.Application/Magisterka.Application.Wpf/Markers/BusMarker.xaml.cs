using GMap.NET.WindowsPresentation;
using Magisterka.Infrastructure.Shared.Structure;
using Magisterka.Modules.Main.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Magisterka.Application.Wpf.Markers
{
    /// <summary>
    /// Interaction logic for BusMarker.xaml
    /// </summary>
    public partial class BusMarker : UserControl
    {

        //// public int Id { get; set; }
        public BusStop BusStop { get; private set; }

        Popup Popup;
        Label Label;
        GMapMarker Marker;
        MainWindow MainWindow;

        bool IsHidden = true;

        public BusMarker(MainWindow window, GMapMarker marker, BusStop busStop)
        {
            InitializeComponent();

            this.BusStop = busStop;
            this.MainWindow = window;
            this.Marker = marker;

            Popup = new Popup();
            Label = new Label();

            this.Loaded += new RoutedEventHandler(CustomMarkerDemo_Loaded);
            this.SizeChanged += new SizeChangedEventHandler(CustomMarkerDemo_SizeChanged);
            this.MouseEnter += new MouseEventHandler(MarkerControl_MouseEnter);
            this.MouseLeave += new MouseEventHandler(MarkerControl_MouseLeave);
       ///     this.MouseMove += new MouseEventHandler(CustomMarkerDemo_MouseMove);
            this.MouseLeftButtonUp += new MouseButtonEventHandler(CustomMarkerDemo_MouseLeftButtonUp);
            this.MouseLeftButtonDown += new MouseButtonEventHandler(CustomMarkerDemo_MouseLeftButtonDown);

            


            Popup.Placement = PlacementMode.Mouse;
            {
                Label.Background = Brushes.Blue;
                Label.Foreground = Brushes.White;
                Label.BorderBrush = Brushes.WhiteSmoke;
                Label.BorderThickness = new Thickness(2);
                Label.Padding = new Thickness(5);
                Label.FontSize = 22;
                Label.Content = string.Format("{0} {1}", BusStop.Id, BusStop.NameAndStreet);
            }
            Popup.Child = Label;


        }

        public void MarkSelected(Brush color = null)
        {
            if (color == null)
            {
                color = System.Windows.Media.Brushes.Blue;
            }

            this.Ikonka.Fill = color;
            this.Ikonka.Stroke = System.Windows.Media.Brushes.Black;
            this.Marker.ZIndex = 100;
            this.Width = 10;
            this.Height = 10;
            this.Opacity = 1;
            this.Ikonka.Opacity = 1;
            IsHidden = false;
            IsEnabled = true;
        }

        public void MarkNormal()
        {
            this.Ikonka.Fill = System.Windows.Media.Brushes.Yellow;
            this.Ikonka.Stroke = System.Windows.Media.Brushes.Black;
            this.Marker.ZIndex = 55;
            this.Width = 10;
            this.Height = 10;
            this.Opacity = 0.5;
            this.Ikonka.Opacity = 0.5;
            IsHidden = false;
            IsEnabled = true;
        }


        public void SizeNormalSelected()
        {
            this.Width = 10;
            this.Height = 10;
            IsHidden = false;
            IsEnabled = true;
        }


        public void SizeBiggerSelected()
        {
            this.Width = 15;
            this.Height = 15;
            IsHidden = false;
            IsEnabled = true;
        }

        public void Hide()
        {
            this.Ikonka.Opacity = 0;

            IsHidden = true;
            IsEnabled = false;
        }

        void CustomMarkerDemo_Loaded(object sender, RoutedEventArgs e)
        {
            //if (icon.Source.CanFreeze)
            //{
            //    icon.Source.Freeze();
            //}
        }

        void CustomMarkerDemo_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Marker.Offset = new Point(-e.NewSize.Width / 2, -e.NewSize.Height);
        }

        void CustomMarkerDemo_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && IsMouseCaptured)
            {
           //     Point p = e.GetPosition(MainWindow.MainMap);
           //     Marker.Position = MainWindow.MainMap.FromLocalToLatLng((int)p.X, (int)p.Y);
            }
        }

        void CustomMarkerDemo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsMouseCaptured)
            {
                /// Mouse.Capture(this);

                if (MainWindow.StartPoint != null && MainWindow.EndPoint != null)
                {
                    MainWindow.StartPoint = null;
                    MainWindow.EndPoint = null;

                    this.MainWindow.MainMap.NormalAll();
                }

                if (MainWindow.StartPoint == null)
                {
                    MainWindow.StartPoint = new Infrastructure.Shared.Basic.LabeledPoint(BusStop);
                    this.MarkSelected();
                }
                else if(MainWindow.EndPoint == null)
                {
                    MainWindow.EndPoint = new Infrastructure.Shared.Basic.LabeledPoint(BusStop);
                    this.MarkSelected();


                    int startId = MainWindow.StartPoint.Id;
                    int endId = MainWindow.EndPoint.Id;

                    MainWindow.UpdateChartWithSelectedPoints();

                    StorageService StorageService = new StorageService();

                    StorageService.SetLastSelectedPoints(startId, endId);
                }



            }
        }

        void CustomMarkerDemo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (IsMouseCaptured)
            {
                Mouse.Capture(null);
            }
        }

        void MarkerControl_MouseLeave(object sender, MouseEventArgs e)
        {
            Marker.ZIndex -= 10000;
            Popup.IsOpen = false;
        }

        void MarkerControl_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!this.IsHidden)
            {
                Marker.ZIndex += 10000;
                Popup.IsOpen = true;
            }
        }
    }
}

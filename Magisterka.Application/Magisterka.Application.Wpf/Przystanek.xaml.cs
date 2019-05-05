using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Magisterka.Application.Wpf
{
    using System.Windows.Forms.DataVisualization.Charting;
    using Magisterka.Infrastructure.Shared.Basic;
    using Magisterka.Infrastructure.Shared.Structure;
    using Magisterka.Infrastructure.Shared.ChartExt;
    using Anotar.Log4Net;

    /// <summary>
    /// Interaction logic for Przystanek.xaml
    /// </summary>
    public partial class Przystanek : Window
    {
        public MainWindow MainWindow { get; set; }

        public LabeledPoint LabeledPoint { get; set; }

        public BusStop BusStop { get; set; }

        private System.Drawing.Color OldColor { get; set; }


        public Przystanek(MainWindow mainWindow, BusStop busStop)
        {
            InitializeComponent();

            this.MainWindow = mainWindow;
            this.BusStop = busStop;

            this.Init();
        }



        public Przystanek(MainWindow mainWindow, DataPoint point)
        {
            InitializeComponent();

            MainWindow = mainWindow;
            LabeledPoint = new LabeledPoint(point);
            OldColor = point.Color;

            BusStop = mainWindow.BusStopList.GetBySinglePoint(LabeledPoint);

            this.Init();
        }

        public void Init()
        {
            this.FillView(BusStop);

            if (BusStop != null)
            {

            ///    MainWindow.WindowsFormsChart.DrowBusStop(BusStop, System.Drawing.Color.Red);
            }
        }

        private void FillView(BusStop busStop)
        {
            if (busStop == null)
            {
                MessageBox.Show("Brak przystanku");
            }
            else
            {
                this.LNazwa.Content = busStop.Name;
                this.LUlica.Content = busStop.Street;
                this.LX.Content = busStop.X;
                this.LY.Content = busStop.Y;


                this.TbId.Text = busStop.Id.ToString();

                var buslines = this.MainWindow.BusLines.GetBusLinesForBusStop(busStop);

              var list = new List<BusLineAndBusStopDto>();

                //if (node.BusLines == null)
                //{
                //    LogTo.Warn("brak buslines dla danego elementu");
                //}
                //else
                //{
                    foreach (var busLine in buslines)
                    {
                        var newItem = new BusLineAndBusStopDto();
                        newItem.BusLine = busLine;

                        var busStopId = busLine.GetNextBusStopId(busStop);

                        if (busStopId.HasValue)
                        {
                            var tempBusStop = this.MainWindow.BusStopList.FirstOrDefault(x => x.Id == busStopId.Value);

                            newItem.BusStop = tempBusStop;

                            list.Add(newItem);
                        }
                    }
             //   }

                this.LvOutCome.ItemsSource = list;
            }
        }

        private void LvOutCome_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = (BusLineAndBusStopDto)LvOutCome.SelectedItem;

            // MainWindow.WindowsFormsChart.DrowBusStop(BusStop, OldColor);

            if (item != null && item.BusStop != null)
            {
                this.FillView(item.BusStop);

             ///   MainWindow.WindowsFormsChart.DrowBusStop(item.BusStop, System.Drawing.Color.Red);
            }
        }
    }
}

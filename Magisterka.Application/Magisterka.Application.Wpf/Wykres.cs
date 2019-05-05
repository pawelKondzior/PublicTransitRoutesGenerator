using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

using Magisterka.Infrastructure.Shared.ChartExt;
using Magisterka.Infrastructure.Shared.Enum;
using Magisterka.Infrastructure.Shared.Extensions;
using Magisterka.Modules.Main.Services;
using Magisterka.Infrastructure.Shared.Basic;
using Magisterka.Infrastructure.Shared.Collections;
using Magisterka.Infrastructure.Shared.Structure;
using Magisterka.Infrastructure.Shared.IoDto;

namespace Magisterka.Application.Wpf
{
    public partial class Wykres : Form
    {
        private DataService DataService = DataService.DataServiceInstance;

        private LabeledPoint StartPoint { get; set; }

        private LabeledPoint EndPoint { get; set; }

        private SquareList SquareList { get; set; }

        private BusStopList BusStopList { get; set; }

        private BusLinesList BusLines { get; set; }

        private BusGraph BusGraph { get; set; }

        private int counter = 0;

        private MainWindow MainWindow { get; set; }

        public Wykres(MainWindow main)
        {
            InitializeComponent();

            MainWindow = main;
            BusLines = DataService.GetBusLines();
            BusStopList = DataService.GetBusStopListCSV();

            BusGraph = new BusGraph(BusStopList, BusLines, MainWindow.AlgorithmParameters.LinkType * 5);

            InitializeChart();
            Initialize();
        }

        private void InitializeChart()
        {
            var chartArea = new ChartArea("Punkty");
            var defoultChartArea = WindowsFormsChart.ChartAreas[0];

            WindowsFormsChart.ChartAreas.Remove(defoultChartArea);

            WindowsFormsChart.ChartAreas.Add(chartArea);

            WindowsFormsChart.ChartAreas["Punkty"].AxisX.MajorGrid.Enabled = false;
            WindowsFormsChart.ChartAreas["Punkty"].AxisY.MajorGrid.Enabled = false;

            WindowsFormsChart.ChartAreas["Punkty"].AxisX.MajorGrid.Enabled = false;
            WindowsFormsChart.ChartAreas["Punkty"].AxisY.MajorGrid.Enabled = false;

            foreach (var legend in WindowsFormsChart.Legends)
            {
                legend.Enabled = false;
            }
        }

        private void Initialize()
        {

            WindowsFormsChart.SetChartScale(BusStopList);
            WindowsFormsChart.DrowBackgroubnd();
            
            WindowsFormsChart.DrowBusStopList(BusStopList, Color.Yellow);

            SquareList = BusStopList.GetEquallyDividedSquares(5);
            WindowsFormsChart.DrowSquares(SquareList);
        }

        private void WindowsFormsChart_Click(object sender, EventArgs e)
        {
            // HitTestResult result = WindowsFormsChart.HitTest(e.ToString(, e.Y);

        }

        private void Wykres_Load(object sender, EventArgs e)
        {

        }

        private void WindowsFormsChart_MouseDown(object sender, MouseEventArgs e)
        {
            HitTestResult result = WindowsFormsChart.HitTest(e.X, e.Y);

            //if (counter  < SquareList.Count)
            //{
            //    WindowsFormsChart.DrowSquare(SquareList[counter]);
            //    counter++;
            //}



            if (result.ChartElementType == ChartElementType.DataPoint)
            {
                var point = WindowsFormsChart.Series["Punkty"].Points[result.PointIndex];
                point.MarkerColor = Color.Blue;

                if (StartPoint == null)
                {
                    StartPoint = new LabeledPoint(point.XValue, point.YValues[0], point.LegendText);
                }
                else if (EndPoint == null)
                {
                    EndPoint = new LabeledPoint(point.XValue, point.YValues[0], point.LegendText);

                    var line = new LineSegment(StartPoint, EndPoint);

                    var selectedSquares = SquareList.GetSquaresInACollisionWithLineSegment(line);

                    WindowsFormsChart.RemoveAllLineCharts();
                    //  WindowsFormsChart.RemoveAllPointCharts();

                    //   WindowsFormsChart.DrowLineSegment(line);
                    WindowsFormsChart.DrowSquares(selectedSquares);

                    // var points = selectedSquares.GetBusStopsLocatedIn(BusStopList);
                    //   WindowsFormsChart.DrowBusStopList(points); 
                }
                else
                {
                    StartPoint = null;
                    EndPoint = null;
                }

                CheckFindRouteButton();
            }
        }


      


       

        private bool CheckFindRouteButton()
        {
            if (StartPoint != null & EndPoint != null)
            {
                BFindRoute.Enabled = true;
                return true;
            }

            BFindRoute.Enabled = false;
            return false;
        }

        private void BFindRoute_Click(object sender, EventArgs e)
        {
            
        }

        private void BReset_Click(object sender, EventArgs e)
        {
            WindowsFormsChart.ChangeBusStopListColor(this.BusStopList, Color.Orange);
            StartPoint = null;
            EndPoint = null;
        }


    }
}

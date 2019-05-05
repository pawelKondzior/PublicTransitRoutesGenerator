// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="">
//   
// </copyright>
// <summary>
//   Interaction logic for MainWindow.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using AutoMapper;

using Magisterka.Infrastructure.Shared.Basic;
using Magisterka.Infrastructure.Shared.ChartExt;
using Magisterka.Infrastructure.Shared.Enum;
using Magisterka.Infrastructure.Shared.Extensions;
using Magisterka.Infrastructure.Shared.Structure;

using Magisterka.Modules.Main.Algoritms.EA;
using Magisterka.Modules.Main.Algoritms.PopulationGenerators;
//using Magisterka.Modules.Main.Aspects;
using MessageBox = System.Windows.MessageBox;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;
using Magisterka.Infrastructure.Shared.Collections;
using Magisterka.Infrastructure.Shared.Helpers;
using Magisterka.Modules.Main.Builders;
using Magisterka.Modules.Main.FileProviders;
using Anotar.Log4Net;
using MoreLinq;
using Magisterka.Data.Access.PP;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using Magisterka.Infrastructure.Shared.IoDto;

namespace Magisterka.Application.Wpf
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    //[StopWatchAttribute]
    public partial class MainWindow : Window
    {

        //public void ShowCandidate(Candidate candidate)
        //{
        //    List<Infrastructure.Shared.Structure.BusStop> temp =
        //        Mapper.Map<List<BusStop>, List<Infrastructure.Shared.Structure.BusStop>>(candidate.BusStops);

        //    WindowsFormsChart.ChangeBusStopListColor(temp, Color.Black);
        //}

        /// <summary>
        ///     The update lb results.
        /// </summary>
        //public void UpdateLbResults()
        //{
        //    if (p != null && p.Candidates != null)
        //    {
        //        LBresults.ItemsSource = p.Candidates;
        //    }
        //}


        /// <summary>
        ///     The b algorithm start_ click.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        private void BAlgorithmStart_Click(object sender, RoutedEventArgs e)
        {
            //if (PopulationGenerator == null)
            //{
            //    MessageBox.Show("Nie uruchomiono generatora populacji");
            //    return;
            //}

            if (CbEvolutionAlgorithmType.SelectedItem == null)
            {
                {
                    MessageBox.Show("Nie wybrano algorytmu genetycznego");
                    return;
                }
            }
            Population = null;

            if (!CheckFindRouteButton())
            {
                return;
            }






            //if (CbEvolutionAlgorithmType.SelectedItem == null)
            //{
            //    MessageBox.Show("Brak wybranego algorytmu");
            //    return;
            //}

            LTime.Content = string.Format("Rozpoczęto generacje populacji GA");

            LBresults.ItemsSource = null;


            var stopWatch = new Stopwatch();
            stopWatch.Start();

            //  try
            //   {
            UpdateAlgorithmParameters();

            EaAlgorithm = GetEaAlgorithm();

            var result = EaAlgorithm.StartAlgoritm(this.Population);

            stopWatch.Stop();


            LTime.Content = string.Format("Czas generacji {0}", stopWatch.Elapsed.TotalSeconds.ToString());
            

            UpdatePopulation(result,true);
            //      }
            ///  catch (Exception ex)
            // //    {
            //       log.Error("BAlgorithmStart_Click", ex);
            //        MessageBox.Show(ex.Message);
            //     }

        }

        private void Mutate_Click(object sender, RoutedEventArgs e)
        {
            object selectedItem = LBresults.SelectedItem;

            if (selectedItem == null)
            {
                MessageBox.Show("Nie wybrano trasy");
                return;
            }

            if (EaAlgorithm == null)
            {
                MessageBox.Show("Nie wybrano algorytmu");
                return;
            }

            if (AlgorithmStartPoint == null || AlgorithmEndPoint == null)
            {
                MessageBox.Show("Nie wybrano punktów");
                return;
            }

            var route = (EntireRoute)selectedItem;

            EntireRoute newItem = EaAlgorithm.BaseMutation(route, AlgorithmStartPoint, AlgorithmEndPoint);
            ///  this.Population = EaAlgorithm.PopulationSummary();
            var temList = new List<EntireRoute>();
            temList.Add(newItem);

            UpdatePopulation(temList, true);
        }

        private void UpdatePopulation(List<EntireRoute> newItems = null, bool clear = false)
        {
            if (ObservableCollection == null || clear)
            {
                ObservableCollection = new ObservableCollection<EntireRoute>();
            }
            Population = new Population(newItems);
            if (Population == null)
            {
                return;
            }

            foreach (var populationItem in Population)
            {
                var timesResult = populationItem.CheckIfTimesAreCorrectlyIncrementing();
                if (timesResult == false)
                {
                    LogTo.Error("bledna inkrementacja czasu w jednej z tras", populationItem);
                }


            }



            if (newItems != null)
            {
                newItems.ForEach(x => ObservableCollection.Add(x));
            }

            if (!ObservableCollection.Any() && Population != null)
            {
                foreach (EntireRoute item in Population)
                {
                    ObservableCollection.Add(item);
                }
            }

            if (LBresults.ItemsSource != ObservableCollection)
            {
                LBresults.ItemsSource = ObservableCollection;
            }
        }

        /// <summary>
        ///     The b find route_ click.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        private void BFindRoute_Click(object sender, RoutedEventArgs e)
        {
            var x = 54;
            //if (!CheckFindRouteButton())
            //{
            //    return;
            //}

            //Infrastructure.Shared.Structure.BusStop startBusStop = BusStopList
            //    // .FirstOrDefault(x => x.Id == StartPoint.Id);
            //    .FirstOrDefault(x => x.X == StartPoint.X && x.Y == StartPoint.Y);

            //Infrastructure.Shared.Structure.BusStop endBusStop = BusStopList
            //    // .FirstOrDefault(x => x.Id == EndPoint.Id);
            //    .FirstOrDefault(x => x.X == EndPoint.X && x.Y == EndPoint.Y);

            //// .FirstOrDefault(x => Math.Abs(x.X - SelectedLine.LastPoint.X) < EPSILON && Math.Abs(x.Y - SelectedLine.LastPoint.Y) < EPSILON);
            //if (startBusStop != null && endBusStop != null)
            //{
            //    var start = new BusStop(startBusStop.Id.ToString(), startBusStop.Name, startBusStop.Street);
            //    var end = new BusStop(endBusStop.Id.ToString(), endBusStop.Name, endBusStop.Street);

            //  //  StartTheAlgorithm(start, end);
            //}
        }

        private void UpdateAlgorithmParameters()
        {
            AlgorithmParameters.Start = BusStopList.GetBySinglePoint(StartPoint);
            AlgorithmParameters.End = BusStopList.GetBySinglePoint(EndPoint);

            AlgorithmParameters.BusStopList = BusStopList;
            AlgorithmParameters.BusLines = BusLines;
           AlgorithmParameters.BusGraph = BusGraph;


           

            //if (AlgorithmParameters.LinkType == 0)
            //{
            //    var matrixFileProvider = new MatrixFileProvider(MatrixTypeEnum.QMatrix, Parameters.ChangeNumber);
            //    var matrix = matrixFileProvider.ReadMatrix(
            //}
            //else
            //{
            //    var matrixFileProvider = new MatrixFileProvider(MatrixTypeEnum.QMatrix, Parameters.ChangeNumber, Parameters.LinkType * 5);
            //    var matrix = matrixFileProvider.ReadMatrix();
            //}
        }


        /// <summary>
        ///     The b generate population_ click.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        private void BGeneratePopulation_Click(object sender, RoutedEventArgs e)
        {
            if (CBPopulationType.SelectedItem == null)
            {
                MessageBox.Show("Należy wybrać generator populacji");
                return;
            }

            Population = null;
            LBresults.ItemsSource = null;

            if (!CheckFindRouteButton())
            {
                MessageBox.Show("Brak punktow start - stop");
                return;
            }

            LTime.Content = string.Format("Rozpoczęto generacje populacji");

            UpdateAlgorithmParameters();

            DateTime startTime = DateTime.Now;

            try
            {
                PopulationGenerator = GetPopulationGenerator();


                Population = PopulationGenerator.Generate();

                AlgorithmParameters.Population = Population;

                DateTime endTime = DateTime.Now;
                TimeSpan time = endTime - startTime;
                LTime.Content = string.Format("Czas generacji {0}",time.ToString());

                UpdatePopulation(Population, true);



            }
            catch (Exception ex)
            {
                LogTo.ErrorException("BGeneratePopulation_Click blad", ex);
                MessageBox.Show("Blad");
            }
        }

        /// <summary>
        ///     The b parameters_ click.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        private void BParameters_Click(object sender, RoutedEventArgs e)
        {
            var okno = new Parametry(this);
            okno.ShowDialog();

            DDLastBusStops_SelectionChanged(null, null);
        }

        /// <summary>
        ///     The check find route button.
        /// </summary>
        /// <returns>
        ///     The System.Boolean.
        /// </returns>
        private bool CheckFindRouteButton()
        {
            if (StartPoint != null & EndPoint != null)
            {
           //     BFindRoute.IsEnabled = true;
                return true;
            }

         //   BFindRoute.IsEnabled = false;
            return false;
        }

        /// <summary>
        ///     The l bresults_ mouse double click.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        private void LBresults_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            log.Info("LBresults_MouseDoubleClick");
            // WindowsFormsChart.SetPointToOneColor(System.Drawing.Color.Orange);
            var entireRoute = (EntireRoute)LBresults.SelectedItem;

            if (entireRoute != null)
            {
                var okno = new Trasa(this, entireRoute);
                okno.ShowDialog();
            }
        }

        /// <summary>
        ///     The l bresults_ selection changed.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        private void LBresults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var entireRoute = (EntireRoute)LBresults.SelectedItem;

            MainMap.NormalAll();
            MainMap.HideAll();
            MainMap.DrowEntireRoute(entireRoute);
        }



        /// <summary>
        ///     The start the algorithm.
        /// </summary>
        /// <param name="start">
        ///     The start.
        /// </param>
        /// <param name="end">
        ///     The end.
        /// </param>
        //private void StartTheAlgorithm(BusStop start, BusStop end)
        //{
        //    var busList = new Dictionary<string, BusLine>();
        //    var busStopList = new Dictionary<string, BusStop>();
        //    var busGraph = new Graph();

        //    XMLConverter.ReadFromMyXMLFile(busList, busStopList, false, busGraph);

        //    Parameters.BusGraph = busGraph;
        //    Parameters.BusList = busList;
        //    Parameters.BusStopList = busStopList;

        //    Parameters.BusGraph.Link = Parameters.LinkType*5;

        //    Parameters.Start = start;
        //    Parameters.End = end;

        //    if (Parameters.LinkType == 0)
        //    {
        //        Parameters.Q = StructureConverter.ReadQ(Parameters.ChangeNumber);
        //        Parameters.D = StructureConverter.ReadD();
        //    }
        //    else
        //    {
        //        Parameters.Q = StructureConverter.ReadQ(Parameters.ChangeNumber, Parameters.LinkType*5);
        //        Parameters.D = StructureConverter.ReadD(Parameters.LinkType*5);
        //    }

        //    // MainWindow.Parameters
        //    if (Parameters.AlgoritmTypeEnum == AlgoritmTypeEnum.First)
        //    {
        //        var algorutm = new EvolutionaryAlgorithm(Parameters);
        //        p = algorutm.StartAlgoritm();
        //    }
        //    else if (Parameters.AlgoritmTypeEnum == AlgoritmTypeEnum.First)
        //    {
        //    }
        //    else if (Parameters.AlgoritmTypeEnum == AlgoritmTypeEnum.First)
        //    {
        //    }

        //    if (p == null || p.Candidates.Count == 0)
        //    {
        //        MessageBox.Show("Brak połączenia między przystankami");
        //    }
        //    else
        //    {
        //        UpdateLbResults();
        //    }
        //}

        /// <summary>
        ///     The windows forms chart left button.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        private void WindowsFormsChartLeftButton(object sender, MouseEventArgs e)
        {
         //   HitTestResult result = WindowsFormsChart.HitTest(e.X, e.Y);

            ///   LPosition.Content = string.Format("X {0} Y {1},", e.X, e.Y);

            //if (result.ChartElementType == ChartElementType.DataPoint)
            //{
            //    DataPoint point = WindowsFormsChart.Series["Punkty"].Points[result.PointIndex];
            //    point.MarkerColor = Color.Blue;
            //    //  point.MarkerSize = 10;

            //    if (StartPoint == null)
            //    {
            //        StartPoint = new LabeledPoint(point);
            //    }
            //    else if (EndPoint == null)
            //    {
            //        EndPoint = new LabeledPoint(point);

            //        UpdateChartWithSelectedPoints();

            //        int startId = StartPoint.Id;
            //        int endId = EndPoint.Id;

            //        StorageService.SetLastSelectedPoints(startId, endId);
            //    }
            //    else
            //    {
            //        WindowsFormsChart.SetPointToOneColor(Color.Orange);
            //        StartPoint = null;
            //        EndPoint = null;
            //    }

            //    CheckFindRouteButton();
            //}
        }


        public void UpdateChartWithSelectedPoints()
        {
            var squareHelper = new SquareListHelper(SquareList);
            SelectedSquares = squareHelper.GetSelectedSquareList(StartPoint, EndPoint, AlgorithmParameters.NumberOfNeighborSquares);

            DrowSelectedSquares();
        }


        private void DrowSelectedSquares()
        {
            this.MainMap.ClearSquares();
            this.MainMap.DrowSquares(SelectedSquares);
            //  LDistance.Content = StartPoint.GetSimpleDistance(EndPoint).ToString();
        }


        /// <summary>
        ///     The windows forms chart middle button.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        private void WindowsFormsChartMiddleButton(object sender, MouseEventArgs e)
        {
            // zrobić przybliżanie
        }

        /// <summary>
        ///     The windows forms chart mouse down.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
      //  [Trace]
        private void WindowsFormsChartMouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    WindowsFormsChartLeftButton(sender, e);
                    break;
                case MouseButtons.Right:
                    WindowsFormsChartRightButton(sender, e);
                    break;
                case MouseButtons.Middle:
                    WindowsFormsChartMiddleButton(sender, e);
                    break;
            }
        }

        /// <summary>
        ///     The windows forms chart right button.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        private void WindowsFormsChartRightButton(object sender, MouseEventArgs e)
        {
            //HitTestResult result = WindowsFormsChart.HitTest(e.X, e.Y);

            //if (result.ChartElementType == ChartElementType.DataPoint)
            //{
            //    DataPoint point = WindowsFormsChart.Series["Punkty"].Points[result.PointIndex];

            //    var przystanek = new Przystanek(this, point);
            //    przystanek.Show();
            //}
        }

        /// <summary>
        ///     The button 1_ click.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (Wykres == null || Wykres.IsDisposed)
            {
                Wykres = new Wykres(this);
            }

            Wykres.Show();
        }

        #region Generators
        private void CbEvolutionAlgorithmType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //else
            //{

            //}
            ///  
        }

        public BaseEaAlgorithm GetEaAlgorithm()
        {
            var selectedItem = (KeyValuePair<EvolutionAlgorithmTypeEnum, string>)CbEvolutionAlgorithmType.SelectedItem;

            EvolutionAlgorithmBuilder evolutionAlgorithmBuilder = new EvolutionAlgorithmBuilder(AlgorithmParameters, selectedItem.Key, SelectedSquares);


            return evolutionAlgorithmBuilder.GetEvolutionAlgorithm();
        }

        public BasePopulationGenerator GetPopulationGenerator()
        {
            if (CBPopulationType.SelectedItem == null)
            {
                return null;
            }

            BasePopulationGenerator basePopulationGenerator = null;
            var selectedItem = (KeyValuePair<PopulationGeneratorTypeEnum, string>)CBPopulationType.SelectedItem;

            var populationGeneratorBuilder = new PopulationGeneratorBuilder(AlgorithmParameters, selectedItem.Key,
                                                                            SelectedSquares);

            return populationGeneratorBuilder.GetPopulationGenerator();
        }

        private void BOthers_Click(object sender, RoutedEventArgs e)
        {
            var window = new ServiceWindow(BusGraph);
            window.ShowDialog();
        }

        #endregion

        private void DDLastBusStops_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DDLastBusStops.SelectedItem != null)
            {
                var lastPoint = (LastPoint)DDLastBusStops.SelectedItem;

                BGetLastBusStops(lastPoint);
            }
        }

      

        private void BTestPoints_Click(object sender, RoutedEventArgs e)
        {
            MainMap.HideAll();
            MainMap.NormalAll();

            //var testPoints = StorageService.GetTestBusStartStopPoints();
            //var distinctIds = testPoints.Select(x => x.StartId).Distinct();


            //var busList = this.BusStopList.Where(x => distinctIds.Contains(x.Id));


            //MainMap.DrowBusStops(busList);
            ///WindowsFormsChart.Series["Punkty"].Points.ForEach(x => x.MarkerColor = Color.Orange);
            
        }


        private void DDLinie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DDBusLines.SelectedItem != null)
            {
                var line = (BusLineIoDto)DDBusLines.SelectedItem;

                MainMap.DrowEntireLine(line);
            }
        }

       
        private void CbMap_Click(object sender, RoutedEventArgs e)
        {
            if (this.MainMap == null)
            {
                return;
            }
            if (CbMap.IsChecked == true)
            {
                this.MainMap.ShowMap();
            }
            else
            {
                this.MainMap.HideMap();
            }
        }
    }
}
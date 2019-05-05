// -----------------------------------------------------------------------
//  <copyright file="BasePopulationGenerator.cs" company="DevCore .NET">
//      Copyright (c) DevCore.NET All rights reserved.
//  </copyright>
//  <author>Paweł Kondzior</author>
// -----------------------------------------------------------------------


using System.Collections.Generic;
using System.Linq;
using Anotar.Log4Net;
using log4net;
using Magisterka.Infrastructure.Shared.Enum;
using Magisterka.Infrastructure.Shared.Extensions;
using MoreLinq;

namespace Magisterka.Modules.Main.Algoritms.PopulationGenerators
{
    using Magisterka.Infrastructure.Shared.Collections;
    using Magisterka.Infrastructure.Shared.Helpers;
    using Magisterka.Infrastructure.Shared.Structure;
    using Magisterka.Modules.Main.Services;

    public class BasePopulationGenerator : BaseAlgorithm
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(BasePopulationGenerator).Name);

        protected int TableGeneratedPopulatonCount = 0;

        protected GeneratorConnectionTypeEnum GeneratorConnectionTypeEnum { get; set; }

      
        public SquareList SquareList { get; set; }

        #region Construcotrs


        public BasePopulationGenerator(AlgorithmParameters baseAlgorithmParameters)
            : base(baseAlgorithmParameters)
        {
            LoadGeneratorData();

            AlgorithmParameters.BusGraph = BusGraphService.BusGraphServiceInstance.LoadBusGraphFromCache(baseAlgorithmParameters);
        }

        #endregion


        #region Methods

       

        public void LoadGeneratorData()
        {
            if (AlgorithmParameters.LoadDataTypeEnum == LoadDataTypeEnum.MyBook)
            {
                var tMatrixFilePath = FileNameHelper.GetMatrixWithPowerFilePath(MatrixTypeEnum.QMatrixTest, AlgorithmParameters.ChangeNumber, AlgorithmParameters.MaxWalkTime);
                AlgorithmParameters.Q = DataService.DataServiceInstance.LoadMatrixFromCache(tMatrixFilePath);

                var dMatrixFilePath = FileNameHelper.GetMatrixFilePath(MatrixTypeEnum.DMatrixTest, AlgorithmParameters.MaxWalkTime);
                AlgorithmParameters.D = DataService.DataServiceInstance.LoadMatrixFromCache(dMatrixFilePath);
            }
            else if (AlgorithmParameters.LoadDataTypeEnum == LoadDataTypeEnum.RealExamples)
            {
                var tMatrixFilePath = FileNameHelper.GetMatrixWithPowerFilePath(MatrixTypeEnum.QMatrix, AlgorithmParameters.ChangeNumber, AlgorithmParameters.MaxWalkTime);
                AlgorithmParameters.Q = DataService.DataServiceInstance.LoadMatrixFromCache(tMatrixFilePath);

                var dMatrixFilePath = FileNameHelper.GetMatrixFilePath(MatrixTypeEnum.DMatrix, AlgorithmParameters.MaxWalkTime);
                AlgorithmParameters.D = DataService.DataServiceInstance.LoadMatrixFromCache(dMatrixFilePath);
            }
        }

        protected void ClearValues()
        {
            TableGeneratedPopulatonCount = 0;

            ///TimeWarning = false;
        }

        /// <summary>
        /// The complete times in population.
        /// </summary>
        public void CompleteTimesInPopulation()
        {
            var resultPopulation = new Population();/// this.Population;

            foreach (EntireRoute item in this.Population)
            {
                item.LogRoute();

                var timesCompleted = item.CompleteWithTimes(AlgorithmParameters.ArriveTime, AlgorithmParameters.DayTypeEnum);

                if (timesCompleted)
                {
                    item.CalculateRouteTime();
                    item.CalculateTimeFromArrive(AlgorithmParameters.ArriveTime);

                    if (!resultPopulation.AddWithCheckingMax(item, AlgorithmParameters.PopulationCount))
                    {
                        break;
                    }

                }
            }



            if (Population.Count != resultPopulation.Count)
            {
                LogTo.Warn("Nie wszystkie trasy posiadały poprawne czasy całość {0}, poprawnych {1}", Population.Count, resultPopulation.Count);
            }

            this.Population = resultPopulation;
        }

        public virtual Population Generate()
        {
            LogTo.Info("Population Generate()");

            this.CompleteTimesInPopulation();


            var orderedPopulation = Population
                //.Where(x => x.TimeFromArrive > 0)
                .OrderBy(x => x.TimeFromArrive).ToList();

            var result = new Population(orderedPopulation);

            this.Population = null;

            return result;
        }



        


        protected virtual void ConnectBusStopsInAndFindConnectingRoutes(
            BusStop nextBusStop,
            ref BusStop prevBusStop,
            ref List<EntireRoute> routesList)
        {
            var connectingBusLines = AlgorithmParameters.BusLines.GetConnectedBusLines(prevBusStop,
                nextBusStop, AlgorithmParameters.MaxWalkTime);

            if (connectingBusLines == null)
            {
                LogTo.Error("Brak connectingBusLines");
            }

            var newRouteList = new List<EntireRoute>();

            //  log.InfoFormat("Ilość dróg {0}", routesList.Count);
            foreach (var route in routesList)
            {
                foreach (var partOfBusLine in connectingBusLines)
                {
                    var newRoute = route.Clone() as EntireRoute;

                    if (partOfBusLine.ConnectionType == ConnectionType.Bus)
                    {
                        var refBusLines = AlgorithmParameters.BusLines;
                        var refBusStopList = AlgorithmParameters.BusStopList;

                        var middleStops = partOfBusLine.GetMiddleBusStops(ref refBusLines, ref refBusStopList);

                        // middleStops.Log("Posrednie przystanki");

                        newRoute.AddToRoute(partOfBusLine, middleStops);
                    }
                    else
                    {
                        newRoute.AddToRoute(partOfBusLine);
                    }

                    newRouteList.Add(newRoute);

                    ///  newRouteList.Log(log);
                }
            }

            routesList = newRouteList;
        }



        protected virtual bool ConnectBusStopsDirectly(
          BusStop nextBusStop,
          ref BusStop prevBusStop,
          ref List<EntireRoute> allRoutesList)
        {
            var newRouteList = new List<EntireRoute>();

            var busGrpah = AlgorithmParameters.BusGraph;

            var connectingBusLines = busGrpah.GetDirectlyConnectedBusLines(prevBusStop, nextBusStop,
                AlgorithmParameters.AllowWalkLinks,
                AlgorithmParameters.MaxWalkTime);

            if (connectingBusLines == null || !connectingBusLines.Any())
            {
                LogTo.Error("Brak connectingBusLines {0} {1}", prevBusStop.Id, nextBusStop.Id);
                allRoutesList = newRouteList;
                return false;
            }


            var (routesWithBusContinuation, routesWithoutContinuation) =
                allRoutesList.Partition(routes =>
                                                connectingBusLines.Where(x => x.ConnectionType == ConnectionType.Bus)
                                                                  .Select(c => c.BusLineIoDto)
                                                                  .Contains(routes.GetLastInputBusLine())
                                              );


            foreach (var route in routesWithBusContinuation)
            {

                var lineContiunation = connectingBusLines.FirstOrDefault(x => x.BusLineIoDto.Name == route.GetLastInputBusLine().Name);

                // dodawanie kolejnego do tej samej linii
                if (lineContiunation != null)
                {
                    var newRoute = route.Clone() as EntireRoute;

                    newRoute.AddToRoute(lineContiunation);
                    newRouteList.Add(newRoute);

                    newRouteList.Log(log);
                }
            }


            foreach (var route in routesWithoutContinuation)
            {
                foreach (var lineContiunation in connectingBusLines)
                {
                    var newRoute = route.Clone() as EntireRoute;

                    newRoute.AddToRoute(lineContiunation);

                    //if (newRoute.TransferCount <= AlgorithmParameters.ChangeNumber)
                    {
                        newRouteList.Add(newRoute);
                    }
                }
            }

            allRoutesList = newRouteList;
            return true;
        }



        protected void GeneratePopulationFromListOfBusStops(List<BusStop> listofBusStops)
        {
            listofBusStops.Log("Przystanki do polaczenia");

            List<EntireRoute> routesList = new List<EntireRoute>();

            EntireRoute entireRoute = new EntireRoute(listofBusStops);

            routesList.Add(entireRoute);

            var prevBusStop = listofBusStops.FirstOrDefault();

            foreach (var nextBusStop in listofBusStops.Skip(1))
            {
                if (GeneratorConnectionTypeEnum == GeneratorConnectionTypeEnum.ConnectDirectly)
                {

                    if (!ConnectBusStopsDirectly(nextBusStop, ref prevBusStop, ref routesList))
                    {
                        break;
                    }
                }
                else if (GeneratorConnectionTypeEnum == GeneratorConnectionTypeEnum.FindMisssingLines)
                {
                    ConnectBusStopsInAndFindConnectingRoutes(nextBusStop, ref prevBusStop, ref routesList);
                }

                prevBusStop = nextBusStop;
            }

            foreach (var route in routesList)
            {
                if (route.PartOfTheRoute.Count > 0)
                {
                    route.CalculateBusStopConnections();
                    Population.Add(route);
                }
            }
        }







        #endregion
    }
}
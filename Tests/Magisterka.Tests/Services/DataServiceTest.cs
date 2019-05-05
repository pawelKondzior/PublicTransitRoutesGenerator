using Magisterka.Infrastructure.Shared.Collections;
using Magisterka.Infrastructure.Shared.Structure;
using Magisterka.Modules.Main.Init;
using Magisterka.Modules.Main.Services;
using System;
using System.Diagnostics;
using Xunit;
using System.Linq;
using System.IO;
using Anotar.Log4Net;
using Magisterka.Infrastructure.Shared.Enum;
using AutoMapper;
using System.Collections.Generic;
using Magisterka.Infrastructure.Shared.Extensions;
using Magisterka.Infrastructure.Shared.Comparers;
using MoreLinq;

// // -----------------------------------------------------------------------
// //  <copyright file="DataService.cs" company="DevCore .NET">
// //      Copyright DevCore.NET All rights reserved.
// //  </copyright>
// //  <author>Paweł Kondzior</author>
// // -----------------------------------------------------------------------

namespace Magisterka.Tests.Services
{

    public class DataServiceTest
    {
        internal DataService DataService { get; set; }

        protected BusLinesList BusLinesList { get; set; }

        protected BusStopList BusStopListTxt { get; set; }

        protected BusStopList BusStopListCSV { get; set; }

        protected BusStopList CombinedBusStopsList { get; set; }

        protected BusGraph BusGraph { get; set; }



        public DataServiceTest()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo("Log4Net.config"));

            LogTo.Info("Uruchomiono aplikacje");

            new AutoMapperInit().Init();

            DataService = DataService.DataServiceInstance;
        }




        private void LoadData()
        {
            BusLinesList = this.DataService.GetBusLines(BusLineLoadTypEnum.WithoutNightLines);


            BusStopListCSV =  this.DataService.GetBusStopListCSV();
            BusStopListTxt = this.DataService.GetNewBusStopListTXT();


           // CombinedBusStopsList = this.DataService.GetBusStopListCSV();

            CombinedBusStopsList = this.DataService.GetCombinedTextBusStops();
         ///   CombinedBusStopsList = this.DataService.GetXmlBusStopList();

            

            ///FilesBusStopsList = new BusStopList(list.ToList());
        }


        [Fact]
        public void CheckIfBusStopsContainsZero()
        {
            LoadData();


            var allCorrect = CombinedBusStopsList.All(x => x.HasGeometryPoints());



            var notCorrectBusStops = CombinedBusStopsList.Where(x => !x.HasGeometryPoints());

            notCorrectBusStops.ForEach(x =>
            {
                LogTo.Warn("Bus stop z blednymi danymi {0}", x.Id);
            });


            Assert.True(allCorrect);
        }

        [Fact]
        public void TimeOfDataLoad()
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            LoadData();

            stopwatch.Stop();

            Console.WriteLine("Time: " + stopwatch.Elapsed.ToString());
        }


        [Fact]
        public void TestMetadataService()
        {
            LoadData();
            var service = new BusStopMetadataService(CombinedBusStopsList, BusLinesList);
            service.GenerateNewFileWithBusStops();

        }


        [Fact]
        public void TestMetadataServiceCombineFiles()
        {
            LoadData();
            var service = new BusStopMetadataService(CombinedBusStopsList, BusLinesList);
            service.GenerateNewFileWithBusStops();

        }


        [Fact]
        public void CombineBusStopFilesService()
        {
            LoadData();
            var service = new CombineBusStopFilesService(BusStopListCSV, BusStopListTxt, BusLinesList);
            service.GenerateNewFileWithBusStops();

        }



        public void CheckRoutesHavingMissingBusStops()
        {
            
        }



        [Fact]
        public void CheckBusStopCount()
        {
            LoadData();

            var correctBusStopList = BusLinesList.GetAllBusStopsDistinct();
            var correctMappedList = Mapper.Map<List<BusStop>>(correctBusStopList);

            var busStopListFromFiles = CombinedBusStopsList.ToList();

            LogTo.Debug("bus stops in lines count {0}", correctMappedList.Count());
            LogTo.Debug("bus stops from file {0}", busStopListFromFiles.Count());

            var busStopsToAdd = new List<BusStop>();
            var busStopsAlreadyInList = new List<BusStop>();
            var busStopsInFileButNotInBusLines = new List<BusStop>();
            var busStopsInFileAndInBusLines = new List<BusStop>();


            correctMappedList.ForEach(newBusItem =>
            {
                if (!busStopListFromFiles.Contains(newBusItem, new BusStopComparer()))
                {
                    busStopsToAdd.Add(newBusItem);
                }
                else
                {
                    busStopsAlreadyInList.Add(newBusItem);
                }
            });

            var linesBroken = BusLinesList.GetLinesContainingBusStops(busStopsToAdd);

            linesBroken.ForEach(x => LogTo.Debug(x.Name));

            var names = string.Join(", ", linesBroken.Select(x => x.Name));

            LogTo.Warn("Broken lines: {0}", names);

            busStopListFromFiles.ForEach(busItem =>
            {
                if (correctMappedList.Contains(busItem, new BusStopComparer()))
                {
                    busStopsInFileAndInBusLines.Add(busItem);
                }
                else
                {
                    busStopsInFileButNotInBusLines.Add(busItem);
                }
            });


            LogTo.Info("bus stops to add {0}", busStopsToAdd.Count);
            LogTo.Info("bus stops already in list {0}", busStopsAlreadyInList.Count);



            Assert.Equal(correctBusStopList.Count(), busStopListFromFiles.Count());

        }
    }
}
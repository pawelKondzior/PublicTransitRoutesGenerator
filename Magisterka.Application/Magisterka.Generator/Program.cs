//#define GenerateTest
///#define GenerateQTable
#define GenerateDTable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anotar.Log4Net;
using Magisterka.Infrastructure.Shared.Collections;
using Magisterka.Infrastructure.Shared.Enum;
using Magisterka.Infrastructure.Shared.Extensions;
using Magisterka.Infrastructure.Shared.Helpers;
using Magisterka.Infrastructure.Shared.Structure;
using Magisterka.Infrastructure.Shared.TestData;
using Magisterka.Modules.Main.Init;
using Magisterka.Modules.Main.Matrix;
using Magisterka.Modules.Main.Services;
using MoreLinq;

using Magisterka.Modules.Main.Algoritms.PopulationGenerators;
using Magisterka.Modules.Main.Builders;
using System.Diagnostics;
using System.Threading;

namespace Magisterka.Generator
{
    class Program
    {
        private static List<int> walkLinksArray = new int[] { 0, 5, 10, 15 }.ToList();

        private static readonly DataService DataService = DataService.DataServiceInstance;

        public static BusGraph BusGraph { get; set; }

        /// <summary>
        /// Gets or sets the bus lines.
        /// </summary>
        public static BusLinesList BusLinesList { get; set; }

        /// <summary>
        /// Gets or sets the bus stop list.
        /// </summary>
        public static BusStopList BusStopList { get; set; }


#region Data
        private static void LoadBookExampleData()
        {
            BusLinesList = TableData.GeneerateBookExampleBusLinesList();
            BusStopList = TableData.GeneerateBookExampleBusStopList();
        }

        private static void LoadData()
        {
            BusLinesList = DataService.GetBusLines();
            BusStopList = DataService.GetXmlBusStopList();
            ///BusStopList = DataService.GetCombinedTextBusStops();
        }
        #endregion

        private static void GenerateAndSaveAllDTable()
        {
            walkLinksArray.ForEach(x => GenerateAndSaveDTable(x));
        }

        private static void GenerateAndSaveDTable(int walkTime)
        {

            BusGraph = new BusGraph(BusStopList, BusLinesList, walkTime);
            var distanceMatrixGenerator = new DistanceMatrixGenerator(BusLinesList, BusStopList, BusGraph);

            var resultMatrix = distanceMatrixGenerator.GenerateDistanceMatrix();
            ///     int[,] resultMatrix = new int[BusLinesList.Count, BusLinesList.Count]; ;

            var path = FileNameHelper.GetMatrixFilePath(MatrixTypeEnum.DMatrix, walkTime);

            DataService.SaveMatrixToFile(path, resultMatrix);
        }

        private static void GenerateAndSaveAllTestDTable()
        {
            walkLinksArray.ForEach(x => GenerateAndSaveTestDTable(x));
        }

        private static void GenerateAndSaveTestDTable(int walkTime)
        {

            BusGraph = new BusGraph(BusStopList, BusLinesList, walkTime);
            var distanceMatrixGenerator = new DistanceMatrixGenerator(BusLinesList, BusStopList, BusGraph);

            var resultMatrix = distanceMatrixGenerator.GenerateDistanceMatrix();
            ///     int[,] resultMatrix = new int[BusLinesList.Count, BusLinesList.Count]; ;

            var path = FileNameHelper.GetMatrixFilePath(MatrixTypeEnum.DMatrixTest, walkTime);

            DataService.SaveMatrixToFile(path, resultMatrix);
        }

        

        private static void GenerateAndSaveAllTTestTable()
        {
            walkLinksArray.ForEach(x => GenerateAndSaveTTestTable(x));
        }

        private static void GenerateAndSaveTTestTable(int walkTime)
        {
            var transferMatrixGenerator = new TransferMatrixGenerator(BusLinesList, BusStopList);

            var resultMatrix = transferMatrixGenerator.GenerateTransferMatrix(walkTime);

            var path = FileNameHelper.GetMatrixFilePath(MatrixTypeEnum.TMatrixTest, walkTime);

            DataService.SaveMatrixToFile(path, resultMatrix);

            var readMatrix = DataService.LoadMatrixFromFile(path);

            LogTo.Info(resultMatrix.ContentEquals(readMatrix).ToString());
        }

        private static void GenerateAndSaveAllTTable()
        {
            walkLinksArray.ForEach(x => GenerateAndSaveTTable(x));
        }

        private static void GenerateAndSaveTTable(int walkTime)
        {
            /// var BusGraph = new BusGraph(BusStopList, BusLines, 3 * 5);

            var transferMatrixGenerator = new TransferMatrixGenerator(BusLinesList, BusStopList);

            var resultMatrix = transferMatrixGenerator.GenerateTransferMatrix(walkTime);
            //int[,] transferMatrix = new int[busStopList.Count, busStopList.Count]; ;
            var path = FileNameHelper.GetMatrixFilePath(MatrixTypeEnum.TMatrix, walkTime);

            DataService.SaveMatrixToFile(path, resultMatrix);

           
        }


        private static void GenerateAndSaveAllTestQTables()
        {
            foreach (var walkLink in walkLinksArray)
            {
                var tMatrixFileName = FileNameHelper.GetMatrixFilePath(MatrixTypeEnum.TMatrixTest, walkLink);

                var tMatrix = DataService.LoadMatrixFromFile(tMatrixFileName);

                var transferMatrixGenerator = new TransferMatrixGenerator(BusLinesList, BusStopList, tMatrix);

                var sequence = MoreLinq.MoreEnumerable.Sequence(1, 5);

                foreach (var power in sequence)
                {
                    var result = transferMatrixGenerator.GenerateQMatrix(power);

                    var path = FileNameHelper.GetMatrixWithPowerFilePath(MatrixTypeEnum.QMatrixTest, power, walkLink);

                    DataService.SaveMatrixToFile(path, result);
                }
            }
        }


        private static void GenerateAndSaveAllQTables()
        {
            foreach (var tNumber in walkLinksArray)
            {
                var tMatrixFileName = FileNameHelper.GetMatrixFilePath(MatrixTypeEnum.TMatrix, tNumber);

                var tMatrix = DataService.LoadMatrixFromFile(tMatrixFileName);

                var transferMatrixGenerator = new TransferMatrixGenerator(BusLinesList, BusStopList, tMatrix);

                var sequence = MoreLinq.MoreEnumerable.Sequence(1, 10);

                foreach (var power in sequence)
                {
                    var result = transferMatrixGenerator.GenerateQMatrix(power);


                    var path = FileNameHelper.GetMatrixWithPowerFilePath(MatrixTypeEnum.QMatrix, power, tNumber);

                    DataService.SaveMatrixToFile(path, result);
                }
            }



            //var resultMatrix = transferMatrixGenerator.GenerateTransferMatrix(walkTime);
            ////int[,] transferMatrix = new int[busStopList.Count, busStopList.Count]; ;
            //var path = FileNameHelper.GetMatrixFilePath(MatrixTypeEnum.TMatrix, walkTime);

            //DataService.SaveMatrixToFile(path, resultMatrix);
        }


        public static void GenerateTables()
        {
#if GenerateTest

            LoadBookExampleData();

            GenerateAndSaveAllTTestTable();

            GenerateAndSaveAllTestQTables();

            GenerateAndSaveAllTestDTable();

#endif

#if GenerateQTable

            LoadData();
            GenerateAndSaveAllTTable();
            GenerateAndSaveAllQTables();
#endif


#if GenerateDTable

            LoadData();

            ///   GenerateAndSaveTTable(0);
          //  GenerateAndSaveDTable(0);

            //GenerateAndSaveAllTTable();
            GenerateAndSaveAllDTable();
#endif

            /// LoadData();

            //   GenerateAndSaveTTestTable(0);


            ///CreateExampleQTable();

            /// GenerateAndSaveExamleDTable(0);
            ///CreateExampleQTable();

            ///GenerateAndSaveQTables();

            // GenerateAndSaveTTable(15);

            //MoreLinq.MoreEnumerable.Sequence(0, 15).ForEach(GenerateAndSaveDTable);


            /// GenerateAndSaveDTable(15);
        }


        static async Task asyncTask(int number)
        {
            TestGenerationService testGenerationService = new TestGenerationService();

            var sw = new Stopwatch();
            sw.Start();

            Console.WriteLine("async: Starting {0}", number);
         
            
            testGenerationService.RunTests();
            
            Console.WriteLine("async {0}: Running for {1} seconds", number, sw.Elapsed.TotalSeconds);
         //   await wait;
            Console.WriteLine("async {0}: Running for {1} seconds", number, sw.Elapsed.TotalSeconds);
            Console.WriteLine("async {0}: Done", number);
        }




        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo("Log4Net.config"));

            LogTo.Info("Uruchomiono aplikacje");

            new AutoMapperInit().Init();

           /// LoadData();

              GenerateTables();

            ///  StorageService storageService = new StorageService();
            ///  StorageService storageService = new StorageService();
            //storageService.GenerateBusStopCombinationFromSquares(BusStopList, 4);
            //   storageService.GenerateAllParameters();



            //Task task1 = Task.Factory.StartNew(() => asyncTask(1));
            //Task task2 = Task.Factory.StartNew(() => asyncTask(2));
            //Task task3 = Task.Factory.StartNew(() => asyncTask(3));
            //Task task4 = Task.Factory.StartNew(() => asyncTask(4));

            //Task.WaitAll(task1, task2, task3, task4);

       //     TestGenerationService testGenerationService = new TestGenerationService();
       //     testGenerationService.RunTests();

            ///    Task.WaitAll(task1);
            ///    

            Console.WriteLine("All threads complete");


     ///       storageService.GenerateAllBusStopCombination(BusStopList);

            ///   storageService.GetAllTestIds();

            //GenerateTables();


            //LoadData();



            Console.WriteLine("KONIEC");
        }
    }
}




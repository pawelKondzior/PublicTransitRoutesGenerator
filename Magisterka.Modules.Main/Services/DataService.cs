// -----------------------------------------------------------------------
//  <copyright file="DataService.cs" company="DevCore .NET">
//      Copyright (c) DevCore.NET All rights reserved.
//  </copyright>
//  <author>Paweł Kondzior</author>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Magisterka.Infrastructure.Shared.Structure;
using Magisterka.Infrastructure.Shared.Collections;
using Magisterka.Data.Access.Repository;
using AutoMapper;
using System.Collections;
using Magisterka.Infrastructure.Shared.IoDto;
using System;
using System.Diagnostics;
using Magisterka.Infrastructure.Shared.Settings;
using System.Linq;
using Magisterka.Infrastructure.Shared.Helpers;
using Magisterka.Infrastructure.Shared.Enum;
using Magisterka.Infrastructure.Shared.Comparers;
using System.Collections.Concurrent;

//using Magisterka.Modules.Main.Aspects;
namespace Magisterka.Modules.Main.Services
{
    //  [StopWatchAttribute]
    public class DataService : IDisposable
    {
        /// public DataService DataServiceInstance = new DataService();

        private static volatile DataService instance;
        private static object syncRoot = new Object();

        private DataService() { }

        public static DataService DataServiceInstance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new DataService();
                    }
                }

                return instance;
            }
        }


        #region Fielnds


        private static readonly string SerializadedBusLinesPath = @"C:\serializated.dat";

        private static readonly bool MatrixCache = true;

        public ConcurrentDictionary<string, int[,]> Cache = new ConcurrentDictionary<string, int[,]>();

        #endregion

        #region Properties

        public BusStopList BusStops { get; set; }

        public BusLinesList BusLines { get; set; }

        #endregion

        #region Conutructors


        #endregion

        #region Methods

        #region Serialize

        public void SerializeBusLinesNow()
        {
            try
            {
                Stream s = File.Open(SerializadedBusLinesPath, FileMode.Create);
                BinaryFormatter b = new BinaryFormatter();
                b.Serialize(s, BusLines);
                s.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void DeSerializeBusLinesNow()
        {
            try
            {
                Stream s = File.Open(SerializadedBusLinesPath, FileMode.Open);
                BinaryFormatter b = new BinaryFormatter();
                BusLines = (BusLinesList)b.Deserialize(s);
                s.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        #endregion


        public void FlushBusStopCashe()
        {
            this.BusStops = null;
        }

        public BusStopList GetBusStopListCSV()
        {

            if (BusStops == null)
            {
                var busStopRepository = new BusStopRepository(FileNameHelper.BusStopFileCSV);
                var collection = busStopRepository.ReadOldFile();

                BusStops = Mapper.Map<IEnumerable<BusStopIoDto>, BusStopList>(collection);


                AddArrayNubers(BusStops);


            }
            return BusStops;
        }


        public BusStopList GetCombinedTextBusStops()
        {
            var newBusStopList = this.GetNewBusStopListTXT();

            this.FlushBusStopCashe();

            var oldBusStopList = this.GetBusStopListCSV();

            var list = newBusStopList.Union(oldBusStopList).Distinct(new BusStopComparer());

            return new BusStopList(list);
        }

        public BusStopList GetNewBusStopListTXT()
        {

       ///     if (BusStops == null)
    //        {
                var busStopRepository = new BusStopRepository(FileNameHelper.BusStopNewFileTXT);
                var collection = busStopRepository.ReadNewFile();

                BusStops = Mapper.Map<IEnumerable<BusStopIoDto>, BusStopList>(collection);


                AddArrayNubers(BusStops);


   //         }
            return BusStops;
        }






        public BusStopList GetXmlBusStopList()
        {
            var busStopRepository = new BusStopRepository(FileNameHelper.BusStopFileXML);
            var list = busStopRepository.LoadAllNew();

            var result = new BusStopList(list);

            AddArrayNubers(result);

            return result;
        }


        private void AddArrayNubers(BusStopList busStopList)
        {
            int i = 0;
            foreach (var item in busStopList)
            {
                item.ArrayNumber = i;
                i++;
            }
        }


        public void SaveBusStopList(List<BusStop> busStopList)
        {
            var busStopRepository = new BusStopRepository(FileNameHelper.BusStopFileXML);
            busStopRepository.SaveNew(busStopList);
        }


        public BusLinesList GetBusLines(BusLineLoadTypEnum busLineLoadTypEnum = BusLineLoadTypEnum.WithoutNightLines)
        {
            //DeSerializeBusLinesNow();

            if (BusLines == null)
            {
                var busLineRepository = new BusLineRepository(FileNameHelper.BusLinesPath, busLineLoadTypEnum);
                BusLines = busLineRepository.All();

                // SerializeBusLinesNow();
            }
            return BusLines;
        }

        public void SaveMatrixToFile(string filePath, int[,] matrix)
        {


            using (TextWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(0); j++)
                    {
                        if (j != 0)
                        {
                            writer.Write(" ");
                        }
                        writer.Write(matrix[i, j]);
                    }
                    writer.WriteLine();
                }
            }
        }



        public void SaveMatrixToFile(string filePath, double[,] matrix)
        {


            using (TextWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(0); j++)
                    {
                        if (j != 0)
                        {
                            writer.Write(" ");
                        }
                        writer.Write(matrix[i, j]);
                    }
                    writer.WriteLine();
                }
            }
        }


        public int[,] LoadMatrixFromCache(string filePath)
        {
            if (!MatrixCache)
            {
                return LoadMatrixFromFile(filePath);
            }

            int[,] matrix = null;


            if (Cache.TryGetValue(filePath, out matrix))
            {
                return matrix;
            }

            matrix = LoadMatrixFromFile(filePath);

            Cache.TryAdd(filePath, matrix);


            return matrix;
        }


        public int[,] LoadMatrixFromFile(string filePath)
        {



            int[][] list = File.ReadAllLines(filePath)
                .Select(l => l.Split(' ').Select(i => int.Parse(i)).ToArray())
                .ToArray();


            return JaggedToMultidimensional(list);
        }



        private T[,] JaggedToMultidimensional<T>(T[][] jaggedArray)
        {
            int rows = jaggedArray.Length;
            int cols = jaggedArray.Max(subArray => subArray.Length);
            T[,] array = new T[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                cols = jaggedArray[i].Length;
                for (int j = 0; j < cols; j++)
                {
                    array[i, j] = jaggedArray[i][j];
                }
            }
            return array;
        }




        #endregion



        public void Dispose()
        {
            BusStops.Clear();
            BusStops = null;

            BusLines.Clear();
            BusLines = null;
        }
    }
}
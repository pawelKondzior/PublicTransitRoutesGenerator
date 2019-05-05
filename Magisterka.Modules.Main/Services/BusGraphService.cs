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
    public class BusGraphService
    {
        /// public DataService DataServiceInstance = new DataService();

        private static volatile BusGraphService instance;
        private static object syncRoot = new Object();

        private BusGraphService() { }

        public static BusGraphService BusGraphServiceInstance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new BusGraphService();
                    }
                }

                return instance;
            }
        }

        

        public ConcurrentDictionary<int, BusGraph> Cache = new ConcurrentDictionary<int, BusGraph>();



        #region Properties

        public BusStopList BusStops { get; set; }

        public BusLinesList BusLines { get; set; }

        #endregion

        #region Conutructors


        #endregion

        #region Methods

     

        public BusGraph LoadBusGraphFromCache(AlgorithmParameters algorithmParameters)
        {
            BusGraph result = null;

            if (Cache.TryGetValue(algorithmParameters.MaxWalkTime, out result))
            {
                return result;
            }

            result = new BusGraph(algorithmParameters.BusStopList, algorithmParameters.BusLines, algorithmParameters.MaxWalkTime);

            Cache.TryAdd(algorithmParameters.MaxWalkTime, result);


            return result;
        }


        #endregion

    }
}
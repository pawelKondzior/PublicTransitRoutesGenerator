using System;
using System.Collections.Generic;
using System.Linq;
using Anotar.Log4Net;
using Magisterka.Infrastructure.Shared.Collections;
using Magisterka.Infrastructure.Shared.Enum;
using Magisterka.Infrastructure.Shared.Generic;
using Magisterka.Infrastructure.Shared.Structure;
using MoreLinq;
using QuickGraph;
using QuickGraph.Algorithms;

namespace Magisterka.Modules.Main.Matrix
{
    public abstract class BaseMatrixGenerator
    {
      ///  private BusGraph BusGraph { get; set; }

        protected BusLinesList BusLinesList { get; set; }

        protected BusStopList BusStopList { get; set; }


       // private int[,] transferMatrix = null;

        protected BaseMatrixGenerator(BusLinesList busLinesList, BusStopList busStopList)
        {
            BusLinesList = busLinesList;
            BusStopList = busStopList;
        }

      
    }
}
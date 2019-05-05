using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Net;
using Castle.Components.DictionaryAdapter;
using Magisterka.Infrastructure.Shared.Collections;
using Magisterka.Infrastructure.Shared.Generic;
using Magisterka.Infrastructure.Shared.IoDto;
using Magisterka.Infrastructure.Shared.Structure;
using Moq;
using Xunit;
using Magisterka.Modules.Main.Matrix;
using Magisterka.Infrastructure.Shared.Extensions;
using Magisterka.Infrastructure.Shared.Helpers;
using Magisterka.Infrastructure.Shared.TestData;
using MoreLinq;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra;

namespace Magisterka.Tests.Tables
{
    /// <summary>
    /// 
    /// Dane z
    /// See discussions, stats, and author profiles for this publication at: https://www.researchgate.net/publication/265246529
    /// dr Approximation Method to Route Generation in
    /// Public Transportation Network
    /// </summary>
    public abstract class BaseTableGenerationTests
    {

        protected BusLinesList BusLinesList { get; set; }

        protected  BusStopList BusStopList { get; set; }

        protected BusGraph BusGraph { get; set; }
         
        protected int[,]  TestTransferMatrix { get; set; }


        protected double[,] TestQ3Matrix { get; set; }

        protected int[,] TestDMatrix { get; set; }

        

       // protected TransferMatrixGenerator TransferMatrixGenerator { get; set; }

        public BaseTableGenerationTests()
        {
            BusStopList = TableData.GeneerateBusStopList();
            BusLinesList = TableData.GeneerateBusLinesList();
            TestTransferMatrix = GetTransferMatixData();
            BusGraph = new BusGraph(BusStopList, BusLinesList, 15);

            TestQ3Matrix = GetQ3MatixData();
            TestDMatrix = TableData.GetDMatixData();


         
        }



        #region DataGeneration

       



        #region JKoszelew work test



        public static int[,] GetTransferMatixData()
        {

            int[,] tMatrix = new int[9, 9] {
                {0, 1, 1, 0, 1, 0, 0, 1, 0},
                {0, 0, 1, 0, 1, 0, 1, 1, 1},
                {0, 0, 0, 0, 1, 0, 1, 0, 1},
                {0, 0, 1, 0, 0, 0, 1, 0, 1},
                {0, 0, 0, 0, 0, 1, 0, 0, 0},
                {0, 0, 0, 0, 1, 0, 1, 1, 1},
                {0, 1, 0, 0, 0, 0, 0, 1, 1},
                {1, 0, 0, 0, 0, 0, 0, 0, 1},
                {0, 0, 0, 0, 0, 0, 0, 0, 0}

            };

            return tMatrix;
        }


        public static double[,] GetQ3MatixData()
        {
            // zmieniłem z oryginałem bo był błedny
            return new double[9, 9] {
                {0, 1, 1, 0, 1, 2, 2, 1, 2},
                {2, 0, 1, 0, 1, 2, 1, 1, 1},
                {3, 2, 0, 0, 1, 2, 1, 2, 1},
                {3, 2, 1, 0, 2, 3, 1, 2, 1},
                {3, 3, 4, 0, 0, 1, 2, 2, 2},
                {2, 2, 3, 0, 1, 0, 1, 1, 1},
                {2, 1, 2, 0, 2, 3, 0, 1, 1}, ///6 3 
                {1, 2, 2, 0, 2, 3, 3, 0, 1},
                {0, 0, 0, 0, 0, 0, 0, 0, 0}
            };
        }

       

       
        #endregion

        #endregion
    }
}


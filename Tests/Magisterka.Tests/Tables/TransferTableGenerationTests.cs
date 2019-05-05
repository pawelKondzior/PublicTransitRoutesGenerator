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
using MoreLinq;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra;
using Magisterka.Infrastructure.Shared.TestData;

namespace Magisterka.Tests.Tables
{
    /// <summary>
    /// 
    /// Dane z
    /// See discussions, stats, and author profiles for this publication at: https://www.researchgate.net/publication/265246529
    /// dr Approximation Method to Route Generation in
    /// Public Transportation Network
    /// </summary>
    public class TransferTableGenerationTests : BaseTableGenerationTests
    {

      

        public TransferTableGenerationTests() : base()
        {
          

        }

        [Fact]
        public void TestMyBookData()
        {
            int maxWalkTime = 0;
            
            //// Important, data loaded second TIME
            BusStopList = TableData.GeneerateBookExampleBusStopList();
            BusLinesList = TableData.GeneerateBookExampleBusLinesList();
            TestTransferMatrix = TableData.GetMyBookTransferMatixData();
            BusGraph = new BusGraph(BusStopList, BusLinesList, maxWalkTime);


            var transferMatrixGenerator = new TransferMatrixGenerator(BusLinesList, BusStopList);

            var resultMatrix = transferMatrixGenerator.GenerateTransferMatrix(maxWalkTime);


            Assert.Equal(resultMatrix.Length, TestTransferMatrix.Length);

            Assert.True(resultMatrix.ContentEquals(TestTransferMatrix));
        }



        [Fact]
        public void GenerateTransferMatrix()
        {
            var transferMatrixGenerator = new TransferMatrixGenerator(BusLinesList, BusStopList);

            var resultMatrix = transferMatrixGenerator.GenerateTransferMatrix(10);


            Assert.Equal(resultMatrix.Length, TestTransferMatrix.Length);

            Assert.True(resultMatrix.ContentEquals(TestTransferMatrix));
        }



        [Fact]
        public void TestQMatrix()
        {
            var transferMatrixGenerator = new TransferMatrixGenerator(BusLinesList, BusStopList, base.TestTransferMatrix);


            var reuslt = transferMatrixGenerator.GenerateQMatrix(3);


            var equals = reuslt.ContentEquals(TestQ3Matrix);


            Assert.True(equals);

        }

       

        [Fact]
        public void TestBusStopMockDefaultValue()
        {
            Assert.Equal(BusStopList[0].GetDistance(BusStopList[1]), TableData.maxDefaultdefaultDistance);
        }


        [Fact]
        public void TestBusStopMockValue()
        {
            Assert.Equal(BusStopList[0].GetDistance(BusStopList[7]), 10);
            Assert.Equal(BusStopList[7].GetDistance(BusStopList[0]), 10);

            Assert.Equal(BusStopList[1].GetDistance(BusStopList[6]), 5);
            Assert.Equal(BusStopList[6].GetDistance(BusStopList[1]), 5);

            Assert.Equal(BusStopList[5].GetDistance(BusStopList[4]), 5);
            Assert.Equal(BusStopList[4].GetDistance(BusStopList[5]), 5);

            //busMock1.Setup(x => x.GetDistance(busMock8.Object)).Returns(10);
            //busMock8.Setup(x => x.GetDistance(busMock1.Object)).Returns(10);

            //busMock2.Setup(x => x.GetDistance(busMock7.Object)).Returns(5);
            //busMock7.Setup(x => x.GetDistance(busMock2.Object)).Returns(5);

            //busMock6.Setup(x => x.GetDistance(busMock5.Object)).Returns(5);
            //busMock5.Setup(x => x.GetDistance(busMock6.Object)).Returns(5);
        }

    }
}


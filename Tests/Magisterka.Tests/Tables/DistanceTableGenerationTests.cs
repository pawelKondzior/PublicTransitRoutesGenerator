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

namespace Magisterka.Tests.Tables
{
    /// <summary>
    /// 
    /// Dane z
    /// See discussions, stats, and author profiles for this publication at: https://www.researchgate.net/publication/265246529
    /// dr Approximation Method to Route Generation in
    /// Public Transportation Network
    /// </summary>
    public class DistanceTableGenerationTests : BaseTableGenerationTests
    {

      

        public DistanceTableGenerationTests() : base()
        {
            BusGraph = new BusGraph(BusStopList, BusLinesList, 15);
        }

        [Fact]
        public void GenerateTransferMatrix()
        {

            var distanceMatrixGenerator = new DistanceMatrixGenerator(BusLinesList, BusStopList, BusGraph);

            var resultMatrix = distanceMatrixGenerator.GenerateDistanceMatrix();

            Assert.Equal(resultMatrix.Length, TestDMatrix.Length);

            Assert.True(resultMatrix.ContentEquals(TestDMatrix));
        }


        

    }
}


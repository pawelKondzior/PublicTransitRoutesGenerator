#define MinimalDistance
#define BookExamples

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
using Magisterka.Modules.Main.Algoritms.EA;
using Magisterka.Modules.Main.Algoritms.PopulationGenerators;
using MoreLinq;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra;
using System.IO;
using Anotar.Log4Net;
using Magisterka.Modules.Main.Services;
using Magisterka.Infrastructure.Shared.Enum;
using Magisterka.Tests.Theory;
using System.Diagnostics;
using Magisterka.Modules.Main.Init;
using System.Linq;

namespace Magisterka.Tests.Paths
{
    /// <summary>
    /// 
    /// Dane z
    /// See discussions, stats, and author profiles for this publication at: https://www.researchgate.net/publication/265246529
    /// dr Approximation Method to Route Generation in
    /// Public Transportation Network
    /// </summary>
    public abstract class BaseGeneratorTests
    {
       

        private static readonly DataService DataService = DataService.DataServiceInstance;

        protected BusLinesList BusLinesList { get; set; }

        protected  BusStopList BusStopList { get; set; }

        protected BusGraph BusGraph { get; set; }

        protected BaseEaAlgorithm EaAlgorithm { get; set; }
        protected AlgorithmParameters AlgorithmParameters { get; set; }
        protected int[,] TestQMatrix { get; set; }
        protected int[,] TestDMatrix { get; set; }

        protected virtual void InitializeData(BasePathTestSet baseTestSet)
        {
            InitBaseData(baseTestSet);

            AlgorithmParameters.Start = this.BusStopList.FirstOrDefault(x => x.Id == baseTestSet.StartBusId);
            AlgorithmParameters.End = this.BusStopList.FirstOrDefault(x => x.Id == baseTestSet.EndBusId);

            //AlgorithmParameters.Start = this.BusStopList[baseTestSet.StartBusId - 1]; // 10
            //AlgorithmParameters.End = this.BusStopList[baseTestSet.EndBusId - 1]; // 9
        }


        private void InitBaseData(BasePathTestSet baseTestSet)
        {
            AlgorithmParameters = new AlgorithmParameters();
            AlgorithmParameters.SetDefaultValues();
            AlgorithmParameters.LoadDataTypeEnum = baseTestSet.LoadDataTypeEnum;

            
            AlgorithmParameters.PopulationCount = 2000;


           // AlgorithmParameters.AllowWalkLinks = baseTestSet.LinkType > 0;
            AlgorithmParameters.ChangeNumber = baseTestSet.ChangeNumber; ///2;
            AlgorithmParameters.LinkType = baseTestSet.LinkType;  /// 1

            if (BusStopList == null && BusLinesList == null)
            {
                if (baseTestSet.LoadDataTypeEnum == LoadDataTypeEnum.MyBook)
                {
                    BusStopList = TableData.GeneerateBookExampleBusStopList();
                    BusLinesList = TableData.GeneerateBookExampleBusLinesList();

                    //var tMatrixFilePath = FileNameHelper.GetMatrixWithPowerFilePath(MatrixTypeEnum.QMatrixTest, AlgorithmParameters.ChangeNumber, AlgorithmParameters.MaxWalkTime);
                    //TestQMatrix = DataService.LoadMatrixFromFile(tMatrixFilePath);

                    //var dMatrixFilePath = FileNameHelper.GetMatrixFilePath(MatrixTypeEnum.DMatrixTest, AlgorithmParameters.MaxWalkTime);
                    //TestDMatrix = DataService.LoadMatrixFromFile(dMatrixFilePath);
                }
                else if (baseTestSet.LoadDataTypeEnum == LoadDataTypeEnum.RealExamples)
                {

                    this.BusLinesList = DataService.GetBusLines();
                    this.BusStopList = DataService.GetXmlBusStopList();

                    var tMatrixFilePath = FileNameHelper.GetMatrixWithPowerFilePath(MatrixTypeEnum.QMatrix, AlgorithmParameters.ChangeNumber, AlgorithmParameters.MaxWalkTime);
                    TestQMatrix = DataService.LoadMatrixFromFile(tMatrixFilePath);

                    var dMatrixFilePath = FileNameHelper.GetMatrixFilePath(MatrixTypeEnum.DMatrix, AlgorithmParameters.MaxWalkTime);
                    TestDMatrix = DataService.LoadMatrixFromFile(dMatrixFilePath);
                }
            }

            BusGraph = new BusGraph(BusStopList, BusLinesList, AlgorithmParameters.MaxWalkTime);

            AlgorithmParameters.Q = TestQMatrix;
            AlgorithmParameters.D = TestDMatrix;

            AlgorithmParameters.BusStopList = BusStopList;
            AlgorithmParameters.BusLines = BusLinesList;
            AlgorithmParameters.BusGraph = BusGraph;
        }

        public BaseGeneratorTests()
        {

            log4net.Config.XmlConfigurator.Configure(new FileInfo("Log4Net.config"));

            LogTo.Info("Uruchomiono aplikacje");

            new AutoMapperInit().Init();
        }




    }
}





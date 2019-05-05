using Anotar.Log4Net;
using Magisterka.Infrastructure.Shared.Enum;
using Magisterka.Infrastructure.Shared.Extensions;
using Magisterka.Infrastructure.Shared.Structure;
using Magisterka.Modules.Main.Algoritms.PopulationGenerators;
using Magisterka.Modules.Main.Builders;
using Magisterka.Tests.Theory;
using System;
using System.Diagnostics;
using Xunit;
using MoreLinq;
using System.Linq;
using Magisterka.Infrastructure.Shared.Generic;
using QuickGraph.Algorithms.Search;
using QuickGraph.Algorithms.Observers;

namespace Magisterka.Tests.Paths
{
    /// <summary>
    /// Testy dla Generowanie populacji z macierz minimalnej liczby przesiadek
    /// </summary>
    public class MinimalDistanceGeneratorTest : BaseGeneratorTests
    {
        protected Stopwatch Timer = new Stopwatch();

        protected MinimalDistanceMatrixGenerator Generator { get; set; }

        public MinimalDistanceGeneratorTest()
        {

        }

        protected override void InitializeData(BasePathTestSet baseTestSet)
        {
            base.InitializeData(baseTestSet);

            var populationGeneratorBuilder = new PopulationGeneratorBuilder(AlgorithmParameters);

            Generator = populationGeneratorBuilder
                 .GetPopulationGenerator(PopulationGeneratorTypeEnum.MinimalDistanceMatrixGenerator)
                 as MinimalDistanceMatrixGenerator;
        }


        /// <summary>
        /// start 10 stop 9, wielkość populacji 6
        /// </summary>

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetMinimalDistanceTestSet),
            MemberType = typeof(TestDataGenerator))]
        public void TestGeneration(BasePathTestSet testData)
        {
            InitializeData(testData);

            Timer.Reset();
            Timer.Start();

            var population = Generator.Generate();

            LogTo.Info("Liczebność populacji {0}", population.Count);

            population.ForEach(x => x.LogRoute());
            Timer.Stop();

            LogTo.Info("test time {0}", Timer.Elapsed.TotalSeconds);

            //          Assert.Equal(true, false);
            //  UpdateAlgorithmParameters();

            //  DateTime startTime = DateTime.Now;

            ///    PopulationGenerator = GetPopulationGenerator();

            //AlgorithmParameters.Population = Population;

            //DateTime endTime = DateTime.Now;
            //TimeSpan time = endTime - startTime;
            //MessageBox.Show(time.ToString());

            //UpdatePopulation(Population, true);

            /// List<EntireRoute> result = EaAlgorithm.StartAlgoritm();

            //    UpdatePopulation(result);
            //}
            //catch (Exception ex)
            //{
            //    log.Error("BAlgorithmStart_Click", ex);
            //    MessageBox.Show(ex.Message);
            //}
        }



        [Theory]
        [MemberData(nameof(TestDataGenerator.RealDataSpeedTest),
            MemberType = typeof(TestDataGenerator))]
        public void RealSpeedTest(BasePathTestSet testData)
        {
            InitializeData(testData);

            var population = Generator.Generate();

            //.. var counter = 1
            //population.ForEach(x =>
            //{
            //    x.LogRoute(counter++.ToString());
            //    Assert.False(x.CheckIfHaveDuplicateBusStopsOneAfterAnother());
            //});
        }







        [Theory]
        [MemberData(nameof(TestDataGenerator.RealDataSpeedTest),
            MemberType = typeof(TestDataGenerator))]
        public void CheckForDuplicates(BasePathTestSet testData)
        {
            InitializeData(testData);

        //    var topVertexNode = Generator.GetTopNodeVertex();


            //topVertexNode.TraverseAction(graphEdge => {
            //    var countby = graphEdge.GraphEdges.CountBy(x => x.Target.BusStop.Id);

            //    var duplicateExist = countby.Any(x => x.Value > 1);

            //    Assert.False(duplicateExist);
            //});

            //.. var counter = 1
            //population.ForEach(x =>
            //{
            //    x.LogRoute(counter++.ToString());
            //    Assert.False(x.CheckIfHaveDuplicateBusStopsOneAfterAnother());
            //});
        }

        [Fact]
        public void TestLine116()
        {
            InitializeData(new BasePathTestSet
            {
                ChangeNumber = 5,
                EndBusId = 23730,
                LinkType = 0,
                LoadDataTypeEnum = LoadDataTypeEnum.RealExamples,
                StartBusId = 20921
            });

            var line116 = new int[] { 20921, 124201, 24303, 24305, 24114, 24112, 23701, 23704, 23713, 23709, 23715, 23717, 23727, 23719, 23721, 23723, 23730 };

            var busStopToCalculate = BusStopList.FirstOrDefault(x => x.Id == 20921);
            var busStops = BusStopList.Where(x => line116.Contains(x.Id));

            var cartesianBusStops = busStops.Cartesian(busStops, (first, second) => new GenericPair<BusStop>(first, second));

            BusGraph = new BusGraph(BusStopList, BusLinesList, 0);


            var searchAlgorithm = new BreadthFirstSearchAlgorithm<GraphNode, GraphEdge>(BusGraph);
            var distanceRecorderObserver = new VertexDistanceRecorderObserver<GraphNode, GraphEdge>(edge => 1);

            GraphNode StartGraphNode;

            using (distanceRecorderObserver.Attach(searchAlgorithm))
            {
                StartGraphNode = BusGraph.GetGraphNode(busStopToCalculate);


                searchAlgorithm.Compute(StartGraphNode);
            }



            foreach (var verticle in BusGraph.Vertices)
            {
                if (busStopToCalculate.ArrayNumber == verticle.BusStop.ArrayNumber)
                {
                  //  matrix[busStopToCalculate.ArrayNumber, verticle.BusStop.ArrayNumber] = 0;
                }
                else if (distanceRecorderObserver.Distances.ContainsKey(verticle))
                {
                    var edgeCount = distanceRecorderObserver.Distances[verticle];

                    ///matrix[busStopToCalculate.ArrayNumber, verticle.BusStop.ArrayNumber] = (int)edgeCount;
                }
                else
                {
                   /// matrix[busStopToCalculate.ArrayNumber, verticle.BusStop.ArrayNumber] = GraphConsts.MaxValue;
                }

            }


            //foreach (var busStopPair in cartesianBusStops)
            //{

            //    var conectedByBusLine = BusLinesList.Any(x => x.BusStopsAreConnected(busStopPair.First, busStopPair.Second));

            //    if (conectedByBusLine == true)
            //    {
            //        LogTo.Debug("{0} {1} connected", busStopPair.First.Id, busStopPair.Second.Id);
            //    }
            //    else
            //    {
            //        LogTo.Error("{0} {1} not connected", busStopPair.First.Id, busStopPair.Second.Id);
            //    }
            //}

            Assert.True(true);
        }

    }
}
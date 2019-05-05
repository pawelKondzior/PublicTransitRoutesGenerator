using Anotar.Log4Net;
using Magisterka.Infrastructure.Shared.Enum;
using Magisterka.Infrastructure.Shared.Extensions;
using Magisterka.Infrastructure.Shared.Generic;
using Magisterka.Infrastructure.Shared.Structure;
using Magisterka.Modules.Main.Algoritms.Matrix;
using Magisterka.Modules.Main.Algoritms.PopulationGenerators;
using Magisterka.Modules.Main.Builders;
using Magisterka.Tests.Theory;
using MoreLinq;
using System.Linq;
using Xunit;

namespace Magisterka.Tests.Paths
{
    public class BaseArrayGeneratorTest : BaseGeneratorTests
    {
        protected BaseArrayGenerator BaseArrayGenerator { get; set; }

        protected MatrixQGenerator ChangeStackGenerator { get; set;  }


        public BaseArrayGeneratorTest()
        {
        }

        protected override void InitializeData(BasePathTestSet baseTestSet)
        {
            base.InitializeData(baseTestSet);

            var populationGeneratorBuilder = new PopulationGeneratorBuilder(AlgorithmParameters);

            BaseArrayGenerator = populationGeneratorBuilder.GetPopulationGenerator(PopulationGeneratorTypeEnum.ArrayTreeMatrixGenerator) as BaseArrayGenerator;
            ChangeStackGenerator = new MatrixQGenerator(AlgorithmParameters);
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetChangeStackTestSet),
            MemberType = typeof(TestDataGenerator))]
        public void GenerateChangeStack(ChangeStackTestSet testData)
        {
            InitializeData(testData);

            var changeStack = ChangeStackGenerator.Generate();

            changeStack.LogJustBusStopNumbers();
            var allSequences = changeStack.Select(x => x.Select(z => z.Id).ToList());

            Assert.True(changeStack.Any());

            Assert.Equal(allSequences.Count(), testData.ChangeStocks.Count());

            foreach (var testSequence in testData.ChangeStocks)
            {
                Assert.True(allSequences.Any(x => x.SequenceEqual(testSequence)));
            }
        }



        [Theory]
        [MemberData(nameof(TestDataGenerator.RealDataSpeedTest),
           MemberType = typeof(TestDataGenerator))]
        public void RealGenerateChangeStackSpeedTest(BasePathTestSet testData)
        {
            InitializeData(testData);

            var changeStack = ChangeStackGenerator.Generate();
        }


        [Theory]
        [MemberData(nameof(TestDataGenerator.ProperChangeStackLengthTest),
           MemberType = typeof(TestDataGenerator))]
        public void ProperChangeStackLengthTest(BasePathTestSet testData)
        {
            InitializeData(testData);

            var changeStack = ChangeStackGenerator.Generate();



        }



        [Theory]
        [MemberData(nameof(TestDataGenerator.RealDataSpeedTest),
            MemberType = typeof(TestDataGenerator))]
        public void RealSpeedTest(BasePathTestSet testData)
        {
            InitializeData(testData);

            var population = BaseArrayGenerator.Generate();

           //.. var counter = 1
            //population.ForEach(x =>
            //{
            //    x.LogRoute(counter++.ToString());
            //    Assert.False(x.CheckIfHaveDuplicateBusStopsOneAfterAnother());
            //});
        }


        [Theory]
        [MemberData(nameof(TestDataGenerator.GetChangeStackTestSet),
            MemberType = typeof(TestDataGenerator))]
        public void TestGeneration(ChangeStackTestSet testData)
        {
            InitializeData(testData);

            var population = BaseArrayGenerator.Generate();

            var counter = 1;
            population.ForEach(x =>
            {
                x.LogRoute(counter++.ToString());
                Assert.False(x.CheckIfHaveDuplicateBusStopsOneAfterAnother());
            });

            //          Assert.Equal(true, false);
            //  UpdateAlgorithmParameters();

            //  DateTime startTime = DateTime.Now;

            ///    PopulationGenerator = GetPopulationGenerator
            ///
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

            var busStops = BusStopList.Where(x => line116.Contains(x.Id));

            var cartesianBusStops = busStops.Cartesian(busStops, (first, second) => new GenericPair<BusStop>(first, second));


            foreach (var busStopPair in cartesianBusStops)
            {

                var conectedByBusLine = BusLinesList.Any(x => x.BusStopsAreConnected(busStopPair.First, busStopPair.Second));

                if (conectedByBusLine == true)
                {
                    LogTo.Debug("{0} {1} connected", busStopPair.First.Id, busStopPair.Second.Id);
                }
                else
                {
                    LogTo.Error("{0} {1} not connected", busStopPair.First.Id, busStopPair.Second.Id);
                }
            }

            Assert.True(true);
        }
    }
}
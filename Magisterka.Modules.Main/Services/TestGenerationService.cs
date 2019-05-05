using Anotar.Log4Net;
using Magisterka.Data.Access.PP;
using Magisterka.Infrastructure.Shared.Collections;
using Magisterka.Infrastructure.Shared.Enum;
using Magisterka.Infrastructure.Shared.Helpers;
using Magisterka.Infrastructure.Shared.Structure;
using Magisterka.Modules.Main.Algoritms;
using Magisterka.Modules.Main.Algoritms.EA;
using Magisterka.Modules.Main.Algoritms.PopulationGenerators;
using Magisterka.Modules.Main.Builders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magisterka.Modules.Main.Services
{
    public class TestGenerationService
    {
        private StorageService StorageService { get; set; }
        private DataService DataService = DataService.DataServiceInstance;

        public BusLinesList BusLinesList { get; set; }


        public BusStopList BusStopList { get; set; }

        public Guid MyGuid { get; set; }


        public Dictionary<int, SquareList> SquareListDictionary { get; set; }
        


   //     public Dictionary<string, SquareList> SelectedSquareListDictionary { get; set; }


        public TestGenerationService()
        {
            /// DataService = new DataService();
            StorageService = new StorageService();
            LoadData();
            MyGuid = Guid.NewGuid();
            SquareListDictionary = new Dictionary<int, SquareList>();
        }





        #region Data


        private void LoadData()
        {
            BusLinesList = DataService.GetBusLines();
            BusStopList = DataService.GetXmlBusStopList();
            ///BusStopList = DataService.GetCombinedTextBusStops();
        }
        #endregion


        private SquareList GetSelectedSquereList(AlgorithmParameters algorithmParameters)
        {
             var squereList = GetSquereListCachad(algorithmParameters);


            var squareHelper = new SquareListHelper(squereList);
            return squareHelper.GetSelectedSquareList(algorithmParameters);
        }

        public SquareList GetSquereListCachad(AlgorithmParameters algorithmParameters)
        {

            if (SquareListDictionary.ContainsKey(algorithmParameters.NumberOfSquares))
            {
                return SquareListDictionary[algorithmParameters.NumberOfSquares];
            }

            var squareList = CreateSquereList(algorithmParameters);

            SquareListDictionary.Add(algorithmParameters.NumberOfSquares, squareList);

            return squareList;
        }


 



        



        public SquareList CreateSquereList(AlgorithmParameters algorithmParameters)
        {
            var squereList = this.BusStopList.GetEquallyDividedSquares(algorithmParameters.NumberOfSquares);
            squereList.CompleteInnerBusStopsForAllSquares(this.BusStopList);

            return squereList;
        }

        private AlgorithmParameters GenerateAlgorithmParametersFromParms(TestToBeDone parameters)
        {
            var algorithmParameters = new AlgorithmParameters();
            algorithmParameters.SetDefaultValues();


            //algorithmParameters.TestAlgorithmTypeEnum { get; set; }
            algorithmParameters.AdaptationFunctionTypeEnum = (AdaptationFunctionTypeEnum)parameters.AdaptationFunctionTypeEnum;
            algorithmParameters.ChangeNumber = parameters.ChangeNumber;
            ///algorithmParameters.DayTypeEnum = parameters.AdaptationFunctionTypeEnum;
            algorithmParameters.LinkType = parameters.LinkType;
            algorithmParameters.PopulationCount = parameters.PopulationCount;


            if (parameters.MutationProbability.HasValue)
            {
                algorithmParameters.MutationProbability = parameters.MutationProbability.Value;
            }

            if (parameters.NumberOfEvaluation.HasValue)
            {
                algorithmParameters.NumberOfEvaluation = parameters.NumberOfEvaluation.Value;
            }

            if (parameters.NumberOfSquares.HasValue)
            {
                algorithmParameters.NumberOfSquares = parameters.NumberOfSquares.Value;
            }

            if (parameters.NumberOfNeighborSquares.HasValue)
            {
                algorithmParameters.NumberOfNeighborSquares = parameters.NumberOfNeighborSquares.Value;
            }



            algorithmParameters.Start = BusStopList.FirstOrDefault(x => x.Id == parameters.StartId);
            algorithmParameters.End = BusStopList.FirstOrDefault(x => x.Id == parameters.StopId);

            algorithmParameters.BusStopList = BusStopList;
            algorithmParameters.BusLines = BusLinesList;


            return algorithmParameters;
        }


        //public BasePopulationGenerator GetEaGenerator(AlgorithmParameters algorithmParameters, TestToBeDone parameters)
        //{

        //    //    BasePopulationGenerator basePopulationGenerator = null;
        //    PopulationGeneratorBuilder populationGeneratorBuilder = null;

        //    /// var selectedItem = 
        //    var testType = (TestAlgorithmTypeEnum)parameters.TestAlgorithmTypeEnum;

        //    switch (testType)
        //    {
        //        case TestAlgorithmTypeEnum.ArrayGenerator:
        //            {
        //                populationGeneratorBuilder = new PopulationGeneratorBuilder(algorithmParameters,
        //                    PopulationGeneratorTypeEnum.ArrayGenerator); //, SelectedSquares);
        //                break;
        //            }
        //        case TestAlgorithmTypeEnum.MinimalDistanceMatrixGenerator:
        //            {
        //                populationGeneratorBuilder = new PopulationGeneratorBuilder(algorithmParameters,
        //                    PopulationGeneratorTypeEnum.MinimalDistanceMatrixGenerator); //, SelectedSquares);
        //                break;
        //            }
        //        case TestAlgorithmTypeEnum.ArrayGeographicGenerator:
        //            {
        //                populationGeneratorBuilder = new PopulationGeneratorBuilder(algorithmParameters,
        //                    PopulationGeneratorTypeEnum.ArrayGeographicGenerator,
        //                    GetSquereListCachad(algorithmParameters));
        //                break;
        //            }

        //        case TestAlgorithmTypeEnum.MinimalDistanceGeographicGenerator:
        //            {
        //                populationGeneratorBuilder = new PopulationGeneratorBuilder(algorithmParameters,
        //                    PopulationGeneratorTypeEnum.MinimalDistanceGeographicGenerator,
        //                    GetSquereListCachad(algorithmParameters));
        //                break;
        //            }
        //        case TestAlgorithmTypeEnum.PopulationWithFewestBusChange:
        //        case TestAlgorithmTypeEnum.PopulationWithFewestBusChangeGeo:
        //        case TestAlgorithmTypeEnum.PopulationWithMinimalDistanceMatrixGenerator:
        //        case TestAlgorithmTypeEnum.PopulationWithMinimalDistanceMatrixGeneratorGeo:
        //        case TestAlgorithmTypeEnum.Mixed:
        //        case TestAlgorithmTypeEnum.MixedGeo:
        //            break;
        //        default:
        //            break;
        //    }

        //    if (populationGeneratorBuilder != null)
        //    {

        //        return populationGeneratorBuilder.GetPopulationGenerator();
        //    }

        //    return null;
        //}


        public (BasePopulationGenerator, BaseEaAlgorithm) GetPopulationGenerator(AlgorithmParameters algorithmParameters, TestToBeDone parameters)
        {

        //    BasePopulationGenerator basePopulationGenerator = null;
            PopulationGeneratorBuilder populationGeneratorBuilder = null;
            EvolutionAlgorithmBuilder evolutionAlgorithmBuilder = null;
            /// var selectedItem = 
            var testType = (TestAlgorithmTypeEnum)parameters.TestAlgorithmTypeEnum;

            switch (testType)
            {
                case TestAlgorithmTypeEnum.ArrayGenerator:
                    {
                        populationGeneratorBuilder = new PopulationGeneratorBuilder(algorithmParameters, 
                            PopulationGeneratorTypeEnum.ArrayTreeMatrixGenerator); //, SelectedSquares);
                        break;
                    }

                case TestAlgorithmTypeEnum.MinimalDistanceMatrixGenerator:
                    {
                        populationGeneratorBuilder = new PopulationGeneratorBuilder(algorithmParameters,
                            PopulationGeneratorTypeEnum.MinimalDistanceMatrixGenerator); //, SelectedSquares);
                        break;
                    }
                case TestAlgorithmTypeEnum.ArrayGeographicGenerator:
                    {
                        populationGeneratorBuilder = new PopulationGeneratorBuilder(algorithmParameters,
                            PopulationGeneratorTypeEnum.ArrayGeographicGenerator,
                            GetSelectedSquereList(algorithmParameters));
                        break;
                    }

                case TestAlgorithmTypeEnum.MinimalDistanceGeographicGenerator:
                    {
                        populationGeneratorBuilder = new PopulationGeneratorBuilder(algorithmParameters,
                            PopulationGeneratorTypeEnum.MinimalDistanceGeographicGenerator,
                            GetSelectedSquereList(algorithmParameters));
                        break;
                    }

                //// GA
                case TestAlgorithmTypeEnum.PopulationWithFewestBusChange:
                    {
                        evolutionAlgorithmBuilder = new EvolutionAlgorithmBuilder(algorithmParameters,
                            EvolutionAlgorithmTypeEnum.PopulationWithFewestBusChange, null);
                            
                        break;
                    }
                case TestAlgorithmTypeEnum.PopulationWithFewestBusChangeGeo:
                    {
                        evolutionAlgorithmBuilder = new EvolutionAlgorithmBuilder(algorithmParameters,
                            EvolutionAlgorithmTypeEnum.PopulationWithFewestBusChangeGeo,
                            GetSelectedSquereList(algorithmParameters));
                        break;
                    }
                case TestAlgorithmTypeEnum.PopulationWithMinimalDistanceMatrixGenerator:
                    {
                        evolutionAlgorithmBuilder = new EvolutionAlgorithmBuilder(algorithmParameters,
                            EvolutionAlgorithmTypeEnum.PopulationWithMinimalDistanceMatrixGenerator, null);
                        break;
                    }
                case TestAlgorithmTypeEnum.PopulationWithMinimalDistanceMatrixGeneratorGeo:
                    {
                        evolutionAlgorithmBuilder = new EvolutionAlgorithmBuilder(algorithmParameters,
                            EvolutionAlgorithmTypeEnum.PopulationWithMinimalDistanceMatrixGeneratorGeo,
                            GetSelectedSquereList(algorithmParameters));
                        break;
                    }
                case TestAlgorithmTypeEnum.Mixed:
                    {
                        evolutionAlgorithmBuilder = new EvolutionAlgorithmBuilder(algorithmParameters,
                            EvolutionAlgorithmTypeEnum.Mixed, null);
                        break;
                    }
                case TestAlgorithmTypeEnum.MixedGeo:
                    {
                        evolutionAlgorithmBuilder = new EvolutionAlgorithmBuilder(algorithmParameters,
                            EvolutionAlgorithmTypeEnum.MixedGeo,
                            GetSelectedSquereList(algorithmParameters));
                        break;
                    }
                default:
                    break;
            }

            if (populationGeneratorBuilder != null)
            {

                return (populationGeneratorBuilder.GetPopulationGenerator(), null);
            }
            else if (evolutionAlgorithmBuilder != null)
            {
                return (null, evolutionAlgorithmBuilder.GetEvolutionAlgorithm() as BaseEaAlgorithm);
            }

            return (null, null);
        }

        private void RunTest(TestToBeDone testToBeDone)
        {

            try
            {
              
                var algorithmParameters = GenerateAlgorithmParametersFromParms(testToBeDone);

                var (populationGenerator, eaGenerator) = GetPopulationGenerator(algorithmParameters, testToBeDone);

                Population population = null;

                Stopwatch sw = Stopwatch.StartNew();

                if (populationGenerator != null)
                {
                    population = populationGenerator.Generate();
                }
                else if (eaGenerator != null)
                {
                    population = eaGenerator.StartAlgoritm();
                }

                sw.Stop();

                StorageService.SaveResults(testToBeDone, population, sw.Elapsed);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error", ex.Message);
                LogTo.ErrorException("RunTests", ex);


                StorageService.SaveResultError(testToBeDone);
            }

            
        }



        public void RunTests()
        {
            LoadData();


            try
            {

                var listOfParameters = StorageService.GetAllParametersWithoutGuid();


                while (listOfParameters.Any())
                {
                    


                    var myparameter = listOfParameters.FirstOrDefault();

                    if (StorageService.LockParameter(myparameter.Id, MyGuid))
                    {


                        var testsToBeDone = StorageService.GetTestToBeDone(myparameter.Id, MyGuid);

                        while (testsToBeDone.Any())
                        {
                            Console.WriteLine("{0}  zestaw danych  {1}", MyGuid, testsToBeDone.Count);

                            testsToBeDone.ForEach(RunTest);
                            testsToBeDone = StorageService.GetTestToBeDone(myparameter.Id, MyGuid);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nie udalo sie zlockowac parametru");
                    }

                    listOfParameters = StorageService.GetAllParametersWithoutGuid();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error", ex.Message);
                LogTo.ErrorException("RunTests", ex);
                

            }




            ///storageService.GenerateAllBusStopCombination(BusStopList);
            //var testsToBeDone = storageService.GetTestToBeDone();

            //while (testsToBeDone.Any())
            //{
            //    testsToBeDone.ForEach(RunTest);
            //    testsToBeDone = storageService.GetTestToBeDone();
            //}




        }




    }
}

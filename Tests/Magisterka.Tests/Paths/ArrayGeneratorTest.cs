using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Net;
using Castle.Components.DictionaryAdapter;
using Magisterka.Infrastructure.Shared.Collections;
using Magisterka.Infrastructure.Shared.Enum;
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
using Magisterka.Modules.Main.Builders;
using Magisterka.Infrastructure.Shared.Extensions;

namespace Magisterka.Tests.Paths
{
  
    public  class ArrayGeneratorTest : BaseGeneratorTests
    {



        //public PartOfTheRoute AlgorithmStartPoint { get; set; }

        //public PartOfTheRoute AlgorithmEndPoint { get; set; }
   //     protected ArrayGenerator ArrayGenerator { get; set; }

        public ArrayGeneratorTest()
        {

            var populationGeneratorBuilder = new PopulationGeneratorBuilder(AlgorithmParameters);

   //         ArrayGenerator = populationGeneratorBuilder.GetPopulationGenerator(PopulationGeneratorTypeEnum.ArrayTreeMatrixGenerator) as ArrayGenerator;
        }





        [Fact]
        public void TestGeneration()
        {

       //     var population = ArrayGenerator.Generate();

            //population.ForEach(x => x.LogRoute());


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
    }
}


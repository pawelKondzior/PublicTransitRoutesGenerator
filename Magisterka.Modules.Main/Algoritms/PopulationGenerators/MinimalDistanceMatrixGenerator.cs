using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Anotar.Log4Net;
using Magisterka.Infrastructure.Shared.Collections;
using Magisterka.Infrastructure.Shared.Enum;
using Magisterka.Infrastructure.Shared.Structure;
using QuickGraph.Algorithms;
using Magisterka.Infrastructure.Shared.Helpers;
using MoreLinq;
using Magisterka.Infrastructure.Shared.Comparers;
using Magisterka.Modules.Main.Algoritms.Matrix;

namespace Magisterka.Modules.Main.Algoritms.PopulationGenerators
{
    public class MinimalDistanceMatrixGenerator : BasePopulationGenerator
    {

        private int[,] D { get; set; }

        private BusStop Start { get; set; }
        private BusStop End { get; set; }





        public MinimalDistanceMatrixGenerator(AlgorithmParameters baseAlgorithmParameters) // :this()
            : base(baseAlgorithmParameters)
        {
            // BusStopStack = new Stack<BusStop>();
            this.Population = new Population();

            Start = AlgorithmParameters.Start;
            End = AlgorithmParameters.End;

            GeneratorConnectionTypeEnum = GeneratorConnectionTypeEnum.ConnectDirectly;

            
        }


        public override Population Generate()
        {
            ClearValues();
            this.Population = new Population();

            var matrixGenerator = new MatrixDGenerator(AlgorithmParameters);

            var listOfBusPaths = matrixGenerator.Generate();

            if (listOfBusPaths != null)
            {
                this.Population = CreatePopulationFromGraph(listOfBusPaths);

                return base.Generate();
            }


            return new Population();
        }



        public Population CreatePopulationFromGraph(List<List<BusStop>> listOfBusPaths)
        {

            foreach (var listOfBusStops in listOfBusPaths)
            {
                GeneratePopulationFromListOfBusStops(listOfBusStops);
            }

            Population.OrderByBusStopConnection();

            return Population;
        }


    
    }
}
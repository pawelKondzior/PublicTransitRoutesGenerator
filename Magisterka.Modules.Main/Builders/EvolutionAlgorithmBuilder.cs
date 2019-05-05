// // -----------------------------------------------------------------------
// //  <copyright company="DevCore .NET">
// //      Copyright (c) DevCore.NET All rights reserved.
// //  </copyright>
// //  <author> Paweł Kondzior</author>
// // -----------------------------------------------------------------------

namespace Magisterka.Modules.Main.Builders
{
    using Magisterka.Infrastructure.Shared.Collections;
    using Magisterka.Infrastructure.Shared.Enum;
    using Magisterka.Infrastructure.Shared.Structure;
    using Magisterka.Modules.Main.Algoritms.EA;
    using Magisterka.Modules.Main.Algoritms.PopulationGenerators;
    using System;

    public class EvolutionAlgorithmBuilder : AlgorithmBuilder
    {
        private EvolutionAlgorithmTypeEnum EvolutionAlgorithmTypeEnum  { get; set; }

        private SquareList SelectedSquares { get; set; }

   //     private SquareList SquareList { get; set; }


        // private BasePopulationGenerator PopulationGenerator { get; set; }

        public EvolutionAlgorithmBuilder(AlgorithmParameters algorithmParameters,
                                         EvolutionAlgorithmTypeEnum evolutionAlgorithmTypeEnum,
                                         SquareList selectedSquares)
                                       //  SquareList squareList)
            : base(algorithmParameters)
        {
            EvolutionAlgorithmTypeEnum = evolutionAlgorithmTypeEnum;
            SelectedSquares = selectedSquares;
         //   SquareList = squareList;

            //var populationGeneratorBuilder = new PopulationGeneratorBuilder(algorithmParameters, 
            //    PopulationGeneratorTypeEnum.MyArrayGenerator, selectedSquares);
        }


        public BaseEaAlgorithm GetEvolutionAlgorithm()
        {
            BaseEaAlgorithm baseEaAlgorithm = null;

            switch (EvolutionAlgorithmTypeEnum)
            {
                //case EvolutionAlgorithmTypeEnum.Basic:
                //    {
                //        // baseEaAlgorithm = new BasicEaAlgorithm(AlgorithmParameters.CloneRequired(), populationGenerator);
                //        baseEaAlgorithm = new BasicEaAlgorithm(AlgorithmParameters.CloneRequired());
                //        break;
                //    }
                case EvolutionAlgorithmTypeEnum.PopulationWithFewestBusChange:
                    {
                        baseEaAlgorithm = new EePopulationWithFewestBusChange(AlgorithmParameters.CloneRequired());
                        break;
                    }
                case EvolutionAlgorithmTypeEnum.PopulationWithFewestBusChangeGeo:
                    {
                        baseEaAlgorithm = new GeoMutationEaAlgorithm(AlgorithmParameters.CloneRequired(), SelectedSquares);
                        break;
                    }

                case EvolutionAlgorithmTypeEnum.PopulationWithMinimalDistanceMatrixGenerator:
                    {
                        baseEaAlgorithm = new MinimalDistanceEA(AlgorithmParameters.CloneRequired());
                        break;
                    }
                case EvolutionAlgorithmTypeEnum.PopulationWithMinimalDistanceMatrixGeneratorGeo:
                    {
                       baseEaAlgorithm = new MinimalDistanceGeoEA(AlgorithmParameters.CloneRequired(), SelectedSquares);
                        break;
                    }
                case EvolutionAlgorithmTypeEnum.Mixed:
                    {
                        baseEaAlgorithm = new MixedEA(AlgorithmParameters.CloneRequired());
                        break;
                    }
                case EvolutionAlgorithmTypeEnum.MixedGeo:
                    {

                        baseEaAlgorithm = new MixedEAGeo(AlgorithmParameters.CloneRequired(), SelectedSquares);
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return baseEaAlgorithm;
        }
    }
}
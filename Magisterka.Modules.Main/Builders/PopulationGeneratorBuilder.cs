using Magisterka.Infrastructure.Shared.Collections;
// // -----------------------------------------------------------------------
// //  <copyright company="DevCore .NET">
// //      Copyright (c) DevCore.NET All rights reserved.
// //  </copyright>
// //  <author> Paweł Kondzior</author>
// // -----------------------------------------------------------------------
using Magisterka.Infrastructure.Shared.Enum;
using Magisterka.Infrastructure.Shared.Structure;
using Magisterka.Modules.Main.Algoritms.PopulationGenerators;
using System;
using Anotar.Log4Net;
using System.Linq;

namespace Magisterka.Modules.Main.Builders
{
    public class PopulationGeneratorBuilder : AlgorithmBuilder
    {
        private PopulationGeneratorTypeEnum? PopulationGeneratorType { get; set; }

        private SquareList SelectedSquares { get; set; }

        public PopulationGeneratorBuilder(AlgorithmParameters algorithmParameters, PopulationGeneratorTypeEnum populationGeneratorType, SquareList selectedSquares)
            : base(algorithmParameters)
        {
            PopulationGeneratorType = populationGeneratorType;
            SelectedSquares = selectedSquares;
        }

        public PopulationGeneratorBuilder(AlgorithmParameters algorithmParameters, PopulationGeneratorTypeEnum populationGeneratorType) 
            : base(algorithmParameters)
        {
            PopulationGeneratorType = populationGeneratorType;

            if (PopulationGeneratorType.Value == PopulationGeneratorTypeEnum.ArrayGeographicGenerator)
            {
                throw new ArgumentException("Musisz podać selectedSquares");
            }
        }

        public PopulationGeneratorBuilder(AlgorithmParameters algorithmParameters)
            : base(algorithmParameters)
        {
            
        }


        public BasePopulationGenerator GetPopulationGenerator()
        {
            if (!PopulationGeneratorType.HasValue)
            {
                LogTo.Error();


                throw new Exception("PopulationGeneratorTypeEnum == null");
            }

            var geographicGenerators = new PopulationGeneratorTypeEnum[] { PopulationGeneratorTypeEnum.MinimalDistanceGeographicGenerator, PopulationGeneratorTypeEnum.ArrayGeographicGenerator };


            

            if (geographicGenerators.Contains(PopulationGeneratorType.Value)  && SelectedSquares == null)
            {
                throw new ArgumentException("Musisz podać selectedSquares");
            }

            return this.GetPopulationGenerator(this.PopulationGeneratorType.Value);
        }

        public BasePopulationGenerator GetPopulationGenerator(PopulationGeneratorTypeEnum populationGeneratorTypeEnum)
        {
            BasePopulationGenerator basePopulationGenerator = null;

            switch (populationGeneratorTypeEnum)
            {
                //case PopulationGeneratorTypeEnum.OldArrayGenerator:
                //    break;
                //case PopulationGeneratorTypeEnum.OldManGenerator:
                 ///  break;
                //case PopulationGeneratorTypeEnum.NewArrayGenerator:
                //    {
                //        basePopulationGenerator = new ArrayGenerator(AlgorithmParameters.CloneRequired());
                //        break;
                //    }

                //case PopulationGeneratorTypeEnum.SimpleGeographicGenerator:
                //    {
                //        basePopulationGenerator = new SimpleGeographicGenerator(AlgorithmParameters.CloneRequired());
                //        break;
                //    }

                case PopulationGeneratorTypeEnum.ArrayGeographicGenerator:
                    {
                        basePopulationGenerator = new ArrayGeographicGenerator(
                            AlgorithmParameters.CloneRequired(), SelectedSquares);
                        break;
                    }

                case PopulationGeneratorTypeEnum.ArrayTreeMatrixGenerator:
                    {
                        basePopulationGenerator = new ArrayTreeMatrixGenerator(AlgorithmParameters.CloneRequired());
                        break;
                    }
                case PopulationGeneratorTypeEnum.ArrayRecursiveMatrixGenerator:
                    {
                        basePopulationGenerator = new ArrayRecursiveMatrixGenerator(AlgorithmParameters.CloneRequired());
                        break;
                    }
                case PopulationGeneratorTypeEnum.MinimalDistanceMatrixGenerator:
                    {
                        basePopulationGenerator = new MinimalDistanceMatrixGenerator(AlgorithmParameters.CloneRequired());
                        break;
                    }
                case PopulationGeneratorTypeEnum.MinimalDistanceGeographicGenerator:
                    {
                        basePopulationGenerator = new MinimalDistanceGeographicGenerator(AlgorithmParameters.CloneRequired(), SelectedSquares);
                        break;
                    }

                    

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return basePopulationGenerator;
        }
    }
}
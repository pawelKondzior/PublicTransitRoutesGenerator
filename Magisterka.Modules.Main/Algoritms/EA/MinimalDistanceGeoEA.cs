using Magisterka.Infrastructure.Shared.Collections;
using Magisterka.Infrastructure.Shared.Enum;
using Magisterka.Infrastructure.Shared.Structure;
using Magisterka.Modules.Main.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magisterka.Modules.Main.Algoritms.EA
{
    public class MinimalDistanceGeoEA : BaseEaAlgorithm
    {
        #region Constructors and Destructors

        private SquareList SquareList { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EePopulationWithFewestBusChange"/> class.
        /// </summary>
        /// <param name="baseAlgorithmParameters">
        /// The base algorithm parameters.
        /// </param>
        public MinimalDistanceGeoEA(AlgorithmParameters baseAlgorithmParameters, SquareList squareList)
            : base(baseAlgorithmParameters)
        {
            SquareList = squareList;


            var basicPopulationGeneratorBuilder = new PopulationGeneratorBuilder(AlgorithmParameters, PopulationGeneratorTypeEnum.MinimalDistanceGeographicGenerator, squareList);


            var mutationParameters = AlgorithmParameters.CloneRequired();
            mutationParameters.LinkType = 0;
            var mutationPopulationGenerator = new PopulationGeneratorBuilder(mutationParameters, PopulationGeneratorTypeEnum.MinimalDistanceGeographicGenerator, squareList);


            BasicPopulationGenerators.Add(basicPopulationGeneratorBuilder.GetPopulationGenerator());
            MutationPopulationGenerators.Add(mutationPopulationGenerator.GetPopulationGenerator());
        }

        #endregion

        #region Public Methods and Operators

        ///// <summary>
        ///// The evaluate.
        ///// </summary>
        ///// <exception cref="NotImplementedException">
        ///// </exception>
        //public override void Evaluate()
        //{

        //    base.Evaluate();

        //}

        #endregion
    }
}

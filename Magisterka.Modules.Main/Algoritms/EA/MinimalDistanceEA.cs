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
    public class MinimalDistanceEA : BaseEaAlgorithm
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EePopulationWithFewestBusChange"/> class.
        /// </summary>
        /// <param name="baseAlgorithmParameters">
        /// The base algorithm parameters.
        /// </param>
        public MinimalDistanceEA(AlgorithmParameters baseAlgorithmParameters)
            : base(baseAlgorithmParameters)
        {
            var basicPopulationGeneratorBuilder = new PopulationGeneratorBuilder(AlgorithmParameters, PopulationGeneratorTypeEnum.MinimalDistanceMatrixGenerator);


            var mutationParameters = AlgorithmParameters.CloneRequired();
            mutationParameters.LinkType = 0;
            var mutationPopulationGenerator = new PopulationGeneratorBuilder(mutationParameters, PopulationGeneratorTypeEnum.MinimalDistanceMatrixGenerator);


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

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BasicEaAlgorithm.cs" company="">
//   
// </copyright>
//  <author>Paweł Kondzior</author>
// <summary>
//   Podstawowy algorytm genetyczny
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Magisterka.Infrastructure.Shared.Enum;
using Magisterka.Modules.Main.Builders;

namespace Magisterka.Modules.Main.Algoritms.EA
{
    using Magisterka.Infrastructure.Shared.Structure;
    using Magisterka.Modules.Main.Algoritms.PopulationGenerators;

    /// <summary>
    /// Podstawowy algorytm genetyczny
    /// </summary>
    public class BasicEaAlgorithm : BaseEaAlgorithm
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicEaAlgorithm"/> class.
        /// </summary>
        /// <param name="baseAlgorithmParameters">
        /// The base algorithm parameters.
        /// </param>
        //public BasicEaAlgorithm(AlgorithmParameters baseAlgorithmParameters, BasePopulationGenerator populationGenerator)
        //    : base(baseAlgorithmParameters)
        //{
        //    this.PopulationGenerator = populationGenerator;
        //}

        public BasicEaAlgorithm(AlgorithmParameters baseAlgorithmParameters)
            : base(baseAlgorithmParameters)
        {
             var generatorBuilder = new 
                 PopulationGeneratorBuilder(AlgorithmParameters, PopulationGeneratorTypeEnum.MyArrayGenerator);

            PopulationGenerator = generatorBuilder.GetPopulationGenerator();

          
        }




        public BasicEaAlgorithm(AlgorithmParameters baseAlgorithmParameters, Population population)
            :this(baseAlgorithmParameters)

        {
            Population = population;
        }


        #endregion
    }
}
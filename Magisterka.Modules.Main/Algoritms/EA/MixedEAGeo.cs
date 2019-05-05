// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EePopulationWithFewestBusChange.cs" company="">
//   
// </copyright>
//  <author>Paweł Kondzior</author>
// <summary>
//   A;gorytm o najmniejszej liczbie przesiadek
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Magisterka.Modules.Main.Algoritms.EA
{
    using System;

    using Magisterka.Infrastructure.Shared.Enum;
    using Magisterka.Infrastructure.Shared.Structure;
    using Magisterka.Modules.Main.Builders;
    using Magisterka.Infrastructure.Shared.Collections;

    /// <summary>
    /// A;gorytm o najmniejszej liczbie przesiadek
    /// </summary>
    public class MixedEAGeo : BaseEaAlgorithm
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EePopulationWithFewestBusChange"/> class.
        /// </summary>
        /// <param name="baseAlgorithmParameters">
        /// The base algorithm parameters.
        /// </param>
        public MixedEAGeo(AlgorithmParameters baseAlgorithmParameters, SquareList squareList)
            : base(baseAlgorithmParameters)
        {
            var fewestBustopsGeneratorBuilder = new PopulationGeneratorBuilder(AlgorithmParameters.CloneRequired(), PopulationGeneratorTypeEnum.ArrayGeographicGenerator, squareList);
            var minimalDistanceBuilder = new PopulationGeneratorBuilder(AlgorithmParameters.CloneRequired(), PopulationGeneratorTypeEnum.MinimalDistanceGeographicGenerator, squareList);

            BasicPopulationGenerators.Add(fewestBustopsGeneratorBuilder.GetPopulationGenerator());
            BasicPopulationGenerators.Add(minimalDistanceBuilder.GetPopulationGenerator());
         
            var mutationParameters = AlgorithmParameters.CloneRequired();
            mutationParameters.LinkType = 0;
            var minimalDistancemutationBuilder= new PopulationGeneratorBuilder(mutationParameters, PopulationGeneratorTypeEnum.MinimalDistanceGeographicGenerator, squareList);


            MutationPopulationGenerators.Add(fewestBustopsGeneratorBuilder.GetPopulationGenerator());
            MutationPopulationGenerators.Add(minimalDistancemutationBuilder.GetPopulationGenerator());
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
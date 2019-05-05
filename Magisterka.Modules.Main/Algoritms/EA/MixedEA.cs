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

    /// <summary>
    /// A;gorytm o najmniejszej liczbie przesiadek
    /// </summary>
    public class MixedEA : BaseEaAlgorithm
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EePopulationWithFewestBusChange"/> class.
        /// </summary>
        /// <param name="baseAlgorithmParameters">
        /// The base algorithm parameters.
        /// </param>
        public MixedEA(AlgorithmParameters baseAlgorithmParameters)
            : base(baseAlgorithmParameters)
        {
            var arrayGeneratorParameters = AlgorithmParameters.CloneRequired();
            var minimalDistanceParameters = AlgorithmParameters.CloneRequired();

        //    arrayGeneratorParameters.PopulationCount = AlgorithmParameters.PopulationCount / 2;
        ///    minimalDistanceParameters.PopulationCount = AlgorithmParameters.PopulationCount / 2;


            var fewestBustopsGeneratorBuilder = new PopulationGeneratorBuilder(arrayGeneratorParameters, PopulationGeneratorTypeEnum.ArrayTreeMatrixGenerator);
            var minimalDistanceBuilder = new PopulationGeneratorBuilder(minimalDistanceParameters, PopulationGeneratorTypeEnum.MinimalDistanceMatrixGenerator);

            BasicPopulationGenerators.Add(fewestBustopsGeneratorBuilder.GetPopulationGenerator());
            BasicPopulationGenerators.Add(minimalDistanceBuilder.GetPopulationGenerator());
         
            var mutationParameters = AlgorithmParameters.CloneRequired();

            mutationParameters.LinkType = 1;
            //if (AlgorithmParameters.LinkType > 0)
            //{
            //    mutationParameters.LinkType = AlgorithmParameters.LinkType-1 ;
            //}
            var minimalDistancemutationBuilder= new PopulationGeneratorBuilder(mutationParameters, PopulationGeneratorTypeEnum.MinimalDistanceMatrixGenerator);


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
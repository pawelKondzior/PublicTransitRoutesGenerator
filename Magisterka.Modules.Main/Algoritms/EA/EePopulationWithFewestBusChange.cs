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
    public class EePopulationWithFewestBusChange : BaseEaAlgorithm
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EePopulationWithFewestBusChange"/> class.
        /// </summary>
        /// <param name="baseAlgorithmParameters">
        /// The base algorithm parameters.
        /// </param>
        public EePopulationWithFewestBusChange(AlgorithmParameters baseAlgorithmParameters)
            : base(baseAlgorithmParameters)
        {
            var basicPopulationGeneratorBuilder = new PopulationGeneratorBuilder(AlgorithmParameters, PopulationGeneratorTypeEnum.ArrayTreeMatrixGenerator);


            BasicPopulationGenerators.Add(basicPopulationGeneratorBuilder.GetPopulationGenerator());
            MutationPopulationGenerators.Add(basicPopulationGeneratorBuilder.GetPopulationGenerator());
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
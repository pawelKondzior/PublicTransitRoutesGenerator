// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseAlgorithm.cs" company="">
//   
// </copyright>
//  <author>Paweł Kondzior</author>
// <summary>
//   The base algorithm.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Magisterka.Modules.Main.Algoritms
{
    using Magisterka.Infrastructure.Shared.Collections;
    using Magisterka.Infrastructure.Shared.Enum;
    using Magisterka.Infrastructure.Shared.Structure;

    /// <summary>
    /// The base algorithm.
    /// </summary>
    public abstract class BaseAlgorithm
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAlgorithm"/> class.
        /// </summary>
        public BaseAlgorithm()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAlgorithm"/> class.
        /// </summary>
        /// <param name="baseAlgorithmParameters">
        /// The base algorithm parameters.
        /// </param>
        public BaseAlgorithm(AlgorithmParameters baseAlgorithmParameters)
        {
            this.AlgorithmParameters = baseAlgorithmParameters; 
        }

        #endregion

        #region Public Properties


        public AlgorithmParameters AlgorithmParameters { get; set; }

        /// <summary>
        /// Gets or sets the population.
        /// </summary>
        protected Population Population { get; set; }

        #endregion
    }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MutationOperatorTypeEnum.cs" company="">
//   
// </copyright>
//  <author>Paweł Kondzior</author>
// <summary>
//   The mutation operator type enum.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Magisterka.Infrastructure.Shared.Enum
{
    /// <summary>
    /// The mutation operator type enum.
    /// </summary>
    public enum MutationOperatorTypeEnum : byte
    {
        /// <summary>
        /// Mutacja
        /// </summary>
        Mutation = 1, 

        /// <summary>
        /// Krzyżowanie
        /// </summary>
        Hybridization = 2
    }
}
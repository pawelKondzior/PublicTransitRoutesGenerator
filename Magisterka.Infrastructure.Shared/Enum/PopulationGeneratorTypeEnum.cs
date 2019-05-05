// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PopulationGeneratorTypeEnum.cs" company="">
//   
// </copyright>
//  <author>Paweł Kondzior</author>
// <summary>
//   The population generator type enum.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Magisterka.Infrastructure.Shared.Enum
{
    /// <summary>
    /// The population generator type enum.
    /// </summary>
    public enum PopulationGeneratorTypeEnum : byte
    {
        ///// <summary>
        ///// The old array generator.
        ///// </summary>
        //OldArrayGenerator = 0, 

        ///// <summary>
        ///// The old man generator.
        ///// </summary>
        //OldManGenerator = 1, 

        /// <summary>
        /// The new array generator.
        /// </summary>
    ///    NewArrayGenerator = 10, 

        /// <summary>
        /// The simple geographic generator.
        /// </summary>
    ///    SimpleGeographicGenerator = 15, 

        /// <summary>
        /// The array geographic generator.
        /// </summary>
        ArrayGeographicGenerator = 16,

        /// <summary>
        /// The my array generator.
        /// </summary>
        ArrayTreeMatrixGenerator = 17,

        ArrayRecursiveMatrixGenerator = 18,




        MinimalDistanceMatrixGenerator = 20,


        MinimalDistanceGeographicGenerator = 21,
    }
}
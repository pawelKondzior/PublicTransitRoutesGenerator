// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EvolutionAlgorithmTypeEnum.cs" company="">
//   
// </copyright>
//  <author>Paweł Kondzior</author>
// <summary>
//   The evolution algorithm type enum.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Magisterka.Infrastructure.Shared.Enum
{
    /// <summary>
    /// The evolution algorithm type enum.
    /// </summary>
    public enum EvolutionAlgorithmTypeEnum : byte
    {
        /// <summary>
        /// Podstawowy algorytm
        /// </summary>
    //    Basic = 1, 

        /// <summary>
        /// Populacja z najmniejszą liczba przesiadek
        /// </summary>
        PopulationWithFewestBusChange = 2,

        /// <summary>
        /// Przystanki do mutacji wybierane geograficznuie
        /// </summary>
        PopulationWithFewestBusChangeGeo = 3,


        PopulationWithMinimalDistanceMatrixGenerator = 4,


        PopulationWithMinimalDistanceMatrixGeneratorGeo = 5,


        Mixed = 6,

        MixedGeo = 7,

    }
}
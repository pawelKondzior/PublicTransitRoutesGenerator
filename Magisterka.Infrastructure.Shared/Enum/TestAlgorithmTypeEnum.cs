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
  
    public enum TestAlgorithmTypeEnum : byte
    {
        ArrayGeographicGenerator = 1,

    
        ArrayGenerator = 2,


        MinimalDistanceMatrixGenerator = 3,


        MinimalDistanceGeographicGenerator = 4,





        PopulationWithFewestBusChange = 10,

        /// <summary>
        /// Przystanki do mutacji wybierane geograficznuie
        /// </summary>
        PopulationWithFewestBusChangeGeo = 11,


        PopulationWithMinimalDistanceMatrixGenerator = 12,


        PopulationWithMinimalDistanceMatrixGeneratorGeo = 13,


        Mixed = 14,

        MixedGeo = 15
    }
}
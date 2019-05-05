// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StaticData.cs" company="">
//   
// </copyright>
//  <author>Paweł Kondzior</author>
// <summary>
//   The static data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Magisterka.Infrastructure.Shared.Consts
{
    using System.Collections.Generic;

    using Magisterka.Infrastructure.Shared.Enum;

    /// <summary>
    /// The static data.
    /// </summary>
    public static class StaticData
    {
        #region Static Fields

        /// <summary>
        /// The day type combo box content.
        /// </summary>
        public static readonly Dictionary<DayTypeEnum, string> DayTypeComboBoxContent =
            new Dictionary<DayTypeEnum, string>
                {
                    { DayTypeEnum.WorkingDay, "Dzień roboczy" }, 
                    { DayTypeEnum.Saturday, "Sobota" }, 
                    { DayTypeEnum.Sunday, "Niedziela" }, 
                };

        /// <summary>
        /// The evolution algorithm type combo box content.
        /// </summary>
        public static readonly Dictionary<EvolutionAlgorithmTypeEnum, string> EvolutionAlgorithmTypeComboBoxContent =
            new Dictionary<EvolutionAlgorithmTypeEnum, string>
                {
                    //{ EvolutionAlgorithmTypeEnum.Basic, "Podstawowy" }, 
                    {
                        EvolutionAlgorithmTypeEnum.PopulationWithFewestBusChange, 
                        "Z najmniejsza liczbą przesiadek"
                    }, 
                    {
                        EvolutionAlgorithmTypeEnum.PopulationWithFewestBusChangeGeo,
                        "Z najmniejsza liczbą przesiadek geograficzny"
                    },
                    {
                        EvolutionAlgorithmTypeEnum.PopulationWithMinimalDistanceMatrixGenerator,
                        "Minimalna liczba przystanków"
                    },
                 {
                        EvolutionAlgorithmTypeEnum.PopulationWithMinimalDistanceMatrixGeneratorGeo,
                        "Minimalna liczba przystanków - geo"
                    },
                  {
                        EvolutionAlgorithmTypeEnum.Mixed,
                        "Miesznany"
                    },
                   {
                        EvolutionAlgorithmTypeEnum.MixedGeo,
                        "Mieszany geograficzny"
                    }
                };

        /// <summary>
        /// The population generator combo box content.
        /// </summary>
        public static readonly Dictionary<PopulationGeneratorTypeEnum, string> PopulationGeneratorComboBoxContent =
            new Dictionary<PopulationGeneratorTypeEnum, string>
                {
                 ///   { PopulationGeneratorTypeEnum.OldArrayGenerator, "Stary Array Generator" }, 
                ///    { PopulationGeneratorTypeEnum.NewArrayGenerator, "Nowy Array Generator" }, 
                    ///{ PopulationGeneratorTypeEnum.OldManGenerator, "Stary  Men Generator" }, 
                  ///  { PopulationGeneratorTypeEnum.SimpleGeographicGenerator, "Prosty geograficzny" }, 
                    
                    { PopulationGeneratorTypeEnum.ArrayTreeMatrixGenerator, "Minimalna liczba przesiadek" },
                    { PopulationGeneratorTypeEnum.ArrayRecursiveMatrixGenerator, "Minimalna liczba przesiadek - rekursywne przszukiwanie macierzy" },
                    { PopulationGeneratorTypeEnum.ArrayGeographicGenerator, "Minimalna liczba przesiadek - gograficzny" },
                    { PopulationGeneratorTypeEnum.MinimalDistanceMatrixGenerator, "Minimalna liczba przystanków" },
                    { PopulationGeneratorTypeEnum.MinimalDistanceGeographicGenerator, "Minimalna liczba przystanków - geograficzny" },
                };

        #endregion
    }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AdaptationFunctionTypeEnum.cs" company="">
//   
// </copyright>
//  <author>Paweł Kondzior</author>
// <summary>
//   The adaptation function type enum.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Magisterka.Infrastructure.Shared.Enum
{
    /// <summary>
    /// The adaptation function type enum.
    /// </summary>
    public enum AdaptationFunctionTypeEnum : byte
    {
        /// <summary>
        /// Przez najmneijszy czas
        /// </summary>
        ByTime = 1, 

        /// <summary>
        /// Przez najmniejszą liczbe przystanków
        /// </summary>
        CalculateAdaptationFunctionByBusStops = 2, 

        /// <summary>
        /// Przez najmniejszą liczbe przesiadek
        /// </summary>
        ByFewestBusChanges = 3, 
    }
}
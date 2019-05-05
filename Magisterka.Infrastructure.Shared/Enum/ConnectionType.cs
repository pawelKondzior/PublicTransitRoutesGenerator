// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConnectionType.cs" company="">
//   
// </copyright>
//  <author>Paweł Kondzior</author>
// <summary>
//   The connection type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Magisterka.Infrastructure.Shared.Enum
{
    /// <summary>
    /// The connection type.
    /// </summary>
    public enum ConnectionType : byte
    {
        /// <summary>
        /// Polaczenie autobusowe
        /// </summary>
        Bus = 0, 

        /// <summary>
        /// Linia piesza
        /// </summary>
        Walk = 1,


        /// <summary>
        /// Dla połęczeń 
        /// </summary>
        Unknown = 10
    }
}
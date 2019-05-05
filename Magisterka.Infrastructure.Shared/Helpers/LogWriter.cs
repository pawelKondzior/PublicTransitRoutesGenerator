// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogWriter.cs" company="">
//   
// </copyright>
//  <author>Paweł Kondzior</author>
// <summary>
//   The log writer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Magisterka.Infrastructure.Shared.Helpers
{
    using System.Diagnostics;

    /// <summary>
    /// The log writer.
    /// </summary>
    public static class LogWriter
    {
        #region Public Methods and Operators

        /// <summary>
        /// The write line.
        /// </summary>
        /// <param name="str">
        /// The str.
        /// </param>
        public static void WriteLine(string str)
        {
            Debug.WriteLine(str);
        }

        #endregion
    }
}
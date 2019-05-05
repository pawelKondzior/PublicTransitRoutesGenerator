// -----------------------------------------------------------------------
//  <copyright file="ISubject.cs" company="DevCore .NET">
//      Copyright (c) DevCore.NET All rights reserved.
//  </copyright>
//  <author>Paweł Kondzior</author>
// -----------------------------------------------------------------------
namespace Magisterka.Infrastructure.Shared.Interfaces
{
    /// <summary>
    /// Interfejs określający osobnika w algorytmie genetycznym
    /// </summary>
    public interface ISubject
    {
        /// <summary>
        /// Wynik funkcji oceny
        /// </summary>
        double EvaluationFunctionResult { get; set; }

        /// <summary>
        /// Wynik funkcji przystosowania
        /// </summary>
        int AdaptationFunctionResult { get; set; }



    }
}
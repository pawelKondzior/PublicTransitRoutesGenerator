// -----------------------------------------------------------------------
//  <copyright file="DayTypeEnum.cs" company="DevCore .NET">
//      Copyright (c) DevCore.NET All rights reserved.
//  </copyright>
//  <author>Paweł Kondzior</author>
// -----------------------------------------------------------------------

using System.ComponentModel;

namespace Magisterka.Infrastructure.Shared.Enum
{
    public enum DayTypeEnum : byte
    {
        /// <summary>
        /// Dzien Roboczy
        /// </summary>
        [Description("W dni robocze")]
        WorkingDay = 0,

        /// <summary>
        /// Sobota
        /// </summary>
        [Description("Sobota")]
        Saturday = 1,

        /// <summary>
        /// Niedziela
        /// </summary>
        [Description("Niedziela")]
        Sunday = 2
    }
}
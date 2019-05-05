// -----------------------------------------------------------------------
//  <copyright file="TimeDto.cs" company="DevCore .NET">
//      Copyright (c) DevCore.NET All rights reserved.
//  </copyright>
//  <author>Paweł Kondzior</author>
// -----------------------------------------------------------------------
namespace Magisterka.Infrastructure.Shared.IoDto
{
    public class TimeIoDto
    {
        #region Constructors

        public TimeIoDto()
        {
            
        }

        public TimeIoDto(string hour, string minute)
        {
            this.Hour = hour;
            this.Minute = minute;
        }


        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the hour.
        /// </summary>
        /// <value>The hour.</value>
        /// <remarks></remarks>
        public string Hour { get; set; }

        /// <summary>
        /// Gets or sets the minute.
        /// </summary>
        /// <value>The minute.</value>
        /// <remarks></remarks>
        public string Minute { get; set; }
        #endregion
    }
}
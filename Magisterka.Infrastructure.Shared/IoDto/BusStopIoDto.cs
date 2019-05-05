// -----------------------------------------------------------------------
//  <copyright file="BusStopDto.cs" company="DevCore .NET">
//      Copyright (c) DevCore.NET All rights reserved.
//  </copyright>
//  <author>Paweł Kondzior</author>
// -----------------------------------------------------------------------

using Magisterka.Infrastructure.Shared.Enum;

namespace Magisterka.Infrastructure.Shared.IoDto
{

    // przyklad w xml   <BusStop Id="24416" Name="BISKUPIN" Street="pętla" X="17.1091461" Y="51.10126" />

    /// <summary>
    /// Dto na przystanek autobustowy
    /// </summary>
    /// <remarks></remarks>
    public class BusStopIoDto
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        /// <remarks></remarks>
        public string Id { get; set; }

        

        public int ParsedId
        {
            get
            {
                int x = 0;
                if (int.TryParse(this.Id, out x))
                {
                    return x;
                }
                return 0;
            }
        }


        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        /// <remarks></remarks>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the street.
        /// </summary>
        /// <value>The street.</value>
        /// <remarks></remarks>
        public string Street { get; set; }


        /// <summary>
        /// Jescze nie wiem po co ale jest a pliku
        /// </summary>
        public string StopCode { get; set; }



        public BusStopType BusStopType  { get; set; }


        /// <summary>
        /// Gets or sets the X. stop_longtitude
        /// </summary>
        /// <value>The X.</value>
        /// <remarks></remarks>
        public string X { get; set; }

        public decimal ParsedX
        {
            get
            {
                decimal x = 0;

               X = this.X.Replace(',', '.');

              if (decimal.TryParse(this.X, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo, out x))
                {
                    return x;
                }
                return 0;
            }
        }

        /// <summary>
        /// Gets or sets the Y.   (LATIT)
        /// </summary>
        /// <value>The Y.</value>
        /// <remarks></remarks>
        public string Y { get; set; }

        public decimal ParsedY
        {
            get
            {
                decimal x = 0;

                Y = this.Y.Replace(',', '.');

                //  if (decimal.TryParse(this.Y.Replace(',', '.'), out x))
                if (decimal.TryParse(this.Y, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo, out x))
                {
                    return x;
                }
                return 0;
            }
        }



        
    }
}


// -----------------------------------------------------------------------
//  <copyright file="BusStopDto.cs" company="DevCore .NET">
//      Copyright (c) DevCore.NET All rights reserved.
//  </copyright>
//  <author>Paweł Kondzior</author>
// -----------------------------------------------------------------------

using log4net;
using Magisterka.Infrastructure.Shared.Basic;
using System.Xml.Serialization;

namespace Magisterka.Infrastructure.Shared.Structure
{

    // przyklad w xml   <BusStop Id="24416" Name="BISKUPIN" Street="pętla" X="17.1091461" Y="51.10126" />

    /// <summary>
    /// Dto na przystanek autobustowy
    /// </summary>
    public class BusStop : SinglePoint
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        /// <remarks></remarks>
        public int Id { get; set; }

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
        /// Kolejność zczytania z pliku, potrzbne do algorytmu tablicowego
        /// </summary>
        [XmlIgnore]
        public int ArrayNumber { get; set; }

        /// <summary>
        /// Jescze nie wiem po co ale jest a pliku
        /// </summary>
        public int StopCode { get; set; }


        public string NameAndStreet
        {
            get
            {
                return string.Format("{0} - {1}", Name, Street);
            }
        }

        public override void LogItem(ILog log)
        {
            log.Info(string.Format("BusStop Id {0) Name {1} Street {2} ArrayNumber {3}", Id, Name, Street, ArrayNumber));
        }


        /// <summary>
        /// Oznacz, ze nie było go w danych z otwarty wrocław :(
        /// Wiec musialem dodac sam
        /// </summary>
        [XmlIgnore]
        public bool NotExistingInBusList { get; set; }






        
    }
}
// -----------------------------------------------------------------------
//  <copyright file="BusStopRepository.cs" company="DevCore .NET">
//      Copyright (c) DevCore.NET All rights reserved.
//  </copyright>
//  <author>Paweł Kondzior</author>
// -----------------------------------------------------------------------

using System;

using System.IO;
using Magisterka.Infrastructure.Shared.Collections;
using Magisterka.Infrastructure.Shared.Enum;
using Magisterka.Infrastructure.Shared.IoDto;

namespace Magisterka.Data.Access.Repository
{
    using System.Collections.Generic;
    using Magisterka.Data.Access.Interfaces;
    using System.Linq;
    using Magisterka.Infrastructure.Shared.Structure;
    using System.Xml.Serialization;
    using Anotar.Log4Net;
    using System.Text;

    /// <summary>
    /// długość geograficzna, szerokość geograficzna, ID słupka, typie przystanku (tramwajowy - 0; autobusowy - 3, tramwajowo-autobusowy - 03)
    /// </summary>
    public class BusStopRepository //: XmlRepository, IRepository<BusStopIoDto>
    {

        private string Path { get; set; }

        #region Constructor


        public BusStopRepository(string path)
        {
            this.Path = path;

        }

        #endregion

        public void SaveNew(List<BusStop> list)
        {
            try
            {
                XmlHelper.SerializeToXmlFile(list, Path, false);
            }
            catch (Exception ex)
            {
                LogTo.ErrorException("xml load ", ex);

                throw;
            }
        }

        public List<BusStop> LoadAllNew( )
        {
            try
            {
                var list = XmlHelper.DeserializeFromXmlFile<List<BusStop>>(Path);

                return list;
            }
            catch (Exception ex)
            {
                LogTo.ErrorException("xml load ", ex);

                throw;
            }
        }


        public List<BusStopIoDto> ReadOldFile()
        {
            var results = new List<BusStopIoDto>();


            using (var fs = File.OpenRead(this.Path))
            using (var reader = new StreamReader(fs))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    var item = new BusStopIoDto();

                    item.X = values[0];
                    item.Y = values[1];
                    item.Id = values[2];
                    item.BusStopType = GetBusStopType(values[3]);

                    results.Add(item);
                    ;
                }
            }


            return results;
        }


        public List<BusStopIoDto> ReadNewFile()
        {
            var results = new List<BusStopIoDto>();
            var encoding = Encoding.GetEncoding("ISO-8859-2");

            using (var fs = File.OpenRead(this.Path))
            using (var reader = new StreamReader(fs))//, Encoding.UTF8))
            {
                reader.ReadLine(); ///   czyta pierwsza linie bo tam nic nie ma

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    line = line.Replace(@"\", "");
                    line = line.Replace("\"", "");


                    var values = line.Split(',');

                    var item = new BusStopIoDto();

                    item.Id = values[1];
                    
                    item.Name = values[2];

                    item.Y = values[3];
                    item.X = values[4];
                    
                    
                   /// item.BusStopType = GetBusStopType(values[3]);

                    results.Add(item);
                    ;
                }
            }


            return results;
        }


        public BusStopType GetBusStopType(string value)
        {

            var result = BusStopType.TramBus; ;

            BusStopType.TryParse(value, out result);

            return result;



        }
    }
}
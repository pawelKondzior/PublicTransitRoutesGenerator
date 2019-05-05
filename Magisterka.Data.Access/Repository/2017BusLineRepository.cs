using System.IO;
using System.Xml.Serialization;
using Anotar.Log4Net;
using AutoMapper;
using Magisterka.Infrastructure.Shared.Extensions;
using Magisterka.Infrastructure.Shared.IoDto2017;

namespace Magisterka.Data.Access.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;
    using Magisterka.Data.Access.Interfaces;
    using Magisterka.Infrastructure.Shared.IoDto;
    using Magisterka.Infrastructure.Shared.Enum;
    using Magisterka.Infrastructure.Shared.Structure;


    public class BusLineRepository //: IRepository<BusLinesList>
    {
        private string Path { get; set; }

        private BusLineLoadTypEnum BusLineLoadTypEnum { get; set; }

        #region Constructor

        public BusLineRepository(string path, BusLineLoadTypEnum busLineLoadTypEnum)
        {
            Path = path;
            BusLineLoadTypEnum = busLineLoadTypEnum;

        }

        #endregion

        public BusLinesList All()
        {
            var files = GetAllFileNames(Path);

            var list = new BusLinesList();

            XmlSerializer serializer = new XmlSerializer(typeof(linie));

            var encoding = Encoding.GetEncoding("ISO-8859-2");

            LogTo.Debug("XMl file list");

            foreach (var file in files)
            {
                LogTo.Debug(file);

                try
                {
                    
                    using (var reader = new StreamReader(file, encoding))
                    {
                        var item = (linie)serializer.Deserialize(reader);

                        MapLineToBusLineIoDto(list, item);

                    }
                }
                catch (Exception ex)
                {
                    LogTo.ErrorException("xml load ", ex);

                    throw;
                }

            }

            return list;
        }

        //configurationExpression.CreateMap<linieLinia, BusLineIoDto>()
        //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.nazwa)).AfterMap((src, dest) =>
        //    {
        //        var wariant = src.wariant.FirstOrDefault();

        //        foreach (var przystanekItem in wariant.przystanek)
        //        {
        //            var busStopItem = Mapper.Map<BusStopLineIoDto>(przystanekItem);

        //            dest.Add(busStopItem);
        //        }                  
        //    });


        private void MapLineToBusLineIoDto(BusLinesList list, linie item)
        {
            if (BusLineLoadTypEnum == BusLineLoadTypEnum.WithoutNightLines)
            {

                if (item.linia.typ != "Nocna autobusowa")
                {
                    foreach (var wariant in item.linia.wariant.Take(2))
                    {
                        var resultItem = new BusLineIoDto();
                        resultItem.Name = item.linia.nazwa;
                        resultItem.Variant = wariant.id;

                        foreach (var przystanekItem in wariant.przystanek)
                        {
                            var busStopItem = Mapper.Map<BusStopLineIoDto>(przystanekItem);

                            resultItem.Add(busStopItem);
                        }


                        list.Add(resultItem);
                    }




                    // var resultItem = Mapper.Map<linieLinia, BusLineIoDto>(item.linia);
                    // list.Add(resultItem);
                }
                else
                {
                    LogTo.Warn("Pomijam nocna linie");
                }
            }
            else
            {
                var resultItem = Mapper.Map<linieLinia, BusLineIoDto>(item.linia);
                list.Add(resultItem);
            }
        }

        private string[] GetAllFileNames(string path)
        {
            string[] files = Directory.GetFiles(path, "*.xml*", SearchOption.AllDirectories);

            return files;
        }
    }
}

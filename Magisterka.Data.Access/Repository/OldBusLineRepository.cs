using System.IO;
using Magisterka.Infrastructure.Shared.Extensions;

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


    public class OldBusLineRepository //: IRepository<BusLinesList>
    {
        private string Path { get; set; }

        #region Constructor

        public OldBusLineRepository(string path)
        {
            Path = path;
        }

        #endregion 
    
        public BusLinesList All()
        {
            var files = GetAllFileNames(Path);

            var list = new BusLinesList();  

            foreach (var file in files)
            {
                XDocument XDocument = XDocument.Load(file);

                BusLineIoDto busLineIoDto = new BusLineIoDto();

                var line = XDocument.Descendants("Line").FirstOrDefault();

                busLineIoDto.Variant = int.Parse(line.Attribute("Variant").Value);

                busLineIoDto.Name = line.Attribute("Name").Value;

                foreach (var xBusStopLine in line.Descendants("BusStopLine"))
                {
                    var busStop = new BusStopLineIoDto();
                    busStop.BusStopId = xBusStopLine.Attribute("BusStop").Value.ForceParseInt();
                   // busStop.BusStopId = xBusStopLine.Attribute("BusStop").Value;

                    busLineIoDto.Add(busStop);

                    foreach (var xSchedule in xBusStopLine.Descendants("Schedule"))
                    {
                        var dayTypeStr = xSchedule.Attribute("DayType").Value;

                        var dayTypeEnum = DayTypeEnum.WorkingDay;

                        if (dayTypeStr == "Sobota")
                        {
                            dayTypeEnum = DayTypeEnum.Saturday;
                        }
                        else if (dayTypeStr == "Niedziela")
                        {
                            dayTypeEnum = DayTypeEnum.Sunday;
                        }

                        foreach (var xHour in xSchedule.Descendants("Hour"))
                        {
                            int hour = int.Parse(xHour.Attribute("h").Value);
                            
                            foreach (var xMinute in xHour.Descendants("Minute"))
                            {
                                int minute = int.Parse(xMinute.Attribute("m").Value);

                                var time = new Time()
                                               {
                                                   Hour = hour,
                                                   Minute = minute
                                               };

                                busStop.AddTime(time, dayTypeEnum);
                            }
                        }
                    }
                }
                list.Add(busLineIoDto);
            }

            return list;
        }


        private string[] GetAllFileNames(string path)
        {
            string[] files = Directory.GetFiles(path, "*.xml*", SearchOption.TopDirectoryOnly);

            return files;
        }
    }
}

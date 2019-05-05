using System;
using System.Linq;
using AutoMapper;
using Magisterka.Infrastructure.Shared.Enum;
using Magisterka.Infrastructure.Shared.Extensions;
using Magisterka.Infrastructure.Shared.IoDto;
using Magisterka.Infrastructure.Shared.IoDto2017;
using Magisterka.Infrastructure.Shared.Structure;

namespace Magisterka.Modules.Main.Init
{
    public static class AutoMapperConfig
    {
        public static IMapperConfigurationExpression AddMappings(
            this IMapperConfigurationExpression configurationExpression)
        {
            //configurationExpression.CreateMap<Job, JobRow>()
            //    .ForMember(x => x.StartedOnDateTime, o => o.PreCondition(p => p.StartedOnDateTimeUtc.HasValue))
            //    .ForMember(x => x.StartedOnDateTime, o => o.MapFrom(p => p.StartedOnDateTimeUtc.Value.DateTime.ToLocalTime()))
            //    .ForMember(x => x.FinishedOnDateTime, o => o.PreCondition(p => p.FinishedOnDateTimeUtc.HasValue))
            //    .ForMember(x => x.FinishedOnDateTime, o => o.MapFrom(p => p.FinishedOnDateTimeUtc.Value.DateTime.ToLocalTime()));




            configurationExpression.CreateMap<linieLiniaWariantPrzystanek, BusStopLineIoDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.nazwa))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.ulica))
                .ForMember(dest => dest.BusStopId, opt => opt.MapFrom(src => src.id))
                //.ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.ulica))


                .AfterMap((src, dest) =>
                {
                    if (src.tabliczka != null)
                    {
                        foreach (var dzienItem in src.tabliczka.dzien)
                        {
                            var dayType = dzienItem.nazwa.GetDayTypeEnumFromString();

                            foreach (var godzItem in dzienItem.godz)
                            {
                                var hour = godzItem.h;

                                foreach (var minuteItem in godzItem.min)
                                {
                                    var minute = minuteItem.m;


                                    var time = new Time()
                                    {
                                        Hour = hour,
                                        Minute = minute
                                    };


                                    dest.AddTime(time, dayType);
                                }
                            }
                        }
                    }

                });

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


            //AutoMapper.Mapper.CreateMap<linie, BusLineIoDto>()
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.linia.nazwa))
            //    .ForMember(dest => dest.Variant, opt => opt.MapFrom(src => src.linia.wariant))
            //    .AfterMap<>()
            //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ParsedId))
            //.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            //.ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
            //.ForMember(dest => dest.X, opt => opt.MapFrom(src => src.ParsedX))
            //.ForMember(dest => dest.Y, opt => opt.MapFrom(src => src.ParsedY));

            


            configurationExpression.CreateMap<BusStopIoDto, BusStop>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ParsedId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
                .ForMember(dest => dest.X, opt => opt.MapFrom(src => src.ParsedX))
                .ForMember(dest => dest.Y, opt => opt.MapFrom(src => src.ParsedY));

            configurationExpression.CreateMap<BusStopLineIoDto, BusStop>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.BusStopId))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street));
               //.ForMember(dest => dest.X, opt => opt.MapFrom(src => src.ParsedX))
               //.ForMember(dest => dest.Y, opt => opt.MapFrom(src => src.ParsedY));


            configurationExpression.CreateMap<TimeIoDto, Time>()
                .ForMember(dest => dest.Hour, opt => opt.MapFrom(x => !string.IsNullOrEmpty(x.Hour) ? int.Parse(x.Hour) : 0))
                .ForMember(dest => dest.Minute, opt => opt.MapFrom(x => !string.IsNullOrEmpty(x.Minute) ? int.Parse(x.Minute) : 0))
                .AfterMap((dto, time) =>
                {
                    time.FixTime();
                });

           


            configurationExpression.CreateMap<AlgorithmParameters, AlgorithmParameters>()
                .ForMember(dest => dest.End, opt => opt.MapFrom(x => x.End))
                .ForMember(dest => dest.Population, opt => opt.MapFrom(x => x.Population != null ? (Population)x.Population.ToList() : null))
                .ForMember(dest => dest.Start, opt => opt.MapFrom(x => x.Start));


            return configurationExpression;
        }
    }
}
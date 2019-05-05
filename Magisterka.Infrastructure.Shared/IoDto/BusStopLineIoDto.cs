// -----------------------------------------------------------------------
//  <copyright file="BusStopLineDto.cs" company="DevCore .NET">
//      Copyright (c) DevCore.NET All rights reserved.
//  </copyright>
//  <author>Paweł Kondzior</author>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Magisterka.Infrastructure.Shared.Structure;
using Magisterka.Infrastructure.Shared.Enum;

namespace Magisterka.Infrastructure.Shared.IoDto
{
    [Serializable]
    public class BusStopLineIoDto
    {
        
        #region Constructors
        public BusStopLineIoDto()
        {
            BusinessDaySchedule = new List<Time>();
            SaturdaySchedule = new List<Time>();
            SundaySchedule = new List<Time>();
        }

        #endregion

        #region Properties

       
         public int BusStopId { get; set; }

        public string Name { get; set; }

        public string Street { get; set; }

        public List<Time> BusinessDaySchedule { get; set; }

        public List<Time> SaturdaySchedule { get; set; }

        public List<Time> SundaySchedule { get; set; }
        
        #endregion

        #region Methods
        public void AddTime(TimeIoDto timeIoDto, DayTypeEnum dayTypeEnum)
        {
            var time = new Time()
                           {
                                Hour = int.Parse(timeIoDto.Hour),
                                Minute = int.Parse(timeIoDto.Minute)
                           };



            AddTime(time, dayTypeEnum);
        }
        public void AddTime(Time time, DayTypeEnum dayTypeEnum)
        {
            switch (dayTypeEnum)
            {
                case DayTypeEnum.WorkingDay:
                    {
                        BusinessDaySchedule.Add(time);
                        break;
                    }
                case DayTypeEnum.Saturday:
                    {
                        this.SaturdaySchedule.Add(time);
                        break;
                    }
                case DayTypeEnum.Sunday:
                    {
                        this.SundaySchedule.Add(time);
                        break;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException("dayTypeEnum");
                    }
            }
        }
        #endregion
        


    }
}
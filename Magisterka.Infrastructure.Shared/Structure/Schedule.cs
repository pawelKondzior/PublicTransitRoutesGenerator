// -----------------------------------------------------------------------
//  <copyright file="ScheduleDto.cs" company="DevCore .NET">
//      Copyright (c) DevCore.NET All rights reserved.
//  </copyright>
//  <author>Paweł Kondzior</author>
// -----------------------------------------------------------------------


using System.Collections.Generic;

namespace Magisterka.Infrastructure.Shared.Structure
{
    public class Schedule
    {
        public ICollection<Time> TimeDtos { get; set; }
    }
}
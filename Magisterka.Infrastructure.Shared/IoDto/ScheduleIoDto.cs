// -----------------------------------------------------------------------
//  <copyright file="ScheduleDto.cs" company="DevCore .NET">
//      Copyright (c) DevCore.NET All rights reserved.
//  </copyright>
//  <author>Paweł Kondzior</author>
// -----------------------------------------------------------------------


using Magisterka.Infrastructure.Shared.IoDto;

namespace Magisterka.Data.Dto.Bus
{
    using System.Collections.Generic;

    public class ScheduleIoDto
    {
        public ICollection<TimeIoDto> TimeDtos { get; set; }
    }
}
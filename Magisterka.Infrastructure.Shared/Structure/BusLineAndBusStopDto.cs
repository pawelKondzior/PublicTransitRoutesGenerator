// -----------------------------------------------------------------------
//  <copyright file="BusLineAndBusStopDto.cs" company="DevCore .NET">
//      Copyright (c) DevCore.NET All rights reserved.
//  </copyright>
//  <author>Paweł Kondzior</author>
// -----------------------------------------------------------------------
using Magisterka.Infrastructure.Shared.IoDto;
namespace Magisterka.Infrastructure.Shared.Structure
{
    public class BusLineAndBusStopDto
    {
        public BusStop BusStop { get; set; }

        public BusLineIoDto BusLine { get; set; } 
    }
}
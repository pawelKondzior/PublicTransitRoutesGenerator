// -----------------------------------------------------------------------
//  <copyright file="WalkChanges.cs" company="DevCore .NET">
//      Copyright (c) DevCore.NET All rights reserved.
//  </copyright>
//  <author>Paweł Kondzior</author>
// -----------------------------------------------------------------------
namespace Magisterka.Infrastructure.Shared.Structure
{
    public class WalkLineChanges
    {
        public BusStop Start { get; set; }

        public BusStop End { get; set; }


        public WalkLineChanges()
        {
        }

        public WalkLineChanges(BusStop start, BusStop end)
        {
            Start = start;
            End = end;
        }
    }
}
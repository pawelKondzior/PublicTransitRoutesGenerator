// // -----------------------------------------------------------------------
// //  <copyright company="DevCore .NET">
// //      Copyright (c) DevCore.NET All rights reserved.
// //  </copyright>
// //  <author> Paweł Kondzior</author>
// // -----------------------------------------------------------------------
namespace Magisterka.Infrastructure.Shared.Structure
{
    public class PrevAndNextBusStopsIds
    {
        public PrevAndNextBusStopsIds(int currentBusStopId)
        {
            this.CurrentBusStopId = currentBusStopId;
        }

        public int? PrevBusStopId { get; set; }

        public int CurrentBusStopId { get; set; }

        public int? NextBusStopId { get; set; }




        public bool HybridizationAvible(PrevAndNextBusStopsIds second)
        {
            if (!this.PrevBusStopId.HasValue || !this.NextBusStopId.HasValue
                || !second.PrevBusStopId.HasValue || !second.NextBusStopId.HasValue)
            {
                return false;
            }

            if (this.CurrentBusStopId != second.CurrentBusStopId)
            {
                return false;
            }

            //if (this.PrevBusStopId.Value == second.PrevBusStopId.Value)
            //{
            //    return false;
            //}

            //if (this.NextBusStopId.Value == second.NextBusStopId.Value)
            //{
            //    return false;
            //}

            return true;
        }
    }
}
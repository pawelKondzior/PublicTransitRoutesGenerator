
using Magisterka.Infrastructure.Shared.IoDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magisterka.Infrastructure.Shared.Comparers
{
    public class BusStopLineIoDtoComparer : IEqualityComparer<BusStopLineIoDto>
    {
        public bool Equals(BusStopLineIoDto x, BusStopLineIoDto y)
        {
            return x.BusStopId == y.BusStopId;
        }

        public int GetHashCode(BusStopLineIoDto obj)
        {
            return obj.BusStopId;
        }
    }
}

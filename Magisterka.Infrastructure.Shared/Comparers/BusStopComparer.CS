using Magisterka.Infrastructure.Shared.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magisterka.Infrastructure.Shared.Comparers
{
    public class BusStopComparer : IEqualityComparer<BusStop>
    {
        public bool Equals(BusStop x, BusStop y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(BusStop obj)
        {
            return obj.Id;
        }
    }
}

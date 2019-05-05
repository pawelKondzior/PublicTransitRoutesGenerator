
using System.Linq;

namespace Magisterka.Infrastructure.Shared.Comparers
{
    using Magisterka.Infrastructure.Shared.Structure;
    using System.Collections.Generic;

    // public class AggregateResultsComparer : EqualityComparer<List<BusStop>>
    public class ListBusStopComparer : EqualityComparer<List<BusStop>>
    {

        public override bool Equals(List<BusStop> x, List<BusStop> y)
        {
            if (x.Count != y.Count)
            {
                return false;
            }

            for (int i = 0; i < x.Count; i++)
            {
                if (x[i] != y[i])
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode(List<BusStop> obj)
        {
            var sum = 0;

            if (obj == null || !obj.Any())
            {
                return sum;
            }

            for (int i = 0; i < obj.Count; i++)
            {
                sum += obj[i].Id * (i+1);
            }
            return sum;
        }
    }
}
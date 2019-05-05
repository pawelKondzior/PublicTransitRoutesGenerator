using Magisterka.Infrastructure.Shared.Extensions;
using Magisterka.Infrastructure.Shared.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magisterka.Infrastructure.Shared.Dto
{
    public class HyrydizationItemDto
    {
        public EntireRoute FirstRoute { get; set; }

        public EntireRoute SecondRoute { get; set; }

        public BusStop Item { get; set; }




        public static HyrydizationItemDto CreateHyrydizationItemDto(EntireRoute firstRoute, EntireRoute secondRoute, PartOfTheRoute item)
        {
            var firstRouteIndexOf = firstRoute.PartOfTheRoute.Select(x => x.BusStop).IndexOfExt(item.BusStop);
            var secondRouteIndexOf = secondRoute.PartOfTheRoute.Select(x => x.BusStop).IndexOfExt(item.BusStop);

            var firstCount = firstRoute.PartOfTheRoute.Count;
            var secoundCount = firstRoute.PartOfTheRoute.Count;

            if (firstRouteIndexOf > 0 || firstRouteIndexOf < secondRouteIndexOf)
            {
                return null;
            }

            if (firstRouteIndexOf > 0 || firstRouteIndexOf < secondRouteIndexOf)
            {
                return null;
            }



                return new HyrydizationItemDto(firstRoute, secondRoute, item.BusStop);
        }

        public HyrydizationItemDto(EntireRoute firstRoute, EntireRoute secondRoute, BusStop item)
        {
            FirstRoute = firstRoute;
            SecondRoute = secondRoute;
            Item = item;
        }
    }


}

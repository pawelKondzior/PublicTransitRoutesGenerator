// -----------------------------------------------------------------------
//  <copyright file="BusStopRepository.cs" company="DevCore .NET">
//      Copyright (c) DevCore.NET All rights reserved.
//  </copyright>
//  <author>Paweł Kondzior</author>
// -----------------------------------------------------------------------

using Magisterka.Infrastructure.Shared.Collections;
using Magisterka.Infrastructure.Shared.IoDto;

namespace Magisterka.Data.Access.Repository
{
    using System.Collections.Generic;
    using Magisterka.Data.Access.Interfaces;
    using System.Linq;
    using Magisterka.Infrastructure.Shared.Structure;

    public class OldBusStopRepository : XmlRepository, IRepository<BusStopIoDto>
    {
        #region Constructor

        public OldBusStopRepository(string path)
            : base(path)
        {}

        #endregion 
    
        public List<BusStopIoDto> All()
        {
            //var stops = XDocument.Descendants("BusStopList");

            var data = XDocument.Descendants("BusStop")
                .Select(x => new BusStopIoDto()
                                 {
                                     Id = x.Attribute("Id").Value,
                                     Name = x.Attribute("Name").Value,
                                     Street = x.Attribute("Street").Value,
                                     X = x.Attribute("X").Value,
                                     Y =  x.Attribute("Y").Value,
                                 });

            
            
            return data.ToList();
        }
    }
}
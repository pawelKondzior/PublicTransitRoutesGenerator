// -----------------------------------------------------------------------
//  <copyright file="AutoMapperInit.cs" company="DevCore .NET">
//      Copyright (c) DevCore.NET All rights reserved.
//  </copyright>
//  <author>Paweł Kondzior</author>
// -----------------------------------------------------------------------

using System;
using AutoMapper;
using Magisterka.Infrastructure.Shared.IoDto2017;

namespace Magisterka.Modules.Main.Init
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using Magisterka.Infrastructure.Shared.Interfaces;
    using Magisterka.Infrastructure.Shared.IoDto;
    using Magisterka.Infrastructure.Shared.Structure;

    public class AutoMapperInit : IInitialize
    {
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1115:ParameterMustFollowComma",
            Justification = "Reviewed. Suppression is OK here."),
         SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1116:SplitParametersMustStartOnLineAfterDeclaration",
             Justification = "Reviewed. Suppression is OK here.")]


        public void Init()
        {

            Mapper.Initialize(cfg => {
                cfg.AddMappings();
            });

            //var config = new MapperConfiguration(cfg => {
            //    cfg.AddMappings();
            //});

            

            //   services.AddSingleton(x => mappingConfig.CreateMapper());

        }



    }
}
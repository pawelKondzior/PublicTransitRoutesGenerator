// // -----------------------------------------------------------------------
// //  <copyright company="DevCore .NET">
// //      Copyright (c) DevCore.NET All rights reserved.
// //  </copyright>
// //  <author> Paweł Kondzior</author>
// // -----------------------------------------------------------------------

namespace Magisterka.Modules.Main.Builders
{
    using System;
    using Magisterka.Infrastructure.Shared.Structure;
    using Magisterka.Modules.Main.Algoritms;
    using Magisterka.Modules.Main.Algoritms.PopulationGenerators;

    public abstract class AlgorithmBuilder
    {
        protected AlgorithmParameters AlgorithmParameters { get; set; }

        protected AlgorithmBuilder(AlgorithmParameters algorithmParameters)
        {
            AlgorithmParameters = algorithmParameters;
        }

        public virtual BaseAlgorithm GetAlgorithm()
        {
            throw new NotImplementedException();   
        }
    }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseArrayGenerator.cs" company="">
//   
// </copyright>
//  <author>Paweł Kondzior</author>
// <summary>
//   The base array generator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------


using Anotar.Log4Net;
using Magisterka.Infrastructure.Shared.Comparers;

namespace Magisterka.Modules.Main.Algoritms.PopulationGenerators
{
    using System.Collections.Generic;
    using System.Linq;

    using Magisterka.Infrastructure.Shared.Enum;
    using Magisterka.Infrastructure.Shared.Extensions;
    using Magisterka.Infrastructure.Shared.Structure;
    using System;
    using log4net;
    using System.Diagnostics;
    using Magisterka.Modules.Main.Algoritms.Matrix;

    //  using Magisterka.Modules.Main.Aspects;

    /// <summary>
    /// The base array generator.
    /// </summary>
    public abstract class BaseArrayGenerator : BasePopulationGenerator
    {
        public BaseArrayGenerator(AlgorithmParameters baseAlgorithmParameters) // :this()
            : base(baseAlgorithmParameters)
        {
            GeneratorConnectionTypeEnum = GeneratorConnectionTypeEnum.FindMisssingLines;
        }


        protected virtual BaseMatrixGenerator GetMatrixGenerator()
        {
            return new NewQMatrixGenerator(AlgorithmParameters);
        }

        /// <summary>
        /// The generate.
        /// </summary>
        /// <returns>
        /// The Magisterka.Infrastructure.Shared.Structure.Population.
        /// </returns>
        public override Population Generate()
        {
            this.Population = new Population();

            //  var changeStackGenerator = new MatrixQGenerator(AlgorithmParameters.CloneRequired());
            var changeStackGenerator = GetMatrixGenerator();

            var listOfBusPaths = changeStackGenerator.Generate();

            var orderedList = listOfBusPaths.OrderBy(x => x.Count);


            LogTo.Info("Wyliczono ChangeStack {0}", orderedList.Count());


            var listOfPathsStack = new Stack<List<BusStop>>(orderedList);

            ///   foreach (var aggregateResult in orderedList.Take(this.AlgorithmParameters.PopulationCount))


            while (Population.Count < this.AlgorithmParameters.PopulationCount && listOfPathsStack.Count != 0)
            {
                var aggregateResult = listOfPathsStack.Pop();

                if (aggregateResult.Count > 1)
                {
                    GeneratePopulationFromListOfBusStops(aggregateResult);
                }
            }

            return base.Generate();

        }
    }
}
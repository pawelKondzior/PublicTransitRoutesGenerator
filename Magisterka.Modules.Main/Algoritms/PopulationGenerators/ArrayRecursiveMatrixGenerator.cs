// -----------------------------------------------------------------------
//  <copyright file="MyArrayGenerator.cs" company="DevCore .NET">
//      Copyright (c) DevCore.NET All rights reserved.
//  </copyright>
//  <author>Paweł Kondzior</author>
// -----------------------------------------------------------------------

namespace Magisterka.Modules.Main.Algoritms.PopulationGenerators
{
    using Magisterka.Infrastructure.Shared.Structure;
    using Magisterka.Modules.Main.Algoritms.Matrix;

    /// <summary>
    /// Moje implementacja algorytmu tablicowego, troche sie rozni od tej "standardowej",
    ///  i raczej w dobrą stronę sie rożni
    /// </summary>
    public class ArrayRecursiveMatrixGenerator : BaseArrayGenerator
    {
        #region Construcotrs
        //public MyArrayGenerator()
        //{  

        //}

        public ArrayRecursiveMatrixGenerator(AlgorithmParameters baseAlgorithmParameters)
            : base(baseAlgorithmParameters)
        {
            //   WindowsFormsChart.DrowBusStopList(points); 
        }

        protected override BaseMatrixGenerator GetMatrixGenerator()
        {
            return new MatrixQGenerator(AlgorithmParameters);
        }

        #endregion 
    }
}
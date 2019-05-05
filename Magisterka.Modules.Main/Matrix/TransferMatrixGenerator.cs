using System;
using System.Collections.Generic;
using System.Linq;
using Anotar.Log4Net;
using Magisterka.Infrastructure.Shared.Collections;
using Magisterka.Infrastructure.Shared.Enum;
using Magisterka.Infrastructure.Shared.Generic;
using Magisterka.Infrastructure.Shared.Structure;
using MoreLinq;
using QuickGraph;
using QuickGraph.Algorithms;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra;


namespace Magisterka.Modules.Main.Matrix
{
    public class TransferMatrixGenerator : BaseMatrixGenerator
    {

        private int[,] TransferMatrix { get; set; }

    

        public TransferMatrixGenerator(BusLinesList busLinesList, BusStopList busStopList)
            : base(busLinesList, busStopList)
        {
            TransferMatrix = new int[BusStopList.Count, BusStopList.Count]; ;
        }


        public TransferMatrixGenerator(BusLinesList busLinesList, BusStopList busStopList, int[,] transferMatrix)
            : base(busLinesList, busStopList)
        {
            TransferMatrix = transferMatrix;
        }


        public double[,] GenerateQMatrix(int maxChange)
        {
            if (TransferMatrix == null)
            {
                throw new Exception("Brak TransferMatrix");
            }

            var matrixLength = TransferMatrix.GetLength(0);
            var transferMatrixDouble = new double[matrixLength, matrixLength];
            var qMatrixDouble = new double[matrixLength, matrixLength];

            Array.Copy(TransferMatrix, transferMatrixDouble, TransferMatrix.Length);
            Array.Copy(transferMatrixDouble, qMatrixDouble, TransferMatrix.Length);

            if (maxChange == 0)
            {
                return transferMatrixDouble;
            }

            foreach (var sequence in MoreLinq.MoreEnumerable.Sequence(2, maxChange + 1))
            {
                var tPowerMatrix = DenseMatrix.OfArray(transferMatrixDouble).Power(sequence);

                for (int i = 0; i < matrixLength; i++)
                {
                    for (int j = 0; j < matrixLength; j++)
                    {
                        
                        if (i != j && qMatrixDouble[i,j] == 0  && tPowerMatrix[i,j] > 0)
                        {
                            qMatrixDouble[i, j] = sequence;
                        }
                    }
                }   
            }

          

            return qMatrixDouble;
        }



        public int[,] GenerateTransferMatrix(int? maxWalkTime)
        {

            var cartesianBusStops = BusStopList.Cartesian(BusStopList, (first, second) => new GenericPair<BusStop>(first, second));

            var totalCount = cartesianBusStops.Count();
            var counter = 0;

            LogTo.Debug("Liczba par {0}", totalCount);

            foreach (var busStopPair in cartesianBusStops)
            {
                counter++;
                if (counter % 5000 == 0)
                {
                    LogTo.Debug("Wykonano {0}%", (counter * 100 / totalCount));
                }

                int valueToInsert = 0;

                if (busStopPair.First.ArrayNumber != busStopPair.Second.ArrayNumber)
                {
                    var conectedByBusLine = BusLinesList.Any(x => x.BusStopsAreConnected(busStopPair.First, busStopPair.Second));

                    var connectedByWalkLink = false;

                    if (maxWalkTime.HasValue)
                    {
                        connectedByWalkLink = busStopPair.First.GetWalkTime(busStopPair.Second) <= maxWalkTime.Value;
                    }

                    if (conectedByBusLine || connectedByWalkLink)
                    {
                        valueToInsert = 1;
                    }
                }

                TransferMatrix[busStopPair.First.ArrayNumber, busStopPair.Second.ArrayNumber] = valueToInsert;
            }

            return TransferMatrix;
        }
    }
}
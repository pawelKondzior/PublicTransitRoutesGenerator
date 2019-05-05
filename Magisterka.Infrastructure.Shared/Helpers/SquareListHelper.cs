// // -----------------------------------------------------------------------
// //  <copyright company="DevCore .NET">
// //      Copyright (c) DevCore.NET All rights reserved.
// //  </copyright>
// //  <author> Paweł Kondzior</author>
// // -----------------------------------------------------------------------

namespace Magisterka.Infrastructure.Shared.Helpers
{
    using Magisterka.Infrastructure.Shared.Basic;
    using Magisterka.Infrastructure.Shared.Collections;
    using Magisterka.Infrastructure.Shared.Extensions;
    using Magisterka.Infrastructure.Shared.Structure;

    public class SquareListHelper
    {
        // private AlgorithmParameters AlgorithmParameters { get; set; }

        private SquareList AllSquareList { get; set; }

        public SquareListHelper(SquareList allSquareList)
        {
         //   AlgorithmParameters = algorithmParameters;
            AllSquareList = allSquareList;
        }


        public SquareList GetSelectedSquareList(AlgorithmParameters algorithmParameters)
        {
            return GetSelectedSquareList(algorithmParameters.Start, algorithmParameters.End, algorithmParameters.NumberOfNeighborSquares);
        }

        private  SquareList GetSelectedSquareList(SinglePoint firstPoint, SinglePoint lastPoint, AlgorithmParameters algorithmParameters)
        {
            return GetSelectedSquareList(firstPoint, lastPoint, algorithmParameters.NumberOfNeighborSquares);
        }

        public SquareList GetSelectedSquareList(SinglePoint firstPoint, SinglePoint lastPoint, int numberOfNeighborSquares)
        {
            var line = new LineSegment(firstPoint, lastPoint);

            SquareList result = AllSquareList.GetSquaresInACollisionWithLineSegment(line);

            result.AddIfNotExists(AllSquareList.GetSquareContainingPoint(firstPoint));
            result.AddIfNotExists(AllSquareList.GetSquareContainingPoint(lastPoint));

            result.AddRange(AllSquareList.GetMostOccupiedSquares(numberOfNeighborSquares, result));

            return result;
        }
    }
}
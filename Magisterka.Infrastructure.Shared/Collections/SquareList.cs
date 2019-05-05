namespace Magisterka.Infrastructure.Shared.Collections
{
    using System.Collections.Generic;
    using Magisterka.Infrastructure.Shared.Basic;
    using System.Linq;
    using Magisterka.Infrastructure.Shared.Structure;

    public class SquareList : List<Square>
    {
        #region  Constructors
        
        public SquareList() : base()
        {
            
        }
        #endregion

        #region Methods

        /// <summary>
        /// Uzupełnia dla każdego z prostokontów, punkty jakie sie w nim znajdują!
        /// (W prostokątach są jakieś punkty, i dla każdego z prostokontów wyznacza jakie to)
        /// </summary>
        public void CompleteInnerBusStopsForAllSquares(BusStopList busStopList)
        {
            var workingList = busStopList.ToList();

            foreach (var item in this)
            {
                item.CalculatePointsInSquare(ref workingList);
            }
        }

        public void CheckIfLineSegmendCrossSquares(LineSegment lineSegment)
        {
             foreach (var item in this)
             {
                 if (item.CheckIfCrossLineSegment(lineSegment));
             }

             var count = this.Count(x => x.CrossLineSegment);
        }

        public SquareList GetSquaresInACollisionWithLineSegment(LineSegment lineSegment)
        {
             foreach (var item in this)
             {
                 item.CheckIfCrossLineSegment(lineSegment);
             }

            var squareList = new SquareList();

            foreach (var item in this.Where(x => x.CrossLineSegment))
            {
                squareList.Add(item);
            }

            return squareList;
         }

        public Square GetSquareContainingPoint(SinglePoint point)
        {
            return this.FirstOrDefault(x => x.CheckIfContainsPoint(point));
        }

        public BusStopList GetBusStopsLocatedIn(BusStopList currentList)
        {
            var busStepList = new BusStopList();

            var tempList = currentList.ToList();

            foreach (var item in this)
            {
                var range = item.GetBusStopsLocatedIn(ref tempList);
                busStepList.AddRange(range);    
            }

            return busStepList;
        }

        public IEnumerable<Square> GetMostOccupiedSquares(int number, List<Square> exeptThis)
        {
           // var mostOccupiedCollection = this.OrderByDescending(x => x.InnerBusStops.Count);

            return this.Except(exeptThis).OrderByDescending(x => x.InnerBusStops.Count).Take(number);
        }

        #endregion
    }
}
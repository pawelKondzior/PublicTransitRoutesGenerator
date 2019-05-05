

namespace Magisterka.Infrastructure.Shared.Basic
{
    using System.Collections.Generic;
    using Magisterka.Infrastructure.Shared.Collections;
    using Magisterka.Infrastructure.Shared.Consts;
    using System.Linq;
    using Magisterka.Infrastructure.Shared.Structure;
    using System.Windows.Media;

    public class Square
    {
        #region Fields
        private List<LineSegment> _lineSegments;

        #endregion

        #region Properties
        public SinglePoint A { get; set; }
        public SinglePoint B { get; set; }
        public SinglePoint C { get; set; }
        public SinglePoint D { get; set; }
 
        public List<LineSegment> LineSegments
        {
            get
            {
                if (_lineSegments == null)
                {
                    var list = new List<LineSegment>();

                    if (A != null && B != null)
                    {
                        list.Add(new LineSegment(A, B));
                    }

                    if (B != null && C != null)
                    {
                        list.Add(new LineSegment(B, C));
                    }

                    if (C != null && D != null)
                    {
                        list.Add(new LineSegment(C, D));
                    }

                    if (D != null && A != null)
                    {
                        list.Add(new LineSegment(D, A));
                    }

                    _lineSegments = list;
                }

                return _lineSegments;
            }
        }

        public bool CrossLineSegment { get; set; }

        public Brush Color { get; set; }

        public List<BusStop> InnerBusStops { get; set; }


     //   public int X { get; set; }

   //     public int Y { get; set; }

        #endregion

        #region Constructors


        public Square()
        {
            CrossLineSegment = false;
            Color = ShapeColors.UnmarkedSquare;
        }

        #endregion


        #region Methods

        public bool HasAllPoints()
        {
            if (A != null && B != null && C != null && D != null)
            {
                return true;
            }
            return false;
        }

        public bool CheckIfCrossLineSegment(LineSegment lineSegment)
        {
            foreach (var segment in LineSegments)
            {
                if (segment.Intersects2D(lineSegment))
                {
                    this.CrossLineSegment = true;
                    this.Color = ShapeColors.MarkedSquare;
                    return true;
                }
            }

            this.CrossLineSegment = false;
            return false;
        }


        public void CalculatePointsInSquare(ref List<BusStop> currentList)
        {
            var query = currentList.Where(x => x.X <= this.B.X  // max x
                                            && x.X >= this.A.X  // min x
                                            && x.Y <= this.A.Y  // max y
                                            && x.Y >= this.D.Y) // min y
                                            .ToList();

            InnerBusStops = new List<BusStop>();

            foreach (var busStop in query)
            {
                InnerBusStops.Add(busStop);
                currentList.Remove(busStop);
            }
        }


        /// <summary>
        /// Pobiera punkty znajdujące sie w danym prostokącie
        /// </summary>
        /// <param name="currentList"></param>
        /// <returns></returns>
        public List<BusStop> GetBusStopsLocatedIn(ref List<BusStop> currentList)
        {
            if (InnerBusStops == null)
            {
                CalculatePointsInSquare(ref currentList);
            }

            return InnerBusStops;
        }

        public bool CheckIfContainsPoint(SinglePoint point)
        {
            if (point.X >= this.A.X && point.X <= this.B.X && 
                point.Y <= this.A.Y && point.Y >= this.D.Y)
            {
                return true;
            }
            return false;
        }


        #endregion

       
    }
}
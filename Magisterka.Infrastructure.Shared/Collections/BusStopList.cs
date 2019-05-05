// -----------------------------------------------------------------------
//  <copyright file="BusStopList.cs" company="DevCore .NET">
//      Copyright (c) DevCore.NET All rights reserved.
//  </copyright>
//  <author>Paweł Kondzior</author>
// -----------------------------------------------------------------------

using Magisterka.Infrastructure.Shared.Dto;

namespace Magisterka.Infrastructure.Shared.Collections
{
    using System.Collections.Generic;
    using Magisterka.Infrastructure.Shared.Structure;
    using System.Linq;
    using Magisterka.Infrastructure.Shared.Basic;

    public class BusStopList : List<BusStop>
    {
        #region Fields

        private double? _minX;

        private double? _maxX;

        private double? _minY;

        private double? _maxY;



        #endregion

        #region Properties 

        #region Max Min
        
        public double MaxLongitude
        {
            get
            {
                if (!_maxX.HasValue)
                {
                    _maxX = this.Select(x => x.X).Max();
                }
                return _maxX.Value;
            }
        }

        public double MinLongitude
        {
            get
            {
                if (!_minX.HasValue)
                {
                    _minX = this.Select(x => x.X).Min();
                }
                return _minX.Value;
            }
        }

        public double MaxLatitude
        {
            get
            {
                if (!_maxY.HasValue)
                {
                    _maxY = this.Select(x => x.Y).Max();
                }
                return _maxY.Value;
            }
        }

        public double MinLatitude
        {
            get
            {
                if (!_minY.HasValue)
                {
                    _minY = this.Select(x => x.Y).Min();
                }
                return _minY.Value;
            }
        }

        #endregion

        #endregion

        #region Constructors

        public BusStopList()
        {
            
        }

        public BusStopList(IEnumerable<BusStop> list)
        {
            foreach (var busStop in list)
            {
                this.Add(busStop);
            }
        }

        
        #endregion

        #region Methods

        private List<double> GetEquallyDividedXLines(int numberOfLines)
        {
            var result = new List<double>();

            var distance = this.MaxLongitude - this.MinLongitude;

            var stepDistance = distance/ numberOfLines;

            var temp = this.MinLongitude;

            result.Add(temp);
            for (int i = 0; i < numberOfLines - 1; i++)
            {
                var value = temp += stepDistance;
                result.Add(value);
            }
            result.Add(this.MaxLongitude);

            return result;
        }

        private List<double> GetEquallyDividedYLines(int numberOfLines)
        {
            var result = new List<double>();

            var distance = this.MaxLatitude - this.MinLatitude;

            var stepDistance = distance / numberOfLines;

            var temp = this.MinLatitude;

            result.Add(temp);
            for (int i = 0; i < numberOfLines - 1; i++)
            {
                var value = temp += stepDistance;
                result.Add(value);
            }
            result.Add(this.MaxLatitude);

            return result;
        }

        public List<LineSegment> GetEquallyDividedLines(int numberOfLines)
        {
            var xAxisSplitResult = GetEquallyDividedXLines(numberOfLines);
            var yAxisSplitResult = GetEquallyDividedYLines(numberOfLines);

            var splitLines = new List<LineSegment>();

            foreach (var item in xAxisSplitResult)
            {
                var firstPoint = new SinglePoint(item, this.MaxLatitude);
                var lastPoint = new SinglePoint(item, this.MinLatitude);

                splitLines.Add(new LineSegment(firstPoint, lastPoint));
            }

            foreach (var item in yAxisSplitResult)
            {
                var firstPoint = new SinglePoint(this.MaxLongitude, item);
                var lastPoint = new SinglePoint(this.MinLongitude, item);

                splitLines.Add(new LineSegment(firstPoint, lastPoint));
            }

            // splitLines.AddRange(from item in yAxisSplitResult let firstPoint = new SinglePoint(item, this.MaxX) let lastPoint = new SinglePoint(item, this.MinX) select new LineSegment(firstPoint, lastPoint));

            return splitLines;
        }

        public SquareList GetEquallyDividedSquares(int numberOfLines)
        {
            var xAxisSplitResult = GetEquallyDividedXLines(numberOfLines);
            var yAxisSplitResult = GetEquallyDividedYLines(numberOfLines);

            var squareList = new SquareList();
            

            var count = numberOfLines + 1;
            // var squareCollection = new Square[count, count];

            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    var square= new Square();

                    square.D = new SinglePoint(xAxisSplitResult[i], yAxisSplitResult[j]);

                    if (i + 1 < count)
                    {
                        square.C = new SinglePoint(xAxisSplitResult[i + 1], yAxisSplitResult[j]);
                    }

                    if (i + 1< count && j + 1< count)
                    {
                        square.B = new SinglePoint(xAxisSplitResult[i + 1], yAxisSplitResult[j + 1]);
                    }

                    if (j + 1 < count)
                    {
                        square.A = new SinglePoint(xAxisSplitResult[i], yAxisSplitResult[j + 1]);
                    }

                    if (square.HasAllPoints())
                    {
                        squareList.Add(square);
                    }
                }
            }

            return squareList;
        }

        public BusStop GetBySinglePoint(SinglePoint singlePoint)
        {
            return this.FirstOrDefault(x => x.X == singlePoint.X && x.Y == singlePoint.Y);
        }


        

        #endregion
    }


}
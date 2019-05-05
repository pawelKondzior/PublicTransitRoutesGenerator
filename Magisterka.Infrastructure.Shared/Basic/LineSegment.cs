namespace Magisterka.Infrastructure.Shared.Basic
{
    public class LineSegment
    {
        public LineSegment()
        {
            
        }

        public LineSegment(SinglePoint firstPoint, SinglePoint lastPoint)
        {
            FirstPoint = firstPoint;
            LastPoint = lastPoint;
        }

        public LineSegment(double x1, double y1, double x2, double y2)
        {
            FirstPoint = new SinglePoint(x1, y1);
            LastPoint = new SinglePoint(x2, y2);
        }

        public SinglePoint FirstPoint { get; set; }

        public SinglePoint LastPoint { get; set; }


        //public bool Intersects2D(LineSegment otherLineSegment) //, out Vector3 intersectionPoint)
        //{
        //    double thisSlopeX, thisSlopeY, secondLineSlopeX, secondLineSlopeY;

        //    thisSlopeX = this.LastPoint.X - this.FirstPoint.X;
        //    thisSlopeY = this.LastPoint.Y - this.FirstPoint.Y;

        //    secondLineSlopeX = otherLineSegment.LastPoint.X - otherLineSegment.FirstPoint.X;
        //    secondLineSlopeY = otherLineSegment.LastPoint.Y - otherLineSegment.FirstPoint.Y;

        //    double s, t;
        //    s = (-thisSlopeY * (this.FirstPoint.X - otherLineSegment.FirstPoint.X) + thisSlopeX * (this.FirstPoint.Y - otherLineSegment.FirstPoint.Y)) / (-secondLineSlopeX * thisSlopeY + thisSlopeX * secondLineSlopeY);
        //    t = (secondLineSlopeX * (this.FirstPoint.Y - otherLineSegment.FirstPoint.Y) - secondLineSlopeY * (this.FirstPoint.X - otherLineSegment.FirstPoint.X)) / (-secondLineSlopeX * thisSlopeY + thisSlopeX * secondLineSlopeY);

        //    if (s >= 0 && s <= 1 && t >= 0 && t <= 1)
        //    {
        //        double intersectionPointX = this.FirstPoint.X + (t * thisSlopeX);
        //        double intersectionPointY = this.FirstPoint.Y + (t * thisSlopeY);

        //        // Collision detected
        //      //  intersectionPoint = new Vector3(intersectionPointX, intersectionPointY, 0);

        //        return true;
        //    }

        // //   intersectionPoint = Vector3.Zero;
        //    return false; // No collision
        //}

        public bool Intersects2D(LineSegment secondLine)
        {

            double Ua, Ub;



            // Equations to determine whether lines intersect

            Ua = ((secondLine.LastPoint.X - secondLine.FirstPoint.X) * (this.FirstPoint.Y - secondLine.FirstPoint.Y) - (secondLine.LastPoint.Y - secondLine.FirstPoint.Y) * (this.FirstPoint.X - secondLine.FirstPoint.X)) /

                    ((secondLine.LastPoint.Y - secondLine.FirstPoint.Y) * (this.LastPoint.X - this.FirstPoint.X) - (secondLine.LastPoint.X - secondLine.FirstPoint.X) * (this.LastPoint.Y - this.FirstPoint.Y));



            Ub = ((this.LastPoint.X - this.FirstPoint.X) * (this.FirstPoint.Y - secondLine.FirstPoint.Y) - (this.LastPoint.Y - this.FirstPoint.Y) * (this.FirstPoint.X - secondLine.FirstPoint.X)) /

                    ((secondLine.LastPoint.Y - secondLine.FirstPoint.Y) * (this.LastPoint.X - this.FirstPoint.X) - (secondLine.LastPoint.X - secondLine.FirstPoint.X) * (this.LastPoint.Y - this.FirstPoint.Y));



            if (Ua >= 0.0f && Ua <= 1.0f && Ub >= 0.0f && Ub <= 1.0f)
            {

                double x = this.FirstPoint.X + Ua * (this.LastPoint.X - this.FirstPoint.X);

                double y = this.FirstPoint.Y + Ua * (this.LastPoint.Y - this.FirstPoint.Y);


                return true;
                // return new Vector2((float)x, (float)y);



            }

            else
            {
                return false;
                //return new Vector2();

            }

        }


    }
}
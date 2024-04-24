using System;
using System.Collections.Generic;

namespace Compute
{
    public class Quadrilateral
    {
        private Point vertice1;
        private Point vertice2;
        private Point vertice3;
        private Point vertice4;

        private double side1;
        private double side2;
        private double side3;
        private double side4;

        private bool sidesParallel;
        private bool oppositeSidesEqual;

        public Quadrilateral(Point point1, Point point2, Point point3, Point point4)
        {
            this.vertice1 = point1;
            this.vertice2 = point2;
            this.vertice3 = point3;
            this.vertice4 = point4;

            this.side1 = Utilities.Distance(vertice1, vertice2);
            this.side2 = Utilities.Distance(vertice2, vertice3);
            this.side3 = Utilities.Distance(vertice3, vertice4);
            this.side4 = Utilities.Distance(vertice4, vertice1);
        }

        public void CheckParallelSides()
        {
            sidesParallel = (Math.Abs((vertice2.GetY() - vertice1.GetY()) * (vertice3.GetX() - vertice4.GetX()) - (vertice3.GetY() - vertice4.GetY()) * (vertice2.GetX() - vertice1.GetX())) < double.Epsilon) ||
                            (Math.Abs((vertice2.GetX() - vertice1.GetX()) * (vertice3.GetZ() - vertice4.GetZ()) - (vertice3.GetX() - vertice4.GetX()) * (vertice2.GetZ() - vertice1.GetZ())) < double.Epsilon) ||
                            (Math.Abs((vertice2.GetY() - vertice1.GetY()) * (vertice3.GetZ() - vertice4.GetZ()) - (vertice3.GetY() - vertice4.GetY()) * (vertice2.GetZ() - vertice1.GetZ())) < double.Epsilon);
        }

        public void CheckOppositeSidesEqual()
        {
            oppositeSidesEqual = Math.Abs(side1 - side2) < double.Epsilon &&
               Math.Abs(side3 - side4) < double.Epsilon;
        }

        public bool AreAllSidesEqual()
        {
            return Math.Abs(side1 - side2) < double.Epsilon &&
                   Math.Abs(side2 - side3) < double.Epsilon &&
                   Math.Abs(side3 - side4) < double.Epsilon;
        }

        public bool AreAllAngles90Degrees()
        {
            Point side1Vector = new Point(vertice2.GetX() - vertice1.GetX(), vertice2.GetY() - vertice1.GetY(), vertice2.GetZ() - vertice1.GetZ());
            Point side2Vector = new Point(vertice3.GetX() - vertice2.GetX(), vertice3.GetY() - vertice2.GetY(), vertice3.GetZ() - vertice2.GetZ());
            Point side3Vector = new Point(vertice4.GetX() - vertice3.GetX(), vertice4.GetY() - vertice3.GetY(), vertice4.GetZ() - vertice3.GetZ());
            Point side4Vector = new Point(vertice1.GetX() - vertice4.GetX(), vertice1.GetY() - vertice4.GetY(), vertice1.GetZ() - vertice4.GetZ());

            double dotProduct1 = Utilities.DotProduct(side1Vector, side2Vector);
            double dotProduct2 = Utilities.DotProduct(side2Vector, side3Vector);
            double dotProduct3 = Utilities.DotProduct(side3Vector, side4Vector);
            double dotProduct4 = Utilities.DotProduct(side4Vector, side1Vector);

            return Math.Abs(dotProduct1) < double.Epsilon &&
                Math.Abs(dotProduct2) < double.Epsilon &&
                Math.Abs(dotProduct3) < double.Epsilon &&
                Math.Abs(dotProduct4) < double.Epsilon;
        }

        public bool IsTrapezoid()
        {
            return sidesParallel;
        }

        public bool IsParallelogram()
        {
            return sidesParallel && oppositeSidesEqual;
        }

        public bool IsRectangle()
        {
            return IsParallelogram() && AreAllAngles90Degrees();
        }

        public bool IsSquare()
        {
            return IsRectangle() && AreAllSidesEqual();
        }

        public bool IsRhombus()
        {
            return AreAllSidesEqual() && sidesParallel;
        }
    }
}

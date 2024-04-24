using System;
using System.Collections.Generic;

namespace Compute
{
    public class Triangle
    {
        private Point vertice1;
        private Point vertice2;
        private Point vertice3;
        private double side1;
        private double side2;
        private double side3;

        public Triangle(Point point1, Point point2, Point point3) 
        {
            this.vertice1 = point1;
            this.vertice2 = point2;
            this.vertice3 = point3;

            this.side1 = Utilities.Distance(vertice1, vertice2);
            this.side2 = Utilities.Distance(vertice2, vertice3);
            this.side3 = Utilities.Distance(vertice3, vertice1);
        }

        public bool IsEquilateral()
        {
            return side1 == side2 && side2 == side3;
        }

        public bool IsIsosceles()
        {
            return side1 == side2 || side1 == side3 || side2 == side3;
        }

        public bool IsRightAngled()
        {
            double[] sides = { side1, side2, side3 };
            Array.Sort(sides);
            return Math.Abs(sides[0] * sides[0] + sides[1] * sides[1] - sides[2] * sides[2]) < 0.0001;
        }

        public bool IsObtuse()
        {
            double[] sides = { side1, side2, side3 };
            Array.Sort(sides);
            return sides[0] * sides[0] + sides[1] * sides[1] < sides[2] * sides[2];
        }
    }
}

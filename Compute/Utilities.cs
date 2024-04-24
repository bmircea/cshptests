using System;
using System.Collections.Generic;

namespace Compute
{
    public class Utilities
    {
        public static double Distance(Point p1, Point p2)
        {
            double dx = p1.GetX() - p2.GetX();
            double dy = p1.GetY() - p2.GetY();
            double dz = p1.GetZ() - p2.GetZ();

            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        public static double DotProduct(Point v1, Point v2)
        {
            return v1.GetX() * v2.GetX() + v1.GetY() * v2.GetY() + v1.GetZ() * v2.GetZ();
        }
    }
}

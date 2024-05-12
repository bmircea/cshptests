namespace Compute
{
    public class Point : IEquatable<Point>
    {
        public double x, y, z;
        public Point(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public double GetX() { return x; }
        public double GetY() { return y; }
        public double GetZ() { return z; }

        public void SetX(double nx) { x = nx; }
        public void SetY(double ny) { y = ny; }
        public void SetZ(double nz) { z = nz; }

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static double operator * (Point a, Point b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        public void Copy(Point toCopy)
        {
            x = toCopy.x;
            y = toCopy.y;
            z = toCopy.z;
        }

        // Distance between two points
        public static double operator %(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow(b.x - a.x, 2) + Math.Pow(b.y - a.y, 2) + Math.Pow(b.z - a.z, 2));
        }

        // Deep comparison
        public static bool operator ==(Point a, Point b)
        {
            return (a.x == b.x) && (a.y == b.y) && (a.z == b.z);
        }

        public static bool operator !=(Point a, Point b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y, z);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Point);
        }

        public bool Equals(Point? other)
        {
            if (other is null) return false;

            if (Object.ReferenceEquals(this, other)) return true;
            
            if (this.GetType() != other.GetType()) return false;


            return (x == other.x) && (y == other.y) && (z == other.z); 

        }
    }

}
namespace Compute
{
    public class Point
    {
        private float x, y, z;
        public Point(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public float GetX() { return x; }
        public float GetY() { return y; }
        public float GetZ() { return z; }
    }

}
using System;
using System.Collections.Generic;

namespace Compute
{
    public class Cube
    {
        private List<Point> vertices;
        private Point center;
        private float edgeLength;

        public Cube() { }
        public Cube(Point point, float length)
        {
            this.center = point;
            this.edgeLength = length;
            vertices = new List<Point>();

            // lower face
            Point A1 = new Point(center.GetX() - edgeLength / 2, center.GetY() + edgeLength / 2, center.GetZ() - edgeLength / 2);
            Point B1 = new Point(center.GetX() + edgeLength / 2, center.GetY() + edgeLength / 2, center.GetZ() - edgeLength / 2);
            Point C1 = new Point(center.GetX() + edgeLength / 2, center.GetY() - edgeLength / 2, center.GetZ() - edgeLength / 2);
            Point D1 = new Point(center.GetX() - edgeLength / 2, center.GetY() - edgeLength / 2, center.GetZ() - edgeLength / 2);
            
            // upper face
            Point A2 = new Point(center.GetX() - edgeLength / 2, center.GetY() + edgeLength / 2, center.GetZ() + edgeLength / 2);
            Point B2 = new Point(center.GetX() + edgeLength / 2, center.GetY() + edgeLength / 2, center.GetZ() + edgeLength / 2);
            Point C2 = new Point(center.GetX() + edgeLength / 2, center.GetY() - edgeLength / 2, center.GetZ() + edgeLength / 2);
            Point D2 = new Point(center.GetX() - edgeLength / 2, center.GetY() - edgeLength / 2, center.GetZ() + edgeLength / 2);

            vertices.Add(A1);
            vertices.Add(A2);
            vertices.Add(B1);
            vertices.Add(B2);
            vertices.Add(C1);
            vertices.Add(C2);
            vertices.Add(D1);
            vertices.Add(D2);
        }
        public Cube(List<Point> points)
        {
            this.vertices = points;
        }

        public List<Point> GetVertices() { return vertices; }
        public Point GetCenter() { return center; }
        public float GetEdgeLength() { return edgeLength; }
    }
}

using System;
using System.Collections.Generic;

namespace Compute
{
    public class Cube
    {
        private List<Point>? vertices;
        private Point? center;
        private double? edgeLength;

        public struct BoundingBox
        {
            public double minX, maxX;
            public double minY, maxY;
            public double minZ, maxZ;

        }

        public Cube() { }
        public Cube(Point point, double length)
        {
            this.center = point;
            this.edgeLength = length;
            vertices = [];

            // lower face
            Point A1 = new Point(point.GetX() - length / 2, point.GetY() + length / 2, point.GetZ() - length / 2);
            Point B1 = new Point(point.GetX() + length / 2, point.GetY() + length / 2, point.GetZ() - length / 2);
            Point C1 = new Point(point.GetX() + length / 2, point.GetY() - length / 2, point.GetZ() - length / 2);
            Point D1 = new Point(point.GetX() - length / 2, point.GetY() - length / 2, point.GetZ() - length / 2);
            
            // upper face
            Point A2 = new Point(point.GetX() - length / 2, point.GetY() + length / 2, point.GetZ() + length / 2);
            Point B2 = new Point(point.GetX() + length / 2, point.GetY() + length / 2, point.GetZ() + length / 2);
            Point C2 = new Point(point.GetX() + length / 2, point.GetY() - length / 2, point.GetZ() + length / 2);
            Point D2 = new Point(point.GetX() - length / 2, point.GetY() - length / 2, point.GetZ() + length / 2);

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

        public List<Point>? GetVertices() { return vertices; }
        public Point? GetCenter() 
        {
            if (vertices != null && vertices.Count > 0)
            {
                Point c = vertices.Aggregate(new Point(0.0, 0.0, 0.0), (total, next) => total + next);
                c.SetX(c.GetX() / 8.0);
                c.SetY(c.GetY() / 8.0);
                c.SetZ(c.GetZ() / 8.0);
                if (center == null)
                {
                    return center;
                }
                center.Copy(c);
                return center;
            }
            return center; 
        }
        public double GetEdgeLength() 
        {
            if (!edgeLength.HasValue)
            {
                if (vertices != null && vertices.Count > 0)
                {
                    edgeLength = vertices[1] % vertices[0];
                }
                else 
                { 
                    return 0.0;
                }
            }
            return edgeLength.Value;
        }

        public BoundingBox? GetBoundingBox()
        {
            if (vertices == null)
                return null;

            BoundingBox bb = new BoundingBox();

            foreach (Point p in vertices)
            {
                if (p.GetX() > bb.maxX) bb.maxX = p.GetX();
                if (p.GetX() < bb.minX) bb.minX = p.GetX();  
                if (p.GetY() < bb.minY) bb.minY = p.GetY();
                if (p.GetY() > bb.maxY) bb.maxY = p.GetY();
                if (p.GetZ() > bb.maxZ) bb.maxZ = p.GetZ();
                if (p.GetZ() < bb.minZ) bb.minZ = p.GetZ();
            }

            return bb;
        }

        public bool IsPointInsideBoundingBox(Point p)
        {
            BoundingBox? bb = this.GetBoundingBox();

            return p.GetX() <= bb.Value.maxX && p.GetX() >= bb.Value.minX
                && p.GetY() <= bb.Value.maxY && p.GetY() >= bb.Value.minY
                && p.GetZ() <= bb.Value.maxZ && p.GetZ() >= bb.Value.minZ;
        }

        
    }
}

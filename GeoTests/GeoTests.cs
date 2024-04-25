using System.Data;
using NUnit.Framework;

namespace Compute
{
    
    public class Tests
    {
        public enum DistType{ 
            euclidean,
            manhattan,
            chebysev
        }
        [SetUp]
        public void Setup()
        {
            Point a = new Point(10, 10, 10);
        }

        [Test]
        public void PointCreation()
        {
            Point a = new Point(5, 8, 10);

            Assert.IsNotNull(a);
            Assert.That(a.GetX(), Is.EqualTo(5));
            Assert.That(a.GetY(), Is.EqualTo(8));
            Assert.That(a.GetZ(), Is.EqualTo(10));
        }

        [Test]
        public void DotProduct()
        {
            Point a = new Point(3, 8, 12);
            Point b = new Point(5, 1, 2);

            double c = a * b;

            Assert.That(c, Is.EqualTo(47));
        }

        [Test]
        public void NullCubeCreation()
        {
            Cube c = new Cube();

            Assert.That(c.GetVertices(), Is.Null);
            Assert.That(c.GetBoundingBox().HasValue, Is.False);
            Assert.That(c.GetEdgeLength(), Is.EqualTo(0.0));
            Assert.That(c.GetCenter(), Is.Null);
        }

        [Test]
        public void CubeCreationVertexList()
        {
            List<Point> vertices = [
                new(0, 0, 1), new(2, 0, 1), new(2, 2, 1), new(0, 2, 1), new(0, 0, 3), new(2, 0, 3), new(2, 2, 3), new(0, 2, 3)];
            Cube c = new Cube(vertices);

            Assert.That(c.GetVertices(), Is.Not.Null);
            Assert.That(c.GetBoundingBox().HasValue, Is.True);
            Assert.That(c.GetEdgeLength(), Is.EqualTo(2.0));
        }

        
        public double ComputeDistance(Point a, Point b, DistType type = DistType.euclidean)
        {
            switch (type)
            {
                case DistType.euclidean:
                    return a % b;
                case DistType.manhattan:
                    return Math.Abs(a.GetX() - b.GetX()) + Math.Abs(a.GetY() - b.GetY()) + Math.Abs(a.GetZ() - b.GetZ());
                case DistType.chebysev:
                    return Math.Max(Math.Max(Math.Abs(a.GetX() - b.GetX()), Math.Abs(a.GetY() - b.GetY())), Math.Abs(a.GetZ() - b.GetZ()));
                default:
                    return 0.0;
            }
        }


        public bool IsPointInsideCubeBB(Point p, double tol, List<Point> vertexList, double?  length = null)
        {
            Cube c;
            try { 
                if (vertexList.Count == 1)
                {
                    if (length != null)
                    {
                        c = new Cube(vertexList[0], length.Value);
                    }
                    else throw new Exception("Length is null");
                }
                else if (vertexList.Count == 8)
                {
                    c = new Cube(vertexList);
                }
                else throw new Exception("Invalid number of vertices");
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return c.IsPointInsideBoundingBox(p);
        }


        // =============================================
        // Equivalence partitioning

        [Test]
        public void EquivalencePartitioningBoundingBox()
        {
            var testBatch = new List<(Point, double, List<Point>, double?, bool)>
            {
                (new Point(0, 0, 0), 0.0, [], null, false),
                (new Point(0, 0, 0), 0.0, [new(1, 1, 1)], null, false),
                (new Point(1, 1, 1), 0.1, [new(1, 1, 1)], 2, true),
                (new Point(5, 7, 6.5), 0.1, [new(1, 1, 1)], 2, false),
                (new Point(0, 0, 0), 0.0, [new(1, 5, 7), new(2, 2, 3), new(3, 4, 2)], null, false),
                (new Point(0, 0, 1), 0.2, [new(-1, -1, 0), new(1, -1, 0), new(1, 1, 0), new(-1, 1, 0), new(-1, -1, 3), new(1, -1, 3), new(1, 1, 3), new(-1, 1, 3)], 2, true),
                (new Point(10, 15, 11), 0.2, [new(-1, -1, 0), new(1, -1, 0), new(1, 1, 0), new(-1, 1, 0), new(-1, -1, 3), new(1, -1, 3), new(1, 1, 3), new(-1, 1, 3)], 2, false),
                (new Point(0, 0, 0), 0.0, [new(-1, -1, 0), new(1, -1, 0), new(1, 1, 0), new(-1, 1, 0), new(-1, -1, 3), new(1, -1, 3), new(1, 1, 3), new(-1, 1, 3), new(2, 2, 2)], 0, false)   
            };

            foreach (var t in testBatch)
            {
                if (t.Item5)
                    Assert.That(IsPointInsideCubeBB(t.Item1, t.Item2, t.Item3, t.Item4), Is.True);
                else
                    Assert.That(IsPointInsideCubeBB(t.Item1, t.Item2, t.Item3, t.Item4), Is.False);
            } 
                
        }


        // ============================================
        // Boundary value analysis
        [Test]
        public void BoundaryTestingBoundingBox()
        { 
            var testBatch = new List<(Point, double, List<Point>, double?, bool)>
            {
                (new Point(0, 0, 0), 0.0, [], null, false),
                (new Point(1, 1, 1), 0.1, [new(1, 1, 1)], 2, true),
                (new Point(0, 0, 0), 0.0, [new(-1, -1, 0), new(1, -1, 0), new(1, 1, 0), new(-1, 1, 0), new(-1, -1, 3)], null, false),
                (new Point(0, 0, 1), 0.2, [new(-1, -1, 0), new(1, -1, 0), new(1, 1, 0), new(-1, 1, 0), new(-1, -1, 3), new(1, -1, 3), new(1, 1, 3), new(-1, 1, 3)], 2, true),
                (new Point(0, 0, 0), 0.0, [new(-1, -1, 0), new(1, -1, 0), new(1, 1, 0), new(-1, 1, 0), new(-1, -1, 3), new(1, -1, 3), new(1, 1, 3), new(-1, 1, 3), new(5, 5, 5)], 0, false)   
            };
            foreach (var t in testBatch)
            {
                if (t.Item5)
                    Assert.That(IsPointInsideCubeBB(t.Item1, t.Item2, t.Item3, t.Item4), Is.True);
                else
                    Assert.That(IsPointInsideCubeBB(t.Item1, t.Item2, t.Item3, t.Item4), Is.False);
            } 
        }

        // =============================================
        // Equivalence partitioning

        [Test]
        public void EquivalencePartitioningDistance()
        {
            var testBatch = new List<(Point, Point, DistType, double)>
            {
                (new Point(1, 1, 1), new Point(0, 0, 0), DistType.euclidean, Math.Sqrt(3)),
                (new Point(1, 1, 1), new Point(0, 0, 0), DistType.manhattan, 3),
                (new Point(1, 1, 1), new Point(0, 0, 0), DistType.chebysev, 1),
                (new Point(1, 1, 1), new Point(0, 0, 0), (DistType)5, 0)
            };

            foreach (var t in testBatch)
            {
                Assert.That(ComputeDistance(t.Item1, t.Item2, t.Item3), Is.EqualTo(t.Item4));
            } 
                
        }

        // ============================================
        // Boundary value analysis
        [Test]
        public void BoundaryTestingDistance()
        { 
            var testBatch = new List<(Point, Point, DistType, double)>
            {
                (new Point(2, 2, 2), new Point(2, 2, 2), DistType.euclidean, 0),
                (new Point(2, 2, 2), new Point(2, 2, 2), DistType.manhattan, 0),
                (new Point(2, 2, 2), new Point(2, 2, 2), DistType.chebysev, 0),
                (new Point(0, 0.001, 0), new Point(0.0001, 0, 0), DistType.euclidean, Math.Sqrt(Math.Pow(10, -6) + Math.Pow(10, -8))),
                (new Point(0, 0.0001, 0), new Point(0.00001, 0, 0), DistType.manhattan, 0.00011),
                (new Point(0, 0.00001, 0), new Point(0.000001, 0, 0), DistType.chebysev, 0.00001),
                 
            };
            foreach (var t in testBatch)
            {
                Assert.That(ComputeDistance(t.Item1, t.Item2, t.Item3), Is.EqualTo(t.Item4));
            } 
        }

    }
}

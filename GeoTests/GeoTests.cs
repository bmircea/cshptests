using System.Data;
using NUnit.Framework;

namespace Compute
{

    public class Tests
    {
        public enum DistType {
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
        public void DotProductTest()
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

            //Point? cen = c.GetCenter();

            //if (cen != null)
            //{
            //    Assert.That(cen, Is.EqualTo(new Point(1, 1, 2)));
            //}
        }

        [Test]
        public void CubeCreationCenter()
        {
            Point center = new Point(0, 0, 0);
            Cube c = new Cube(center, 2);

            Assert.That(c.GetVertices().Count, Is.EqualTo(8));
            Assert.That(c.GetVertices()[0], Is.EqualTo(new Point(-1, 1, -1)));
            Assert.That(c.GetVertices()[2], Is.EqualTo(new Point(1, 1, -1)));
            Assert.That(c.GetVertices()[4], Is.EqualTo(new Point(1, -1, -1)));
            Assert.That(c.GetVertices()[6], Is.EqualTo(new Point(-1, -1, -1)));
            Assert.That(c.GetVertices()[1], Is.EqualTo(new Point(-1, 1, 1)));
            Assert.That(c.GetVertices()[3], Is.EqualTo(new Point(1, 1, 1)));
            Assert.That(c.GetVertices()[5], Is.EqualTo(new Point(1, -1, +1)));
            Assert.That(c.GetVertices()[7], Is.EqualTo(new Point(-1, -1, +1)));



            Assert.That(c.IsPointInsideBoundingBox(center), Is.True);
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


        public bool IsPointInsideCubeBB(Point p, double tol, List<Point> vertexList, double? length = null)
        {
            Cube c;
            try
            {
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return c.IsPointInsideBoundingBox(p);
        }

        [Test]
        public void UtilitiesTestDotProductTest()
        {
            Point point1 = new Point(2, 3, 5);
            Point point2 = new Point(5, 3, 2);
            double expectedResult = 2 * 5 + 3 * 3 + 5 * 2;

            Assert.That(Utilities.DotProduct(point1, point2), Is.EqualTo(expectedResult));
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



        [Test]
        public void MiscTests()
        {
            Point a = new Point(1, 1, 1);
            a.SetX(3);
            a.SetY(2);
            a.SetZ(3);

            Point d = new Point(3, 2, 3);

            Assert.That(a, Is.EqualTo(d));

            Point b = new Point(1, 1, 1);

            Assert.That(a + b == new Point(4, 3, 4), Is.True);
            Assert.That(a != b, Is.True);
            a.Copy(b);

            Assert.That(a, Is.EqualTo(b));


        }

        [Test]
        public void TriangleTests()
        {
            Triangle t = new Triangle(new Point(1, 1, 1), new Point(2, 2, 2), new Point(3, 3, 3));
            Triangle t2 = new Triangle(new Point(2, 4, 6), new Point(6, 0, 1), new Point(9, 2, 1));
            Triangle t3 = new Triangle(new Point(0, 0, 0), new Point(3, 0, 0), new Point(0, 2, 0));

            Assert.IsFalse(t.IsEquilateral());
            Assert.IsTrue(t.IsIsosceles());
            Assert.IsFalse(t.IsRightAngled());

            Assert.IsTrue(t.IsObtuse());

            Assert.IsFalse(t2.IsIsosceles());

            Assert.IsFalse(t2.IsRightAngled());

            Assert.IsTrue(t3.IsRightAngled());

        }

        [Test, Category("Normal")]
        public void QuadriTests()
        {
            Quadrilateral q = new Quadrilateral(new Point(1, 1, 1), new Point(2, 2, 2), new Point(3, 3, 3), new Point(4, 4, 4));

            Quadrilateral q2 = new Quadrilateral(new Point(2, 2, 2), new Point(3, 3, 3), new Point(1, 1, 1), new Point(4, 4, 4));

            Quadrilateral q3 = new Quadrilateral(new Point(1, 1, 4), new Point(4, 4, 4), new Point(6, 5, 4), new Point(3, 2, 4));

            Quadrilateral q4 = new Quadrilateral(new Point(5, 9, 0), new Point(1, 2, 6), new Point(2, 0, 9), new Point(3, 2, 6));

            q2.CheckParallelSides();

            q.CheckParallelSides();

            q.CheckOppositeSidesEqual();

            q3.CheckParallelSides();

            q4.CheckParallelSides();

            q4.CheckOppositeSidesEqual();


            Assert.IsFalse(q4.IsSquare());

            Assert.IsFalse(q4.IsRectangle());

            Assert.IsFalse(q2.IsParallelogram());

            Assert.IsTrue(q.IsTrapezoid());

            Assert.IsFalse(q.IsParallelogram());

            Assert.IsFalse(q.AreAllAngles90Degrees());

            Assert.IsFalse(q.IsRectangle());

            Assert.IsFalse(q.IsSquare());

            Assert.IsFalse(q.IsRhombus());

        }

        private Cube cube;

        //[SetUp]
        //public void Setup()
        //{
        //    cube = new Cube(new Point(0, 0, 0), 2);
        //}

        // Test for equivalence class 1: Points that are calculated using the Euclidean distance
        [Test, Category("AI")]
        public void ComputeDistance_Euclidean_ReturnsCorrectDistance()
        {
            var pointA = new Point(0, 0, 0);
            var pointB = new Point(1, 1, 1);
            var expectedDistance = Math.Sqrt(3);
            Assert.AreEqual(expectedDistance, ComputeDistance(pointA, pointB, DistType.euclidean));
        }

        // Test for equivalence class 2: Points that are calculated using the Manhattan distance
        [Test, Category("AI")]
        public void ComputeDistance_Manhattan_ReturnsCorrectDistance()
        {
            var pointA = new Point(0, 0, 0);
            var pointB = new Point(1, 1, 1);
            var expectedDistance = 3;
            Assert.AreEqual(expectedDistance, ComputeDistance(pointA, pointB, DistType.manhattan));
        }

        // Test for equivalence class 3: Points that are calculated using the Chebyshev distance
        [Test, Category("AI")]
        public void ComputeDistance_Chebyshev_ReturnsCorrectDistance()
        {
            var pointA = new Point(0, 0, 0);
            var pointB = new Point(1, 1, 1);
            var expectedDistance = 1;
            Assert.AreEqual(expectedDistance, ComputeDistance(pointA, pointB, DistType.chebysev));
        }

        // Test for equivalence class 1: Points that are inside the bounding box
        [Test, Category("AI")]
        public void IsPointInsideBoundingBox_Inside_ReturnsTrue()
        {
            var point = new Point(0.5, 0.5, 0.5);
            Assert.IsTrue(cube.IsPointInsideBoundingBox(point));
        }

        // Test for equivalence class 2: Points that are outside the bounding box
        [Test, Category("AI")]
        public void IsPointInsideBoundingBox_Outside_ReturnsFalse()
        {
            var point = new Point(2.5, 2.5, 2.5);
            Assert.IsFalse(cube.IsPointInsideBoundingBox(point));
        }

        // Test for equivalence class 3: Points that are exactly on the boundary of the bounding box
        [Test, Category("AI")]
        public void IsPointInsideBoundingBox_OnBoundary_ReturnsTrue()
        {
            var point = new Point(1, 1, 1);
            Assert.IsTrue(cube.IsPointInsideBoundingBox(point));
        }

    }
}

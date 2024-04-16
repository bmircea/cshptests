using System.Data;
using NUnit.Framework;

namespace Compute
{
    
    public class Tests
    {
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
            //Assert.That(c.GetCenter(), Is.EqualTo(new Point(1.0, 1.0, 2.0)));
        }


    }
}

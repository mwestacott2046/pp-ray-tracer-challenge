using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using RayTracer.Shapes;

namespace RayTracer.UnitTests
{
    [TestFixture]
    public class CubeTests
    {
        // ReSharper disable once InconsistentNaming
        public static object[] CubeIntersectsCases =
        {
            new object[] {"+x", new Point(5, 0.5, 0), new Vector(-1, 0, 0), 4, 6},
            new object[] {"-x", new Point(-5, 0.5, 0), new Vector(1, 0, 0), 4, 6},
            new object[] {"+y", new Point(0.5, 5, 0), new Vector(0, -1, 0), 4, 6},
            new object[] {"-y", new Point(0.5, -5, 0), new Vector(0, 1, 0), 4, 6},
            new object[] {"+z", new Point(0.5, 0, 5), new Vector(0, 0, -1), 4, 6},
            new object[] {"-z", new Point(0.5, 0, -5), new Vector(0, 0, 1), 4, 6},
            new object[] {"inside", new Point(0, 0.5, 0), new Vector(0, 0, 1), -1, 1},
        };


        [TestCaseSource(nameof(CubeIntersectsCases))]
        public void RayIntersectsCube(string desc ,Point origin, Vector direction, int t1, int t2)
        {
            var c = new Cube();
            var ray = new Ray(origin, direction);

            var xs = c.Intersects(ray);
            
            Assert.AreEqual(2, xs.Length);
            Assert.AreEqual(t1,xs[0].T);
            Assert.AreEqual(t2, xs[1].T);
        }



        public static object[] CubeMissesCases =
        {
            new object[] { new Point(-2, 0, 0),  new Vector(0.2673, 0.5345, 0.8018) },
            new object[] { new Point(0, -2, 0),  new Vector(0.8018, 0.2673, 0.5345) },
            new object[] { new Point(0, 0, -2),  new Vector(0.5345, 0.8018, 0.2673) },
            new object[] { new Point(2, 0, 2),  new Vector(0, 0, -1) },
            new object[] { new Point(0, 2, 2),  new Vector(0, -1, 0) },
            new object[] { new Point(2, 2, 0),  new Vector(-1, 0, 0) },
        };


        [TestCaseSource(nameof(CubeMissesCases))]
        public void RayMissesCube(Point origin, Vector direction)
        {
            var c = new Cube();
            var ray = new Ray(origin, direction);

            var xs = c.Intersects(ray);

            Assert.AreEqual(0, xs.Length);
        }

        public static object[] SurfaceNormalCases =
        {
            new object[] {new Point(1, 0.5, -0.8), new Vector(1, 0, 0)},
            new object[] {new Point(-1, -0.2, 0.9), new Vector(-1, 0, 0)},
            new object[] {new Point(-0.4, 1, -0.1), new Vector(0, 1, 0)},
            new object[] {new Point(0.3, -1, -0.7), new Vector(0, -1, 0)},
            new object[] {new Point(-0.6, 0.3, 1), new Vector(0, 0, 1)},
            new object[] {new Point(0.4, 0.4, -1), new Vector(0, 0, -1)},
            new object[] {new Point(1, 1, 1), new Vector(1, 0, 0)},
            new object[] {new Point(-1, -1, -1), new Vector(-1, 0, 0)},
        };

        [TestCaseSource(nameof(SurfaceNormalCases))]
        public void NormalOnSurfaceOfCube(Point point, Vector expectedNormal)
        {
            var c = new Cube();
            var normal = c.NormalAt(point);

            Assert.AreEqual(expectedNormal, normal);

        }


    }
}

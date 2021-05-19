using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using NUnit.Framework;
using RayTracer.Shapes;

namespace RayTracer.UnitTests
{
    [TestFixture]
    public class CylinderTests
    {
        public static object[] RayMissesCylinderCases =
        {
            new object[] {new Point(1, 0, 0), new Vector(0, 1, 0)},
            new object[] {new Point(0, 0, 0), new Vector(0, 1, 0)},
            new object[] {new Point(0, 0, -5), new Vector(1, 1, 1)},
        };

        [TestCaseSource(nameof(RayMissesCylinderCases))]
        public void RayMissesCylinder(Point origin, Vector direction)
        {
            var cylinder = new Cylinder();
            var dir = direction.Normalize().ToVector();
            var r = new Ray(origin, dir);

            var xs = cylinder.Intersects(r);
            Assert.AreEqual(0, xs.Length);
        }


        public static object[] RayHitsCylinderCases =
        {
            new object[] {new Point(1, 0, -5), new Vector(0, 0, 1), 5.0, 5.0},
            new object[] {new Point(0, 0, -5), new Vector(0, 0, 1), 4.0, 6.0},
            new object[] {new Point(0.5, 0, -5), new Vector(0.1, 1, 1), 6.80798, 7.08872},

        };

        [TestCaseSource(nameof(RayHitsCylinderCases))]
        public void RayHitsCylinder(Point origin, Vector direction, double t0, double t1)
        {
            var cylinder = new Cylinder();
            var dir = direction.Normalize().ToVector();
            var r = new Ray(origin, dir);

            var xs = cylinder.Intersects(r);
            Assert.AreEqual(2, xs.Length);
            Assert.True(DoubleUtils.DoubleEquals(  t0,xs[0].T));
            Assert.True(DoubleUtils.DoubleEquals( t1, xs[1].T));

        }


        public static object[] NormalOnCylinderCases =
        {
            new object[] {new Point(1, 0, 0), new Vector(1, 0, 0)},
            new object[] {new Point(0, 5, -1), new Vector(0, 0, -1)},
            new object[] {new Point(0, -2, 1), new Vector(0, 0, 1)},
            new object[] {new Point(-1, 1, 0), new Vector(-1, 0, 0)},
        };


        [TestCaseSource(nameof(NormalOnCylinderCases))]
        public void NormalOnCylinder(Point point, Vector normal)
        {
            var cyl = new Cylinder();
            var n = cyl.NormalAt(point);
            Assert.AreEqual(normal,n);
        }

        [Test]
        public void DefaultMinMaxY ()
        {
            var cyl = new Cylinder();

            Assert.AreEqual(Double.MinValue, cyl.Minimum);
            Assert.AreEqual(Double.MaxValue, cyl.Maximum);
        }


        /*
        |   | point             | direction         | count |
​ 	    | 1 | point(0, 1.5, 0)  | vector(0.1, 1, 0) | 0     |
​ 	    | 2 | point(0, 3, -5)   | vector(0, 0, 1)   | 0     |
​ 	    | 3 | point(0, 0, -5)   | vector(0, 0, 1)   | 0     |
​ 	    | 4 | point(0, 2, -5)   | vector(0, 0, 1)   | 0     |
​ 	    | 5 | point(0, 1, -5)   | vector(0, 0, 1)   | 0     |
​ 	    | 6 | point(0, 1.5, -2) | vector(0, 0, 1)   | 2     |
         */
        public static object[] IntersectingConstrainedCylinderCases =
        {
            new object[] {new Point(0, 1.5, 0), new Vector(0.1, 1, 0), 0},
            new object[] {new Point(0, 3, -5), new Vector(0, 0, 1), 0},
            new object[] {new Point(0, 0, -5), new Vector(0, 0, 1), 0},
            new object[] {new Point(0, 2, -5), new Vector(0, 0, 1), 0},
            new object[] {new Point(0, 1, -5), new Vector(0, 0, 1), 0},
            new object[] {new Point(0, 1.5, -2), new Vector(0, 0, 1), 2},
        };

        [TestCaseSource(nameof(IntersectingConstrainedCylinderCases))]
        public void IntersectingConstrainedCylinder(Point point, Vector direction, int expectedCount)
        {
            var cylinder = new Cylinder {Maximum = 2, Minimum = 1};

            var dir = direction.Normalize().ToVector();
            var r = new Ray(point, dir);

            var xs = cylinder.Intersects(r);
            Assert.AreEqual(expectedCount, xs.Length);
        }

        [Test]
        public void DefaultClosedValue()
        {
            var cyl = new Cylinder();
            Assert.False(cyl.Closed);
        }
    }
}

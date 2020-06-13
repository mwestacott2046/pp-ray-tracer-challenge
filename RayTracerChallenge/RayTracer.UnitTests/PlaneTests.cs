using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RayTracer.Shapes;

namespace RayTracer.UnitTests
{
    [TestFixture]
    public class PlaneTests
    {
        [Test]
        public void TheNormalOfAPlaneIsSameEverywhere()
        {
            var plane = new Plane();

            var n1 = plane.NormalAt(new Point(0,0,0));
            var n2 = plane.NormalAt(new Point(10, 0, -10));
            var n3 = plane.NormalAt(new Point(-5, 0, 150));

            var expectedNormal = new Vector(0,1,0);

            Assert.AreEqual(expectedNormal, n1);
            Assert.AreEqual(expectedNormal, n2);
            Assert.AreEqual(expectedNormal, n3);
        }

        [Test]
        public void IntersectWithRayParallelToPlane()
        {
            var p = new Plane();
            var r = new Ray(new Point(0,10,0), new Vector(0,0,1));
            var result = p.Intersects(r);

            Assert.IsFalse(result.Any());
        }

        [Test]
        public void IntersectWitCoplanarRay()
        {
            var p = new Plane();
            var r = new Ray(new Point(0, 0, 0), new Vector(0, 0, 1));
            var result = p.Intersects(r);

            Assert.IsFalse(result.Any());
        }

        [Test]
        public void IntersectsFromAbove()
        {
            var p = new Plane();
            var r = new Ray(new Point(0, 1, 0), new Vector(0, -1, 0));
            var result = p.Intersects(r);

            Assert.AreEqual(1,result.Length);
            Assert.AreEqual(1,result[0].T);
            Assert.AreEqual(p, result[0].Object);
        }

        [Test]
        public void IntersectsFromBelow()
        {
            var p = new Plane();
            var r = new Ray(new Point(0, -1, 0), new Vector(0, 1, 0));
            var result = p.Intersects(r);

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(1, result[0].T);
            Assert.AreEqual(p, result[0].Object);
        }

    }
}

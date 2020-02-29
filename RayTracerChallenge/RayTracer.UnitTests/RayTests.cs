using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using NUnit.Framework;

namespace RayTracer.UnitTests
{
    [TestFixture]
    public class RayTests
    {
        [Test]
        public void CreateAndQueryRay()
        {
            var origin = new Point(1,2,3);
            var direction = new Vector(4,5,6);

            var ray = new Ray(origin, direction);

            Assert.AreEqual(origin, ray.Origin);
            Assert.AreEqual(direction, ray.Direction);
        }

        [Test]
        public void ComputePointFromDistance()
        {
            var ray = new Ray(new Point(2,3,4), new Vector(1,0,0));

            Assert.AreEqual(new Point(2, 3, 4), ray.Position(0));
            Assert.AreEqual(new Point(3, 3, 4), ray.Position(1));
            Assert.AreEqual(new Point(1, 3, 4), ray.Position(-1));
            Assert.AreEqual(new Point(4.5, 3, 4), ray.Position(2.5));
        }

        [Test]
        public void RayIntersectsSphereAtTwoPoints()
        {
            var ray = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));

            var sphere = new Sphere();

            var xs = ray.Intersects(sphere);

            Assert.AreEqual(2, xs.Length);
            Assert.AreEqual( 4.0, xs[0].T);
            Assert.AreEqual(6.0, xs[1].T);
        }

        [Test]
        public void RayIntersectsSphereAtTangent()
        {
            var ray = new Ray(new Point(0, 1, -5), new Vector(0, 0, 1));

            var sphere = new Sphere();

            var xs = ray.Intersects(sphere);

            Assert.AreEqual(2, xs.Length);
            Assert.AreEqual(5.0, xs[0].T);
            Assert.AreEqual(5.0, xs[1].T);
        }

        [Test]
        public void RayMissesSphere()
        {
            var ray = new Ray(new Point(0, 2, -5), new Vector(0, 0, 1));

            var sphere = new Sphere();

            var xs = ray.Intersects(sphere);

            Assert.AreEqual(0, xs.Length);
        }

        [Test]
        public void RayOriginatesInsideSphere()
        {
            var ray = new Ray(new Point(0, 0, 0), new Vector(0, 0, 1));

            var sphere = new Sphere();

            var xs = ray.Intersects(sphere);

            Assert.AreEqual(2, xs.Length);
            Assert.AreEqual(-1.0, xs[0].T);
            Assert.AreEqual(1.0, xs[1].T);
        }

        [Test]
        public void SphereIsBehindARay()
        {
            var ray = new Ray(new Point(0, 0, 5), new Vector(0, 0, 1));

            var sphere = new Sphere();

            var xs = ray.Intersects(sphere);

            Assert.AreEqual(2, xs.Length);
            Assert.AreEqual(-6.0, xs[0].T);
            Assert.AreEqual(-4.0, xs[1].T);
        }

        [Test]
        public void IntersectSetsObjectOnIntersection()
        {
            var ray = new Ray(new Point(0, 0, 5), new Vector(0, 0, 1));

            var sphere = new Sphere();

            var xs = ray.Intersects(sphere);

            Assert.AreEqual(2, xs.Length);
            Assert.AreEqual(sphere, xs[0].Object);
            Assert.AreEqual(sphere, xs[1].Object);
        }

    }
}

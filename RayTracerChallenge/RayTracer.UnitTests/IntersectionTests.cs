using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace RayTracer.UnitTests
{
    [TestFixture]
    public class IntersectionTests
    {
        [Test]
        public void IntersectionEncapsulatesTAndObject()
        {
            var s = new Sphere();
            var intersection = new Intersection(3.5,s);

            Assert.AreEqual(3.5, intersection.T);
            Assert.AreEqual(s, intersection.Object);
        }

        [Test]
        public void AggregatingIntersections()
        {
            var s = new Sphere();
            var i1 = new Intersection(1, s);
            var i2 = new Intersection(2, s);

            var xs = Intersection.Intersections(i1, i2);

            Assert.AreEqual(1, xs[0].T);
            Assert.AreEqual(2, xs[1].T);
            
        }

        [Test]
        public void HitWhenAllIntersectionsHavePositiveT()
        {
            var s = new Sphere();

            var i1 = new Intersection(1, s);
            var i2 = new Intersection(2, s);

            var xs = Intersection.Intersections(i1, i2);

            var result = xs.Hit();

            Assert.AreEqual(i1,result);
        }

        [Test]
        public void HitWhenAllIntersectionsHaveNegativeT()
        {
            var s = new Sphere();

            var i1 = new Intersection(-1, s);
            var i2 = new Intersection(-2, s);

            var xs = Intersection.Intersections(i1, i2);

            var result = xs.Hit();

            Assert.IsNull(result);
        }

        [Test]
        public void HitWhenSomeIntersectionsHaveNegativeT()
        {
            var s = new Sphere();

            var i1 = new Intersection(1, s);
            var i2 = new Intersection(-1, s);

            var xs = Intersection.Intersections(i1, i2);

            var result = xs.Hit();

            Assert.AreEqual(i1,result);
        }

        [Test]
        public void HitAlwaysReturnLowestNonNegativeTIntersection()
        {
            var s = new Sphere();

            var i1 = new Intersection(5, s);
            var i2 = new Intersection(7, s);
            var i3 = new Intersection(-3, s);
            var i4 = new Intersection(2, s);

            var xs = Intersection.Intersections(i1, i2, i3, i4);

            var result = xs.Hit();

            Assert.AreEqual(i4, result);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using NUnit.Framework;
using RayTracer.Shapes;

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

        [Test]
        public void PreComputingStateOfAnIntersection()
        {
            var r = new Ray(new Point(0,0,-5), new Vector(0,0,1));
            var shape = new Sphere();
            var i = new Intersection(4, shape);

            var comps = i.PrepareComputations(r);

            Assert.AreEqual(i.T,comps.T);
            Assert.AreEqual(i.Object, comps.Object);
            Assert.AreEqual(new Point(0, 0, -1),comps.Point);
            Assert.AreEqual(new Vector(0,0,-1), comps.EyeV);
            Assert.AreEqual(new Vector(0, 0, -1), comps.NormalV);

        }
        // The hit, when an intersection occurs on the outside
        [Test]
        public void PrepareComputation_WhenIntersectionIsOnOutside()
        {
            var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            var shape = new Sphere();
            var i = new Intersection(4, shape);

            var comps = i.PrepareComputations(r);

            Assert.IsFalse(comps.Inside);

        }

        // The hit, when an intersection occurs on the inside
        [Test]
        public void PrepareComputation_WhenIntersectionIsOnInside()
        {
            var r = new Ray(new Point(0, 0, 0), new Vector(0, 0, 1));
            var shape = new Sphere();
            var i = new Intersection(1, shape);

            var comps = i.PrepareComputations(r);

            Assert.IsTrue(comps.Inside);

            Assert.AreEqual(new Point(0, 0, 1), comps.Point);
            Assert.AreEqual(new Vector(0, 0, -1), comps.EyeV);
            Assert.AreEqual(new Vector(0, 0, -1), comps.NormalV);

        }


        [Test]
        public void TheHitShouldOffsetThePoint()
        {
            var ray = new Ray(new Point(0,0,-5), new Vector(0,0,1));

            var shape = new Sphere();
            shape.Transform = Matrix.Translation(0,0,1);

            var i = new Intersection(5, shape);

            var comps = i.PrepareComputations(ray);

            Assert.IsTrue(comps.OverPoint.Z < -DoubleUtils.Epsilon / 2);
            Assert.IsTrue(comps.Point.Z > comps.OverPoint.Z);

        }

        [Test]
        public void PreComputingTheReflectionVector()
        {
            var shape = new Plane();
            var r = new Ray(new Point(0,1,-1), new Vector(0, -Math.Sqrt(2)/2, Math.Sqrt(2)/2));
            var i = new Intersection(Math.Sqrt(2), shape);

            var comps = i.PrepareComputations(r);

            Assert.AreEqual(new Vector(0, Math.Sqrt(2) / 2, Math.Sqrt(2) / 2), comps.ReflectV);
        }

        [Test]
        [TestCase(0, 1.0, 1.5)]
        [TestCase(1, 1.5, 2.0)]
        [TestCase(2, 2.0, 2.5)]
        [TestCase(3, 2.5, 2.5)]
        [TestCase(4, 2.5, 1.5)]
        [TestCase(5, 1.5, 1.0)]
        public void FindingN1AndN2AtIntersections(int index, double n1, double n2)
        {
            var sphereA = Sphere.GlassSphere();
            sphereA.Transform = Matrix.Scaling(2,2,2);
            sphereA.Material.RefractiveIndex = 1.5;

            var sphereB = Sphere.GlassSphere();
            sphereB.Transform = Matrix.Translation(0,0,-0.25);
            sphereB.Material.RefractiveIndex = 2.0;

            var sphereC = Sphere.GlassSphere();
            sphereC.Transform = Matrix.Translation(0, 0, 0.25);
            sphereC.Material.RefractiveIndex = 2.5;

            var ray = new Ray(new Point(0,0,-4), new Vector(0,0,1));

            var xs = new List<Intersection>
            {
                new Intersection(2, sphereA), 
                new Intersection(2.75, sphereB), 
                new Intersection(3.25, sphereC),
                new Intersection(4.75, sphereB),
                new Intersection(5.25, sphereC),
                new Intersection(6, sphereA)
            };

            var comps = xs[index].PrepareComputations(ray,xs);



            Assert.IsTrue(DoubleUtils.DoubleEquals(n1, comps.N1));
            Assert.IsTrue(DoubleUtils.DoubleEquals(n2, comps.N2));
        }

        [Test]
        public void UnderPointIsOffsetBelowSurface()
        {
            var ray = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));

            var shape = Sphere.GlassSphere();
            shape.Transform = Matrix.Translation(0,0,1);
            var i = new Intersection(5, shape);
            var xs = new List<Intersection> {i};

            var comps = i.PrepareComputations(ray, xs);

            Assert.Greater(comps.UnderPoint.Z, DoubleUtils.Epsilon / 2);
            Assert.Less(comps.Point.Z, comps.UnderPoint.Z);
        }

        [Test]
        public void SchlickUnderTotalInternalReflection()
        {
            var shape = Sphere.GlassSphere();
            var ray = new Ray(new Point(0,0,Math.Sqrt(2)/2), new Vector(0,1,0));
            var xs = new List<Intersection>
            {
                new Intersection(-Math.Sqrt(2)/2,shape),
                new Intersection(Math.Sqrt(2)/2,shape)
            };

            var comps = xs[1].PrepareComputations(ray, xs);
            var reflectance = SchlickReflectance.Schlick(comps);
            Assert.AreEqual(1.0,reflectance);
        }

        [Test]
        public void SchlickWithPerpendicularViewingAngle()
        {
            var shape = Sphere.GlassSphere();
            var ray = new Ray(new Point(0, 0, 0), new Vector(0, 1, 0));
            var xs = new List<Intersection>
            {
                new Intersection(-1, shape),
                new Intersection(1, shape)
            };

            var comps = xs[1].PrepareComputations(ray, xs);
            var reflectance = SchlickReflectance.Schlick(comps);
            Assert.IsTrue(DoubleUtils.DoubleEquals( 0.04, reflectance));
        }

        [Test]
        public void SchlickWithSmallAngleAndN2GreaterThanN1()
        {
            var shape = Sphere.GlassSphere();
            var ray = new Ray(new Point(0, 0.99, -2), new Vector(0, 0, 1));
            var xs = new List<Intersection>
            {
                new Intersection(1.8589, shape),
            };

            var comps = xs[0].PrepareComputations(ray, xs);
            var reflectance = SchlickReflectance.Schlick(comps);
            Assert.IsTrue(DoubleUtils.DoubleEquals( 0.48873, reflectance));
        }
    }


}

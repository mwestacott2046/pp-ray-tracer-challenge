using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using RayTracer.Shapes;

namespace RayTracer.UnitTests
{
    [TestFixture]
    public class SphereTests
    {
        [Test]
        public void SphereDefaultTranslation()
        {
            var sphere = new Sphere();

            Assert.AreEqual(Matrix.IdentityMatrix, sphere.Transform);
        }


        [Test]
        public void SphereSetTransform()
        {
            var sphere = new Sphere();

            var t = Matrix.Translation(2, 3, 4);
            sphere.Transform = t;
            Assert.AreEqual(t, sphere.Transform);
        }

        [Test]
        public void IntersectingScaledSphereWithARay()
        {
            var r = new Ray(new Point(0,0,-5), new Vector(0,0,1));
            var s = new Sphere();
            s.Transform = Matrix.Scaling(2, 2, 2);
            var xs = s.Intersects(r);
            Assert.AreEqual(2, xs.Length);
            Assert.AreEqual(3, xs[0].T);
            Assert.AreEqual(7, xs[1].T);
        }

        [Test]
        public void IntersectingTranslatedSphereWithARay()
        {
            var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            var s = new Sphere();
            s.Transform = Matrix.Translation(5, 0, 0);
            var xs = s.Intersects(r);
            Assert.AreEqual(0, xs.Length);
        }

        [Test]
        public void NormalOnSpherePointingInXDirection()
        {
            var s = new Sphere();
            var point = new Point(1,0,0);

            var result = s.NormalAt(point);

            Assert.AreEqual(new Vector(1,0,0), result);
        }

        [Test]
        public void NormalOnSpherePointingInYDirection()
        {
            var s = new Sphere();
            var point = new Point(0, 1, 0);

            var result = s.NormalAt(point);

            Assert.AreEqual(new Vector(0, 1, 0), result);
        }

        [Test]
        public void NormalOnSpherePointingInZDirection()
        {
            var s = new Sphere();
            var point = new Point(0, 0, 1);

            var result = s.NormalAt(point);

            Assert.AreEqual(new Vector(0, 0, 1), result);
        }

        [Test]
        public void NormalOnSphereAtNonAxialPoint()
        {
            var s = new Sphere();
            var point = new Point(Math.Sqrt(3) / 3, Math.Sqrt(3) / 3, Math.Sqrt(3) / 3);

            var result = s.NormalAt(point);

            Assert.AreEqual(new Vector(Math.Sqrt(3)/3, Math.Sqrt(3) / 3, Math.Sqrt(3) / 3), result);
        }

        [Test]
        public void NormalOnSphereIsNormalized()
        {
            var s = new Sphere();
            var point = new Point(Math.Sqrt(3) / 3, Math.Sqrt(3) / 3, Math.Sqrt(3) / 3);

            var result = s.NormalAt(point);

            Assert.AreEqual(result.Normalize(), result);
        }

        [Test]
        public void NormalOnTranslatedSphere()
        {
            var s = new Sphere
            {
                Transform = Matrix.Translation(0, 1, 0)
            };
            var point = new Point(0, 1.70711, -0.70711);

            var result = s.NormalAt(point);

            Assert.AreEqual(new Vector(0, 0.70711, -0.70711), result);
        }

        [Test]
        public void NormalOnTransformedSphere()
        {
            var s = new Sphere();
            var m1 = Matrix.Scaling(1, 0.5, 1);
            var m2 = Matrix.RotationZ(Math.PI / 5);

            s.Transform = m1; //* m2;
            var point = new Point(0, Math.Sqrt(2)/2, -Math.Sqrt(2)/2);

            var result = s.NormalAt(point);

            Assert.AreEqual(new Vector(0, 0.97014, -0.24254), result);
        }

        [Test]
        public void NormalOnRotatedSphere()
        {
            var s = new Sphere();
            var m1 = Matrix.Scaling(1, 0.5, 1);
            var m2 = Matrix.RotationZ(Math.PI);

            s.Transform = m1* m2;
            var point = new Point(0, Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2);

            var result = s.NormalAt(point);

            Assert.AreEqual(new Vector(0, 0.97014, -0.24254), result);
        }

        [Test]
        public void SphereHasDefaultMaterial()
        {
            var sphere = new Sphere();

            var expectedMaterial = new Material();
            Assert.AreEqual(expectedMaterial, sphere.Material);
        }

        [Test]
        public void SphereCanBeAssignedAMaterial()
        {
            var sphere = new Sphere();

            var material = new Material {Ambient = 1};
            sphere.Material = material;
            
            Assert.AreEqual(material, sphere.Material);
        }

        [Test]
        public void RayIntersectsSphereAtTwoPoints()
        {
            var ray = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));

            var sphere = new Sphere();

            var xs = sphere.Intersects(ray);

            Assert.AreEqual(2, xs.Length);
            Assert.AreEqual(4.0, xs[0].T);
            Assert.AreEqual(6.0, xs[1].T);
        }

        [Test]
        public void RayIntersectsSphereAtTangent()
        {
            var ray = new Ray(new Point(0, 1, -5), new Vector(0, 0, 1));

            var sphere = new Sphere();

            var xs = sphere.Intersects(ray);

            Assert.AreEqual(2, xs.Length);
            Assert.AreEqual(5.0, xs[0].T);
            Assert.AreEqual(5.0, xs[1].T);
        }

        [Test]
        public void RayMissesSphere()
        {
            var ray = new Ray(new Point(0, 2, -5), new Vector(0, 0, 1));

            var sphere = new Sphere();

            var xs = sphere.Intersects(ray);

            Assert.AreEqual(0, xs.Length);
        }

        [Test]
        public void RayOriginatesInsideSphere()
        {
            var ray = new Ray(new Point(0, 0, 0), new Vector(0, 0, 1));

            var sphere = new Sphere();

            var xs = sphere.Intersects(ray);

            Assert.AreEqual(2, xs.Length);
            Assert.AreEqual(-1.0, xs[0].T);
            Assert.AreEqual(1.0, xs[1].T);
        }

        [Test]
        public void SphereIsBehindARay()
        {
            var ray = new Ray(new Point(0, 0, 5), new Vector(0, 0, 1));

            var sphere = new Sphere();

            var xs = sphere.Intersects(ray);

            Assert.AreEqual(2, xs.Length);
            Assert.AreEqual(-6.0, xs[0].T);
            Assert.AreEqual(-4.0, xs[1].T);
        }

        [Test]
        public void IntersectSetsObjectOnIntersection()
        {
            var ray = new Ray(new Point(0, 0, 5), new Vector(0, 0, 1));

            var sphere = new Sphere();

            var xs = sphere.Intersects(ray);

            Assert.AreEqual(2, xs.Length);
            Assert.AreEqual(sphere, xs[0].Object);
            Assert.AreEqual(sphere, xs[1].Object);
        }


    }
}

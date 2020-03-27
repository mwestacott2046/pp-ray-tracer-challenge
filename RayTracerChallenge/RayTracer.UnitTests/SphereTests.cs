using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

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
            var xs = r.Intersects(s);
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
            var xs = r.Intersects(s);
            Assert.AreEqual(0, xs.Length);
        }

    }
}

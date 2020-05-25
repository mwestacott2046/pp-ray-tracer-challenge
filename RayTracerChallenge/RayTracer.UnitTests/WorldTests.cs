using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using NUnit.Framework;

namespace RayTracer.UnitTests
{
    [TestFixture]
    public class WorldTests
    {
        [Test]
        public void CreateAWorld()
        {
            var world = new World();
            
            Assert.Null(world.LightSource);
            Assert.IsEmpty(world.SceneObjects);
        }

        [Test]
        public void DefaultWorld()
        {
            var expectedLight = new Light(new Point(-10, 10, -10), new Colour(1, 1, 1));

            var material1 = new Material {Colour = new Colour(0.8, 1, 0.6), Diffuse = 0.7, Specular = 0.2};
            var expectedSphere1 = new Sphere {Material = material1};
            var expectedSphere2 = new Sphere { Transform = Matrix.Scaling(0.5, 0.5, 0.5) };

            var world = World.DefaultWorld;

            Assert.NotNull(world.LightSource);
            Assert.AreEqual(expectedLight, world.LightSource);
            Assert.Contains(expectedSphere1, world.SceneObjects);
            Assert.Contains(expectedSphere2, world.SceneObjects);

        }

        [Test]
        public void IntersectWorldWithRay()
        {
            var world = World.DefaultWorld;
            var r = new Ray(new Point(0,0,-5), new Vector(0,0,1));

            var xs = world.Intersects(r);

            Assert.AreEqual(4,xs.Length);
            Assert.AreEqual(4,xs[0].T);
            Assert.AreEqual(4.5, xs[1].T);
            Assert.AreEqual(5.5, xs[2].T);
            Assert.AreEqual(6, xs[3].T);
        }


        [Test]
        public void ShadingAnIntersection()
        {
            var world = World.DefaultWorld;
            var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));

            var shape = world.SceneObjects.First();
            var i = new Intersection(4, shape);

            var comp = i.PrepareComputations(r);
            var c = world.ShadeHit(comp);

            Assert.AreEqual(new Colour(0.38066, 0.47583, 0.2855), c);

        }

        [Test]
        public void ShadingAnIntersectionFromTheInside()
        {
            var world = World.DefaultWorld;
            world.LightSource = new Light(new Point(0,0.25,0), new Colour(1,1,1));
            var r = new Ray(new Point(0, 0, 0), new Vector(0, 0, 1));

            var shape = world.SceneObjects.Last();
            var i = new Intersection(0.5, shape);

            var comp = i.PrepareComputations(r);
            var c = world.ShadeHit(comp);

            Assert.AreEqual(new Colour(0.90498, 0.90498, 0.90498), c);
        }

        [Test]
        public void ColourAtWhenRayMisses()
        {
            var w = World.DefaultWorld;
            var r = new Ray(new Point(0,0,-5), new Vector(0,1,0));

            var result = w.ColourAt(r);
            Assert.AreEqual(new Colour(0,0,0), result);
        }

        [Test]
        public void ColourAtWhenRayHits()
        {
            var w = World.DefaultWorld;
            var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));

            var result = w.ColourAt(r);
            Assert.AreEqual(new Colour(0.38066, 0.47583, 0.2855), result);
        }

        [Test]
        public void ColourAtWithAnIntersectionBehindTheRay()
        {
            var w = World.DefaultWorld;
            var outer = w.SceneObjects.First();
            outer.Material.Ambient = 1;

            var inner = w.SceneObjects.Last();
            inner.Material.Ambient = 1;

            var r = new Ray(new Point(0, 0, 0.75), new Vector(0, 0, -1));

            var result = w.ColourAt(r);
            Assert.AreEqual(inner.Material.Colour, result);
        }

        [Test]
        public void IsShadowed_WhenNothingCoLinearWithPointToLight_False()
        {
            var world = World.DefaultWorld;
            var point = new Point(0,10,0);

            Assert.IsFalse(world.IsShadowed(point));
        }

        [Test]
        public void IsShadowed_WhenObjectBetweenPointAndLight_True()
        {
            var world = World.DefaultWorld;
            var point = new Point(10, -10, 10);

            Assert.IsTrue(world.IsShadowed(point));
        }

        [Test]
        public void IsShadowed_WhenPointBehindLight_False()
        {
            var world = World.DefaultWorld;
            var point = new Point(-20, 20, -20);

            Assert.IsFalse(world.IsShadowed(point));
        }

        [Test]
        public void IsShadowed_WhenPointBetweenLightAndObject_False()
        {
            var world = World.DefaultWorld;
            var point = new Point(-2, -2, 2);

            Assert.IsFalse(world.IsShadowed(point));
        }

        [Test]
        public void ShadeHit()
        {
            var world = new World
            {
                LightSource = new Light(new Point(0, 0, -10), new Colour(1, 1, 1))
            };

            var sphere1 = new Sphere();
            world.SceneObjects.Add(sphere1);

            var sphere2 = new Sphere();
            world.SceneObjects.Add(sphere2);
            sphere2.Transform = Matrix.Translation(0,0,10);

            var ray = new Ray(new Point(0,0,5), new Vector(0,0,1));

            var i = new Intersection(4, sphere2);

            var comps = i.PrepareComputations(ray);

            var result = world.ShadeHit(comps);

            Assert.AreEqual(new Colour(0.1,0.1,0.1), result);

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using RayTracer.Patterns;
using RayTracer.Shapes;

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

        [Test]
        public void ReflectedColourForNonReflectiveMaterial()
        {
            var world = World.DefaultWorld;
            var r = new Ray(new Point(0,0,0), new Vector(0,0,1));
            var shape = world.SceneObjects[1];
            shape.Material.Ambient = 1;
            var i = new Intersection(1, shape);
            var comps = i.PrepareComputations(r);
            var colour = world.ReflectedColour(comps);

            Assert.AreEqual(ColourFactory.Black, colour);
        }

        [Test]
        public void ReflectedColourForReflectiveMaterial()
        {
            var world = World.DefaultWorld;

            var shape = new Plane();
            shape.Material.Reflective = 0.5;
            shape.Transform = Matrix.Translation(0,-1,0);
            world.SceneObjects.Add(shape);

            var r = new Ray(new Point(0, 0, -3), new Vector(0, -Math.Sqrt(2)/2, Math.Sqrt(2) / 2));
            var i = new Intersection(Math.Sqrt(2), shape);
            var comps = i.PrepareComputations(r);
            var colour = world.ReflectedColour(comps);
            var expected = new Colour(0.190332320, 0.237915, 0.14274);
            
            Assert.AreEqual(expected, colour);
        }


        [Test]
        public void ShadeHitForReflectiveMaterial()
        {
            var world = World.DefaultWorld;

            var shape = new Plane();
            shape.Material.Reflective = 0.5;
            shape.Transform = Matrix.Translation(0, -1, 0);
            world.SceneObjects.Add(shape);

            var r = new Ray(new Point(0, 0, -3), new Vector(0, -Math.Sqrt(2) / 2, Math.Sqrt(2) / 2));
            var i = new Intersection(Math.Sqrt(2), shape);
            var comps = i.PrepareComputations(r);
            var colour = world.ShadeHit(comps);
            var expected = new Colour(0.8767572, 0.9243403, 0.8291742);

            Assert.AreEqual(expected, colour);
        }

        [Test]
        public void ColourAtWithMultipleReflectiveSurfaces()
        {
            var w = new World();
            w.LightSource = new Light(new Point(0,0,0), ColourFactory.White);

            var lower = new Plane {Material = {Reflective = 1}, Transform = Matrix.Translation(0, -1, 0)};
            w.SceneObjects.Add(lower);
            var upper = new Plane { Material = { Reflective = 1 }, Transform = Matrix.Translation(0, 1, 0) };
            w.SceneObjects.Add(upper);

            var r = new Ray(new Point(0,0,0), new Vector(0,1,0));

            Assert.DoesNotThrow(() => w.ColourAt(r));
            
        }

        [Test]
        public void ReflectiveColourAtMaxRecursionDepth()
        {
            var world = World.DefaultWorld;

            var shape = new Plane();
            shape.Material.Reflective = 0.5;
            shape.Transform = Matrix.Translation(0, -1, 0);
            world.SceneObjects.Add(shape);

            var r = new Ray(new Point(0, 0, -3), new Vector(0, -Math.Sqrt(2) / 2, Math.Sqrt(2) / 2));
            var i = new Intersection(Math.Sqrt(2), shape);
            var comps = i.PrepareComputations(r);
            var colour = world.ReflectedColour(comps, 0);

            Assert.AreEqual(ColourFactory.Black, colour);
        }

        [Test]
        public void RefractedColourWithAnOpaqueSurface()
        {
            var w = World.DefaultWorld;
            var shape = w.SceneObjects.First();

            var ray = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            var xs = new List<Intersection>{new Intersection(4, shape), new Intersection(6,shape)};

            var comps = xs[0].PrepareComputations(ray, xs);
            var c = w.RefractedColour(comps, 5);

            Assert.AreEqual(ColourFactory.Black, c);
        }

        [Test]
        public void RefractedColourAtMaximumRecursionDepth()
        {
            var w = World.DefaultWorld;
            var shape = w.SceneObjects.First();
            shape.Material.Transparency = 1.0;
            shape.Material.RefractiveIndex = 1.5;


            var ray = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            var xs = new List<Intersection> { new Intersection(4, shape), new Intersection(6, shape) };

            var comps = xs[0].PrepareComputations(ray, xs);
            var c = w.RefractedColour(comps, 0);

            Assert.AreEqual(ColourFactory.Black, c);
        }

        [Test]
        public void RefractedColourUnderTotalInternalReflection()
        {
            var w = World.DefaultWorld;
            var shape = w.SceneObjects.First();
            shape.Material.Transparency = 1.0;
            shape.Material.RefractiveIndex = 1.5;


            var ray = new Ray(new Point(0, 0, Math.Sqrt(2)/2), new Vector(0, 1, 0));
            var xs = new List<Intersection> {
                new Intersection(-Math.Sqrt(2) / 2, shape), 
                new Intersection(Math.Sqrt(2) / 2, shape)
            };
            

            var comps = xs[1].PrepareComputations(ray, xs);
            var c = w.RefractedColour(comps, 5);

            Assert.AreEqual(ColourFactory.Black, c);
        }

        [Test]
        public void RefractedColourWithRefractedRay()
        {
            var w = World.DefaultWorld;

            var a = w.SceneObjects[0];
            a.Material.Ambient = 1.0;
            a.Material.Pattern = new TestPattern();

            var b = w.SceneObjects[1];
            b.Material.Transparency = 1.0;
            b.Material.RefractiveIndex = 1.5;

            var ray = new Ray(new Point(0,0,0.1), new Vector(0,1,0));

            var xs = new List<Intersection>
            {
                new Intersection(-0.9899,a),
                new Intersection(-0.4899,b),
                new Intersection(0.4899,b),
                new Intersection(0.9899,a)
            };

            var comps = xs[2].PrepareComputations(ray, xs);
            var c = w.RefractedColour(comps, 5);

            Assert.AreEqual(new Colour(0, 0.99887, 0.04721), c);

        }

        [Test]
        public void ShadeHitWithTransparentMaterial()
        {
            var w = World.DefaultWorld;
            var floor = new Plane();
            floor.Transform = Matrix.Translation(0,-1,0);
            floor.Material.Transparency = 0.5;
            floor.Material.RefractiveIndex = 1.5;
            w.SceneObjects.Add(floor);

            var ball = new Sphere();
            ball.Transform = Matrix.Translation(0, -3.5, -0.5);
            ball.Material.Colour = new Colour(1,0,0);
            ball.Material.Ambient = 0.5;
            w.SceneObjects.Add(ball);

            var ray = new Ray(new Point(0,0,-3), new Vector(0, -(Math.Sqrt(2) / 2), Math.Sqrt(2) / 2));
            var xs = new List<Intersection>{new Intersection(Math.Sqrt(2),floor)};
            var comps = xs[0].PrepareComputations(ray, xs);
            var c = w.ShadeHit(comps, 5);

            Assert.AreEqual(new Colour(0.93642, 0.68642, 0.68642), c);
        }

        [Test]
        public void ShadeHitWithReflectiveTransparentMaterial()
        {
            var w = World.DefaultWorld;
            var floor = new Plane();
            floor.Transform = Matrix.Translation(0, -1, 0);
            floor.Material.Transparency = 0.5;
            floor.Material.Reflective = 0.5;
            floor.Material.RefractiveIndex = 1.5;
            w.SceneObjects.Add(floor);

            var ball = new Sphere();
            ball.Transform = Matrix.Translation(0, -3.5, -0.5);
            ball.Material.Colour = new Colour(1, 0, 0);
            ball.Material.Ambient = 0.5;
            w.SceneObjects.Add(ball);

            var ray = new Ray(new Point(0, 0, -3), new Vector(0, -(Math.Sqrt(2) / 2), Math.Sqrt(2) / 2));
            var xs = new List<Intersection> { new Intersection(Math.Sqrt(2), floor) };
            var comps = xs[0].PrepareComputations(ray, xs);
            var c = w.ShadeHit(comps, 5);

            Assert.AreEqual(new Colour(0.93391, 0.69643, 0.69243), c);
        }
    }
}
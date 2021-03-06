﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using RayTracer.Shapes;

namespace RayTracer
{
    public class World
    {
        public World()
        {
            SceneObjects = new List<ISceneObject>();
        }

        public Light LightSource { get; set; }


        public List<ISceneObject> SceneObjects { get; set; }

        public static World DefaultWorld
        {
            get {
                var light = new Light(new Point(-10, 10, -10), new Colour(1, 1, 1));
                var material = new Material
                {
                    Colour = new Colour(0.8, 1, 0.6), 
                    Diffuse = 0.7, 
                    Specular = 0.2
                };
                var sphere1 = new Sphere { Material = material };
                var sphere2 = new Sphere {Transform = Matrix.Scaling(0.5, 0.5, 0.5)};

                var worldObjects =new List<ISceneObject>
                {
                    sphere1,
                    sphere2
                };
                return new World {LightSource = light, SceneObjects = worldObjects};
            }
        }

        public Intersection[] Intersects(Ray ray)
        {
            var allIntersections = new List<Intersection>();
            foreach (var sceneObject in SceneObjects)
            {
                allIntersections.AddRange( sceneObject.Intersects(ray));
            }

            allIntersections.Sort((x, y) =>
            {
                if (DoubleUtils.DoubleEquals(x.T, y.T))
                {
                    return 0;
                }

                if (x.T > y.T)
                {
                    return 1;
                }

                return -1;

            });
            return allIntersections.ToArray();
        }

        public Colour ShadeHit(Computation comp, int remaining=5)
        {
            
            var shadowed = !comp.Object.CastsNoShadow && IsShadowed(comp.OverPoint);

            var surface =  Light.Lighting(comp.Object.Material,
                comp.Object,
                this.LightSource, 
                comp.OverPoint, 
                comp.EyeV, 
                comp.NormalV,
                shadowed);

            var reflected = ReflectedColour(comp, remaining);
            var refracted = RefractedColour(comp, remaining);
            var material = comp.Object.Material;
            if (material.Reflective >0 && material.Transparency >0)
            {
                var reflectance = SchlickReflectance.Schlick(comp);
                return surface.Add(reflected.Multiply(reflectance)).Add(refracted.Multiply(1-reflectance));
            }
            return surface.Add(reflected).Add(refracted);
        }

        public Colour ColourAt(Ray ray, int remaining=5)
        {
            var intersections = this.Intersects(ray);

            var hit = intersections.Hit();
            if (hit != null)
            {
                var comp = hit.PrepareComputations(ray);
                var colour = ShadeHit(comp, remaining);
                return colour;
            }

            return ColourFactory.Black;
        }

        public bool IsShadowed(Point point)
        {

            var v = LightSource.Position - point;
            var distance = v.Magnitude();
            var direction = v.Normalize();

            var ray = new Ray(point, direction.ToVector());

            var intersections = Intersects(ray);

            var hit = intersections.Hit();

            return hit != null && hit.T < distance;
        }

        public Colour ReflectedColour(Computation comps, int remaining=5)
        {
            if (remaining <= 0)
            {
                return ColourFactory.Black;
            }

            if (DoubleUtils.DoubleEquals (comps.Object.Material.Reflective,0))
            {
                return ColourFactory.Black;
            }
            
            var reflectedRay = new Ray(comps.OverPoint, comps.ReflectV);
            var colour = ColourAt(reflectedRay,remaining-1);

            return colour.Multiply(comps.Object.Material.Reflective);
        }

        public Colour RefractedColour(Computation comps, int remaining)
        {
            if (remaining <=0)
            {
                return ColourFactory.Black;
            }

            if (DoubleUtils.DoubleEquals( comps.Object.Material.Transparency,0))
            {
                return ColourFactory.Black;
            }

            var nRatio = comps.N1 / comps.N2;
            var cosI = comps.EyeV.Dot(comps.NormalV);

            var sin2T = Math.Pow(nRatio,2) * (1-Math.Pow(cosI,2));

            if (sin2T > 1)
            {
                return ColourFactory.Black;
            }

            var cosT = Math.Sqrt(1.0 - sin2T);

            var direction = (comps.NormalV.Multiply(nRatio * cosI - cosT) - comps.EyeV.Multiply( nRatio)).ToVector();

            var refractRay = new Ray(comps.UnderPoint, direction);

            var colour = ColourAt(refractRay, remaining - 1).Multiply(comps.Object.Material.Transparency);
            return colour;
        }
    }
}

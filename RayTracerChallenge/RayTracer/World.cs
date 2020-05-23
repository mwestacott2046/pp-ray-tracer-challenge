using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

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
                allIntersections.AddRange( ray.Intersects(sceneObject));
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

        public Colour ShadeHit(Computation comp)
        {
            return Light.Lighting(comp.Object.Material, 
                this.LightSource, 
                comp.Point, 
                comp.EyeV, 
                comp.NormalV);
        }

        public Colour ColourAt(Ray ray)
        {
            var intersections = this.Intersects(ray);

            var hit = intersections.Hit();
            if (hit != null)
            {
                var comp = hit.PrepareComputations(ray);
                var colour = ShadeHit(comp);
                return colour;
            }

            return Colour.Black;
        }
    }
}

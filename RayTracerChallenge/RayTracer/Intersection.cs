using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer
{
    public class Intersection
    {
        public double T { get; private set; }
        public ISceneObject Object { get; private set; }

        public Intersection(double t, ISceneObject obj)
        {
            T = t;
            Object = obj;
        }

        public static List<Intersection> Intersections(params Intersection[] intersections)
        {
            var list = intersections.ToList();
            return list;
        }

        public Computation PrepareComputations(Ray ray)
        {
            var comp = new Computation
            {
                T = this.T, 
                Object = this.Object
            };

            comp.Point = ray.Position(comp.T);
            comp.EyeV = (-ray.Direction).ToVector();
            comp.NormalV = comp.Object.NormalAt(comp.Point);

            if (comp.NormalV.Dot(comp.EyeV) < 0)
            {
                comp.Inside = true;
                comp.NormalV = comp.NormalV.Negate().ToVector();
            }
            else
            {
                comp.Inside = false;
            }


            return comp;
        }
    }
}

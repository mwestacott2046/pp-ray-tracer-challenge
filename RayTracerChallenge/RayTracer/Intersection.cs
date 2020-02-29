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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RayTracer.Shapes;

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

        public Computation PrepareComputations(Ray ray, List<Intersection> xs = null)
        {
            if (xs == null)
            {
                xs = new List<Intersection> {this};
            }

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

            comp.ReflectV = ray.Direction.Reflect(comp.NormalV);
            comp.OverPoint = (comp.Point + comp.NormalV * (DoubleUtils.Epsilon)).ToPoint();
            comp.UnderPoint = (comp.Point - comp.NormalV * (DoubleUtils.Epsilon)).ToPoint();


            var containers = new List<ISceneObject>();

            foreach (var intersection in xs)
            {
                if (intersection.Equals(this))
                {
                    if (!containers.Any())
                    {
                        comp.N1 = 1.0;
                    }
                    else
                    {
                        var last = containers.Last();
                        comp.N1 = last.Material.RefractiveIndex;
                    }
                }

                if (containers.Contains(intersection.Object))
                {
                    containers.Remove(intersection.Object);
                }
                else
                {
                    containers.Add(intersection.Object);
                }

                if (intersection == this)
                {
                    if (!containers.Any())
                    {
                        comp.N2 = 1.0;
                    }
                    else
                    {
                        var last = containers.Last();
                        comp.N2 = last.Material.RefractiveIndex;
                    }

                    break;
                }
            }

            return comp;
        }
    }
}

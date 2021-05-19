using System;
using System.Collections.Generic;

namespace RayTracer.Shapes
{
    public class Cylinder : AbstractShape
    {
        public Cylinder()
        {
            Minimum = double.MinValue;
            Maximum = double.MaxValue;
            Closed = false;
        }

        protected override Intersection[] LocalIntersects(Ray localRay)
        {
            var a =  Math.Pow(localRay.Direction.X,2) + Math.Pow(localRay.Direction.Z,2);
            // Is parallel to cylinder
            if (DoubleUtils.DoubleEquals(a, 0))
            {
                return new Intersection[] {};
            }

            var b = 2 * localRay.Origin.X * localRay.Direction.X +
                    2 * localRay.Origin.Z * localRay.Direction.Z;
            var c = Math.Pow(localRay.Origin.X, 2) + Math.Pow(localRay.Origin.Z, 2) -1;

            var disc = Math.Pow(b, 2) - 4 * a * c;
            
            //if Ray has missed
            if (disc < 0)
            {
                return new Intersection[] { };
            }

            var t0 = (-b - Math.Sqrt(disc)) / (2 * a);
            var t1 = (-b + Math.Sqrt(disc)) / (2 * a);

            if (t0 > t1)
            {
                (t0, t1) = (t1, t0);
            }

            var xs = new List<Intersection>();

            var y0 = localRay.Origin.Y + t0 * localRay.Direction.Y;
            if (this.Minimum < y0 && y0 < this.Maximum)
            {
                xs.Add(new Intersection(t0,this));
            }

            var y1 = localRay.Origin.Y + t1 * localRay.Direction.Y;
            if (this.Minimum < y1 && y1 < this.Maximum)
            {
                xs.Add(new Intersection(t1, this));
            }

            return xs.ToArray();
        }

        protected override Vector LocalNormalAt(Point localPoint)
        {
            return new Vector(localPoint.X, 0, localPoint.Z);
        }

        public double Minimum { get; set; }
        public double Maximum { get; set; }
        public bool Closed { get; set; }
    }
}
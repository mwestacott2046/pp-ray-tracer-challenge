using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Ray
    {
        public Ray(Point origin, Vector direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public Vector Direction { get; private set; }
        public Point Origin { get; private set; }

        public Point Position(double t)
        {
            return Origin.Add(Direction.Multiply(t)).ToPoint();
        }

        public Intersection[] Intersects(Sphere sphere)
        {
            var sphereToRay = Origin.Subtract(Point.Zero());

            var a = Direction.Dot(Direction);
            var b = 2 * Direction.Dot(sphereToRay);
            var c = sphereToRay.Dot(sphereToRay) - 1;

            var discriminant = (b * b) - 4 * a * c;
            if (discriminant < 0)
            {
                return new Intersection[]{};
            }

            var t1 = (-b - Math.Sqrt(discriminant)) / (2 * a);
            var t2 = (-b + Math.Sqrt(discriminant)) / (2 * a);

            return new[] {new Intersection(t1, sphere), new Intersection(t2, sphere)};
        }
    }
}

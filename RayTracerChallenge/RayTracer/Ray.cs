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
    }
}

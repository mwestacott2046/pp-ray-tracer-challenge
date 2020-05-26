using System;
using System.Collections.Generic;
using System.Text;
using RayTracer.Shapes;

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


        public Ray Transform(Matrix transformMatrix)
        {
            var transPoint = transformMatrix.Multiply(this.Origin);
            var transDirection = transformMatrix.Multiply(this.Direction);
            return new Ray(transPoint.ToPoint(), transDirection.ToVector());
        }
    }
}

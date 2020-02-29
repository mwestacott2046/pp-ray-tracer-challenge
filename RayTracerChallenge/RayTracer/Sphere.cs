using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Sphere
    {
        public Sphere()
        {
            Origin = new Point(0,0,0);
            Radius = 1.0;
        }

        public Point Origin { get; private set; }
        public double Radius { get; private set; }
    }
}

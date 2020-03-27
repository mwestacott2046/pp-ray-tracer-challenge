using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Sphere :ISceneObject
    {
        public Sphere()
        {
            Origin = new Point(0,0,0);
            Radius = 1.0;
            Transform = Matrix.IdentityMatrix;
        }

        public Point Origin { get; private set; }
        public double Radius { get; private set; }
        public Matrix Transform { get; set; }
    }
}

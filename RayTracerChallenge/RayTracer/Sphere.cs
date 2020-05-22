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
            Material = new Material();
        }

        public Point Origin { get; private set; }
        public double Radius { get; private set; }
        public Matrix Transform { get; set; }
        public Material Material { get; set; }

        public Vector NormalAt(Point worldPoint)
        {
            var inverse = Transform.Inverse();
            var objectPoint = inverse.Multiply(worldPoint);

            var objectNormal = objectPoint.Subtract(Point.Zero());
            var worldNormal = (inverse.Transpose()).Multiply(objectNormal.ToVector()).ToVector();
            
            return worldNormal
                .Normalize()
                .ToVector();
        }
    }
}

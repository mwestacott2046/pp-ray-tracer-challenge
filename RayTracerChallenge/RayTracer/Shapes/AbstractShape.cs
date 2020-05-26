using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer.Shapes
{
    public abstract class AbstractShape : ISceneObject
    {
        protected AbstractShape()
        {
            Material = new Material();
            Transform = Matrix.IdentityMatrix;
        }

        public Vector NormalAt(Point worldPoint)
        {
            var inverse = Transform.Inverse();
            var objectPoint = inverse.Multiply(worldPoint);


            var objectNormal = LocalNormalAt(objectPoint.ToPoint());

            var worldNormal = (inverse.Transpose()).Multiply(objectNormal.ToVector()).ToVector();

            return worldNormal
                .Normalize()
                .ToVector();
        }

        protected virtual Vector LocalNormalAt(Point localPoint)
        {
            throw new NotImplementedException();
        }

        public Material Material { get; set; }
        public Matrix Transform { get; set; }
        public Intersection[] Intersects(Ray ray)
        {
            var localRay = ray.Transform(this.Transform.Inverse());
            return LocalIntersects(localRay);
        }

        protected virtual Intersection[] LocalIntersects(Ray localRay)
        {
            throw new NotImplementedException();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using RayTracer.Shapes;

namespace RayTracer
{
    public class Pattern
    {
        public Pattern(Colour a, Colour b)
        {
            A = a;
            B = b;
            this.Transform = Matrix.IdentityMatrix;
        }

        public Colour A { get; set; }
        public Colour B { get; set; }
        public Matrix Transform { get; set; }

        public Colour StripeAt(Point point)
        {
            var mod = (int)Math.Abs(Math.Truncate(point.X) % 2);
            if (point.X >= 0)
            {
                return mod == 0 ? A : B;
            }

            return mod == 0 ? B : A;
        }

        public Colour StripeAtObject(ISceneObject sceneObject, Point point)
        {
            var objectPoint = (sceneObject.Transform.Inverse() * point).ToPoint();
            var patternPoint = (this.Transform.Inverse() * objectPoint).ToPoint();

            return StripeAt(patternPoint);
        }
    }
}

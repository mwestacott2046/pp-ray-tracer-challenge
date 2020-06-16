using System;
using System.Collections.Generic;
using System.Text;
using RayTracer.Shapes;

namespace RayTracer.Patterns
{
    public abstract class AbstractPattern: IPattern
    {
        protected AbstractPattern()
        {
            this.Transform = Matrix.IdentityMatrix;
        }

        public Colour PatternAtShape(ISceneObject sceneObject, Point point)
        {
            var objectPoint = (sceneObject.Transform.Inverse() * point).ToPoint();
            var patternPoint = (this.Transform.Inverse() * objectPoint).ToPoint();
            return PatternAt(patternPoint);
        }

        public virtual Colour PatternAt(Point point)
        {
            throw new NotImplementedException();
        }

        public Matrix Transform { get; set; }
    }
}

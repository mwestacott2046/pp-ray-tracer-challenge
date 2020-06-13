using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer.Shapes
{
    public class Plane : AbstractShape
    {
        protected override Vector LocalNormalAt(Point localPoint)
        {
            return new Vector(0,1,0);
        }

        protected override Intersection[] LocalIntersects(Ray localRay)
        {
            if (Math.Abs(localRay.Direction.Y) < DoubleUtils.Epsilon)
            {
                return new Intersection[]{};
            }

            var t = -localRay.Origin.Y / localRay.Direction.Y;
            return new Intersection[] {new Intersection(t, this)};
        }
    }
}

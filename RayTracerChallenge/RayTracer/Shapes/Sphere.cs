using System;

namespace RayTracer.Shapes
{
    public class Sphere : AbstractShape
    {
        public Sphere(): base()
        {
            Origin = new Point(0,0,0);
            Radius = 1.0;
        }

        public Point Origin { get; private set; }
        public double Radius { get; private set; }

        protected override Vector LocalNormalAt(Point localPoint)
        {
            return localPoint.Subtract(Point.Zero()).ToVector();
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Radius, Origin.GetHashCode(), Transform.GetHashCode(), Material.GetHashCode());
        }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                if (obj is Sphere compareSphere)
                {
                    return DoubleUtils.DoubleEquals(this.Radius, compareSphere.Radius)
                           && this.Origin.Equals(compareSphere.Origin)
                           && this.Material.Equals(compareSphere.Material)
                           && this.Transform.Equals(compareSphere.Transform);
                }
            }
            return false;
        }


        protected override Intersection[] LocalIntersects(Ray localRay)
        {
            var sphereToRay = localRay.Origin.Subtract(Point.Zero());

            var a = localRay.Direction.Dot(localRay.Direction);
            var b = 2 * localRay.Direction.Dot(sphereToRay);
            var c = sphereToRay.Dot(sphereToRay) - 1;

            var discriminant = (b * b) - 4 * a * c;
            if (discriminant < 0)
            {
                return new Intersection[] { };
            }

            var t1 = (-b - Math.Sqrt(discriminant)) / (2 * a);
            var t2 = (-b + Math.Sqrt(discriminant)) / (2 * a);

            return new[] { new Intersection(t1, this), new Intersection(t2, this) };

        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.Shapes
{
    public class Cube: AbstractShape
    {
        private struct MinMax
        {
            public double Min { get;}
            public double Max { get;}

            public MinMax(double min, double max)
            {
                this.Min = min;
                this.Max = max;
            }
        }

        protected override Intersection[] LocalIntersects(Ray localRay)
        {
            var xtMinMax = CheckAxis(localRay.Origin.X, localRay.Direction.X);
            var ytMinMax = CheckAxis(localRay.Origin.Y, localRay.Direction.Y);
            var ztMinMax = CheckAxis(localRay.Origin.Z, localRay.Direction.Z);

            var tMin = new double[] {xtMinMax.Min,ytMinMax.Min, ztMinMax.Min}.Max();
            var tMax = new double[] { xtMinMax.Max, ytMinMax.Max, ztMinMax.Max }.Min();

            if (tMin > tMax)
            {
                return new Intersection[]{};
            }

            return new Intersection[] {new Intersection(tMin, this), new Intersection(tMax, this) };
        }

        private MinMax CheckAxis(double origin, double direction)
        {
            var tMinNumerator = (-1 -origin);
            var tMaxNumerator = (1-origin);

            double tMin;
            double tMax;

            if (Math.Abs(direction) >= DoubleUtils.Epsilon)
            {
                tMin = tMinNumerator / direction;
                tMax = tMaxNumerator / direction;
            }
            else
            {
                tMin = tMinNumerator * double.PositiveInfinity;
                tMax = tMaxNumerator * double.PositiveInfinity;
            }

            if (tMin > tMax)
            {
                (tMax, tMin) = (tMin, tMax);
            }

            return new MinMax(tMin, tMax);
        }

        protected override Vector LocalNormalAt(Point localPoint)
        {
            var maxC = new double[] {Math.Abs(localPoint.X), Math.Abs(localPoint.Y), Math.Abs(localPoint.Z)}.Max();

            if (DoubleUtils.DoubleEquals(Math.Abs(localPoint.X),maxC))
            {
                return new Vector(localPoint.X,0,0);
            }
            else if (DoubleUtils.DoubleEquals(Math.Abs(localPoint.Y), maxC))
            {
                return new Vector( 0, localPoint.Y,0);
            }

            return new Vector(0,0,localPoint.Z);
        }
    }
}

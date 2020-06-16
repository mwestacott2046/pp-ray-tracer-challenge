using System;
using RayTracer.Shapes;

namespace RayTracer.Patterns
{
    public class StripePattern: AbstractPattern
    {
        public StripePattern(Colour a, Colour b)
        {
            A = a;
            B = b;
        }

        public Colour A { get; }
        public Colour B { get; }


        public override Colour PatternAt(Point point)
        {
            var mod = (int)Math.Abs(Math.Truncate(point.X) % 2);
            if (point.X >= 0)
            {
                return mod == 0 ? A : B;
            }

            return mod == 0 ? B : A;
        }

    }
}

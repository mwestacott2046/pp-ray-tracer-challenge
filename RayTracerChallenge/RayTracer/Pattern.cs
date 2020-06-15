using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Pattern
    {
        public Pattern(Colour a, Colour b)
        {
            A = a;
            B = b;
        }

        public Colour A { get; set; }
        public Colour B { get; set; }

        public Colour StripeAt(Point point)
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

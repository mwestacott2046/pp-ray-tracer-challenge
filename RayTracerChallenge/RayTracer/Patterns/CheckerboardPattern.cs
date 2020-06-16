using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RayTracer.Patterns
{
    public class CheckerboardPattern: AbstractPattern
    {
        public CheckerboardPattern(Colour a, Colour b)
        {
            A = a;
            B = b;
        }

        public Colour A { get; }
        public Colour B { get; }

        public override Colour PatternAt(Point point)
        {
            var mod2 = (int)(Math.Floor(point.X) + Math.Floor(point.Y) + Math.Floor(point.Z)) % 2;
            if (mod2 ==0)
            {
                return A;
            }

            return B;
        }
    }
}

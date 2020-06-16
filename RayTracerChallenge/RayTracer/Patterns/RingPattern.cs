using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer.Patterns
{
    public class RingPattern:AbstractPattern
    {
        public RingPattern(Colour a, Colour b)
        {
            A = a;
            B = b;
        }

        public Colour A { get; }
        public Colour B { get; }

        public override Colour PatternAt(Point point)
        {
            var testValue =  Math.Floor( Math.Sqrt((point.X * point.X) + (point.Z * point.Z))) % 2;

            if (DoubleUtils.DoubleEquals(testValue,0))
            {
                return A;
            }

            return B;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer.Patterns
{
    public class GradientPattern : AbstractPattern
    {
        public GradientPattern(Colour a, Colour b)
        {
            A = a;
            B = b;
        }

        public Colour A { get; }
        public Colour B { get; }

        public override Colour PatternAt(Point point)
        {
            var distance = this.B.Subtract(this.A);
            var fraction = point.X - Math.Floor(point.X);

            return this.A.Add(distance.Multiply(fraction));
        }
    }
}

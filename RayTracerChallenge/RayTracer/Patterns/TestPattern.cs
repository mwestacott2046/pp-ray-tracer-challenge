using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer.Patterns
{
    public class TestPattern: AbstractPattern
    {
        public override Colour PatternAt(Point point)
        {
            return new Colour(point.X, point.Y, point.Z);
        }
    }
}

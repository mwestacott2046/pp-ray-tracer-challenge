using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Colour
    {
        public Colour(double r, double g, double b)
        {
            R = r;
            G = g;
            B = b;
        }

        public double R { get; private set; }
        public double G { get; private set; }
        public double B { get; private set; }
        public static Colour Black => new Colour(0,0,0);
        public static Colour White => new Colour(1, 1, 1);

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                if (obj is Colour compareColour)
                {
                    return DoubleUtils.DoubleEquals(this.R, compareColour.R)
                           && DoubleUtils.DoubleEquals(this.G, compareColour.G)
                           && DoubleUtils.DoubleEquals(this.B, compareColour.B);
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(R, G, B);
        }

        public override string ToString()
        {
            return $"(R: {R}, G: {G}, B{B})";
        }

        public Colour Add(Colour add)
        {
            return new Colour(this.R + add.R, this.G + add.G, this.B + add.B);
        }

        public Colour Subtract(Colour sub)
        {
            return new Colour(this.R - sub.R, this.G - sub.G, this.B - sub.B);
        }

        public Colour Multiply(double scalar)
        {
            return new Colour(this.R * scalar,this.G * scalar, this.B * scalar);
        }

        public Colour Multiply(Colour colour)
        {
            return new Colour(this.R *colour.R, this.G * colour.G, this.B * colour.B);
        }
    }
}

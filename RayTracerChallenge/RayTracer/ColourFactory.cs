using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public static class ColourFactory
    {
        public static Colour FromRgbInt(int red, int green, int blue)
        {
            var r = ConvertColourDigit(red);
            var g = ConvertColourDigit(green);
            var b = ConvertColourDigit(blue);
            return new Colour(r, g, b);
        }

        private static double ConvertColourDigit(int value)
        {
            if (value < 0)
            {
                return 0;
            }
            if (value > 255)
            {
                return 1;
            }

            return (double)value / 255;
        }

        public static Colour Black => new Colour(0, 0, 0);
        public static Colour White => new Colour(1, 1, 1);
        public static Colour Red => new Colour(1, 0, 0);
        public static Colour Lime => new Colour(0, 1, 0);
        public static Colour Green => new Colour(0, 0.5, 0);
        public static Colour Blue => new Colour(0, 0, 1);
        public static Colour Navy => new Colour(0, 0, 0.25);

        public static Colour Silver => new Colour(0.75, 0.75, 0.75);
        public static Colour Grey => new Colour(0.5, 0.5, 0.5);
        public static Colour DarkGrey => new Colour(0.25, 0.25, 0.25);

        public static Colour Purple => new Colour(0.5,0,0.5);

    }
}

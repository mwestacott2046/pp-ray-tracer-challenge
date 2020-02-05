using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public static class DoubleUtils
    {
        private static double Precision = 0.00001;

        public static bool DoubleEquals(double a, double b)
        {
            return Math.Abs(a - b) < Precision;
        }
    }
}

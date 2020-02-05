using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class RtTuple
    {
        private const double VectorW = 0.0;
        private const double PointW = 1.0;
        

        public RtTuple(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public static RtTuple Point(in double x, in double y, in double z)
        {
            return new RtTuple(x, y, z, PointW);
        }
        public static RtTuple Vector(in double x, in double y, in double z)
        {
            return new RtTuple(x, y, z, VectorW);
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double W { get; set; }

        public bool IsPoint()
        {
            return DoubleUtils.DoubleEquals(W, PointW);
        }
        public bool IsVector()
        {
            return DoubleUtils.DoubleEquals(W, VectorW);
        }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                if (obj is RtTuple compareTuple)
                {
                    return DoubleUtils.DoubleEquals(this.X, compareTuple.X)
                           && DoubleUtils.DoubleEquals(this.Y, compareTuple.Y)
                           && DoubleUtils.DoubleEquals(this.Z, compareTuple.Z)
                           && DoubleUtils.DoubleEquals(this.W, compareTuple.W);
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z, W);
        }

        public RtTuple Add(RtTuple addTuple)
        {
            return new RtTuple(this.X + addTuple.X,
                this.Y + addTuple.Y,
                this.Z + addTuple.Z,
                this.W + addTuple.W);
        }

        public RtTuple Subtract(RtTuple subTuple)
        {
            return new RtTuple(this.X - subTuple.X,
                this.Y - subTuple.Y,
                this.Z - subTuple.Z,
                this.W - subTuple.W);
        }

        public RtTuple Negate()
        {
            var zeroTuple = new RtTuple(0, 0, 0, 0);
            return zeroTuple.Subtract(this);
        }

        public RtTuple Multiply(double scalar)
        {
            return new RtTuple(this.X * scalar,
                this.Y * scalar,
                this.Z * scalar,
                this.W * scalar);
        }

        public RtTuple Divide(double scalar)
        {
            return new RtTuple(this.X / scalar,
                this.Y / scalar,
                this.Z / scalar,
                this.W / scalar);
        }

        public double Magnitude()
        {
            return Math.Sqrt(Square(X) + Square(Y) + Square(Z) + Square(W));
        }

        private static double Square(double value)
        {
            return value * value;
        }

        public RtTuple Normalize()
        {
            var magnitude = Magnitude();
            return new RtTuple(this.X / magnitude,
                this.Y / magnitude,
                this.Z / magnitude,
                this.W / magnitude);
        }

        public override string ToString()
        {
            return $"(x: {X}, y: {Y}, z: {Z}, w: {W})";
        }

        public double Dot(RtTuple other)
        {
            return (X * other.X) + (Y * other.Y) + (Z * other.Z) + (W * other.W);
        }

        public RtTuple Cross(RtTuple other)
        {
            return Vector(Y * other.Z - Z * other.Y,
                            Z * other.X - X * other.Z,
                            X * other.Y - Y * other.X);
        }
    }
}

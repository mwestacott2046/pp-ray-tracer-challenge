﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class RtTuple
    {

        public RtTuple(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }
        public double W { get; private set; }


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
            return Subtract(this,subTuple);
        }

        public static RtTuple Subtract(RtTuple a, RtTuple b)
        {
            return new RtTuple(a.X - b.X,
                a.Y - b.Y,
                a.Z - b.Z,
                a.W - b.W);
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
            return new Vector(Y * other.Z - Z * other.Y,
                                Z * other.X - X * other.Z,
                                X * other.Y - Y * other.X);
        }

        public double[] AsArray()
        {
            return new[] {X, Y, Z, W};
        }


        public static RtTuple operator +(RtTuple tuple) => tuple;
        public static RtTuple operator -(RtTuple tuple) => tuple.Negate();
        public static RtTuple operator -(RtTuple a, RtTuple b) => a.Subtract(b);
        public static RtTuple operator +(RtTuple a, RtTuple b) => a.Add(b);

        public static RtTuple operator *(RtTuple tuple, double scalar) => tuple.Multiply(scalar);

        public Point ToPoint()
        {
            return new Point(this.X,this.Y, this.Z);
        }

        public Vector ToVector()
        {
            return new Vector(this.X, this.Y, this.Z);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Matrix
    {
        private readonly double[,] _matrixData;
        public int RowCount { get; private set; }
        public int ColCount { get; private set; }
        public Matrix(double[,] data)
        {
            _matrixData = data;
            RowCount = _matrixData.GetLength(0);
            ColCount = _matrixData.GetLength(1);
        }

        private Matrix(int rows, int columns) :this(new double[rows, columns])
        {
        }

        public double Get(int row, int col)
        {
            return _matrixData[row,col];
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj is Matrix compareTo)
            {
                if (this.RowCount == compareTo.RowCount && this.ColCount == compareTo.ColCount)
                {
                    for (var row = 0; row < this.RowCount; row++)
                    {
                        for (var col = 0; col < this.ColCount; col++)
                        {
                            if (!DoubleUtils.DoubleEquals(Get(row, col) ,compareTo.Get(row, col)))
                            {
                                return false;
                            }
                        }
                    }
                    return true;
                }
            }


            return false;
        }

        public override int GetHashCode()
        {

            var hash = new HashCode();
            for (var row = 0; row < this.RowCount; row++)
            {
                for (var col = 0; col < this.ColCount; col++)
                {
                    hash.Add(Get(row,col));
                }
            }

            return hash.ToHashCode();
        }

        public Matrix Multiply(Matrix other)
        {
            var result = new Matrix(this.RowCount, this.ColCount);

            for (var row = 0; row < this.RowCount; row++)
            {
                for (var col = 0; col < this.ColCount; col++)
                {
                    double value = 0;
                    for (var index = 0; index < this.RowCount; index++)
                    {
                        value += this.Get(row, index) * other.Get(index, col);
                    }

                    result.Set(row, col, value);
                }
            }

            return result;
        }

        private void Set(int row, int col, double value)
        {
            _matrixData[row, col] = value;
        }

        public RtTuple Multiply(RtTuple tuple)
        {
            var  result = new double[RowCount];
            var tupleValues = tuple.AsArray();
            for (var row = 0; row < this.RowCount; row++)
            {
                double rowValue = 0;
                for (var col = 0; col < this.ColCount; col++)
                {
                    rowValue += Get(row, col) * tupleValues[col];
                }

                result[row] = rowValue ;
            }

            return new RtTuple(result[0], result[1], result[2], result[3]);
        }

        public static Matrix IdentityMatrix => new Matrix(new double[,]
            {{1, 0, 0, 0}, {0, 1, 0, 0}, {0, 0, 1, 0}, {0, 0, 0, 1}});


        public Matrix Transpose()
        {
            var transposed= new Matrix(this.ColCount, this.RowCount);
            for (var row = 0; row < this.RowCount; row++)
            {
                for (var column = 0; column < this.ColCount; column++)
                {
                    transposed.Set(column,row, Get(row,column));
                }
            }
            return transposed;
        }

        public double Determinant()
        {
            double determinant = 0.0;

            if (RowCount ==2 && ColCount ==2)
            {
                determinant = (Get(0, 0) * Get(1, 1)) - (Get(0, 1) * Get(1, 0));

                return determinant;
            }
            else
            {
                for (var col = 0; col < ColCount; col++)
                {
                    determinant += Get(0, col) * CoFactor(0, col);
                }
            }

            return determinant;
        }

        public Matrix SubMatrix(int removeRow, int removeColumn)
        {
            var subMatrix = new Matrix(this.RowCount -1, this.ColCount-1);
            var destRow = 0;
            for (var srcRow = 0; srcRow < RowCount; srcRow++)
            {
                var destCol = 0;
                if (srcRow != removeRow)
                {
                    for (var srcCol = 0; srcCol < ColCount; srcCol++)
                    {
                        if (srcCol != removeColumn)
                        {
                            subMatrix.Set(destRow, destCol, Get(srcRow, srcCol));
                            destCol++;
                        }
                    }
                    destRow++;
                }
            }
            return subMatrix;
        }

        public double Minor(int row, int col)
        {
            var subMatrix = this.SubMatrix(row, col);
            return subMatrix.Determinant();
        }

        public double CoFactor(int row, int col)
        {
            var coFactor = Minor(row, col);
            var shouldNegate = (row + col) % 2 == 1;
            if (shouldNegate)
            {
                coFactor *=-1.0;
            }

            return coFactor;
        }

        public bool IsInvertible()
        {
            return  IsInvertible(Determinant());
        }

        public static bool IsInvertible(double determinant)
        {
            return !DoubleUtils.DoubleEquals(0.0, determinant);
        }

        public Matrix Inverse()
        {
            var determinant = Determinant();
            if (!IsInvertible(determinant))
            {
                throw new Exception("Unable to invert Matrix");
            }

            var inverseMatrix = new Matrix(RowCount,ColCount);

            for (var row = 0; row < RowCount; row++)
            {

                for (var col = 0; col < ColCount; col++)
                {
                    var coFactor = CoFactor(row, col);
                    inverseMatrix.Set(col,row, coFactor / determinant);
                }
            }
            
            return inverseMatrix;
        }

        public static Matrix operator *(Matrix a, Matrix b) => a.Multiply(b);
        public static RtTuple operator *(Matrix m, RtTuple t) => m.Multiply(t);

        public static Matrix Translation(double x, double y, double z)
        {
            var translation = IdentityMatrix;
            translation.Set(0, 3, x);
            translation.Set(1, 3, y);
            translation.Set(2, 3, z);

            return translation;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            for (int row = 0; row < RowCount; row++)
            {
                var values = new List<string>();
                for (int col = 0; col < ColCount; col++)
                {
                    values.Add($"{_matrixData[row, col]:0.0000}");
                }

                builder.Append("[ ");
                builder.Append(string.Join(", ",  values));
                builder.AppendLine(" ]");
            }

            return builder.ToString();
        }

        public static Matrix Scaling(double x, double y, double z)
        {
            var scaling = IdentityMatrix;
            scaling.Set(0,0,x);
            scaling.Set(1, 1, y);
            scaling.Set(2, 2, z);
            return scaling;
        }

        public static Matrix RotationX(double radians)
        {
            var rotationX = IdentityMatrix;

            var sin = Math.Sin(radians);
            var cos = Math.Cos(radians);

            rotationX.Set(2,1, sin);
            rotationX.Set(1, 2, -sin);
            rotationX.Set(2, 2, cos);
            rotationX.Set(1, 1, cos);
            return rotationX;
        }

        public static Matrix RotationY(double radians)
        {
            var rotationY = IdentityMatrix;
            var sin = Math.Sin(radians);
            var cos = Math.Cos(radians);

            rotationY.Set(0, 2, sin);
            rotationY.Set(2, 0, -sin);
            rotationY.Set(0, 0, cos);
            rotationY.Set(2, 2, cos);
            return rotationY;

        }

        public static Matrix RotationZ(double radians)
        {
            var rotationZ = IdentityMatrix;
            var sin = Math.Sin(radians);
            var cos = Math.Cos(radians);

            rotationZ.Set(0, 1, sin);
            rotationZ.Set(0, 1, -sin);
            rotationZ.Set(0, 0, cos);
            rotationZ.Set(1, 1, cos);
            return rotationZ;
        }

        public static Matrix Shearing(double xy, double xz, double yx, double yz, double zx, double zy)
        {
            var shearing = IdentityMatrix;

            shearing.Set(0, 1, xy);
            shearing.Set(0, 2, xz);
            shearing.Set(1, 0, yx);
            shearing.Set(1, 2, yz);
            shearing.Set(2, 0, zx);
            shearing.Set(2, 1, zy);

            return shearing;
        }

        public static Matrix ViewTransform(Point from, Point to, Vector up)
        {
            var forward = to.Subtract(from).Normalize().ToPoint();
            var upN = up.Normalize();
            var left = forward.Cross(upN);
            var trueUp = left.Cross(forward);

            var orientation = new Matrix(new double[,]
            {
                {left.X, left.Y, left.Z, 0},
                {trueUp.X, trueUp.Y, trueUp.Z, 0},
                {-forward.X, -forward.Y, -forward.Z, 0},
                {0.0, 0.0, 0.0, 1.0}
            });

            return orientation.Multiply( Matrix.Translation(-from.X, -from.Y, -from.Z));
        }
    }
}

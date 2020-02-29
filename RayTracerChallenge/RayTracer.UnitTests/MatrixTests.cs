using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace RayTracer.UnitTests
{
    [TestFixture]
    public class MatrixTests
    {
        [Test]
        public void Constructing4X4Matrix()
        {
            var data = new[,]
            {
                {1, 2, 3, 4}, 
                {5.5, 6.5, 7.5, 8.5}, 
                {9, 10, 11, 12}, 
                {13.5, 14.5, 15.5, 16.5}
            };
        
            var matrix = new Matrix(data);
            
            Assert.AreEqual(1, matrix.Get(0,0));
            Assert.AreEqual(4, matrix.Get(0, 3));
            Assert.AreEqual(5.5, matrix.Get(1, 0));
            Assert.AreEqual(7.5, matrix.Get(1, 2));
            Assert.AreEqual(11, matrix.Get(2, 2));
            Assert.AreEqual(13.5, matrix.Get(3, 0));
            Assert.AreEqual(15.5, matrix.Get(3, 2));
        }

        [Test]
        public void Constructing2X2Matrix()
        {
            var data = new double[,]
            {
                {-3, 5},
                {1, -2}
            };

            var matrix = new Matrix(data);

            Assert.AreEqual(-3, matrix.Get(0, 0));
            Assert.AreEqual(5, matrix.Get(0, 1));
            Assert.AreEqual(1, matrix.Get(1, 0));
            Assert.AreEqual(-2, matrix.Get(1, 1));
        }

        [Test]
        public void Constructing3X3Matrix()
        {
            var data = new double[,]
            {
                {-3, 5, 0},
                {1, -2, 7},
                {0, 1, 1}
            };

            var matrix = new Matrix(data);

            Assert.AreEqual(-3, matrix.Get(0, 0));
            Assert.AreEqual(0, matrix.Get(0, 2));
            Assert.AreEqual(-2, matrix.Get(1, 1));
            Assert.AreEqual(7, matrix.Get(1, 2));
            Assert.AreEqual(1, matrix.Get(2, 2));
        }

        [Test]
        public void EqualsWhenBothMatricesAreSame()
        {
            var matrixA = new Matrix(new double[,] {{1, 2, 3, 4}, {5, 6, 7, 8}, {9, 8, 7, 6}, {5, 4, 3, 2}});
            var matrixB = new Matrix(new double[,] {{1, 2, 3, 4}, {5, 6, 7, 8}, {9, 8, 7, 6}, {5, 4, 3, 2}});

            Assert.IsTrue(matrixA.Equals(matrixB));
        }

        [Test]
        public void EqualsWhenBothMatricesAreNotEqual()
        {
            var matrixA = new Matrix(new double[,] {{1, 2, 3, 4}, {5, 6, 7, 8}, {9, 8, 7, 6}, {5, 4, 3, 2}});
            var matrixB = new Matrix(new double[,] {{2, 3, 4, 5}, {6, 7, 8, 9}, {8, 7, 6, 5}, {4, 3, 2, 1}});

            Assert.IsFalse(matrixA.Equals(matrixB));
        }


        [Test]
        public void EqualsWhenBothMatricesAreNotSameSize()
        {
            var matrixA = new Matrix(new double[,] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 8, 7, 6 }, { 5, 4, 3, 2 } });
            var matrixB = new Matrix(new double[,] { { 3, 4, 5 }, { 6, 7, 8 }, { 7, 6, 5 } });

            Assert.IsFalse(matrixA.Equals(matrixB));
        }

        [Test]
        public void MultiplyTwoMatrices()
        {
            var matrixA = new Matrix(new double[,] {{1, 2, 3, 4}, {5, 6, 7, 8}, {9, 8, 7, 6}, {5, 4, 3, 2}});
            var matrixB = new Matrix(new double[,] {{-2, 1, 2, 3}, {3, 2, 1, -1}, {4, 3, 6, 5}, {1, 2, 7, 8}});

            var result = matrixA.Multiply(matrixB);

            var expectedMatrix = new Matrix(new double[,]
                {{20, 22, 50, 48}, {44, 54, 114, 108}, {40, 58, 110, 102}, {16, 26, 46, 42}});
            Assert.AreEqual(expectedMatrix, result);

        }

        [Test]
        public void MultiplyMatrixByTuple()
        {
            var matrix = new Matrix(new double[,] {{1, 2, 3, 4}, {2, 4, 4, 2}, {8, 6, 4, 1}, {0, 0, 0, 1}});
            
            var tuple = new RtTuple(1,2,3,1);

            var result = matrix.Multiply(tuple);

            var expectedResult = new RtTuple(18, 24, 33, 1);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void MultiplyMatrixByIdentityMatrix()
        {
            var matrixA = new Matrix(new double[,] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 8, 7, 6 }, { 5, 4, 3, 2 } });

            var result = matrixA.Multiply(Matrix.IdentityMatrix);

            Assert.AreEqual(matrixA, result);

        }

        [Test]
        public void MultiplyIdentityMatrixByTuple()
        {
            var tuple = new RtTuple(1, 2, 3, 1);

            var result = Matrix.IdentityMatrix.Multiply(tuple);

            Assert.AreEqual(tuple, result);
        }


        [Test]
        public void TransposeMatrix()
        {
            var matrixA = new Matrix(new double[,] {{0, 9, 3, 0}, {9, 8, 0, 8}, {1, 8, 5, 3}, {0, 0, 5, 8}});

            var result = matrixA.Transpose();

            var expectedMatrix = new Matrix(new double[,]
                {{0, 9, 1, 0}, {9, 8, 8, 0}, {3, 0, 5, 5,}, {0, 8, 3, 8}});
            Assert.AreEqual(expectedMatrix, result);

        }

        [Test]
        public void TransposeIdentityMatrix()
        {
            var result = Matrix.IdentityMatrix.Transpose();

            Assert.AreEqual(Matrix.IdentityMatrix, result);

        }

        [Test]
        public void CalculateDeterminant2x2MAtrix()
        {
            var matrix = new Matrix(new double[2, 2] {{1, 5}, {-3, 2}});

            Assert.AreEqual(17,matrix.Determinant());
        }

        [Test]
        public void SubMatrix3x3_Is2x2Matrix()
        {
            var matrix = new Matrix(new double[,] {{1, 5, 0}, {-3, 2, 7}, {0, 6, -3}});

            var expectedResult = new Matrix(new double[,] { {-3,2},{0,6}});

            Assert.AreEqual(expectedResult,matrix.SubMatrix(0,2));
        }

        [Test]
        public void SubMatrix4x4_Is3x3Matrix()
        {
            var matrix = new Matrix(new double[,] {{-6, 1, 1, 6}, {-8, 5, 8, 6}, {-1, 0, 8, 2}, {-7, 1, -1, 1}});

            var expectedResult = new Matrix(new double[,] {{-6, 1, 6}, {-8, 8, 6}, {-7, -1, 1}});

            Assert.AreEqual(expectedResult, matrix.SubMatrix(2, 1));
        }

        [Test]
        public void MinorOfA3x3Matrix()
        {

            var matrix = new Matrix(new double[,] { { 3,5,0 }, { 2,-1,-7 }, { 6,-1,5 } });

            Assert.AreEqual(25, matrix.Minor(1, 0));
        }

        [Test]
        public void CoFactorOfA3x3Matrix()
        {

            var matrix = new Matrix(new double[,] { { 3, 5, 0 }, { 2, -1, -7 }, { 6, -1, 5 } });

            Assert.AreEqual(-12, matrix.CoFactor(0, 0));
            Assert.AreEqual(-25, matrix.CoFactor(1, 0));
        }

        [Test]
        public void DeterminantOfA3x3Matrix()
        {
            var matrix = new Matrix(new double[,] {{1, 2, 6}, {-5, 8, -4}, {2, 6, 4}});

            Assert.AreEqual(56, matrix.CoFactor(0, 0));
            Assert.AreEqual(12, matrix.CoFactor(0, 1));
            Assert.AreEqual(-46, matrix.CoFactor(0, 2));
            Assert.AreEqual(-196, matrix.Determinant());
        }

        [Test]
        public void DeterminantOfA4x4Matrix()
        {
            var matrix = new Matrix(new double[,] {{-2, -8, 3, 5}, {-3, 1, 7, 3}, {1, 2, -9, 6}, {-6, 7, 7, -9}});

            Assert.AreEqual(690, matrix.CoFactor(0, 0));
            Assert.AreEqual(447, matrix.CoFactor(0, 1));
            Assert.AreEqual(210, matrix.CoFactor(0, 2));
            Assert.AreEqual(51, matrix.CoFactor(0, 3));
            Assert.AreEqual(-4071, matrix.Determinant());
        }

        [Test]
        public void IsInvertible_WithInvertibleMatrix()
        {
            var matrix = new Matrix(new double[,] {{6, 4, 4, 4}, {5, 5, 7, 6}, {4, -9, 3, -7}, {9, 1, 7, -6}});

            Assert.AreEqual(-2120, matrix.Determinant());
            Assert.IsTrue(matrix.IsInvertible());
        }

        [Test]
        public void IsNotInvertible_WithNonInvertibleMatrix()
        {
            var matrix = new Matrix(new double[,] { { -4, 2, -2, -3 }, { 9, 6, 2, 6 }, { 0, -5, 1, -5 }, { 0, 0, 0, 0 } });

            Assert.AreEqual(0, matrix.Determinant());
            Assert.IsFalse(matrix.IsInvertible());
        }


        [Test]
        public void InverseOfAMatrix()
        {
            var matrix = new Matrix(new double[,] {{-5, 2, 6, -8}, {1, -5, 1, 8}, {7, 7, -6, -7}, {1, -3, 7, 4}});

            var result = matrix.Inverse();

            var expectedMatrix = new Matrix(new double[,]
            {
                {0.21805, 0.45113, 0.24060, -0.04511},
                {-0.80827, -1.45677, -0.44361, 0.52068},
                {-0.07895, -0.22368, -0.05263, 0.19737},
                {-0.52256, -0.81391, -0.30075, 0.30639}
            });
            Assert.AreEqual(expectedMatrix,result);
            Assert.AreEqual(532, matrix.Determinant());
        }

        [Test]
        public void InverseOfASecondMatrix()
        {
            var matrix = new Matrix(new double[,] {
                { 8 , -5 , 9 , 2 },
                { 7 , 5 , 6 , 1 },
                { -6 , 0 , 9 , 6 },
                { -3 , 0 , -9 , -4 }

            });

            var result = matrix.Inverse();

            var expectedMatrix = new Matrix(new double[,]
            {
                { -0.15385 , -0.15385 , -0.28205 , -0.53846 },
                { -0.07692 , 0.12308 , 0.02564 , 0.03077 },
                { 0.35897 , 0.35897 , 0.43590 , 0.92308 },
                { -0.69231 , -0.69231 , -0.76923 , -1.92308 }
            });
            Assert.AreEqual(expectedMatrix, result);
        }

        [Test]
        public void InverseOfAThirdMatrix()
        {
            var matrix = new Matrix(new double[,] {
                {  9 ,  3 ,  0 ,  9 },
                { -5 , -2 , -6 , -3 },
                { -4 ,  9 ,  6 ,  4 },
                { -7 ,  6 ,  6 ,  2 }
            });

            var result = matrix.Inverse();

            var expectedMatrix = new Matrix(new double[,]
            {
                { -0.04074 , -0.07778 ,  0.14444 , -0.22222 },
                { -0.07778 ,  0.03333 ,  0.36667 , -0.33333 },
                { -0.02901 , -0.14630 , -0.10926 ,  0.12963 },
                {  0.17778 ,  0.06667 , -0.26667 ,  0.33333 }
            });
            Assert.AreEqual(expectedMatrix, result);
        }

        [Test]
        public void MultiplyAProductByItsInverse()
        {
            var matrixA = new Matrix(new double[,]
            {
                {3, 9, 7, 3},
                {3, -8, 2, -9},
                {-4, 4, 4, 1},
                {-6, 5, -1, 1}
            });

            var matrixB = new Matrix(new double[,]
            {
                {8, 2, 2, 2},
                {3, -1, 7, 0},
                {7, 0, 5, 4},
                {6, -2, 0, 5}
            });

            var matrixC = matrixA.Multiply(matrixB);
            var result = matrixC.Multiply(matrixB.Inverse());
            Assert.AreEqual(matrixA, result);
        }

        [Test]
        public void ToStringTest()
        {
            var matrixA = new Matrix(new double[,]
            {
                {3, 9, 7, 3},
                {3, -8, 2, -9},
                {-4, 4, 4, 1},
                {-6, 5, -1, 1}
            });
            var result = matrixA.ToString();
            var expectedResult =
                "[ 3.0000, 9.0000, 7.0000, 3.0000 ]\r\n[ 3.0000, -8.0000, 2.0000, -9.0000 ]\r\n"
                + "[ -4.0000, 4.0000, 4.0000, 1.0000 ]\r\n[ -6.0000, 5.0000, -1.0000, 1.0000 ]\r\n";
            Assert.AreEqual(expectedResult, result);

        }

        [Ignore("Experimental")]
        [Test]
        public void ExploreMatrices()
        {
            var inverseIdentity = Matrix.IdentityMatrix.Inverse();
            Assert.NotNull(inverseIdentity);

            var matrixA = new Matrix(new double[,]
            {
                {3, 9, 7, 3},
                {3, -8, 2, -9},
                {-4, 4, 4, 1},
                {-6, 5, -1, 1}
            });

            var multiplyByInverse = matrixA.Multiply(matrixA.Inverse());
            Assert.NotNull(inverseIdentity);
        }

        [Test]
        public void MultiplyByTranslationMatrix()
        {
            var translation = Matrix.Translation(5, -3, 2);
            var point = RtTuple.Point(-3, 4, 5);
            var result = translation * point;
            
            var expectedResult = RtTuple.Point(2, 1, 7);
            Assert.AreEqual(expectedResult, result);
        }


        [Test]
        public void MultiplyByInverseTranslationMatrix()
        {
            var translation = Matrix.Translation(5, -3, 2);
            var inv = translation.Inverse();
            var point = RtTuple.Point(-3, 4, 5);
            var result = inv * point;

            var expectedResult = RtTuple.Point(-8, 7, 3);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void TranslationDoesNotAffectVectors()
        {
            var translation = Matrix.Translation(5, -3, 2);
            var vector = RtTuple.Vector(-3, 4, 5);
            var result = translation * vector;

            Assert.AreEqual(vector, result);
        }

        [Test]
        public void ScalingMatrixMultiplyByPoint()
        {
            var scaling = Matrix.Scaling(2, 3, 4);
            var point = RtTuple.Point(-4, 6, 8);
            var result = scaling * point;

            var expectedResult = RtTuple.Point(-8, 18, 32);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void ScalingMatrixMultiplyByVector()
        {
            var scaling = Matrix.Scaling(2, 3, 4);
            var vector = RtTuple.Vector(-4, 6, 8);
            var result = scaling * vector;

            var expectedResult = RtTuple.Vector(-8, 18, 32);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void ScalingInversMatrixMultiplyByVector()
        {
            var scaling = Matrix.Scaling(2, 3, 4);
            var inv = scaling.Inverse();
            var vector = RtTuple.Vector(-4, 6, 8);
            var result = inv * vector;

            var expectedResult = RtTuple.Vector(-2, 2, 2);
            Assert.AreEqual(expectedResult, result);
        }
        [Test]
        public void ReflectionIsScalingByNegativeValue()
        {
            var scaling = Matrix.Scaling(-1, 1, 1);
            var point = RtTuple.Point(2, 3, 4);
            var result = scaling * point;

            var expectedResult = RtTuple.Point(-2, 3, 4);
            Assert.AreEqual(expectedResult, result);
        }


        [Test]
        public void RotatingPointAroundXAxis()
        {
            var point = RtTuple.Point(0, 1, 0);
            var halfQuarter = Matrix.RotationX(Math.PI / 4);
            var fullQuarter = Matrix.RotationX(Math.PI / 2);
            
            var halfResult = halfQuarter * point;
            Assert.AreEqual(RtTuple.Point(0, Math.Sqrt(2)/2, Math.Sqrt(2) / 2), halfResult);

            var fullResult = fullQuarter * point;
            Assert.AreEqual(RtTuple.Point(0, 0, 1), fullResult);

        }

        [Test]
        public void RotatingPointAroundYAxis()
        {
            var point = RtTuple.Point(0, 0, 1);
            var halfQuarter = Matrix.RotationY(Math.PI / 4);
            var fullQuarter = Matrix.RotationY(Math.PI / 2);

            var halfResult = halfQuarter * point;
            Assert.AreEqual(RtTuple.Point( Math.Sqrt(2) / 2, 0, Math.Sqrt(2) / 2), halfResult);

            var fullResult = fullQuarter * point;
            Assert.AreEqual(RtTuple.Point(1, 0, 0), fullResult);
        }

        [Test]
        public void RotatingPointAroundZAxis()
        {
            var point = RtTuple.Point(0, 1, 0);
            var halfQuarter = Matrix.RotationZ(Math.PI / 4);
            var fullQuarter = Matrix.RotationZ(Math.PI / 2);

            var halfResult = halfQuarter * point;
            Assert.AreEqual(RtTuple.Point(-(Math.Sqrt(2) / 2), Math.Sqrt(2) / 2, 0), halfResult);

            var fullResult = fullQuarter * point;
            Assert.AreEqual(RtTuple.Point(-1, 0, 0), fullResult);
        }

        [Test]
        public void ShearingTransformMoveXInProportionToY()
        {
            var shearing = Matrix.Shearing(1,0,0,0,0,0);
            var point = RtTuple.Point(2, 3, 4);
            var result = shearing * point;
            Assert.AreEqual(RtTuple.Point(5,3,4),result);
        }

        [Test]
        public void ShearingTransformMoveXInProportionToZ()
        {
            var shearing = Matrix.Shearing(0, 1, 0, 0, 0, 0);
            var point = RtTuple.Point(2, 3, 4);
            var result = shearing * point;
            Assert.AreEqual(RtTuple.Point(6, 3, 4), result);
        }

        [Test]
        public void ShearingTransformMoveYInProportionToX()
        {
            var shearing = Matrix.Shearing(0, 0, 1, 0, 0, 0);
            var point = RtTuple.Point(2, 3, 4);
            var result = shearing * point;
            Assert.AreEqual(RtTuple.Point(2, 5, 4), result);
        }

        [Test]
        public void ShearingTransformMoveYInProportionToZ()
        {
            var shearing = Matrix.Shearing(0, 0, 0, 1, 0, 0);
            var point = RtTuple.Point(2, 3, 4);
            var result = shearing * point;
            Assert.AreEqual(RtTuple.Point(2, 7, 4), result);
        }

        [Test]
        public void ShearingTransformMoveZInProportionToX()
        {
            var shearing = Matrix.Shearing(0, 0, 0, 0, 1, 0);
            var point = RtTuple.Point(2, 3, 4);
            var result = shearing * point;
            Assert.AreEqual(RtTuple.Point(2, 3, 6), result);
        }

        [Test]
        public void ShearingTransformMoveZInProportionToY()
        {
            var shearing = Matrix.Shearing(0, 0, 0, 0, 0, 1);
            var point = RtTuple.Point(2, 3, 4);
            var result = shearing * point;
            Assert.AreEqual(RtTuple.Point(2, 3, 7), result);
        }

        [Test]
        public void IndividualTransformationsAppliedInSequence()
        {
            var p = RtTuple.Point(1, 0, 1);

            var a = Matrix.RotationX(Math.PI / 2);
            var b = Matrix.Scaling(5, 5, 5);
            var c = Matrix.Translation(10, 5, 7);

            var p2 = a * p;
            Assert.AreEqual(RtTuple.Point(1,-1,0), p2);

            var p3 = b * p2;
            Assert.AreEqual(RtTuple.Point(5, -5, 0), p3);

            var p4 = c * p3;
            Assert.AreEqual(RtTuple.Point(15, 0, 7), p4);
        }

        [Test]
        public void ChainedTransformationsMustBeAppliedReverse()
        {
            var p = RtTuple.Point(1, 0, 1);

            var a = Matrix.RotationX(Math.PI / 2);
            var b = Matrix.Scaling(5, 5, 5);
            var c = Matrix.Translation(10, 5, 7);

            var t = c * b * a;
            var result = t * p;
            Assert.AreEqual(RtTuple.Point(15, 0, 7), result);
        }
    }
}

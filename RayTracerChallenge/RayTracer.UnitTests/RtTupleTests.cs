using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace RayTracer.UnitTests
{
    public class RtTupleTests
    {
        [Test(Description = "Scenario: A tuple with w = 1.0 is a point")]
        public void TupleWith_W_AsOneIsAPoint()
        {
            var a = new RtTuple(4.3, -4.2, 3.1, 1.0);
            Assert.IsTrue(DoubleUtils.DoubleEquals(a.X, 4.3), "X values do not match");
            Assert.IsTrue(DoubleUtils.DoubleEquals(a.Y, -4.2), "Y values do not match");
            Assert.IsTrue(DoubleUtils.DoubleEquals(a.Z, 3.1), "Z values do not match");
            Assert.IsTrue(DoubleUtils.DoubleEquals(a.W, 1.0), "W values do not match");
            Assert.IsTrue(a.IsPoint());
            Assert.IsFalse(a.IsVector());
        }

        [Test(Description = "Scenario: A tuple with w = 0.0 is a vector")]
        public void TupleWith_W_AsZeroIsAPoint()
        {
            var a = new RtTuple(4.3, -4.2, 3.1, 0.0);
            Assert.IsTrue(DoubleUtils.DoubleEquals(a.X, 4.3), "X values do not match");
            Assert.IsTrue(DoubleUtils.DoubleEquals(a.Y, -4.2), "Y values do not match");
            Assert.IsTrue(DoubleUtils.DoubleEquals(a.Z, 3.1), "Z values do not match");
            Assert.IsTrue(DoubleUtils.DoubleEquals(a.W, 0.0), "W values do not match");
            Assert.IsFalse(a.IsPoint());
            Assert.IsTrue(a.IsVector());
        }

        [Test(Description = "Scenario: point() creates tuples with w = 1")]
        public void CreatePoint()
        {
            var p = new Point(4,-4,3);
            var expected = new RtTuple(4,-4,3,1);
            Assert.IsTrue(p.Equals(expected));
        }

        [Test(Description = "Scenario: vector() creates tuples with w = 0")]
        public void CreateVector()
        {
            var p = new Vector(4, -4, 3);
            var expected = new RtTuple(4, -4, 3, 0);
            Assert.IsTrue(p.Equals(expected));
        }


        [Test(Description = "Scenario: Adding two tuples")]
        public void AddTwoTuples()
        {
            var a1 = new RtTuple(3,-2,5,1);
            var a2 = new RtTuple(-2, 3, 1, 0);
            var result = a1.Add(a2);
            var expectedResult = new RtTuple(1, 1, 6, 1);
            Assert.AreEqual(expectedResult,result);
        }

        [Test(Description = "Scenario: Subtracting two points")]
        public void SubtractTwoPoints_GivesVector()
        {
            var p1 = new Point(3, 2, 1);
            var p2 = new Point(5, 6, 7);
            var result = p1.Subtract(p2);
            var expectedResult = new Vector(-2,-4,-6);
            Assert.AreEqual(expectedResult,result);
        }


        [Test(Description = "Scenario: Subtracting a vector from a point")]
        public void SubtractVectorFromPoint_GivesPoint()
        {
            var p1 = new Point(3, 2, 1);
            var p2 = new Vector(5, 6, 7);
            var result = p1.Subtract(p2);
            var expectedResult = new Point(-2, -4, -6);
            Assert.AreEqual(expectedResult, result);
        }

        [Test(Description = "Scenario: Subtracting two vectors")]
        public void SubtractTwoVectors_GivesVector()
        {
            var v1 = new Vector(3, 2, 1);
            var v2 = new Vector(5, 6, 7);
            var result = v1.Subtract(v2);
            var expectedResult = new Vector(-2, -4, -6);
            Assert.AreEqual(expectedResult, result);
        }



        //Scenario: Subtracting a vector from the zero vector
        //    Given zero ← vector(0, 0, 0)
        //And v ← vector(1, -2, 3)
        //Then zero - v = vector(-1, 2, -3)
        [Test(Description = "Scenario: Subtracting a vector from the zero vector")]
        public void SubtractVectorFromZeroVector_GivesVector()
        {
            var v1 = new Vector(0, 0, 0);
            var v2 = new Vector(1, -2, 3);
            var result = v1.Subtract(v2);
            var expectedResult = new Vector(-1, 2, -3);
            Assert.AreEqual(expectedResult, result);
        }

        [Test(Description = "Scenario: Negating a tuple")]
        public void NegateTuple()
        {
            var tuple = new RtTuple(1, -2, 3, -4);
            var result = tuple.Negate();
            Assert.AreEqual(new RtTuple(-1, 2, -3, 4), result);
        }

        [Test(Description = "Scenario: Negating a tuple with operator")]
        public void NegateTupleOperator()
        {
            var tuple = new RtTuple(1, -2, 3, -4);
            var result = -tuple;
            Assert.AreEqual(new RtTuple(-1, 2, -3, 4), result);
        }

        [Test(Description = "Multiplying a tuple by a scalar")]
        public void MultiplyTupleByScalar()
        {
            var tuple = new RtTuple(1, -2, 3, -4);
            var result = tuple.Multiply(3.5);
            Assert.AreEqual(new RtTuple(3.5, -7, 10.5, -14), result);
        }

        [Test(Description = "Multiplying a tuple by a fraction")]
        public void MultiplyTupleByFraction()
        {
            var tuple = new RtTuple(1, -2, 3, -4);
            var result = tuple.Multiply(0.5);
            Assert.AreEqual(new RtTuple(0.5, -1, 1.5, -2), result);
        }

        [Test(Description = "Scenario: Dividing a tuple by a scalar")]
        public void DivideTupleByScalar()
        {
            var tuple = new RtTuple(1, -2, 3, -4);
            var result = tuple.Divide(2);
            Assert.AreEqual(new RtTuple(0.5, -1, 1.5, -2), result);
        }

        [Test(Description = "Scenario: Computing the magnitude of vector(1, 0, 0)")]
        public void MagnitudeOfVector_WithXOfOne()
        {
            var tuple = new Vector(1, 0, 0);
            var result = tuple.Magnitude();
            Assert.IsTrue(DoubleUtils.DoubleEquals(1, result), $"Expecting: 1, actual: {result}");
        }

        [Test(Description = "Scenario: Computing the magnitude of vector(0, 1, 0)")]
        public void MagnitudeOfVector_WithYOfOne()
        {
            var tuple = new Vector(0, 1, 0);
            var result = tuple.Magnitude();
            Assert.IsTrue(DoubleUtils.DoubleEquals(1, result), $"Expecting: 1, actual: {result}");
        }

        [Test(Description = "Scenario: Computing the magnitude of vector(0, 0, 1)")]
        public void MagnitudeOfVector_WithZOfOne()
        {
            var tuple = new Vector(0, 0, 1);
            var result = tuple.Magnitude();
            Assert.IsTrue(DoubleUtils.DoubleEquals(1, result), $"Expecting: 1, actual: {result}");
        }

        [Test(Description = "Scenario: Computing the magnitude of vector(1, 2, 3)")]
        public void MagnitudeOfVector_WithVectorOfOneTwoThree()
        {
            var tuple = new Vector(1, 2, 3);
            var result = tuple.Magnitude();
            Assert.IsTrue(DoubleUtils.DoubleEquals(Math.Sqrt(14), result), $"Expecting: 1, actual: {result}");
        }

        [Test(Description = "Scenario: Computing the magnitude of vector(-1, -2, -3)")]
        public void MagnitudeOfVector_WithVectorOfMinusOneTwoThree()
        {
            var tuple = new Vector(-1, -2, -3);
            var result = tuple.Magnitude();
            Assert.IsTrue(DoubleUtils.DoubleEquals(Math.Sqrt(14), result), $"Expecting: 1, actual: {result}");
        }


        [Test(Description = "Scenario: Normalizing vector(4, 0, 0) gives(1, 0, 0)")]
        public void NormalizeVector_WithVectorXOfFour()
        {
            var tuple = new Vector(4, -0, 0);
            var result = tuple.Normalize();
            Assert.AreEqual(new Vector(1, 0, 0), result);
        }

        [Test(Description = "Scenario: Normalizing vector(1, 2, 3)")]
        public void NormalizeVector_WithVectorOfOneTwoThree()
        {
            var tuple = new Vector(1, 2, 3);
            var result = tuple.Normalize();
            Assert.AreEqual(new Vector(0.26726, 0.53452, 0.80178), result);
        }

        [Test(Description = "Scenario: The magnitude of a normalized vector")]
        public void MagintudeOfNormalizedVector()
        {
            var tuple = new Vector(1, 2, 3);
            var normalized = tuple.Normalize();

            Assert.AreEqual(normalized.Magnitude(), 1);
        }

        [Test(Description = "Scenario: The dot product of two tuples")]
        public void DotProductOfTwoTuples()
        {
            var tupleA = new Vector(1, 2, 3);
            var tupleB = new Vector(2, 3, 4);
            var result = tupleA.Dot(tupleB);

            Assert.AreEqual(20, result);
        }

        [Test(Description = "Scenario: The cross product of two tuples")]
        public void CrossProductOfTwoVectors()
        {
            var tupleA = new Vector(1, 2, 3);
            var tupleB = new Vector(2, 3, 4);

            Assert.AreEqual(new Vector(-1, 2, -1), tupleA.Cross(tupleB));
            Assert.AreEqual(new Vector(1, -2, 1), tupleB.Cross(tupleA));
        }
        
    }
}

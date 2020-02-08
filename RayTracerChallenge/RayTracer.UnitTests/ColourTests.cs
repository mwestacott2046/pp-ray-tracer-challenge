using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace RayTracer.UnitTests
{
    [TestFixture]
    public class ColourTests
    {
        [Test]
        public void ColoursAreRgbValues()
        {
            var c = new Colour(-0.5, 0.4, 1.7);
            Assert.AreEqual(-0.5, c.R);
            Assert.AreEqual(0.4, c.G);
            Assert.AreEqual(1.7, c.B);
        }

        [Test]
        public void AddColourTest()
        {
            var c1 = new Colour(0.9, 0.6, 0.75);
            var c2 = new Colour(0.7, 0.1, 0.25);
            var result = c1.Add(c2);
            Assert.AreEqual(new Colour(1.6, 0.7, 1.0), result);
        }

        [Test]
        public void SubtractColourTest()
        {
            var c1 = new Colour(0.9, 0.6, 0.75);
            var c2 = new Colour(0.7, 0.1, 0.25);
            var result = c1.Subtract(c2);
            Assert.AreEqual(new Colour(0.2, 0.5, 0.5), result);
        }

        [Test]
        public void MultiplyColourByScalarTest()
        {
            var c1 = new Colour(0.2, 0.3, 0.4);
            var result = c1.Multiply(2);
            Assert.AreEqual(new Colour(0.4, 0.6, 0.8), result);
        }

        [Test]
        public void MultiplyColourTest()
        {
            var c1 = new Colour(1, 0.2, 0.4);
            var c2 = new Colour(0.9, 1, 0.1);
            var result = c1.Multiply(c2);
            Assert.AreEqual(new Colour(0.9, 0.2, 0.04), result);
        }


    }
}

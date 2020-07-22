using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace RayTracer.UnitTests
{
    public class ColourFactoryTests
    {
        [Test]
        public void FromRgbIntAllZeros()
        {
            var result = ColourFactory.FromRgbInt(0, 0, 0);

            Assert.AreEqual(new Colour(0, 0, 0), result);
        }

        [Test]
        public void FromRgbIntAll255()
        {
            var result = ColourFactory.FromRgbInt(255, 255, 255);

            Assert.AreEqual(new Colour(1, 1, 1), result);
        }

        [Test]
        public void FromRgbIntAll127()
        {
            var result = ColourFactory.FromRgbInt(127, 127, 127);

            Assert.AreEqual(0.5, Math.Round(result.R, 1));
            Assert.AreEqual(0.5, Math.Round(result.G, 1));
            Assert.AreEqual(0.5, Math.Round(result.B, 1));
        }

        [Test]
        public void FromRgbIntAllTooLarge()
        {
            var result = ColourFactory.FromRgbInt(500, 1024, 256);
            Assert.AreEqual(new Colour(1, 1, 1), result);
        }

        [Test]
        public void FromRgbIntAllNegative()
        {
            var result = ColourFactory.FromRgbInt(-500, -1024, -256);
            Assert.AreEqual(new Colour(0, 0, 0), result);
        }
    }
}

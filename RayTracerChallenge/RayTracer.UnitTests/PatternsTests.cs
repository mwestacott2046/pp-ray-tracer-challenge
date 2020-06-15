using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace RayTracer.UnitTests
{
    public class PatternsTests
    {
        [Test]
        public void CreatingStripePattern()
        {
            var pattern = new Pattern(Colour.Black, Colour.White);

            Assert.AreEqual(Colour.Black, pattern.A);
            Assert.AreEqual(Colour.White, pattern.B);
        }
        
        [Test]
        public void StripPatternIsConstantY()
        {
            var pattern = new Pattern(Colour.White, Colour.Black);

            Assert.AreEqual(Colour.White, pattern.StripeAt(new Point(0, 0, 0)));
            Assert.AreEqual(Colour.White, pattern.StripeAt(new Point(0, 1, 0)));
            Assert.AreEqual(Colour.White, pattern.StripeAt(new Point(0, 2, 0)));
        }

        [Test]
        public void StripPatternIsConstantZ()
        {
            var pattern = new Pattern(Colour.White, Colour.Black);

            Assert.AreEqual(Colour.White, pattern.StripeAt(new Point(0, 0, 0)));
            Assert.AreEqual(Colour.White, pattern.StripeAt(new Point(0, 0, 1)));
            Assert.AreEqual(Colour.White, pattern.StripeAt(new Point(0, 0, 2)));
        }

        [Test]
        public void StripPatternAlternatesInX()
        {
            var pattern = new Pattern(Colour.White, Colour.Black);

            Assert.AreEqual(Colour.White, pattern.StripeAt(new Point(0, 0, 0)));
            Assert.AreEqual(Colour.White, pattern.StripeAt(new Point(0.9, 0, 0)));
            Assert.AreEqual(Colour.Black, pattern.StripeAt(new Point(1, 0, 0)));
            Assert.AreEqual(Colour.White, pattern.StripeAt(new Point(2, 0, 0)));
            Assert.AreEqual(Colour.Black, pattern.StripeAt(new Point(3, 0, 0)));
            Assert.AreEqual(Colour.Black, pattern.StripeAt(new Point(-0.1, 0, 0)));
            Assert.AreEqual(Colour.White, pattern.StripeAt(new Point(-1, 0, 0)));
            Assert.AreEqual(Colour.White, pattern.StripeAt(new Point(-1.1, 0, 0)));
            Assert.AreEqual(Colour.Black, pattern.StripeAt(new Point(-2, 0, 0)));
        }
    }

}

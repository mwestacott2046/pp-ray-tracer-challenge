using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using RayTracer.Shapes;

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

        [Test]
        public void StripesWithObjectTransformation()
        {
            var s = new Sphere();

            s.Transform = Matrix.Scaling(2,2,2);

            var pattern = new Pattern(Colour.White, Colour.Black);

            var colour = pattern.StripeAtObject(s, new Point(1.5,0,0));

            Assert.AreEqual(Colour.White,colour);
        }

        [Test]
        public void StripesWithPatternTransformation()
        {
            var s = new Sphere();

            var pattern = new Pattern(Colour.White, Colour.Black);
            pattern.Transform = Matrix.Scaling(2, 2, 2);

            var colour = pattern.StripeAtObject(s, new Point(1.5, 0, 0));

            Assert.AreEqual(Colour.White, colour);
        }

        [Test]
        public void StripesWithObjectAndPatternTransformation()
        {
            var s = new Sphere();
            s.Transform = Matrix.Scaling(2, 2, 2);

            var pattern = new Pattern(Colour.White, Colour.Black);
            pattern.Transform = Matrix.Translation(0.5, 0, 0);

            var colour = pattern.StripeAtObject(s, new Point(1.5, 0, 0));

            Assert.AreEqual(Colour.White, colour);
        }
    }

}

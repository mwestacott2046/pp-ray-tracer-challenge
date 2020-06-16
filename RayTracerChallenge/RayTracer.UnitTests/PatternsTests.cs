using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using RayTracer.Patterns;
using RayTracer.Shapes;

namespace RayTracer.UnitTests
{
    public class PatternsTests
    {
        [Test]
        public void CreatingStripePattern()
        {
            var pattern = new StripePattern(Colour.Black, Colour.White);

            Assert.AreEqual(Colour.Black, pattern.A);
            Assert.AreEqual(Colour.White, pattern.B);
        }
        
        [Test]
        public void StripPatternIsConstantY()
        {
            var pattern = new StripePattern(Colour.White, Colour.Black);

            Assert.AreEqual(Colour.White, pattern.PatternAt(new Point(0, 0, 0)));
            Assert.AreEqual(Colour.White, pattern.PatternAt(new Point(0, 1, 0)));
            Assert.AreEqual(Colour.White, pattern.PatternAt(new Point(0, 2, 0)));
        }

        [Test]
        public void StripPatternIsConstantZ()
        {
            var pattern = new StripePattern(Colour.White, Colour.Black);

            Assert.AreEqual(Colour.White, pattern.PatternAt(new Point(0, 0, 0)));
            Assert.AreEqual(Colour.White, pattern.PatternAt(new Point(0, 0, 1)));
            Assert.AreEqual(Colour.White, pattern.PatternAt(new Point(0, 0, 2)));
        }

        [Test]
        public void StripPatternAlternatesInX()
        {
            var pattern = new StripePattern(Colour.White, Colour.Black);

            Assert.AreEqual(Colour.White, pattern.PatternAt(new Point(0, 0, 0)));
            Assert.AreEqual(Colour.White, pattern.PatternAt(new Point(0.9, 0, 0)));
            Assert.AreEqual(Colour.Black, pattern.PatternAt(new Point(1, 0, 0)));
            Assert.AreEqual(Colour.White, pattern.PatternAt(new Point(2, 0, 0)));
            Assert.AreEqual(Colour.Black, pattern.PatternAt(new Point(3, 0, 0)));
            Assert.AreEqual(Colour.Black, pattern.PatternAt(new Point(-0.1, 0, 0)));
            Assert.AreEqual(Colour.White, pattern.PatternAt(new Point(-1, 0, 0)));
            Assert.AreEqual(Colour.White, pattern.PatternAt(new Point(-1.1, 0, 0)));
            Assert.AreEqual(Colour.Black, pattern.PatternAt(new Point(-2, 0, 0)));
        }

        [Test]
        public void StripesWithObjectTransformation()
        {
            var s = new Sphere();

            s.Transform = Matrix.Scaling(2,2,2);

            var pattern = new StripePattern(Colour.White, Colour.Black);

            var colour = pattern.PatternAtShape(s, new Point(1.5,0,0));

            Assert.AreEqual(Colour.White,colour);
        }

        [Test]
        public void StripesWithPatternTransformation()
        {
            var s = new Sphere();

            var pattern = new StripePattern(Colour.White, Colour.Black);
            pattern.Transform = Matrix.Scaling(2, 2, 2);

            var colour = pattern.PatternAtShape(s, new Point(1.5, 0, 0));

            Assert.AreEqual(Colour.White, colour);
        }

        [Test]
        public void StripesWithObjectAndPatternTransformation()
        {
            var s = new Sphere();
            s.Transform = Matrix.Scaling(2, 2, 2);

            var pattern = new StripePattern(Colour.White, Colour.Black);
            pattern.Transform = Matrix.Translation(0.5, 0, 0);

            var colour = pattern.PatternAtShape(s, new Point(1.5, 0, 0));

            Assert.AreEqual(Colour.White, colour);
        }

        [Test]
        public void GradientLinearlyInterpolatesBetweenColors()
        {
            var pattern = new GradientPattern(Colour.White, Colour.Black);

            Assert.AreEqual(Colour.White, pattern.PatternAt(new Point(0, 0, 0)));
            Assert.AreEqual(new Colour(0.75,0.75,0.75), pattern.PatternAt(new Point(0.25, 0, 0)));
            Assert.AreEqual(new Colour(0.5, 0.5, 0.5), pattern.PatternAt(new Point(0.5, 0, 0)));
            Assert.AreEqual(new Colour(0.25, 0.25, 0.25), pattern.PatternAt(new Point(0.75, 0, 0)));
        }

        [Test]
        public void RingPattern()
        {
            var pattern = new RingPattern(Colour.White, Colour.Black);

            Assert.AreEqual(Colour.White,pattern.PatternAt(new Point(0,0,0)));
            Assert.AreEqual(Colour.Black, pattern.PatternAt(new Point(1, 0, 0)));
            Assert.AreEqual(Colour.Black, pattern.PatternAt(new Point(0, 0, 1)));
            Assert.AreEqual(Colour.Black, pattern.PatternAt(new Point(0.708, 0, 0.708)));
        }

        [Test]
        public void CheckersShouldRepeatInX()
        {
            var pattern = new CheckerboardPattern(Colour.White, Colour.Black);

            Assert.AreEqual(Colour.White, pattern.PatternAt(new Point(0, 0, 0)));
            Assert.AreEqual(Colour.White, pattern.PatternAt(new Point(0.99, 0, 0)));
            Assert.AreEqual(Colour.Black, pattern.PatternAt(new Point(1.01, 0, 0)));
        }

        [Test]
        public void CheckersShouldRepeatInY()
        {
            var pattern = new CheckerboardPattern(Colour.White, Colour.Black);

            Assert.AreEqual(Colour.White, pattern.PatternAt(new Point(0, 0, 0)));
            Assert.AreEqual(Colour.White, pattern.PatternAt(new Point(0, 0.99, 0)));
            Assert.AreEqual(Colour.Black, pattern.PatternAt(new Point(0, 1.01, 0)));
        }

        [Test]
        public void CheckersShouldRepeatInZ()
        {
            var pattern = new CheckerboardPattern(Colour.White, Colour.Black);

            Assert.AreEqual(Colour.White, pattern.PatternAt(new Point(0, 0, 0)));
            Assert.AreEqual(Colour.White, pattern.PatternAt(new Point(0, 0, 0.99)));
            Assert.AreEqual(Colour.Black, pattern.PatternAt(new Point(0, 0, 1.01)));
        }
    }

}

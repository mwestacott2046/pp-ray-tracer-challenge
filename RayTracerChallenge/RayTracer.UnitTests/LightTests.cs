using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace RayTracer.UnitTests
{
    public class LightTests
    {
        [Test]
        public void LightHasPositionAndIntensity()
        {
            var intensity = new Colour(1,1,1);
            var position = new Point(0,0,0);

            var light = new Light(position, intensity);

            Assert.AreEqual(intensity, light.Intensity);
            Assert.AreEqual(position, light.Position);
        }
    }
}

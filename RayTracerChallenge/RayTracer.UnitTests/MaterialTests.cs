using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace RayTracer.UnitTests
{
    public class MaterialTests
    {
        [Test]
        public void DefaultMaterial()
        {
            var m = new Material();

            Assert.AreEqual(new Colour(1,1,1), m.Colour);
            Assert.AreEqual(0.1, m.Ambient);
            Assert.AreEqual(0.9, m.Diffuse);
            Assert.AreEqual(0.9, m.Specular);
            Assert.AreEqual(200.0, m.Shininess);
        }


        //Lighting with the eye between the light and the surface
        [Test]
        public void LightingWithEyeBetweenLightAndSurface()
        {
            var m = new Material();
            var position = new Point(0, 0, 0);

            var eyeV = new Vector(0,0,-1);
            var normalV = new Vector(0, 0, -1);
            var light = new Light(new Point(0, 0, -10), new Colour(1, 1, 1));

            var result = Light.Lighting(m, light, position, eyeV, normalV);

            Assert.AreEqual(new Colour(1.9, 1.9, 1.9), result);
        }

        //Lighting with the eye between light and surface, eye offset 45°
        [Test]
        public void LightingWithEyeOffsetAt45deg()
        {
            var m = new Material();
            var position = new Point(0, 0, 0);

            var eyeV = new Vector(0, Math.Sqrt(2)/2, -Math.Sqrt(2) / 2);
            var normalV = new Vector(0, 0, -1);
            var light = new Light(new Point(0, 0, -10), new Colour(1, 1, 1));

            var result = Light.Lighting(m, light, position, eyeV, normalV);

            Assert.AreEqual(new Colour(1.0, 1.0, 1.0), result);
        }

        //Lighting with eye opposite surface, light offset 45°
        [Test]
        public void LightingWithEyeOppositeOffsetAt45deg()
        {
            var m = new Material();
            var position = new Point(0, 0, 0);

            var eyeV = new Vector(0, 0,-1);
            var normalV = new Vector(0, 0, -1);
            var light = new Light(new Point(0, 10, -10), new Colour(1, 1, 1));

            var result = Light.Lighting(m, light, position, eyeV, normalV);

            Assert.AreEqual(new Colour(0.7364, 0.7364, 0.7364), result);
        }

        //Lighting with eye in the path of the reflection vector
        [Test]
        public void LightingWithEyeInPathOfReflectionVector()
        {
            var m = new Material();
            var position = new Point(0, 0, 0);

            var eyeV = new Vector(0, -Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2);

            var normalV = new Vector(0, 0, -1);
            var light = new Light(new Point(0, 10, -10), new Colour(1, 1, 1));

            var result = Light.Lighting(m, light, position, eyeV, normalV);

            Assert.AreEqual(new Colour(1.6364, 1.6364, 1.6364), result);
        }

        // Lighting with the light behind the surface
        [Test]
        public void LightingWithLightBehindTheSurface()
        {
            var m = new Material();
            var position = new Point(0, 0, 0);

            var eyeV = new Vector(0, 0, -1);
            var normalV = new Vector(0, 0, -1);
            var light = new Light(new Point(0, 0, 10), new Colour(1, 1, 1));

            var result = Light.Lighting(m, light, position,eyeV, normalV);

            Assert.AreEqual(new Colour(0.1,0.1,0.1), result);
        }
    }
}

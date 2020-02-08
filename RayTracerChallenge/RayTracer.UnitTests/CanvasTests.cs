using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace RayTracer.UnitTests
{
    [TestFixture]
    public class CanvasTests
    {
        [Test]
        public void CreateCanvas()
        {
            var canvas = new Canvas(10, 20);

            var expectedPixel = new Colour(0, 0, 0);
            for (var x = 0; x < 10; x++)
            {
                for (var y = 0; y < 20; y++)
                {
                    Assert.AreEqual(expectedPixel, canvas.GetPixel(x,y));
                }
            }
        }

        [Test]
        public void WritePixel()
        {
            var canvas = new Canvas(10, 20);
            var red = new Colour(1, 0, 0);

            canvas.SetPixel(2, 3, red);

            Assert.AreEqual(red,canvas.GetPixel(2,3));
        }

        [Test]
        public void ToPPMHeader()
        {

            var canvas = new Canvas(5, 3);
            const string expectedHeader = "P3\r\n5 3\r\n255";
            var result = canvas.ToPpm();

            Assert.AreEqual(expectedHeader, result.Substring(0, expectedHeader.Length));

        }

        [Test]
        public void ToPpmBody()
        {

            var c1 = new Colour(1.5, 0, 0);
            var c2 = new Colour(0, 0.5, 0);
            var c3 = new Colour(-0.5, 0, 1);

            var canvas = new Canvas(5, 3);

            canvas.SetPixel(0, 0, c1);
            canvas.SetPixel(2, 1, c2);
            canvas.SetPixel(4, 2, c3);

            const string expectedHeader = "P3\r\n5 3\r\n255\r\n";
            var expectedBody = "255 0 0 0 0 0 0 0 0 0 0 0 0 0 0\r\n0 0 0 0 0 0 0 127 0 0 0 0 0 0 0\r\n0 0 0 0 0 0 0 0 0 0 0 0 0 0 255\r\n";
            var result = canvas.ToPpm();
            var resultBody = result.Substring(expectedHeader.Length);
            Assert.AreEqual(expectedBody, resultBody);

        }

        [Test]
        public void ToPpmBodyMultiline()
        {

            var c1 = new Colour(1, 0.8, 0.6);

            var canvas = new Canvas(10, 2);
            for (var x = 0; x < 10; x++)
            {
                for (var y = 0; y < 2; y++)
                {
                    canvas.SetPixel(x, y, c1);
                }
            }
            
            const string expectedHeader = "P3\r\n10 2\r\n255\r\n";
            var result = canvas.ToPpm();
            var bodyString = result.Substring(expectedHeader.Length);
            var expectedBody = "255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204 153\r\n255 204 153 255 204 153 255 204 153 255 204 153\r\n255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204 153\r\n255 204 153 255 204 153 255 204 153 255 204 153\r\n";
            Assert.AreEqual(expectedBody, bodyString);
        }

        [Test]
        public void ToPpmFileEndsWithNewline()
        {

            var c1 = new Colour(1, 0.8, 0.6);

            var canvas = new Canvas(10, 2);
            for (var x = 0; x < 10; x++)
            {
                for (var y = 0; y < 2; y++)
                {
                    canvas.SetPixel(x, y, c1);
                }
            }

            var result = canvas.ToPpm();
            Assert.IsTrue(result.EndsWith("\r\n"));
        }

    }
}

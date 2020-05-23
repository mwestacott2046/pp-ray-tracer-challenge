using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace RayTracer.UnitTests
{
    [TestFixture]
    public class CameraTests
    {
        [Test]
        public void ConstructingACamera()
        {
            var hSize = 160;
            var vSize = 120;
            var fieldOfView = Math.PI / 2;

            var camera = new Camera(hSize, vSize, fieldOfView);

            Assert.AreEqual(hSize, camera.HSize);
            Assert.AreEqual(vSize, camera.VSize);
            Assert.AreEqual(fieldOfView, camera.FieldOfView);
            Assert.AreEqual(Matrix.IdentityMatrix, camera.Transform);
        }

        [Test]
        public void PixelSizeForHorizontalCanvas()
        {

            var camera = new Camera(200, 125, Math.PI/2);

            Assert.IsTrue(DoubleUtils.DoubleEquals(0.01, camera.PixelSize));
        }

        [Test]
        public void PixelSizeForVerticalCanvas()
        {

            var camera = new Camera(125, 200, Math.PI / 2);

            Assert.IsTrue(DoubleUtils.DoubleEquals(0.01, camera.PixelSize));
        }

        [Test]
        public void CreateRayThroughCentreOfCanvas()
        {
            var camera = new Camera(201, 101, Math.PI / 2);

            var ray = camera.RayForPixel(100, 50);

            Assert.AreEqual(new Point(0,0,0), ray.Origin);
            Assert.AreEqual(new Vector(0, 0, -1), ray.Direction);
        }

        [Test]
        public void CreateRayThroughCornerOfCanvas()
        {
            var camera = new Camera(201, 101, Math.PI / 2);

            var ray = camera.RayForPixel(0, 0);

            Assert.AreEqual(new Point(0, 0, 0), ray.Origin);
            Assert.AreEqual(new Vector(0.66519, 0.33259, -0.66851), ray.Direction);
        }

        [Test]
        public void CreateRayWhenCameraTransformed()
        {
            var camera = new Camera(201, 101, Math.PI / 2);
            camera.Transform = Matrix.RotationY(Math.PI/4) * Matrix.Translation(0,-2,5);

            var ray = camera.RayForPixel(100, 50);

            Assert.AreEqual(new Point(0, 2, -5), ray.Origin);
            Assert.AreEqual(new Vector(Math.Sqrt(2) / 2, 0, -(Math.Sqrt(2) / 2)), ray.Direction);
        }

        [Test]
        public void RenderWorldWithCamera()
        {
            var world = World.DefaultWorld;
            var camera = new Camera(11,11,Math.PI/2);

            var from = new Point(0,0,-5);
            var to = new Point(0,0,0);
            var up = new Vector(0,1,0);

            camera.Transform = Matrix.ViewTransform(from,to, up);
            var image = camera.Render(world);

            Assert.AreEqual(new Colour(0.38066, 0.47583, 0.2855), image.GetPixel(5,5));

        }
    }
}

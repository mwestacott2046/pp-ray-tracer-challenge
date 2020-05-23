using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace RayTracer.UnitTests
{
    public class TransformationsTests
    {
        //The transformation matrix for the default orientation
        [Test]
        public void ViewTransformMatrixForTheDefaultOrientation()
        {
            var from = new Point(0,0,0);
            var to = new Point(0, 0, -1);
            var up = new Vector(0, 1, 0);

            var result = Matrix.ViewTransform(from, to, up);

            Assert.AreEqual(Matrix.IdentityMatrix, result);
        }

        [Test]
        public void ViewTransformMatrixLookingInPositiveZDirection()
        {
            var from = new Point(0, 0, 0);
            var to = new Point(0, 0, 1);
            var up = new Vector(0, 1, 0);

            var result = Matrix.ViewTransform(from, to, up);

            Assert.AreEqual(Matrix.Scaling(-1,1,-1), result);
        }

        [Test]
        public void ViewTransformMatrixMovesTheWorld()
        {
            var from = new Point(0, 0, 8);
            var to = new Point(0, 0, 0);
            var up = new Vector(0, 1, 0);

            var result = Matrix.ViewTransform(from, to, up);

            Assert.AreEqual(Matrix.Translation(0,0, -8), result);
        }

        [Test]
        public void ArbitraryViewTransform()
        {
            var from = new Point(1,3,2);
            var to = new Point(4,-2,8);
            var up = new Vector(1, 1, 0);

            var result = Matrix.ViewTransform(from, to, up);


            var expectedMatrix = new Matrix(new double[,]
            {
                {-0.50709, 0.50709, 0.67612, -2.36643},
                {0.76772, 0.60609, 0.12122, -2.82843},
                {-0.35857, 0.59761, -0.71714, 0.00000},
                {0, 0, 0, 1.0}
            });
            Assert.AreEqual(expectedMatrix, result);
        }
    }
}

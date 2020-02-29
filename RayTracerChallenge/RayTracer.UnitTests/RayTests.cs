using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace RayTracer.UnitTests
{
    [TestFixture]
    public class RayTests
    {
        [Test]
        public void CreateAndQueryRay()
        {
            var origin = new Point(1,2,3);
            var direction = new Vector(4,5,6);

            var ray = new Ray(origin, direction);

            Assert.AreEqual(origin, ray.Origin);
            Assert.AreEqual(direction, ray.Direction);
        }

        [Test]
        public void ComputePointFromDistance()
        {
            var ray = new Ray(new Point(2,3,4), new Vector(1,0,0));

            Assert.AreEqual(new Point(2, 3, 4), ray.Position(0));
            Assert.AreEqual(new Point(3, 3, 4), ray.Position(1));
            Assert.AreEqual(new Point(1, 3, 4), ray.Position(-1));
            Assert.AreEqual(new Point(4.5, 3, 4), ray.Position(2.5));
        }
    }
}

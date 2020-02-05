using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace RayTracer.UnitTests
{
    [TestFixture]
    public class ProjectilesTest
    {
        [Test]
        public void PlayProjectiles()
        {
            var projectiles = new Projectiles();
            var result = projectiles.RunProjectiles();
            Console.Out.WriteLine(result);
            Assert.NotNull(result);
        }
    }
}

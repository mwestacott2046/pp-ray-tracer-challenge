using System;
using NUnit.Framework;
using RayTracer.ProjectileExperiments;

namespace RayTracer.UnitTests.ProjectileExperiments
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

using NUnit.Framework;
using RayTracer.ProjectileExperiments;

namespace RayTracer.UnitTests.ProjectileExperiments
{
    public class ProjectilesOnCanvasTest
    {
        [Test]
        public void RunCanvasProjectile()
        {
            var proj = new ProjectilesOnCanvas();
            proj.RunProjectiles();
        }
        
    }
}

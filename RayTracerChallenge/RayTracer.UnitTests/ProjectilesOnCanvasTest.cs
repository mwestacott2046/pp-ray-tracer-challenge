using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace RayTracer.UnitTests
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

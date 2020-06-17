using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using RayTracer.Patterns;
using RayTracer.Scenes;
using RayTracer.Shapes;

namespace RayTracer.UnitTests.IntegrationTests
{
    [TestFixture]
    public class ReflectionRefractionWorldSceneTests
    {
        [Ignore("LongRunningTest")]
        [Test]
        public void RenderScene()
        {
            var scene = new ShinyBallPlaneScene("RefractionWorldScene.ppm");
            SceneRender.Render(scene);
        }
    }
}

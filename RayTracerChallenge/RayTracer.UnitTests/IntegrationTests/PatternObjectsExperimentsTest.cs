using System;
using System.IO;
using NUnit.Framework;
using RayTracer.Shapes;

namespace RayTracer.UnitTests.IntegrationTests
{
    [TestFixture]
    public class PatternObjectsExperimentsTest
    {
        [Ignore("LongRunningTest")]
        [Test]
        public void RenderScene()
        {
            var world = new World();

            world.LightSource = new Light(new Point(-10,10,-10), new Colour(1,1,1));


            var floorPlane = new Plane();
            var floorPattern = new Pattern(new Colour(0.9,0.3,0.3), new Colour(0,0.8,0.6));
            
            floorPlane.Material.Pattern = floorPattern;
            world.SceneObjects.Add(floorPlane);

            var middleSphere = new Sphere
            {
                Transform = Matrix.Translation(-0.5, 1, 0.5),
                Material = new Material
                {
                    Colour = new Colour(0.1, 1, 0.5), 
                    Diffuse = 0.7, 
                    Specular = 0.2,
                    Pattern = new Pattern(new Colour(0.1, 1, 0.5), new Colour(0.5, 0.8, 0.6))
        }
            };

            world.SceneObjects.Add(middleSphere);


            var rightSphere = new Sphere
            {
                Transform = Matrix.Translation(1.5, 0.5, -0.5) * Matrix.Scaling(0.5,0.5,0.5),
                Material = new Material
                {
                    Colour = new Colour(0.5, 1, 0.1),
                    Diffuse = 0.7,
                    Specular = 0.2
                }
            };

            world.SceneObjects.Add(rightSphere);

            var leftSphere = new Sphere
            {
                Transform = Matrix.Translation(-1.5, 0.33, -0.75) * Matrix.Scaling(0.33, 0.33, 0.33),
                Material = new Material
                {
                    Colour = new Colour(1, 0.8, 0.1),
                    Diffuse = 0.7,
                    Specular = 0.2
                }
            };

            world.SceneObjects.Add(leftSphere);

            var rightSphere2 = new Sphere
            {
                Transform = Matrix.Translation(1.5, 2.5, -0.5) * Matrix.Scaling(0.4, 0.2, 0.8),
                Material = new Material
                {
                    Colour = new Colour(0.6, 0.2, 0.7),
                    Diffuse = 0.7,
                    Specular = 0.2
                }
            };

            world.SceneObjects.Add(rightSphere2);


            var camera = new Camera(500,400, Math.PI/3);
            camera.Transform = Matrix.ViewTransform(new Point(0, 1.5, -6), new Point(0, 1, 0), new Vector(0.3, 1, 0));

            var canvas = camera.Render(world);

            var output = canvas.ToPpm();
            File.WriteAllText("PatternExperiment.ppm", output);
        }


    }
}

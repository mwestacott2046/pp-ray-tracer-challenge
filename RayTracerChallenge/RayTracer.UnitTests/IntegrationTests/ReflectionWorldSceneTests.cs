using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using RayTracer.Patterns;
using RayTracer.Shapes;

namespace RayTracer.UnitTests.IntegrationTests
{
    [TestFixture]
    public class ReflectionWorldSceneTests
    {
        [Ignore("LongRunningTest")]
        [Test]
        public void RenderScene()
        {
            var world = new World();
            world.LightSource = new Light(new Point(-10, 10, -10), new Colour(0.8, 1, 1));


            var floorPlane = new Plane();
            floorPlane.Material.Colour = new Colour(0.6,0.6,0.6);
            floorPlane.Material.Shininess = 100;
            floorPlane.Material.Reflective = 0.7;
            floorPlane.Transform = Matrix.Translation(0,-3,0);

            world.SceneObjects.Add(floorPlane);

            //var rightWall = new Plane()
            //{
            //    Transform = Matrix.Translation(0, 0, 5)
            //                * Matrix.RotationY(Math.PI / 4)
            //                * Matrix.RotationX(Math.PI / 2)
            //                * Matrix.Scaling(10, 0.01, 10),
            //    Material = {Reflective = 0.3, Colour = new Colour(0,0.5,0.5), Shininess = 50}
            //};
            //world.SceneObjects.Add(rightWall);

            var middleSphere = new Sphere
            {
                Transform = Matrix.Translation(-0.5, 1, 0.5),
                Material = new Material
                {
                    Colour = new Colour(0.1, 1, 0.5),
                    Diffuse = 0.7,
                    Specular = 0.2,
                    Reflective = 0.3,
                    Pattern = new GradientPattern(new Colour(0, 0.5, 0.5), new Colour(1, 0.5, 0.5))
                }
            };

            world.SceneObjects.Add(middleSphere);


            var rightSphere = new Sphere
            {
                Transform = Matrix.Translation(1.5, 0.5, -0.5) * Matrix.Scaling(0.5, 0.5, 0.5),
                Material = new Material
                {
                    Colour = new Colour(0.5, 1, 0.1),
                    Diffuse = 0.7,
                    Specular = 0.2,
                    Pattern = new RingPattern(new Colour(0, 1, 0), new Colour(0, 0, 1))

                }
            };
            rightSphere.Material.Pattern.Transform = Matrix.Scaling(0.25, 0.25, 0.25) * Matrix.Translation(1,1,0.8);
            world.SceneObjects.Add(rightSphere);

            var leftSphere = new Sphere
            {
                Transform = Matrix.Translation(-1.5, 0.33, -0.75) * Matrix.Scaling(0.33, 0.33, 0.33),
                Material = new Material
                {
                    Colour = new Colour(1, 0.8, 0.1),
                    Diffuse = 0.7,
                    Specular = 0.2,
                    Pattern = new GradientPattern(new Colour(1, 0.5, 0), new Colour(0, 0.5, 1))
                }
            };

            world.SceneObjects.Add(leftSphere);



            var camera = new Camera(500, 400, Math.PI / 3);
            camera.Transform = Matrix.ViewTransform(new Point(0, 1.5, -6), new Point(0, 1, 0), new Vector(0.3, 1, 0));

            var canvas = camera.Render(world);

            var output = canvas.ToPpm();
            File.WriteAllText("ReflectionWorldScene.ppm", output);
        }
    }
}

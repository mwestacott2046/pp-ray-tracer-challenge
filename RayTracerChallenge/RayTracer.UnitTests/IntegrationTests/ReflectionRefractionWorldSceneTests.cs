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
    public class ReflectionRefractionWorldSceneTests
    {
        //[Ignore("LongRunningTest")]
        [Test]
        public void RenderScene()
        {
            var world = new World();
            world.LightSource = new Light(new Point(-10, 10, -10), new Colour(1, 1, 1));


            var floorPlane = new Plane();
            floorPlane.Material.Colour = ColourFactory.Navy;
            floorPlane.Material.Shininess = 100;
            floorPlane.Material.Reflective = 0.8;
            floorPlane.Material.Transparency = 0.9;
            floorPlane.Material.RefractiveIndex = 1.5;
            floorPlane.Transform = Matrix.Translation(0,-1,0);

            world.SceneObjects.Add(floorPlane);


            var leftRedSphere = new Sphere
            {
                Transform = Matrix.Translation(-2, 1, 1),
                Material = new Material
                {
                    Colour = ColourFactory.Red,
                    Diffuse = 0.7,
                    Specular = 0.2,
                    Reflective = 0.3,
                    Transparency = 0.5,
                    Shininess = 200
                }
            };
            world.SceneObjects.Add(leftRedSphere);

            var middleSphere = new Sphere
            {
                Transform = Matrix.Translation(0, 1, 0.5),
                Material = new Material
                {
                    Colour = ColourFactory.Green,
                    Diffuse = 0.7,
                    Specular = 0.2,
                    Reflective = 0.5,
                    RefractiveIndex = 2.4,
                    Transparency = 0.8
                    //Pattern = new GradientPattern(new Colour(0, 0.5, 0.5), new Colour(1, 0.5, 0.5))
                }
            };

            world.SceneObjects.Add(middleSphere);


            var rightSphere = new Sphere
            {
                Transform = Matrix.Translation(3, 0.5, -0.5) ,
                Material = new Material
                {
                    Colour = ColourFactory.Blue,
                    Diffuse = 0.7,
                    Specular = 0.2,
                    Reflective = 0.3,
                    RefractiveIndex = 1.2,
                    Transparency = 0.8
                    //Pattern = new RingPattern(new Colour(0, 1, 0), new Colour(0, 0, 1))

                }
            };
            //rightSphere.Material.Pattern.Transform = Matrix.Scaling(0.25, 0.25, 0.25) * Matrix.Translation(1,1,0.8);
            world.SceneObjects.Add(rightSphere);


            var lowSphere = new Sphere
            {
                Transform = Matrix.Translation(0, -5, -0) * Matrix.Scaling(3,3,3),
                Material = new Material
                {
                    Colour = ColourFactory.Purple,
                    Diffuse = 0.7,
                    Ambient = 0.8,
                    Specular = 0.2,
                    Reflective = 0.9
                    //Pattern = new RingPattern(new Colour(0, 1, 0), new Colour(0, 0, 1))

                }
            };
            world.SceneObjects.Add(lowSphere);


            var camera = new Camera(400, 400, Math.PI / 3);
            camera.Transform = Matrix.ViewTransform(new Point(-0.5, 1.5, -6), new Point(0, 1, 0), new Vector(0.5, 1, 0));

            var canvas = camera.Render(world);

            var output = canvas.ToPpm();
            File.WriteAllText("RefractionWorldScene.ppm", output);
        }
    }
}

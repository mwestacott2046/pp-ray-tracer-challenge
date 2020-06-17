using System;
using System.Collections.Generic;
using System.Text;
using RayTracer.Patterns;
using RayTracer.Shapes;

namespace RayTracer.Scenes
{
    public class ShinyBallPlaneScene: ISceneToRender
    {
        public ShinyBallPlaneScene(string filename="image.ppm",  int width = 960, int height=540 )
        {
            World = MakeWorld();
            Camera = MakeCamera(width, height);
            FileName = filename;
        }

        private static Camera MakeCamera(in int width, in int height)
        {
            var camera = new Camera(width, height, Math.PI / 3)
            {
                Transform = Matrix.ViewTransform(new Point(-0.5, 1.5, -8), new Point(0, 1, 0),
                    new Vector(0.5, 1, 0))
            };

            return camera;
        }


        private static World MakeWorld()
        {
            var world = new World();
            world.LightSource = new Light(new Point(-10, 10, -10), new Colour(1, 1, 1));


            var floorPlane = new Plane();
            floorPlane.Material.Colour = ColourFactory.Navy;
            floorPlane.Material.Shininess = 200;
            floorPlane.Material.Reflective = 0.9;
            floorPlane.Material.Transparency = 0.9;
            floorPlane.Material.RefractiveIndex = 1.2;
            floorPlane.Transform = Matrix.Translation(0, -1, 0);
            floorPlane.Material.Pattern = new StripePattern(ColourFactory.Navy, ColourFactory.DarkGrey);
            floorPlane.Material.Pattern.Transform = Matrix.Scaling(0.5, 0.5, 0.5) * Matrix.RotationX(Math.PI / 3);
            world.SceneObjects.Add(floorPlane);


            var leftRedSphere = new Sphere
            {
                Transform = Matrix.Translation(-2, 1, -1),
                Material = new Material
                {
                    Colour = new Colour(0.5, 0, 0),
                    Diffuse = 0.9,
                    Specular = 1,
                    Reflective = 0.9,
                    RefractiveIndex = 1.6,
                    Transparency = 0.5,
                    Shininess = 250
                }
            };
            world.SceneObjects.Add(leftRedSphere);

            var middleSphere = new Sphere
            {
                Transform = Matrix.Translation(0.5, 1, -1.5),
                Material = new Material
                {
                    Colour = ColourFactory.Green,
                    Diffuse = 0.7,
                    Specular = 0.2,
                    Reflective = 0.5,
                    RefractiveIndex = 2.4,
                    Transparency = 0.8
                }
            };

            world.SceneObjects.Add(middleSphere);


            var rightSphere = new Sphere
            {
                Transform = Matrix.Translation(3, 0.5, -0.5),
                Material = new Material
                {
                    Colour = ColourFactory.Blue,
                    Diffuse = 0.7,
                    Specular = 0.2,
                    Reflective = 0.3,
                    RefractiveIndex = 1.2,
                    Transparency = 0.8

                }
            };
            world.SceneObjects.Add(rightSphere);


            var lowSphere = new Sphere
            {
                Transform = Matrix.Translation(0, -5, -0) * Matrix.Scaling(1.5, 1.5, 1.5),
                Material = new Material
                {
                    Colour = ColourFactory.White,
                    Diffuse = 0.7,
                    Ambient = 0.5,
                    Specular = 0.2,
                    Reflective = 0.9

                }
            };
            world.SceneObjects.Add(lowSphere);
            return world;
        }

        public World World { get; }
        public Camera Camera { get; }
        public string FileName { get; }
    }
}

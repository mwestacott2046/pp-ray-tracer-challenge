using System.IO;
using RayTracer.Shapes;

namespace RayTracer.UnitTests.IntegrationTests
{
    public class RaySphereIntersectionsExample
    {
        public static void Execute ()
        {
            var rayOrigin = new Point(0, 0, -5);
            var wallZ = 10;
            var wallSize = 8;
            var halfWall = wallSize / 2;
            var canvasPixels = 600;
            var shape = new Sphere();
            shape.Material = new Material {Colour = new Colour(1, 0.3, 1)};

            var lightPosition = new Point(-10,10,-10);
            var lightColour = new Colour(0.8,1,1);
            var light = new Light(lightPosition, lightColour);

            var pixelSize = (double)wallSize / canvasPixels;

            var canvas = new Canvas(canvasPixels, canvasPixels);

            var red = new Colour(1,0,0);

            for (int y = 0; y < canvasPixels; y++)
            {
                var worldY = halfWall - pixelSize * y;

                for (int x = 0; x < canvasPixels; x++)
                {
                    var worldX = -halfWall + pixelSize * x;

                    var position = new Point(worldX, worldY, wallZ);

                    var r = new Ray(rayOrigin, position.Subtract(rayOrigin).ToVector());

                    var xs = shape.Intersects(r);

                    var hit = xs.Hit();
                    if (hit != null)
                    {
                        var point = r.Position(hit.T);
                        var normal = hit.Object.NormalAt(point);
                        var eye = (-r.Direction.Normalize()).ToVector();

                        var colour = Light.Lighting(hit.Object.Material, hit.Object, light, point, eye, normal,false);
                        canvas.SetPixel(x, y, colour);
                    }

                }
            }

            var output = canvas.ToPpm();
            File.WriteAllText("RaySphere.ppm", output);
        }
    }
}

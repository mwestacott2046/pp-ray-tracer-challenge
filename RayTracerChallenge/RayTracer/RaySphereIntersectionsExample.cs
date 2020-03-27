using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RayTracer
{
    public class RaySphereIntersectionsExample
    {
        public static void Execute ()
        {
            var rayOrigin = new Point(0, 0, -5);
            var wallZ = 10;
            var wallSize = 8;
            var halfWall = wallSize / 2;
            var canvasPixels = 150;
            var shape = new Sphere();

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

                    var xs = r.Intersects(shape);

                    if (xs.Hit() != null)
                    {
                        canvas.SetPixel(x,y,red);
                    }
                }
            }

            var output = canvas.ToPpm();
            File.WriteAllText("RaySphere.ppm", output);
        }
    }
}

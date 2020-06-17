using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class Camera
    {
        public int HSize { get; }
        public int VSize { get; }
        public double FieldOfView { get; }
        public Matrix Transform { get; set; }

        public double HalfWidth { get;  }

        public double HalfHeight { get; }

        public double PixelSize { get; }

        public Camera(int hSize, int vSize, double fieldOfView)
        {
            HSize = hSize;
            VSize = vSize;
            FieldOfView = fieldOfView;
            Transform = Matrix.IdentityMatrix;


            var halfView = Math.Tan(FieldOfView / 2);
            var aspect = CalcAspect(HSize, VSize);

            if (aspect >= 1)
            {
                this.HalfWidth = halfView;
                this.HalfHeight = halfView / aspect;
            }
            else
            {
                this.HalfWidth = halfView * aspect;
                this.HalfHeight = halfView;
            }

            PixelSize = (HalfWidth * 2) / hSize;
        }

        private static double CalcAspect(double hSize, double vSize)
        {
            return hSize / vSize;
        }

        public Ray RayForPixel(int px, int py)
        {
            var xOffset = (px + 0.5) * PixelSize;
            var yOffset = (py + 0.5) * PixelSize;

            var worldX = HalfWidth - xOffset;
            var worldY = HalfHeight - yOffset;

            var pixel = Transform.Inverse() * new Point(worldX, worldY, -1);
            var origin = Transform.Inverse() * new Point(0, 0, 0);
            var direction = pixel.Subtract(origin).Normalize();

            return new Ray(origin.ToPoint(), direction.ToVector());
        }

        public Canvas Render(World world)
        {
            var image = new Canvas(HSize,VSize);

            for (var y = 0; y < VSize; y++)
            {
                for (var x = 0; x < HSize; x++)
                {
                    var ray = RayForPixel(x, y);
                    var colour = world.ColourAt(ray);
                    image.SetPixel(x, y, colour);
                }
            }

            return image;
        }

        public Canvas RenderP(World world)
        {
            var image = new Canvas(HSize, VSize);

            var pixelStorage = new ConcurrentBag<RayPixelResult>();
            for (var y = 0; y < VSize; y++)
            {
                var xEnum = Enumerable.Range(0, HSize).ToList();

                Parallel.ForEach(xEnum, (x) =>
                {
                    var ray = RayForPixel(x, y);
                    var colour = world.ColourAt(ray);
                    pixelStorage.Add(new RayPixelResult(x, y, colour));

                });
            }

            foreach (var pixel in pixelStorage)
            {
                image.SetPixel(pixel.X,pixel.Y, pixel.C);
            }
            return image;
        }
    }

    public class RayPixelResult
    {
        public RayPixelResult(int x, int y, Colour c)
        {
            X = x;
            Y = y;
            C= c;

        }

        public Colour C { get;}

        public int Y { get;}

        public int X { get; }
    }
}

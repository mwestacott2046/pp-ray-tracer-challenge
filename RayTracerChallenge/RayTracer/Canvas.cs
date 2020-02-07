using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Canvas
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        private readonly Colour[][] _pixels;
        public Canvas(int width, int height)
        {
            Width = width;
            Height = height;
            _pixels = new Colour[width][];
            for (var w = 0; w < width; w++)
            {
                _pixels[w] = new Colour[height];

                for (var h = 0; h < height; h++)
                {
                    _pixels[w][h] = new Colour(0, 0, 0);
                }
            }
        }

        public Colour GetPixel(int x, int y)
        {
            return _pixels[x][y];
        }

        public void SetPixel(int x, int y, Colour c)
        {
            _pixels[x][y] = c;
        }

        public string ToPpm()
        {
            const int colourMax = 255;
            var ppmBuilder = new StringBuilder();
            ppmBuilder.AppendLine("P3");
            ppmBuilder.AppendLine($"{Width} {Height}");
            ppmBuilder.AppendLine($"{colourMax}");

            var currentLine = string.Empty;
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    var colour = GetPixel(x, y);

                    var r = CalcColour(colour.R, colourMax);
                    var g = CalcColour(colour.G, colourMax);
                    var b = CalcColour(colour.B, colourMax);

                    var colourString =$"{r} {g} {b} ";
                    currentLine += colourString;
                    if (currentLine.Length > 60)
                    {
                        ppmBuilder.AppendLine(currentLine.Trim());
                        currentLine = string.Empty;
                    }
                }
                ppmBuilder.AppendLine(currentLine.Trim());
                currentLine = string.Empty;
            }

            return ppmBuilder.ToString();
        }

        private static int CalcColour(double colour, int colourMax)
        {
            var val = (int) (colour * colourMax);
            if (val > colourMax)
            {
                return colourMax;
            }

            return val < 0 ? 0 : val;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RayTracer
{
    public class ProjectilesOnCanvas
    {
        public Projectile Tick(Environment env, Projectile proj)
        {
            var position = proj.Position.Add(proj.Velocity);
            var velocity = proj.Velocity.Add(env.Gravity).Add(env.Wind);
            return new Projectile(position, velocity);
        }

        public RtTuple RunProjectiles()
        {
            
            var proj = new Projectile(RtTuple.Point(0, 1, 0), (RtTuple.Vector(1, 1.8, 0).Normalize().Multiply(10)));
            var env = new Environment(RtTuple.Vector(0, -0.1, 0), RtTuple.Vector(-0.001, 0, 0));

            var canvas = new Canvas(900, 550);
            var red = new Colour(1, 0, 0);

            while (proj.Position.Y >= 0.0)
            {
                proj = Tick(env, proj);

                var x = (int)Math.Round(proj.Position.X);
                var y = (int)(549 - Math.Round(proj.Position.Y));

                if (x >= 0 && x < 900 && y >= 0 && y < 550)
                {
                    canvas.SetPixel(x, y, red);
                }
                else
                {
                    Console.WriteLine($" Out of bounds plot x:{x}, y:{y}");
                }

            }

            var result = canvas.ToPpm();
            File.WriteAllText("projectile.ppm",result);

            return proj.Position;
        }

    }
}
﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Projectiles
    {
        public Projectile Tick(Environment env, Projectile proj)
        {
            var position = proj.Position.Add(proj.Velocity);
            var velocity = proj.Velocity.Add(env.Gravity).Add(env.Wind);
            return new Projectile(position, velocity);
        }

        public RtTuple RunProjectiles()
        {
            var proj = new Projectile(RtTuple.Point(0, 1, 0), (RtTuple.Vector(1, 1, 0).Normalize()));
            var env = new Environment(RtTuple.Vector(0, -0.1, 0), RtTuple.Vector(-0.01, 0, 0));

            while (proj.Position.Y >= 0.0)
            {
                proj = Tick(env, proj);
            }

            return proj.Position;
        }
    }

    public class Projectile
    {
        public Projectile(RtTuple position, RtTuple velocity)
        {
            Position = position;
            Velocity = velocity;
        }
        public RtTuple Position { get; private set; }
        public RtTuple Velocity { get; private set; }
    }

    public class Environment
    {
        public Environment(RtTuple gravity, RtTuple wind)
        {
            Gravity = gravity;
            Wind = wind;
        }
        public RtTuple Gravity { get; private set; }
        public RtTuple Wind { get; private set; }
    }

}
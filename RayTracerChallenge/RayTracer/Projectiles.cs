using System;
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
            var proj = new Projectile(new Point(0, 1, 0), (new Vector(1, 1, 0).Normalize()));
            var env = new Environment(new Vector(0, -0.1, 0), new Vector(-0.01, 0, 0));

            while (proj.Position.Y >= 0.0)
            {
                proj = Tick(env, proj);
            }

            return proj.Position;
        }
    }
}

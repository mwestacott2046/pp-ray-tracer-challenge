namespace RayTracer
{
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
}
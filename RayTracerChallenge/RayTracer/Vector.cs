namespace RayTracer
{
    public class Vector : RtTuple
    {
        public Vector(double x, double y, double z) : base(x, y, z, 0.0)
        {
        }

        public Vector Reflect(Vector normal)
        {
            return Reflect(this, normal);
        }

        public static Vector Reflect(Vector v, Vector normal)
        {
           return (v - normal * 2 * v.Dot(normal)).ToVector();
        }
    }
}
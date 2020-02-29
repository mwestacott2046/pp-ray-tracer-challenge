namespace RayTracer
{
    public class Point : RtTuple
    {
        public Point(double x, double y, double z) : base(x, y, z, 1.0)
        {
        }

        public static Point Zero()
        {
            return new Point(0,0,0);
        }
    }
}
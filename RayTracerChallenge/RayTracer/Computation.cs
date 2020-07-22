using RayTracer.Shapes;

namespace RayTracer
{
    public class Computation
    {
        public double T { get; set; }
        public Point Point { get; set; }
        public Vector EyeV { get; set; }
        public Vector NormalV { get; set; }
        public ISceneObject Object { get; set; }
        public bool Inside { get; set; }
        public Point OverPoint { get; set; }
        public Point UnderPoint { get; set; }
        public Vector ReflectV { get; set; }
        public double N1 { get; set; }
        public double N2 { get; set; }
    }
}
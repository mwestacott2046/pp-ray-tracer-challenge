namespace RayTracer.ProjectileExperiments
{
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
namespace RayTracer
{
    public interface ISceneObject
    {
        Vector NormalAt(Point worldPoint);
        public Material Material { get; }

        public Matrix Transform { get; }
    }
}
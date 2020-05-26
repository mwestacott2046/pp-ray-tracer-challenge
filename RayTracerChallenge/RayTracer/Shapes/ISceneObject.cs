using System.Collections.Generic;

namespace RayTracer.Shapes
{
    public interface ISceneObject
    {
        Vector NormalAt(Point worldPoint);
        public Material Material { get; }

        public Matrix Transform { get; }
        Intersection[] Intersects(Ray ray);
    }
}
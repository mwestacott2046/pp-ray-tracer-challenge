using System;
using System.Collections.Generic;
using System.Text;
using RayTracer.Shapes;

namespace RayTracer.Patterns
{
    public interface IPattern
    {
        Colour PatternAtShape(ISceneObject sceneObject, Point point);
        Matrix Transform { get; set; }
    } 
}

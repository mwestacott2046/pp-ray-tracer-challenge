﻿namespace RayTracer
{
    public class Computation
    {
        public double T { get; set; }
        public Point Point { get; set; }
        public Vector EyeV { get; set; }
        public Vector NormalV { get; set; }
        public ISceneObject Object { get; set; }
        public bool Inside { get; set; }
    }
}
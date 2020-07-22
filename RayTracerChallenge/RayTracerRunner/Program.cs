using System;
using RayTracer;
using RayTracer.Scenes;

namespace RayTracerRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("RayTracer");
            var started = DateTime.Now;
            Console.WriteLine("Started: " + started);
            SceneRender.Render(new ShinyBallPlaneScene("ShinyBallsCMD.ppm"));
            var ended = DateTime.Now;
            Console.WriteLine("Finished: " + ended);
            Console.WriteLine("Time Taken" + (ended-started));
        }
    }
}

using System.IO;
using RayTracer.Scenes;

namespace RayTracer
{
    public class SceneRender
    {
        public static void Render(ISceneToRender scene)
        {
            var canvas = scene.Camera.RenderP(scene.World);

            var output = canvas.ToPpm();
            File.WriteAllText(scene.FileName, output);
        }
    }
}
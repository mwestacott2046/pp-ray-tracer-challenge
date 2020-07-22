namespace RayTracer.Scenes
{
    public interface ISceneToRender
    {
        World World { get; }
        Camera Camera { get; }
        string FileName { get; }
    }
}

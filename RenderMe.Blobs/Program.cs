using RenderMe.Blobs.GameObjects;
using RenderMe.Engine;
using System;
using System.Linq;

namespace RenderMe.Blobs
{
    /// <summary>
    /// This was the first program I did with opengl.
    /// Its essentially just a test / learning.
    /// </summary>
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            RenderEngine engine = new RenderEngine(200, 200, "Triangle", new OpenToolkit.Mathematics.Vector3(0, 0, 1.5f));

            engine.UseShaders(@"./Shaders");
            engine.ShaderManager.OnLoad();

            var triangle = new Triangle(engine.ShaderManager.Shaders.FirstOrDefault(x => x.Name.ToLower() == "basic"));
            triangle.OnLoad();
            engine.AddEntity(triangle);

            // Start game
            engine.Run();
        }
    }
}

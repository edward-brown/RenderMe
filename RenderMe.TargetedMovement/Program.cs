using OpenToolkit.Mathematics;
using RenderMe.Engine;
using RenderMe.TargetedMovement.GameObjects;
using System;
using System.Linq;

namespace RenderMe.TargetedMovement
{
    /// <summary>
    /// First attempt at making "Target" based movement.
    /// Could be used for anything, an NPC moving along a list of targets for example.
    /// </summary>
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var engine = new RenderEngine(200, 200, "TargetedMovement", new Vector3(5, 5, 5));

            // Load shaders
            engine.UseShaders(@"./Shaders");
            engine.ShaderManager.OnLoad();

            // Get basic shader
            var shader = engine.ShaderManager.Shaders.FirstOrDefault(x => x.Name.ToLower() == "basic");

            // Create entities
            var triangle = new Triangle(shader);
            triangle.OnLoad();

            // Add entities
            engine.AddEntity(triangle);

            engine.Run();
        }
    }
}

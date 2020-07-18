using RenderMe.Boids.Managers;
using RenderMe.Engine;
using System;
using System.Linq;

namespace RenderMe.Boids
{
    /// <summary>
    /// RenderMe implementation of Boids
    /// Aka - learning compute shaders
    /// https://en.wikipedia.org/wiki/Boids
    /// </summary>
    static class Program
    {
        static void Main(string[] args)
        {
            var engine = new RenderEngine(1080, 1920, "Boids");

            engine.UseShaders(@"./Shaders");
            engine.ShaderManager.OnLoad();

            var boidManager = new BoidManager(engine.ShaderManager.Shaders.FirstOrDefault(x => x.Name.ToLower() == "boid") as Shader, 10);
            boidManager.OnLoad();

            engine.Run();
        }
    }
}

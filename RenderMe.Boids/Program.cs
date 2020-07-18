using RenderMe.Engine;
using System;

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

            

            engine.Run();
        }
    }
}

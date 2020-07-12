﻿using RenderMe.Blobs.GameObjects;
using RenderMe.Engine;
using RenderMe.Engine.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RenderMe.Blobs
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            RenderEngine engine = new RenderEngine(200, 200, "Blobs");

            engine.UseShaders(@"./Shaders");
            engine.ShaderManager.OnLoad();

            var triangle = new Triangle(engine.ShaderManager.Shaders.FirstOrDefault(x => x.Name.ToLower() == "basic"));
            triangle.OnLoad();
            engine.Entities.Add(triangle);

            // Start game
            engine.Run();
        }
    }
}

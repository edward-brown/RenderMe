using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace RenderMe.Engine.Shaders
{
    public class ShaderManager : IDisposable
    {
        public string ShaderFolderPath { get; set; }
        public List<Shader> Shaders { get; set; } = new List<Shader>();

        public ShaderManager(string shaderFolderPath)
        {
            ShaderFolderPath = shaderFolderPath;
        }

        public void Dispose()
        {
            foreach(Shader shader in Shaders)
            {
                shader.Dispose();
            }
        }

        protected void LoadShaders(bool debug)
        {
            // Create shader objects
            var dirs = Directory.GetDirectories(ShaderFolderPath);
            foreach (string dir in dirs)
            {
                var dirInfo = new DirectoryInfo(dir);
                var name = dirInfo.Name;
                var vertexShaderPath = dirInfo.GetFiles("*.vertex.glsl").First().FullName;
                var fragmentShaderPath = dirInfo.GetFiles("*.fragment.glsl").First().FullName;


                Shaders.Add(new Shader(name, vertexShaderPath, fragmentShaderPath));
            }

            // Load shader files
            foreach (Shader shader in Shaders)
            {
                if (debug)
                {
                    shader.LoadTimed();
                }
                else
                {
                    shader.Load();
                }
            }
        }

        protected void CompileShaders(bool debug)
        {
            foreach(Shader shader in Shaders)
            {
                if (debug)
                {
                    shader.CompileTimed();
                }
                else
                {
                    shader.Compile();
                }
            }
        }

        public void OnLoad(bool debug = false)
        {
            if (debug)
            {
                var sw = new Stopwatch();
                sw.Start();
                Console.WriteLine("Loading & compiling shaders");
                Console.WriteLine("---------------------------");

                LoadShaders(true);
                CompileShaders(true);

                sw.Stop();
                Console.WriteLine("---------------------------");
                Console.WriteLine($"Loaded & compiled all shaders in {sw.ElapsedMilliseconds}ms");
            }
            else
            {
                LoadShaders(false);
                CompileShaders(false);
            }
        }
    }
}

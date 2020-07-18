using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace RenderMe.Engine.Shaders
{
    public class ShaderManager : IDisposable
    {
        public string ShaderFolderPath { get; set; }
        public List<IShader> Shaders { get; set; } = new List<IShader>();

        public ShaderManager(string shaderFolderPath)
        {
            ShaderFolderPath = shaderFolderPath;
        }

        

        protected void LoadShaders()
        {
            // Create shader objects
            var dirs = Directory.GetDirectories(ShaderFolderPath);
            foreach (string dir in dirs)
            {
                var dirInfo = new DirectoryInfo(dir);
                var name = dirInfo.Name;

                // Get shader types
                var vertexShaderPath = dirInfo.GetFiles("*.vertex.glsl").First().FullName;
                var fragmentShaderPath = dirInfo.GetFiles("*.fragment.glsl").First().FullName;
                var geometryShaderPath = dirInfo.GetFiles("*.geometry.glsl").FirstOrDefault()?.FullName;
                var computeShaderPath = dirInfo.GetFiles("*.compute.glsl").FirstOrDefault()?.FullName;

                Shaders.Add(new Shader(name, vertexShaderPath, fragmentShaderPath, geometryShaderPath));

                // Add compute shader if it exists
                if (computeShaderPath != null)
                {
                    Shaders.Add(new ComputeShader(computeShaderPath));
                }
            }

            // Load shader files
            foreach (IShader shader in Shaders)
            {
                shader.Load();
            }
        }

        protected void CompileShaders()
        {
            foreach(IShader shader in Shaders)
            {
                shader.Compile();
            }
        }

        public void OnLoad()
        {
            LoadShaders();
            CompileShaders();
        }

        public void Dispose()
        {
            foreach (IShader shader in Shaders)
            {
                shader.Dispose();
            }
        }
    }
}

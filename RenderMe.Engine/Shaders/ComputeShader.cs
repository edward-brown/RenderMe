using OpenToolkit.Graphics.ES30;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RenderMe.Engine.Shaders
{
    public class ComputeShader : IShader
    {
        public int Program { get; set; }

        public string Name { get; set; }

        private string Path { get; set; }
        private string Source { get; set; }
        private int ShaderId { get; set; }

        public ComputeShader(string path)
        {
            Path = path;
        }

        public void Load()
        {
            using StreamReader reader = new StreamReader(Path, Encoding.UTF8);
            Source = reader.ReadToEnd();
        }

        public void Compile()
        {
            // Create & Compile
            ShaderId = GL.CreateShader(ShaderType.ComputeShader);
            GL.ShaderSource(ShaderId, Source);
            GL.CompileShader(ShaderId);

            // Log info
            string infoLogVert = GL.GetShaderInfoLog(ShaderId);
            if (infoLogVert != string.Empty)
                Console.WriteLine(infoLogVert);

            // Create program + link shader
            Program = GL.CreateProgram();
            GL.AttachShader(Program, ShaderId);
            GL.LinkProgram(Program);
        }

        public void Use()
        {
            GL.UseProgram(Program);
        }

        public void Dispose()
        {
            GL.DetachShader(Program, ShaderId);
            GL.DeleteShader(ShaderId);
            GL.DeleteProgram(Program);
        }
    }
}

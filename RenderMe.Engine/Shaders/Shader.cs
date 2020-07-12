using OpenToolkit.Graphics.ES20;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace RenderMe.Engine
{
    public class Shader : IDisposable
    {
        // Program id
        public int Program { get; private set; }
        
        public string Name { get; }

        // Paths
        private readonly string VertexPath;
        private readonly string FragmentPath;

        // Source
        private string VertexSource { get; set; }
        private string FragmentSource { get; set; }

        private int VertexShader { get; set; }
        private int FragmentShader { get; set; }

        public Shader(string name, string vertexPath, string fragmentPath)
        {
            Name = name;
            VertexPath = vertexPath;
            FragmentPath = fragmentPath;
        }

        // Dispose
        public void Dispose()
        {
            // Cleanup
            GL.DetachShader(Program, VertexShader);
            GL.DetachShader(Program, FragmentShader);
            GL.DeleteShader(FragmentShader);
            GL.DeleteShader(VertexShader);
            GL.DeleteProgram(Program);
        }

        public void Load()
        {
            using (StreamReader reader = new StreamReader(VertexPath, Encoding.UTF8))
            {
                VertexSource = reader.ReadToEnd();
            }

            using (StreamReader reader = new StreamReader(FragmentPath, Encoding.UTF8))
            {
                FragmentSource = reader.ReadToEnd();
            }
        }

        public long LoadTimed()
        {
            var sw = new Stopwatch();
            sw.Start();

            Load();

            sw.Stop();
            Console.WriteLine($"{Name} loaded in {sw.ElapsedMilliseconds}ms");
            return sw.ElapsedMilliseconds;
        }

        public void Compile()
        {
            VertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(VertexShader, VertexSource);

            FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(FragmentShader, FragmentSource);

            GL.CompileShader(VertexShader);

            string infoLogVert = GL.GetShaderInfoLog(VertexShader);
            if (infoLogVert != System.String.Empty)
                System.Console.WriteLine(infoLogVert);

            GL.CompileShader(FragmentShader);

            string infoLogFrag = GL.GetShaderInfoLog(FragmentShader);

            if (infoLogFrag != System.String.Empty)
                System.Console.WriteLine(infoLogFrag);

            Program = GL.CreateProgram();

            GL.AttachShader(Program, VertexShader);
            GL.AttachShader(Program, FragmentShader);

            GL.LinkProgram(Program);
        }

        public long CompileTimed()
        {
            var sw = new Stopwatch();
            sw.Start();

            Compile();

            sw.Stop();
            Console.WriteLine($"{Name} compiled in {sw.ElapsedMilliseconds}ms");
            return sw.ElapsedMilliseconds;
        }

        public void Use()
        {
            GL.UseProgram(Program);
        }
    }
}

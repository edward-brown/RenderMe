using OpenToolkit.Graphics.ES20;
using RenderMe.Engine.Shaders;
using System;
using System.IO;
using System.Text;

namespace RenderMe.Engine
{
    public class Shader : IShader
    {
        // Program id
        public int Program { get; private set; }
        
        public string Name { get; set; }

        // Paths
        private readonly string VertexPath;
        private readonly string FragmentPath;
        private readonly string GeometryPath;

        // Source
        private string VertexSource { get; set; }
        private string FragmentSource { get; set; }
        private string GeometrySource { get; set; }

        private int VertexShader { get; set; }
        private int FragmentShader { get; set; }
        private int GeometryShader { get; set; }

        public Shader(string name, string vertexPath, string fragmentPath, string geometry = null)
        {
            Name = name;
            VertexPath = vertexPath;
            FragmentPath = fragmentPath;
            GeometryPath = geometry;
        }

        public void Load()
        {
            // Read vertex source
            using (StreamReader reader = new StreamReader(VertexPath, Encoding.UTF8))
            {
                VertexSource = reader.ReadToEnd();
            }

            // Read fragment source
            using (StreamReader reader = new StreamReader(FragmentPath, Encoding.UTF8))
            {
                FragmentSource = reader.ReadToEnd();
            }

            // Optionally read geometry source
            if (GeometryPath != null)
            {
                using StreamReader reader = new StreamReader(GeometryPath, Encoding.UTF8);
                GeometrySource = reader.ReadToEnd();
            }
        }

        public void Compile()
        {
            // Create + compile
            VertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(VertexShader, VertexSource);
            GL.CompileShader(VertexShader);

            FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(FragmentShader, FragmentSource);
            GL.CompileShader(FragmentShader);

            if (GeometryPath != null)
            {
                GeometryShader = GL.CreateShader(ShaderType.GeometryShader);
                GL.ShaderSource(GeometryShader, GeometrySource);
                GL.CompileShader(GeometryShader);
            }

            // Log info
            string infoLogVert = GL.GetShaderInfoLog(VertexShader);
            if (infoLogVert != string.Empty)
                Console.WriteLine(infoLogVert);

            string infoLogFrag = GL.GetShaderInfoLog(FragmentShader);
            if (infoLogFrag != string.Empty)
                Console.WriteLine(infoLogFrag);

            if (GeometryPath != null)
            {
                string infoLogGeometry = GL.GetShaderInfoLog(GeometryShader);
                if (infoLogGeometry != string.Empty)
                    Console.WriteLine(infoLogGeometry);
            }

            // Create program + link shaders
            Program = GL.CreateProgram();
            GL.AttachShader(Program, VertexShader);
            GL.AttachShader(Program, FragmentShader);

            if(GeometryPath != null)
            {
                GL.AttachShader(Program, GeometryShader);
            }

            GL.LinkProgram(Program);
        }

        public void Use()
        {
            GL.UseProgram(Program);
        }

        public void Dispose()
        {
            // Detach
            GL.DetachShader(Program, VertexShader);
            GL.DetachShader(Program, FragmentShader);

            // Delete
            GL.DeleteShader(FragmentShader);
            GL.DeleteShader(VertexShader);

            // Optionally detach + delete geometry
            if (GeometryPath != null)
            {
                GL.DetachShader(Program, GeometryShader);
                GL.DeleteShader(GeometryShader);
            }

            // Delete program
            GL.DeleteProgram(Program);
        }
    }
}

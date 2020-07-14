using OpenToolkit.Graphics.ES30;
using OpenToolkit.Mathematics;
using RenderMe.Engine;
using RenderMe.Engine.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RenderMe.Blobs.GameObjects
{
    public class Triangle : Entity2D
    {
        private Vector2[] translations;

        public Triangle(Shader shader) : base()
        {
            Vertices = new float[]
            {
                -0.05f, 0.0f, 0.0f, 1.0f,
                0.05f, 0.0f, 0.0f, 1.0f,
                0.0f, 0.05f, 0.0f, 1.0f
            };

            Shader = shader ?? throw new ArgumentNullException(nameof(shader));
        }

        public new void OnLoad()
        {
            base.OnLoad();

            var positionLocation = GL.GetAttribLocation(Shader.Program, "position");
            GL.VertexAttribPointer(positionLocation, 4, VertexAttribPointerType.Float, false, 0, 0);

            translations = new Vector2[100];
            var index = 0;
            for (var y = -10; y < 10; y += 2)
            {
                for (var x = -10; x < 10; x += 2)
                {
                    var vec = new Vector2();
                    vec.X = x / 10.0f;
                    vec.Y = y / 10.0f;
                    translations[index++] = vec;
                }
            }

            GL.UseProgram(Shader.Program);
            for (var i = 0; i < 100; i++)
            {
                var offsetLocation = GL.GetUniformLocation(Shader.Program, $"offsets[{i}]");
                GL.Uniform2(offsetLocation, translations[i]);
            }

            IsLoaded = true;
        }

        public override void Render()
        {
            // Bind
            base.Bind();

            // Set position
            var offset = (float)Math.Sin(Engine.Stopwatch.ElapsedMilliseconds / (float)1000) / 2.0f;

            //var posLocation = GL.GetUniformLocation(Shader.Program, "pos");
            //GL.Uniform3(posLocation, new Vector3(offset, 0.0f, 0.0f));

            // Set color
            var colorLocation = GL.GetUniformLocation(Shader.Program, "color");
            GL.Uniform4(colorLocation, new Vector4(offset, 0.0f, -offset, 1.0f));

            // Draw
            GL.DrawArraysInstanced(PrimitiveType.Triangles, 0, 3, 100);
        }
    }
}

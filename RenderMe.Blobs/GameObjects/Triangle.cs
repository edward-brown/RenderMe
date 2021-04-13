using OpenToolkit.Graphics.ES30;
using OpenToolkit.Mathematics;
using RenderMe.Engine;
using RenderMe.Engine.Entity;
using RenderMe.Engine.Shaders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RenderMe.Blobs.GameObjects
{
    public class Triangle : Entity3D
    {
        private Vector2[] translations;

        public Triangle(Engine.Shaders.IShader shader) : base()
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
                    var vec = new Vector2
                    {
                        X = x / 10.0f,
                        Y = y / 10.0f
                    };
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

            // Sin color offset
            var offset = (float)Math.Sin(Engine.GetTime);

            // Set color
            var colorLocation = GL.GetUniformLocation(Shader.Program, "color");
            GL.Uniform4(colorLocation, new Vector4(offset, 0.0f, -offset, 1.0f));

            // Projection
            var model = Matrix4.Identity * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(Engine.GetTime * 10));
            var modelLocation = GL.GetUniformLocation(Shader.Program, "model");
            var viewLocation = GL.GetUniformLocation(Shader.Program, "view");
            var projectionLocation = GL.GetUniformLocation(Shader.Program, "projection");

            var viewMatrix = Engine.Camera.GetViewMatrix();
            var projectionMatrix = Engine.Camera.GetProjectionMatrix();

            GL.UniformMatrix4(modelLocation, true, ref model);
            GL.UniformMatrix4(viewLocation, true, ref viewMatrix);
            GL.UniformMatrix4(projectionLocation, true, ref projectionMatrix);

            // Draw
            GL.DrawArraysInstanced(PrimitiveType.Triangles, 0, 3, 100);
        }
    }
}

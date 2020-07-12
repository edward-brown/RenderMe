using OpenToolkit.Graphics.ES30;
using RenderMe.Engine;
using RenderMe.Engine.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RenderMe.Blobs.GameObjects
{
    public class Triangle : Entity2D
    {
        public Triangle(Shader shader) : base()
        {
            Vertices = new float[]
            {
                -0.5f, 0.0f, 0.0f, 1.0f,
                0.5f, 0.0f, 0.0f, 1.0f,
                0.0f, 0.5f, 0.0f, 1.0f
            };

            Shader = shader ?? throw new ArgumentNullException(nameof(shader));
        }

        public new void OnLoad()
        {
            base.OnLoad();

            var positionLocation = GL.GetAttribLocation(Shader.Program, "position");
            GL.VertexAttribPointer(positionLocation, 4, VertexAttribPointerType.Float, false, 0, 0);
            IsLoaded = true;
        }

        public override void Render()
        {
            // Bind
            base.Bind();

            // Draw
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        }
    }
}

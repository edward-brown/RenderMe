using OpenToolkit.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace RenderMe.Engine.Entity
{
    public abstract class Entity2D : Entity
    {
        public Vector2 Position { get; set; }

        protected Entity2D()
        {

        }

        protected Entity2D(float[] vertices)
        {
            Vertices = vertices;
        }

        protected Entity2D(float[] vertices, int x, int y) : base(x, y)
        {
            Vertices = vertices;
            Position = new Vector2(x, y);
        }

        public override void HandleKeyboardInputs()
        {
            return;
        }
    }
}

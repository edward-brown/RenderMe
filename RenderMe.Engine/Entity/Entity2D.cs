using OpenToolkit.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace RenderMe.Engine.Entity
{
    public abstract class Entity2D : Entity
    {
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
        }

        public override void HandleKeyboardInputs()
        {
            return;
        }
    }
}

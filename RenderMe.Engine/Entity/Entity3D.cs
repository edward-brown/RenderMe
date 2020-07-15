using OpenToolkit.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace RenderMe.Engine.Entity
{
    public abstract class Entity3D : Entity
    {
        protected Entity3D()
        {

        }

        protected Entity3D(float[] vertices)
        {
            Vertices = vertices;
        }

        protected Entity3D(float[] vertices, int x, int y, int z) : base(x, y, z)
        {
            Vertices = vertices;
        }

        public override void HandleKeyboardInputs()
        {
            return;
        }
    }
}

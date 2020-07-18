using RenderMe.Engine;
using RenderMe.Engine.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RenderMe.Boids.GameObjects
{
    public class Boid : Entity2D
    {
        public Boid(Shader shader) : base()
        {
            Shader = shader ?? throw new ArgumentNullException(nameof(shader));
        }

        public new void OnLoad()
        {
            throw new NotImplementedException();
        }

        public override void Render()
        {
            throw new NotImplementedException();
        }
    }
}

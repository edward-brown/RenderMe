using OpenToolkit.Graphics.ES30;
using OpenToolkit.Mathematics;
using RenderMe.Boids.GameObjects;
using RenderMe.Engine;
using RenderMe.Engine.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RenderMe.Boids.Managers
{
    public class BoidManager : Entity
    {
        private Boid[] Boids { get; set; }

        public float Speed { get; set; }

        private int _numberOfBoids;
        public int NumberOfBoids
        {
            get
            {
                return _numberOfBoids;
            }
            set
            {
                _numberOfBoids = value;
                OnLoad();
            }
        }

        public BoidManager(IShader shader, int numberOfBoids)
        {
            Shader = shader ?? throw new ArgumentNullException(nameof(shader));
            _numberOfBoids = numberOfBoids;

            Vertices = new float[]
            {
                -0.05f, 0.0f,
                0.05f, 0.0f,
                0.0f, 0.05f
            };
        }

        private void CalculateNewPositions()
        {
            foreach (Boid boid in Boids)
            {
                List<Boid> LocalBoids = new List<Boid>();
                foreach (Boid otherBoid in Boids)
                {
                    if (boid.Id != otherBoid.Id && Vector2.Distance(boid.Position, otherBoid.Position) <= 0.5)
                    {
                        LocalBoids.Add(otherBoid);
                    }
                }

                var avgX = LocalBoids.Average(x => x.Position.X);
                var avgY = LocalBoids.Average(x => x.Position.Y);
                var target = new Vector2(avgX, avgY);

                boid.Position = target - boid.Position * Engine.DeltaTime * Speed;
            }
        }

        public new void OnLoad()
        {
            // Create boid objects
            Boids = new Boid[_numberOfBoids];
            for (int i = 0; i < _numberOfBoids; i++)
            {
                Boids[i] = new Boid(i, Shader);
            }

            base.OnLoad();

            var positionLocation = GL.GetAttribLocation(Shader.Program, "position");
            GL.VertexAttribPointer(positionLocation, 2, VertexAttribPointerType.Float, false, 0, 0);

            Shader.Use();
            for (var i = 0; i < _numberOfBoids; i++)
            {
                var instancePositionLocation = GL.GetUniformLocation(Shader.Program, $"positions[{i}]");
                GL.Uniform2(instancePositionLocation, Boids[i].Position);
            }

            IsLoaded = true;
        }

        public override void HandleKeyboardInputs()
        {
            return;
        }

        public override void Render()
        {
            CalculateNewPositions();

            base.Bind();

            // Projection
            var model = Matrix4.Identity;
            var modelLocation = GL.GetUniformLocation(Shader.Program, "model");
            var viewLocation = GL.GetUniformLocation(Shader.Program, "view");
            var projectionLocation = GL.GetUniformLocation(Shader.Program, "projection");

            var viewMatrix = Engine.Camera.GetViewMatrix();
            var projectionMatrix = Engine.Camera.GetProjectionMatrix();

            GL.UniformMatrix4(modelLocation, true, ref model);
            GL.UniformMatrix4(viewLocation, true, ref viewMatrix);
            GL.UniformMatrix4(projectionLocation, true, ref projectionMatrix);

            // Draw
            GL.DrawArraysInstanced(PrimitiveType.Triangles, 0, 3, _numberOfBoids);
        }
    }
}

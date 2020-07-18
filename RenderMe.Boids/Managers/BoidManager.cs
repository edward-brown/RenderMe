using RenderMe.Boids.GameObjects;
using RenderMe.Engine;
using RenderMe.Engine.Entity;
using System;
using OpenToolkit.Graphics.OpenGL4;
using System.Collections.Generic;
using System.Text;

namespace RenderMe.Boids.Managers
{
    public class BoidManager : Entity
    {
        private Boid[] Boids { get; set; }
        private Shader BoidShader { get; set; }

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

        public BoidManager(Shader shader, int numberOfBoids)
        {
            BoidShader = shader ?? throw new ArgumentNullException(nameof(shader));
            _numberOfBoids = numberOfBoids;
        }

        private void CalculateNewPositions()
        {
            throw new NotImplementedException();
        }

        public new void OnLoad()
        {
            // Create boid objects
            Boids = new Boid[_numberOfBoids];
            for (int i = 0; i < _numberOfBoids; i++)
            {
                Boids[i] = new Boid(BoidShader);
            }

            throw new NotImplementedException();
        }

        public override void HandleKeyboardInputs()
        {
            throw new NotImplementedException();
        }

        public override void Render()
        {
            throw new NotImplementedException();
        }
    }
}

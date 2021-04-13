
using OpenToolkit.Graphics.ES30;
using OpenToolkit.Mathematics;
using RenderMe.Engine;
using RenderMe.Engine.Entity;
using System;

namespace RenderMe.TargetedMovement.GameObjects
{
    /// <summary>
    /// Target based movement entity.
    /// </summary>
    public class Triangle : Entity2D
    {
        public float Speed { get; set; } = 1;
        public float Accuracy { get; set; } = 0.5f;

        private Vector2 Target { get; set; }
        private int PositionLocation { get; set; }

        public Triangle(IShader shader) : base()
        {
            Shader = shader;
            Position = new Vector2(0, 0);
            Vertices = new float[]
            {
               -0.5f, 0.0f,
                0.5f, 0.0f,
                0.0f, 0.5f
            };
        }

        private bool IsEntityAtTarget()
        {
            var d = Math.Sqrt(Math.Pow(Target.X - (Position.X), 2) + Math.Pow(Target.Y - (Position.Y), 2));
            return d <= Accuracy;
        }

        private void GetNewTarget()
        {
            var random = new Random();
            var x = (float)random.NextDouble() * 10;
            var y = (float)random.NextDouble() * 10;
            Target = new Vector2(x, y);
        }

        private void GetNewPosition()
        {
            var newPos = (Target - Position) * Engine.DeltaTime * Speed;
            Position += newPos;
        }

        public new void OnLoad()
        {
            base.OnLoad();

            var positionLocation = GL.GetAttribLocation(Shader.Program, "aPos");
            GL.VertexAttribPointer(positionLocation, 2, VertexAttribPointerType.Float, false, 0, 0);

            GL.UseProgram(Shader.Program);

            // Set position location
            PositionLocation = GL.GetUniformLocation(Shader.Program, "Pos");

            // Set color
            var colorLocation = GL.GetUniformLocation(Shader.Program, "color");
            GL.Uniform4(colorLocation, new Vector4(1.0f, 0.0f, 0.0f, 1.0f));

            Model = Matrix4.Identity;

            IsLoaded = true;
        }

        public override void Render()
        {
            // If the entity is at the current target, get a new target.
            if (IsEntityAtTarget())
            {
                GetNewTarget();
            }

            // Get new position
            GetNewPosition();

            // Bind
            base.Bind();

            // Set position
            GL.Uniform2(PositionLocation, Position);

            // Projection -> Should be abstracted out
            var model = Matrix4.Identity;
            var modelLocation = GL.GetUniformLocation(Shader.Program, "model");
            var viewLocation = GL.GetUniformLocation(Shader.Program, "view");
            var projectionLocation = GL.GetUniformLocation(Shader.Program, "projection");

            var viewMatrix = Engine.Camera.GetViewMatrix();
            var projectionMatrix = Engine.Camera.GetProjectionMatrix();

            GL.UniformMatrix4(modelLocation, true, ref model);
            GL.UniformMatrix4(viewLocation, true, ref viewMatrix);
            GL.UniformMatrix4(projectionLocation, true, ref projectionMatrix);

            // Render
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        }
    }
}

using OpenToolkit.Graphics.ES30;
using OpenToolkit.Mathematics;
using OpenToolkit.Windowing.Common;
using OpenToolkit.Windowing.Common.Input;
using OpenToolkit.Windowing.Desktop;
using RenderMe.Engine.Shaders;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RenderMe.Engine
{
    public class RenderEngine : GameWindow
    {
        private IList<Entity.Entity> Entities { get; set; } = new List<Entity.Entity>();

        public ShaderManager ShaderManager { get; private set; }

        public Stopwatch Stopwatch { get; set; }
        public float GetTime 
        {
            get
            {
                return Stopwatch.ElapsedMilliseconds / 1000.0f;
            } 
        }

        public float DeltaTime { get; set; }
        private long LastTime { get; set; }

        public Camera Camera { get; private set; }

        private bool _firstMove = true;

        private Vector2 _lastPos;

        public RenderEngine(int height, int width, string title)
            : base (new GameWindowSettings(), new NativeWindowSettings() { Size = new OpenToolkit.Mathematics.Vector2i(width, height), Title = title })
        {
            Stopwatch = new Stopwatch();
            Stopwatch.Start();

            Camera = new Camera(Vector3.UnitZ * 3, width / (float)height);
        }

        public void AddEntity(Entity.Entity entity)
        {
            entity.Engine = this;
            Entities.Add(entity);
        }

        public void UseShaders(string shaderFolderPath)
        {
            ShaderManager = new ShaderManager(shaderFolderPath);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            // Calculate delta time
            DeltaTime = (Stopwatch.ElapsedMilliseconds - LastTime) / 1000.0f;
            LastTime = Stopwatch.ElapsedMilliseconds;

            // Call functions for pressed keys
            if (KeyboardState.IsAnyKeyDown)
            {
                foreach (Entity.Entity entity in Entities)
                {
                    entity.HandleKeyboardInputs();
                }
            }

            // Camera stuff - needs to be put into camera class
            const float cameraSpeed = 1.5f;
            const float sensitivity = 0.2f;

            if (KeyboardState.IsKeyDown(Key.W))
            {
                Camera.Position += Camera.Front * cameraSpeed * (float)args.Time; // Forward
            }

            if (KeyboardState.IsKeyDown(Key.S))
            {
                Camera.Position -= Camera.Front * cameraSpeed * (float)args.Time; // Backwards
            }
            if (KeyboardState.IsKeyDown(Key.A))
            {
                Camera.Position -= Camera.Right * cameraSpeed * (float)args.Time; // Left
            }
            if (KeyboardState.IsKeyDown(Key.D))
            {
                Camera.Position += Camera.Right * cameraSpeed * (float)args.Time; // Right
            }
            if (KeyboardState.IsKeyDown(Key.Space))
            {
                Camera.Position += Camera.Up * cameraSpeed * (float)args.Time; // Up
            }
            if (KeyboardState.IsKeyDown(Key.LShift))
            {
                Camera.Position -= Camera.Up * cameraSpeed * (float)args.Time; // Down
            }

            // Mouse position -> Camera target
            var mouse = MouseState;

            if (IsFocused)
            {
                if (_firstMove) // this bool variable is initially set to true
                {
                    _lastPos = new Vector2(mouse.X, mouse.Y);
                    _firstMove = false;
                }
                else
                {
                    // Calculate the offset of the mouse position
                    var deltaX = mouse.X - _lastPos.X;
                    var deltaY = mouse.Y - _lastPos.Y;
                    _lastPos = new Vector2(mouse.X, mouse.Y);

                    // Apply the camera pitch and yaw (we clamp the pitch in the camera class)
                    Camera.Yaw += deltaX * sensitivity;
                    Camera.Pitch -= deltaY * sensitivity; // reversed since y-coordinates range from bottom to top
                }
            }
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            // Clear screen
            GL.Clear(ClearBufferMask.ColorBufferBit);

            // Render entities
            foreach (Entity.Entity entity in Entities)
            {
                entity.Render();
            }

            // Swap buffers
            SwapBuffers();
            base.OnRenderFrame(args);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, e.Width, e.Height);
            base.OnResize(e);
        }

        protected override void OnLoad()
        {
            GL.ClearColor(1, 1, 1, 1);

            base.OnLoad();
        }

        protected override void OnUnload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            foreach (Entity.Entity entity in Entities)
            {
                entity.OnUnload();
            }

            base.OnUnload();
        }
    }
}

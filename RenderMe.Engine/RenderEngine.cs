using OpenToolkit.Graphics.ES30;
using OpenToolkit.Mathematics;
using OpenToolkit.Windowing.Common;
using OpenToolkit.Windowing.Common.Input;
using OpenToolkit.Windowing.Desktop;
using RenderMe.Engine.Camera;
using RenderMe.Engine.Shaders;
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

        public BaseCamera Camera { get; private set; }

        public RenderEngine(int height, int width, string title, Vector3? cameraPos = null)
            : base (new GameWindowSettings(), new NativeWindowSettings() { Size = new Vector2i(width, height), Title = title })
        {
            Stopwatch = new Stopwatch();
            Stopwatch.Start();

            Camera = new BasicCamera(cameraPos ?? new Vector3(1, 1, 1), width / (float)height, this);
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

            // Update camera
            Camera.Update(args);
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

            // Unload all entities
            foreach (Entity.Entity entity in Entities)
            {
                entity.OnUnload();
            }

            // Dispose shaders
            ShaderManager.Dispose();

            base.OnUnload();
        }
    }
}

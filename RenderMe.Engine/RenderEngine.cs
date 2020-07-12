#define Debug

using OpenToolkit.Graphics.ES30;
using OpenToolkit.Windowing.Common;
using OpenToolkit.Windowing.Desktop;
using RenderMe.Engine.Shaders;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RenderMe.Engine
{
    public class RenderEngine : GameWindow
    {
        public IList<KeyboardEventMapping> KeyboardEventMappings { get; set; } = new List<KeyboardEventMapping>();
        public IList<Entity.Entity> Entities { get; set; } = new List<Entity.Entity>();

        public ShaderManager ShaderManager { get; private set; }

        public Stopwatch Stopwatch { get; set; }

        public RenderEngine(int height, int width, string title)
            : base (new GameWindowSettings(), new NativeWindowSettings() { Size = new OpenToolkit.Mathematics.Vector2i(width, height), Title = title })
        {
            #if Debug
                Stopwatch = new Stopwatch();
                Stopwatch.Start();
                Console.WriteLine($"Total frame-time {Stopwatch.ElapsedMilliseconds}ms");
            #endif
        }

        public void UseShaders(string shaderFolderPath)
        {
            ShaderManager = new ShaderManager(shaderFolderPath);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            // Call functions for pressed keys
            if (KeyboardState.IsAnyKeyDown)
            {
                foreach (KeyboardEventMapping eventMapping in KeyboardEventMappings)
                {
                    if (KeyboardState.IsKeyDown(eventMapping.InputKey))
                    {
                        eventMapping.Action();
                    }
                }
            }
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            // Restart stopwatch if in debug mode
            #if Debug
                Stopwatch.Restart();
            #endif

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

            #if Debug
                Console.SetCursorPosition(17, 0);
                Console.Write($"{Stopwatch.ElapsedMilliseconds}ms  ");
            #endif
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

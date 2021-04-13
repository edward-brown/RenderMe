using OpenToolkit.Mathematics;
using OpenToolkit.Windowing.Common;
using OpenToolkit.Windowing.Common.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace RenderMe.Engine.Camera
{
    public class BasicCamera : BaseCamera
    {
        private RenderEngine Engine { get; set; }

        private bool _firstMove = true;

        private Vector2 _lastPos;

        public BasicCamera(Vector3 position, float aspectRatio, RenderEngine engine) : base(position, aspectRatio)
        {
            Engine = engine;
        }

        public override void Update(FrameEventArgs args)
        {
            // Camera stuff - needs to be put into camera class
            const float cameraSpeed = 1.5f;
            const float sensitivity = 0.2f;

            if (Engine.KeyboardState.IsKeyDown(Key.W))
            {
                Position += Front * cameraSpeed * (float)args.Time; // Forward
            }

            if (Engine.KeyboardState.IsKeyDown(Key.S))
            {
                Position -= Front * cameraSpeed * (float)args.Time; // Backwards
            }
            if (Engine.KeyboardState.IsKeyDown(Key.A))
            {
                Position -= Right * cameraSpeed * (float)args.Time; // Left
            }
            if (Engine.KeyboardState.IsKeyDown(Key.D))
            {
                Position += Right * cameraSpeed * (float)args.Time; // Right
            }
            if (Engine.KeyboardState.IsKeyDown(Key.Space))
            {
                Position += Up * cameraSpeed * (float)args.Time; // Up
            }
            if (Engine.KeyboardState.IsKeyDown(Key.LShift))
            {
                Position -= Up * cameraSpeed * (float)args.Time; // Down
            }

            // Mouse position -> Camera target
            // var mouse = Engine.MouseState;

            // if (Engine.IsFocused)
            // {
            //     if (_firstMove) // this bool variable is initially set to true
            //     {
            //         _lastPos = new Vector2(mouse.X, mouse.Y);
            //         _firstMove = false;
            //     }
            //     else
            //     {
            //         // Calculate the offset of the mouse position
            //         var deltaX = mouse.X - _lastPos.X;
            //         var deltaY = mouse.Y - _lastPos.Y;
            //         _lastPos = new Vector2(mouse.X, mouse.Y);

            //         // Apply the camera pitch and yaw (we clamp the pitch in the camera class)
            //         Yaw += deltaX * sensitivity;
            //         Pitch -= deltaY * sensitivity; // reversed since y-coordinates range from bottom to top
            //     }
            // }
        }
    }
}

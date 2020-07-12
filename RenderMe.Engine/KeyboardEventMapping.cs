using OpenToolkit.Windowing.Common.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace RenderMe.Engine
{
    public class KeyboardEventMapping
    {
        public Key InputKey { get; }
        public Action Action { get; }

        public KeyboardEventMapping(Key key, Action action)
        {
            InputKey = key;
            Action = action ?? throw new ArgumentNullException(nameof(action));
        }
    }
}

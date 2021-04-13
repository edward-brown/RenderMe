using System;

namespace RenderMe.Engine.Shaders
{
    public interface IShader : IDisposable
    {
        public string Name { get; set; }
        public int Program { get; }

        // Functions
        void Load();
        void Compile();
        public void Use();
    }
}

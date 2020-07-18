using System;

namespace RenderMe.Engine.Shaders
{
    public interface IShader : IDisposable
    {
        void Load();
        void Compile();
        public void Use();
    }
}

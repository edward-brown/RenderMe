using OpenToolkit.Graphics.ES30;
using OpenToolkit.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace RenderMe.Engine.Entity
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }

        public Shader Shader { get; set; }

        private bool _isLoaded { get; set; }
        public bool IsLoaded
        {
            get
            {
                return _isLoaded;
            }
            set
            {
                if (value)
                {
                    GL.EnableVertexAttribArray(0);
                    GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                    GL.BindVertexArray(0);
                }

                _isLoaded = value;
            }
        }

        // Vertex Objects
        public int VBO { get; protected set; }
        public int VAO { get; protected set; }
        
        // Vertices
        public float[] Vertices { get; set; }

        // Positions
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        protected Entity()
        {

        }

        protected Entity(int x, int y, int z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public abstract void Render();

        public void OnLoad()
        {
            VAO = GL.GenVertexArray();
            VBO = GL.GenBuffer();

            GL.BindVertexArray(VAO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * sizeof(float), Vertices, BufferUsageHint.StaticDraw);
        }

        public void OnUnload()
        {
            IsLoaded = false;
            GL.DeleteBuffer(VBO);
        }

        public void Bind()
        {
            GL.UseProgram(Shader.Program);
            GL.BindVertexArray(VAO);
        }
    }
}

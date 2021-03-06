﻿using OpenToolkit.Graphics.ES30;
using OpenToolkit.Mathematics;
using RenderMe.Engine.Shaders;
using System;
using System.Collections.Generic;
using System.Text;

namespace RenderMe.Engine.Entity
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }

        public IShader Shader { get; set; }

        public RenderEngine Engine { get; set; }

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

        // Camera matrixes
        public Matrix4 Model { get; protected set; }
        public int ModelLocation { get; protected set; }
        public int ViewLocation { get; protected set; }
        public int ProjectionLocation { get; protected set; }

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

        public abstract void HandleKeyboardInputs();

        public void OnLoad()
        {
            VAO = GL.GenVertexArray();
            VBO = GL.GenBuffer();

            GL.BindVertexArray(VAO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * sizeof(float), Vertices, BufferUsageHint.StaticDraw);

            ModelLocation = GL.GetUniformLocation(Shader.Program, "model");
            ViewLocation = GL.GetUniformLocation(Shader.Program, "view");
            ProjectionLocation = GL.GetUniformLocation(Shader.Program, "projection");
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

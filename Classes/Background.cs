using System;
using System.Numerics;
using OpenTK.Graphics.OpenGL4;

namespace clockworks.Classes
{
    public class Background
    {
        public string imgSrc {get;set;}
        private Texture texture {get;set;}
        private Shader shader {get;set;}
        private Program program {get;set;}
        public Camera camera {get;set;}
        public Vector2 position {get;set;}
        public Vector2 size {get;set;}
        private float[] vertices {get;set;}
        private uint[] indices {get;set;}
        private int VB {get;set;}
        private int EB {get;set;}
        private int VAO {get;set;}
        public Background(string _imgSrc, Vector2 _position, Vector2 _size, Camera _camera)
        {
            imgSrc = _imgSrc;
            position =_position;
            size = _size;
            camera = _camera;
            texture = new Texture(
                imgSrc, OpenTK.Graphics.OpenGL4.TextureUnit.Texture0, 
                OpenTK.Graphics.OpenGL4.TextureWrapMode.ClampToEdge, OpenTK.Graphics.OpenGL4.TextureMinFilter.Nearest, 
                OpenTK.Graphics.OpenGL4.TextureMagFilter.Nearest
            );
            shader = new Shader("Engine/bg.vert", "Engine/bg.frag");
            program = new Program();

            VB = GL.GenBuffer();
            EB = GL.GenBuffer();
            VAO = GL.GenVertexArray();

            if(Main.MainClass.window is null)
            {
                Console.Write("Some Value is null \n value: ");
                Console.WriteLine("MainClass.window;");
                vertices = new float[]
                {
                    0, 0, 0.0f, 0f, 0f,   // Top-left
                    1000, 0, 0.0f, 1f, 0f,    // Top-right
                    1000, 1000, 0.0f, 1f, 1f,   // Bottom-right
                    0, 1000, 0.0f, 0f, 1f   // Bottom-left
                };
                indices = new uint[]
                {
                    0, 1, 2,
                    2, 3, 0
                };
                return;
            }
            vertices = new float[]
            {
                position.X, position.Y, 0.0f, 0f, 0f,   // Top-left
                position.X + size.X, position.Y, 0.0f, 1f, 0f,    // Top-right
                position.X + size.X, position.Y + size.Y, 0.0f, 1f, 1f,   // Bottom-right
                position.X, position.Y + size.Y, 0.0f, 0f, 1f   // Bottom-left
            };
            indices = new uint[]
            {
                0, 1, 2,
                2, 3, 0
            };

            program.AttachShader(shader.vShader.GLObject);
            program.AttachShader(shader.fShader.GLObject);

            program.Link();

            GL.BindVertexArray(VAO);

            GL.BindBuffer(BufferTarget.ArrayBuffer, VB);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StreamDraw);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EB);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(int), indices, BufferUsageHint.StreamDraw);

            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
        }
        public void Use()
        {
            program.Use();

            program.SetMatrix4x4("projection", camera.GetProjectionMatrix());
            
            texture.Activate();
        }
        public void refreshVertices()
        {
            vertices = new float[]
            {
                position.X, position.Y, 0.0f, 0f, 0f,   // Top-left
                position.X + size.X, position.Y, 0.0f, 1f, 0f,    // Top-right
                position.X + size.X, position.Y + size.Y, 0.0f, 1f, 1f,   // Bottom-right
                position.X, position.Y + size.Y, 0.0f, 0f, 1f   // Bottom-left
            };
            indices = new uint[]
            {
                0, 1, 2,
                2, 3, 0
            };

            GL.BindVertexArray(VAO);

            GL.BindBuffer(BufferTarget.ArrayBuffer, VB);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StreamDraw);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EB);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(int), indices, BufferUsageHint.StreamDraw);

            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
        }
        public void render()
        {
            program.Use();
            texture.Activate();
            GL.BindVertexArray(VAO);
            GL.DrawElements(PrimitiveType.Triangles, this.indices.Length, DrawElementsType.UnsignedInt, 0);
        }
    }
}
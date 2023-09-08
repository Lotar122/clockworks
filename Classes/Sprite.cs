using System.Numerics;
using OpenTK.Graphics.OpenGL4;
using clockworks.Enums;

namespace clockworks.Classes
{
    public class Sprite
    {
        public string ID {get;set;}
        public Shader shader {get;set;}
        public Program program {get;set;}
        public Texture texture {get;set;}
        public Camera camera {get;set;}
        public Vector2 position {get;set;}
        public Vector2 size {get;set;}
        public PhysicsObject phys {get;set;}
        public BoxCollider collider {get;set;}
        public Mask mask {get;set;}
        public float[] vertices {get;set;}
        public uint[] indices {get;set;}
        public int VB {get;set;}
        public int EB {get;set;}
        public int VAO {get;set;}
        private bool FitToSize {get;set;}
        public Sprite(string _ID, Vector2 _position, Vector2 _size, Shader _shader, Program _program, Texture _texture, Camera _camera, bool _FitToSize = false)
        {   
            ID = _ID;
            position = _position;
            size = _size;
            shader = _shader;
            program = _program;
            texture = _texture;
            camera = _camera;
            FitToSize = _FitToSize;
            phys = new PhysicsObject(1);
            collider = new BoxCollider();
            // Calculate the scale factor for upscaling

            // Calculate the new dimensions
            int newWidth = (int)size.X;
            int newHeight = (int)size.Y;

            // Upscale the image using ImageSharp
            Image<Rgba32> upscaledImage = texture.image.Clone(ctx => ctx.Resize(newWidth, newHeight, KnownResamplers.NearestNeighbor));
            mask = new Mask(upscaledImage);

            VB = GL.GenBuffer();
            EB = GL.GenBuffer();
            VAO = GL.GenVertexArray();

            if(FitToSize)
            {
                vertices = new float[]
                {
                    position.X, position.Y, 0.0f, 0f, 0f,   // Top-left
                    position.X + size.X, position.Y, 0.0f, 1f, 0f,    // Top-right
                    position.X + size.X, position.Y + size.Y, 0.0f, 1f, 1f,   // Bottom-right
                    position.X, position.Y + size.Y, 0.0f, 0f, 1f   // Bottom-left
                };
            }
            else
            {
                vertices = new float[]
                {
                    position.X, position.Y, 0.0f, 0f, 0f,   // Top-left
                    position.X + size.X, position.Y, 0.0f, texture.image.Width, 0f,    // Top-right
                    position.X + size.X, position.Y + size.Y, 0.0f, texture.image.Width, texture.image.Height,   // Bottom-right
                    position.X, position.Y + size.Y, 0.0f, 0f, texture.image.Height   // Bottom-left
                };
            }
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
            if(FitToSize)
            {
                vertices = new float[]
                {
                    position.X, position.Y, 0.0f, 0f, 0f,   // Top-left
                    position.X + size.X, position.Y, 0.0f, 1f, 0f,    // Top-right
                    position.X + size.X, position.Y + size.Y, 0.0f, 1f, 1f,   // Bottom-right
                    position.X, position.Y + size.Y, 0.0f, 0f, 1f   // Bottom-left
                };
            }
            else
            {
                vertices = new float[]
                {
                    position.X, position.Y, 0.0f, 0f, 0f,   // Top-left
                    position.X + size.X, position.Y, 0.0f, texture.image.Width, 0f,    // Top-right
                    position.X + size.X, position.Y + size.Y, 0.0f, texture.image.Width, texture.image.Height,   // Bottom-right
                    position.X, position.Y + size.Y, 0.0f, 0f, texture.image.Height   // Bottom-left
                };
            }
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
        public void physicsUpdate()
        {
            phys.Update();
            position += phys.velocity;
        }
        public void render()
        {
            program.Use();
            texture.Activate();
            GL.BindVertexArray(VAO);
            GL.DrawElements(PrimitiveType.Triangles, this.indices.Length, DrawElementsType.UnsignedInt, 0);
        }
        public void attachPhysics(PhysicsObject _phys)
        {
            phys = _phys;
        }
        public void usePhysics(bool x)
        {
            phys.use = x;
        }
        public void detailedCollisionInfo(Collision info)
        {
            phys.bottomCollision = info.bottom;
        }
    }
}
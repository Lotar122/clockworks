using System.Numerics;
using OpenTK.Graphics.OpenGL4;

namespace clockworks.Classes
{
    public class Program
    {
        public int GLObject {get;set;}
        public Program()
        {
            GLObject = GL.CreateProgram();
        }
        public void AttachShader(int _shader)
        {
            GL.AttachShader(GLObject, _shader);
        }
        public void Link()
        {
            GL.LinkProgram(GLObject);
        }
        public void Use()
        {
            GL.UseProgram(GLObject);
        }
        public void SetMatrix4x4(string uniformName, Matrix4x4 mat)
        {
            int location = GL.GetUniformLocation(GLObject, uniformName);
            GL.UniformMatrix4(location, 1, false, GetMatrix4x4Values(mat));
        }
        public void SetVector4(string uniformName, Vector4 vec)
        {
            int location = GL.GetUniformLocation(GLObject, uniformName);
            OpenTK.Mathematics.Vector4 vecb = new OpenTK.Mathematics.Vector4(vec.X, vec.Y, vec.Z, vec.W);
            GL.Uniform4(location, vecb);
        }
        private float[] GetMatrix4x4Values(Matrix4x4 mat)
        {
            return new float[]
            {
                mat.M11, mat.M12, mat.M13, mat.M14,
                mat.M21, mat.M22, mat.M23, mat.M24,
                mat.M31, mat.M32, mat.M33, mat.M34,
                mat.M41, mat.M42, mat.M43, mat.M44
            };
        }
    }
}
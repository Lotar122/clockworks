using System.Numerics;

namespace clockworks.Methods
{
    public static partial class Functions
    {
        public static Matrix4x4 CreateModelMatrix(Vector2 scale, Vector2 position)
        {
            Matrix4x4 trans = Matrix4x4.CreateTranslation(position.X, position.Y, 0);
            Matrix4x4 sca = Matrix4x4.CreateScale(scale.X, scale.Y, 1);
            Matrix4x4 rot = Matrix4x4.CreateRotationZ(0);

            Matrix4x4 modelMatrix = sca * rot * trans;

            return modelMatrix;
        }
    }
}
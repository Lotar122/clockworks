using System.Numerics;

namespace clockworks.Classes
{
    public class Methods
    {
        public static int SizeOf(string _param)
        {
            if(_param == "float")
            {
                return sizeof(float);
            }
            else if(_param == "double")
            {
                return sizeof(double);
            }
            else if(_param == "int")
            {
                return sizeof(int);
            }
            Console.WriteLine("Unknown Type");
            return 404;
        }
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
namespace clockworks.Classes
{
    public class Vector
    {
        public static System.Numerics.Vector2 Vector2(float x, float y)
        {
            return new System.Numerics.Vector2((float)x, (float)y);
        }
        public static System.Numerics.Vector2 Vector2(double x, double y)
        {
            return new System.Numerics.Vector2((float)x, (float)y);
        }
        public static System.Numerics.Vector4 Vector4(float x, float y, float z, float w)
        {
            return new System.Numerics.Vector4((float)x, (float)y, (float)z, (float)w);
        }
        public static System.Numerics.Vector4 Vector4(double x, double y, double z, double w)
        {
            return new System.Numerics.Vector4((float)x, (float)y, (float)z, (float)w);
        }
    }
}
using System.Numerics;
using clockworks.Main;

namespace clockworks.Classes
{
    public class Camera
    {
        public Vector2 FocusPosition {get;set;}
        public float Zoom {get;set;}
        public Matrix4x4 scaleMatrix {get;set;}
        public Camera(float _Zoom, Vector2 _FocusPosition)
        {
            Zoom = (float)_Zoom;
            FocusPosition = _FocusPosition;

            scaleMatrix = Matrix4x4.CreateScale((float)Zoom);
        }
        public Matrix4x4 GetProjectionMatrix()
        {
            if(MainClass.window is null)
            {
                Console.WriteLine("Some Value is null \n value: MainClass.nws;");
                return Matrix4x4.Identity;
            }
            float left = 0;
            float right = MainClass.window.Size.X;
            float bottom = MainClass.window.Size.Y;  // Note: Inverted compared to "top"
            float top = 0;                         // Note: Inverted compared to "bottom"
            float zNear = 0.1f;
            float zFar = 100f;

            Matrix4x4 orthoMatrix = Matrix4x4.CreateOrthographicOffCenter(left, right, bottom, top, zNear, zFar);
            return orthoMatrix;
        }
        public void RefreshZoom(float zoom = 0)
        {
            if(zoom == 0)
            {
                scaleMatrix = Matrix4x4.CreateScale((float)Zoom);
            }
            else
            {
                scaleMatrix = Matrix4x4.CreateScale((float)zoom);
            }
        }
    }
}
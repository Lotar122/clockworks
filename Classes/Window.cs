using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace clockworks.Classes
{
    public class Window : GameWindow
    {
        public Window(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws)
        {

        }
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
        }
    }
}
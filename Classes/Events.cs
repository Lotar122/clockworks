using System.Reflection;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using clockworks.Classes;
using clockworks.Main;
using System.Numerics;
using clockworks.Methods;

namespace clockworks.Events
{
    public class InternalEventManager
    {
        public static void Deafult()
        {
            if(
                MainClass.window is null || MainClass.engine is null ||
                MainClass.engine is null || MainClass.eventManager is null ||
                MainClass.keyboardRegister is null || MainClass.spriteRegister is null ||
                MainClass.nws is null || MainClass.gws is null
            )
            {
                Console.Write("Some Value is null \n value: ");
                if(MainClass.window is null)
                {
                    Console.WriteLine("MainClass.window;");
                }
                if(MainClass.engine is null)
                {
                    Console.WriteLine("MainClass.engine;");
                }
                if(MainClass.eventManager is null)
                {
                    Console.WriteLine("MainClass.eventManager;");
                }
                if(MainClass.keyboardRegister is null)
                {
                    Console.WriteLine("MainClass.keyboardRegister;");
                }
                if(MainClass.spriteRegister is null)
                {
                    Console.WriteLine("MainClass.spriteRegister;");
                }
                if(MainClass.nws is null)
                {
                    Console.WriteLine("MainClass.nws;");
                }
                if(MainClass.gws is null)
                {
                    Console.WriteLine("MainClass.gws;");
                }
                Console.WriteLine();
                return;
            }
            GameWindow window = MainClass.window;
            Engine engine = MainClass.engine;
            EventManager eventManager = MainClass.eventManager;
            KeyboardRegister keyboardRegister = MainClass.keyboardRegister;
            SpriteRegister spriteRegister = MainClass.spriteRegister;
            NativeWindowSettings nws = MainClass.nws;
            GameWindowSettings gws = MainClass.gws;

            BoxCollider collider = new BoxCollider();

            List<Sprite> physicsQueue = new List<Sprite>();

            window.Load += delegate
            {
                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
                GL.Viewport(0, 0, nws.Size.X, nws.Size.Y);

                Functions.SetInterval((sender, e) => {
                    List<Thread> TList = new List<Thread>();

                    for (int i = 0; i < spriteRegister.reg.Count; i++)
                    {
                        Sprite sprite = spriteRegister.reg[i];
                        BoxCollider collider = new BoxCollider();

                        Thread t = new Thread(() => {
                            Physics.ColisionThread(sprite, collider, spriteRegister);
                            Physics.FrictionThread(sprite, sprite.phys.friction);
                            sprite.physicsUpdate();
                        });
                        t.Start();
                        TList.Add(t);
                    }
                    foreach(Thread t in TList)
                    {
                        t.Join();
                    }
                }, 1000 / 60).Start();

                engine.Run();
                engine.setup();
            };

            window.Resize += delegate(ResizeEventArgs e)
            {
                nws.Size = e.Size;
                GL.Viewport(0, 0, e.Size.X, e.Size.Y);
                engine.onResize();
            };

            window.UpdateFrame += delegate(FrameEventArgs e)
            {
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                engine.loop();

                foreach(Sprite sprite in spriteRegister.reg)
                {
                    sprite.refreshVertices();
                    sprite.render();
                }

                window.SwapBuffers();
            };
            window.KeyDown += delegate(KeyboardKeyEventArgs e)
            {
                eventManager.FireEvents("onKeyDown", e);
                Type keyboardRegType = keyboardRegister.Key.GetType();
                PropertyInfo[] properties = keyboardRegType.GetProperties();
                Type enumType = typeof(Keys);
                foreach(PropertyInfo property in properties)
                {
                    //Console.WriteLine(property.Name);
                    Keys key = (Keys)Enum.Parse(enumType, property.Name);
                    if(e.Key == key)
                    {
                        property.SetValue(keyboardRegister.Key, true);
                    }
                }
                engine.addHostObject("KeyboardRegister", keyboardRegister);
            };
            window.KeyUp += delegate(KeyboardKeyEventArgs e)
            {  
                eventManager.FireEvents("onKeyUp", e);
                Type keyboardRegType = keyboardRegister.Key.GetType();
                PropertyInfo[] properties = keyboardRegType.GetProperties();
                Type enumType = typeof(Keys);
                foreach(PropertyInfo property in properties)
                {
                    Keys key = (Keys)Enum.Parse(enumType, property.Name);
                    if(e.Key == key)
                    {
                        property.SetValue(keyboardRegister.Key, false);
                    }
                }
                engine.addHostObject("KeyboardRegister", keyboardRegister);
            };
            window.Maximized += delegate(MaximizedEventArgs e)
            {
                nws.Size = window.Size;
                GL.Viewport(0, 0, nws.Size.X, nws.Size.Y);
                engine.onResize();
                eventManager.FireEvents("onMaximized", e);
            };
            window.Minimized += delegate(MinimizedEventArgs e)
            {
                eventManager.FireEvents("onMinimized", e);
            };
            window.MouseUp += delegate(MouseButtonEventArgs e)
            {
                eventManager.FireEvents("onMouseUp", e);
            };
            window.MouseDown += delegate(MouseButtonEventArgs e)
            {
                eventManager.FireEvents("onMouseDown", e);
            };
            window.MouseWheel += delegate(MouseWheelEventArgs e)
            {
                eventManager.FireEvents("onMouseWheel", e);
            };
            window.MouseEnter += delegate()
            {
                eventManager.FireEvents("onMouseEnter");
            };
            window.MouseLeave += delegate()
            {
                eventManager.FireEvents("onMouseLeave");
            };
            window.MouseMove += delegate(MouseMoveEventArgs e)
            {
                eventManager.FireEvents("onMouseMove", e);
            };
        }
    }
}
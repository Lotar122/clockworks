using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using System.Text.Json;

using clockworks.Classes;
using clockworks.Events;
using System.IO;
using System;

namespace clockworks.Main
{
    public class MainClass
    {
        public static string? fpath {get;set;}
        public static GameWindowSettings? gws {get;set;}
        public static NativeWindowSettings? nws {get;set;}
        public static GameWindow? window {get;set;}
        public static Engine? engine {get;set;}
        public static EventManager? eventManager {get;set;}
        public static KeyboardRegister? keyboardRegister {get;set;}
        public static SpriteRegister? spriteRegister {get;set;}

        public static void Main(string[] args)
        {
            string path = @"../conf.sconfig";
            string[] fCont = File.ReadAllLines(path);
            fpath = fCont[0];
            fpath = fpath.Replace("Resources", "");
            fpath = fpath.Replace(":", "");
            fpath = fpath.Replace(" ", "");
            Console.WriteLine(fpath);
            if(fpath is null) fpath = "";
            string rawJSON_Properties = File.ReadAllText(@"../" + fpath + "/Properties/properties.json");
            Properties? _Properties = JsonSerializer.Deserialize<Properties>(rawJSON_Properties);

            if(_Properties is null)
            {
                Console.WriteLine("it seems like the Properties/properties.json is not present");
                return;
            }
            if(_Properties.Icon is null || _Properties.APIVersion is null)
            {
                Console.WriteLine("something is wrong with the Properties/properties.json");
                return;
            }
            if(_Properties.Icon.path is null)
            {
                Console.WriteLine("something is wrong with the Properties/properties.json");
                return;
            }

            Image<Rgba32> image = SixLabors.ImageSharp.Image.Load<Rgba32>(@"../" + _Properties.Icon.path);
            // image.Mutate(x => x.Flip(FlipMode.Vertical));
            var pixels = new byte[4 * image.Width * image.Height];
            image.CopyPixelDataTo(pixels);

            gws = GameWindowSettings.Default;
            nws = NativeWindowSettings.Default;

            gws.UpdateFrequency = _Properties.RenderFrequency;

            nws.API = ContextAPI.OpenGL;
            nws.APIVersion = Version.Parse(_Properties.APIVersion);
            nws.AutoLoadBindings = true;
            nws.Size = new Vector2i(_Properties.Width, _Properties.Height);
            nws.Title = _Properties.Title;
            //Console.WriteLine(_Properties.Icon.size.x + " " + _Properties.Icon.size.y);
            nws.Icon = new OpenTK.Windowing.Common.Input.WindowIcon(
                new OpenTK.Windowing.Common.Input.Image(image.Width, image.Height, pixels)
            );


            window = new GameWindow(gws, nws);

            if(_Properties.VSync)
            {
                window.VSync = VSyncMode.On;
            }
            else
            {
                window.VSync = VSyncMode.Off;
            }

            engine = new Engine();
            keyboardRegister = new KeyboardRegister();
            eventManager = new EventManager(engine);
            spriteRegister = new SpriteRegister();
            
            engine.addHostType<clockworks.Classes.Shader>();
            engine.addHostType<clockworks.Classes.Program>();
            engine.addHostType<clockworks.Classes.Texture>();
            engine.addHostType<clockworks.Classes.Methods>();
            engine.addHostType<clockworks.Classes.Camera>();
            engine.addHostType<clockworks.Classes.Sprite>();
            engine.addHostType<clockworks.Classes.PhysicsObject>();
            engine.addHostType<clockworks.Classes.Background>();
            engine.addHostType<OpenTK.Graphics.OpenGL4.GL>();
            engine.addHostType<OpenTK.Graphics.OpenGL4.TextureUnit>();
            engine.addHostType<OpenTK.Graphics.OpenGL4.TextureWrapMode>();
            engine.addHostType<OpenTK.Graphics.OpenGL4.TextureMinFilter>();
            engine.addHostType<OpenTK.Graphics.OpenGL4.TextureMagFilter>();
            engine.addHostType<OpenTK.Graphics.OpenGL4.VertexAttribPointerType>();
            engine.addHostType<OpenTK.Graphics.OpenGL4.DrawElementsType>();
            engine.addHostType<OpenTK.Graphics.OpenGL4.PrimitiveType>();
            engine.addHostType<OpenTK.Graphics.OpenGL4.ClearBufferMask>();
            engine.addHostType<OpenTK.Windowing.GraphicsLibraryFramework.Keys>();
            engine.addHostType<clockworks.Classes.Vector>();
            engine.addHostType<System.Numerics.Matrix4x4>();
            
            engine.addHostObject("Engine", engine);
            engine.addHostObject("Window", window);
            engine.addHostObject("nws", nws);
            engine.addHostObject("KeyboardRegister", keyboardRegister);
            engine.addHostObject("EventManager", eventManager);
            engine.addHostObject("SpriteRegister", spriteRegister);

            InternalEventManager.Deafult();
            window.Run();
        }
    }
}
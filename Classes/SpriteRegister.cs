using OpenTK.Windowing.Common;
using System.Collections.Generic;

namespace clockworks.Classes
{
    public class SpriteRegister
    {
        public List<Sprite> reg {get;set;}
        public SpriteRegister()
        {
            reg = new List<Sprite>();
            if(Main.MainClass.window is null) return;
            Main.MainClass.window.Resize += delegate(ResizeEventArgs e)
            {
                foreach(var sprite in reg)
                {
                    sprite.program.SetMatrix4x4("projection", sprite.camera.GetProjectionMatrix());
                }
            };
        }
        public void addSprite(Sprite sprite)
        {
            reg.Add(sprite);
        }
        public void removeSprite(string ID)
        {
            int index = reg.FindIndex(x => x.ID == ID);
            reg.RemoveAt(index);
        }
        public void setGlobalFriction(float _friction)
        {
            foreach(Sprite sprite in reg)
            {
                sprite.phys.setFriction(_friction);
            }
        }
        public void setGlobalFriction(double _friction)
        {
            foreach(Sprite sprite in reg)
            {
                sprite.phys.setFriction(_friction);
            }
        }
        public void setGlobalFriction(int _friction)
        {
            foreach(Sprite sprite in reg)
            {
                sprite.phys.setFriction(_friction);
            }
        }
    }
}
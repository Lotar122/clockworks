using OpenTK.Windowing.Common;
using System.Collections.Generic;

namespace clockworks.Classes
{
    public class EventManager
    {
        public Engine engine {get;set;}
        private Dictionary<string, string> keyEvents {get;set;}
        public EventManager(Engine _engine)
        {
            engine = _engine;
            keyEvents = new Dictionary<string, string>();
        }
        public void FireEvents(string EventKey, KeyboardKeyEventArgs e)
        {
            foreach(var val in keyEvents)
            {
                if(val.Key == EventKey)
                {
                    engine.Invoke(val.Value, new object[] {e});
                }
            }
        }
        public void FireEvents(string EventKey, MaximizedEventArgs e)
        {
            foreach(var val in keyEvents)
            {
                if(val.Key == EventKey)
                {
                    engine.Invoke(val.Value, new object[] {e});
                }
            }
        }
        public void FireEvents(string EventKey, MinimizedEventArgs e)
        {
            foreach(var val in keyEvents)
            {
                if(val.Key == EventKey)
                {
                    engine.Invoke(val.Value, new object[] {e});
                }
            }
        }
        public void FireEvents(string EventKey, MouseButtonEventArgs e)
        {
            foreach(var val in keyEvents)
            {
                if(val.Key == EventKey)
                {
                    engine.Invoke(val.Value, new object[] {e});
                }
            }
        }
        public void FireEvents(string EventKey, MouseMoveEventArgs e)
        {
            foreach(var val in keyEvents)
            {
                if(val.Key == EventKey)
                {
                    engine.Invoke(val.Value, new object[] {e});
                }
            }
        }
        public void FireEvents(string EventKey, MouseWheelEventArgs e)
        {
            foreach(var val in keyEvents)
            {
                if(val.Key == EventKey)
                {
                    engine.Invoke(val.Value, new object[] {e});
                }
            }
        }
        public void FireEvents(string EventKey)
        {
            foreach(var val in keyEvents)
            {
                if(val.Key == EventKey)
                {
                    engine.Invoke(val.Value);
                }
            }
        }
        public void registerEvent(string EventKey, string Callback)
        {
            keyEvents.Add(EventKey, Callback);
        }
    }
}
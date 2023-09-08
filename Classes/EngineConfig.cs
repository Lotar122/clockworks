using System.Collections.Generic;

namespace clockworks.Classes
{
    public class EngineConfig
    {
        public Dictionary<string, MethodProperty> methods {get;set;}
        public EngineConfig()
        {
            methods = new Dictionary<string, MethodProperty>();
        }
    }
}
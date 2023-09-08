using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clockworks.Classes
{
    public class JSConsole
    {
        public void log(object msg)
        {
            Console.WriteLine("<JS-LOG> " + msg.ToString() + " <JS-LOG>");
        }
        public void error(object error)
        {
            throw new Exception("<JS-ERROR> " + error.ToString() + " <JS-ERROR>");
        }
        public void warn(object msg)
        {
            Console.WriteLine("<JS-WARN> " + msg.ToString() + " <JS-WARN>");
        }
    }
}
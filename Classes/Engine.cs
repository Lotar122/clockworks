using Microsoft.ClearScript.V8;
using System.Text.Json;

namespace clockworks.Classes
{
    public class Engine
    {
        public V8ScriptEngine v8Engine {get;set;}
        public string? Code {get;set;}
        private EngineConfig config {get;set;}
        public Engine()
        {
            config = new EngineConfig();

            string rawJSON_Config = File.ReadAllText(@"../" + Main.MainClass.fpath + "/Properties/engine.json");
            var x = new List<MethodProperty>();
            x = JsonSerializer.Deserialize<List<MethodProperty>>(rawJSON_Config);

            v8Engine = new V8ScriptEngine(V8ScriptEngineFlags.EnableDebugging);

            if(x is null)
            {
                return;
            }

            foreach(var prop in x)
            {
                if(prop.name is null) break;
                config.methods.Add(prop.name, prop);
            }

            //v8Engine.AddHostType(typeof(Console));
            v8Engine.AddHostObject("console", new JSConsole());

            string rawJSON_Files = File.ReadAllText(@"../" + Main.MainClass.fpath + "/Properties/files.json");
            FileList? fileList = JsonSerializer.Deserialize<FileList>(rawJSON_Files);

            string totalCode = "";

            if(fileList is null)
            {
                Console.WriteLine("it seems like the Properties/files.json is not present");
                return;
            }
            
            foreach(string filePath in fileList.FilePaths)
            {
                string fileContent = File.ReadAllText(@"../" + Main.MainClass.fpath + "/" + filePath);
                totalCode += fileContent;
            }

            Code = totalCode;
            Code ??= "console.log('you did not provide any scripts');";
        }
        public void addHostType<T>()
        {
            v8Engine.AddHostType(typeof(T));
        }
        public void addHostObject(string _name, object _object)
        {
            v8Engine.AddHostObject(_name, _object);
        }
        public void Invoke(string x, params object[] parameters)
        {
            v8Engine.Invoke(x, parameters);
        }
        public void Run()
        {
            v8Engine.Execute(Code);
        }
        public void setup()
        {
            if(!config.methods["setup"].use) return;
            v8Engine.Invoke("setup");
        }
        public void loop()
        {
            if(!config.methods["loop"].use) return;
            v8Engine.Invoke("loop");
        }
        public void onResize()
        {
            if(!config.methods["onResize"].use) return;
            v8Engine.Invoke("onResize");
        }
    }
}
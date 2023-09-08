using OpenTK.Graphics.OpenGL4;

namespace clockworks.Classes
{
    public class Shader
    {
        public Property vShader {get;set;}
        public Property fShader {get;set;}
        private string vShaderSource {get;set;}
        private string fShaderSource {get;set;}
        public Shader(string _vPath, string _fPath)
        {
            _vPath = "../" + Main.MainClass.fpath + "/" + _vPath;
            _fPath = "../" + Main.MainClass.fpath + "/" + _fPath;
            vShader = new Property(GL.CreateShader(ShaderType.VertexShader));
            fShader = new Property(GL.CreateShader(ShaderType.FragmentShader));
            vShaderSource = File.ReadAllText(_vPath);
            fShaderSource = File.ReadAllText(_fPath);
            GL.ShaderSource(vShader.GLObject, vShaderSource);
            GL.ShaderSource(fShader.GLObject, fShaderSource);
            GL.CompileShader(vShader.GLObject);
            GL.CompileShader(fShader.GLObject);
        }
        public void reConstruct()
        {
            GL.ShaderSource(vShader.GLObject, vShaderSource);
            GL.ShaderSource(fShader.GLObject, fShaderSource);
            GL.CompileShader(vShader.GLObject);
            GL.CompileShader(fShader.GLObject);
            Console.WriteLine(GL.GetShaderInfoLog(vShader.GLObject) + "  v");
            Console.WriteLine(GL.GetShaderInfoLog(fShader.GLObject) + "  f");
        }
    }
}
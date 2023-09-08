using System.Numerics;
using OpenTK.Graphics.OpenGL4;

namespace clockworks.Classes
{
    public class Texture
    {
        public int GLObject {get;set;}
        public TextureUnit texUnit {get;set;}
        public TextureWrapMode texWrapMode {get;set;}
        public TextureMinFilter texMinFilter {get;set;}
        public TextureMagFilter texMagFilter {get;set;}
        public Image<Rgba32> image {get;set;}
        public Texture(string _imgSrc, TextureUnit _texUnit, TextureWrapMode _texWrapMode, TextureMinFilter _texMinFilter, TextureMagFilter _texMagFilter)
        {
            _imgSrc = "../" + Main.MainClass.fpath + "/" + _imgSrc;
            texUnit = _texUnit;
            texWrapMode = _texWrapMode;
            texMinFilter = _texMinFilter;
            texMagFilter = _texMagFilter;

            image = Image.Load<Rgba32>(_imgSrc);
            //image.Mutate(x => x.Flip(FlipMode.Vertical));
            var pixels = new byte[4 * image.Width * image.Height];
            image.CopyPixelDataTo(pixels);
            GLObject = GL.GenTexture();
                
            //GL.ActiveTexture(_texUnit);
            GL.BindTexture(TextureTarget.Texture2D, GLObject);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)_texWrapMode);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)_texWrapMode);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)_texMagFilter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)_texMinFilter);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pixels);
        }
        public void SetTextureUnit(TextureUnit _texUnit)
        {
            texUnit = _texUnit;
        }
        public void Activate()
        {
            GL.ActiveTexture(texUnit);
            GL.BindTexture(TextureTarget.Texture2D, GLObject);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)texWrapMode);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)texWrapMode);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)texMagFilter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)texMinFilter);
        }
    }
}
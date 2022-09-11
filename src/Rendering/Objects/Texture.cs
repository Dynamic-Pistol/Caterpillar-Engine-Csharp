using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Silk.NET.OpenGL;

namespace CaterpillarEngine.Rendering.Objects
{
	public struct Texture : IDisposable
	{
		private uint _handle;

		internal unsafe Texture(Image<Rgba32> img)
		{
			_handle = GraphicsDevice.GL.GenTexture();
			Bind();
			byte[] pixeldata = new byte[4 * img.Width * img.Height];
			img.CopyPixelDataTo(pixeldata);
			fixed(void* src = pixeldata)
			{
				GraphicsDevice.GL.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgba8, (uint)img.Width, (uint)img.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte,src);
				SetParameters();
			}
		}

		internal unsafe Texture(Assimp.EmbeddedTexture img)
		{
			_handle = GraphicsDevice.GL.GenTexture();
			Bind();
			byte[] pixeldata = new byte[4 * img.Width * img.Height];
			pixeldata = img.CompressedData;
			fixed(void* src = pixeldata)
			{
				GraphicsDevice.GL.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgba8, (uint)img.Width, (uint)img.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte,src);
				SetParameters();
			}
		}

		public void Bind(TextureUnit textureSlot = TextureUnit.Texture0)
		{
			GraphicsDevice.GL.ActiveTexture(textureSlot);
			GraphicsDevice.GL.BindTexture(TextureTarget.Texture2D, _handle);
		}

		public void Dispose()
		{
			GraphicsDevice.GL.DeleteTexture(_handle);
		}

		private void SetParameters()
		{
			GraphicsDevice.GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)GLEnum.ClampToEdge);
			GraphicsDevice.GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)GLEnum.ClampToEdge);
			GraphicsDevice.GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)GLEnum.LinearMipmapLinear);
			GraphicsDevice.GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)GLEnum.Linear);
			GraphicsDevice.GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureBaseLevel, 0);
			GraphicsDevice.GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMaxLevel, 8);
			GraphicsDevice.GL.GenerateMipmap(TextureTarget.Texture2D);
		}
	}
}

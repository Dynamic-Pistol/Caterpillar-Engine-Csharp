using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Silk.NET.OpenGL;
namespace CaterpillarEngine.Rendering
{
	public class FrameBuffer
	{
		private uint _handle;
		private uint tex;
		private int Width;
		private int Height;

		public FrameBuffer(int width, int height)
		{
			Bind();
			Width = width;
			Height = height;
		}

		public unsafe void Bind()
		{
			_handle = GraphicsDevice.GL.GenFramebuffer();
			GraphicsDevice.GL.BindFramebuffer(FramebufferTarget.Framebuffer, _handle);
			if (GraphicsDevice.GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer) != GLEnum.FramebufferComplete)
				throw new Exception("I have no idea what to put here i supposed i should actually put something useful here");
			GraphicsDevice.GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
			GraphicsDevice.GL.DeleteFramebuffer(_handle);
			tex = GraphicsDevice.GL.GenTexture();
			GraphicsDevice.GL.BindTexture(TextureTarget.Texture2D, tex);
			GraphicsDevice.GL.TexImage2D(TextureTarget.Texture2D, 0, (int)PixelFormat.Rgba, 1280, 780, 0, PixelFormat.Rgba, PixelType.UnsignedByte, null);
			GraphicsDevice.GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, tex, 0);
			
		}

		public void Dispose()
		{
			GraphicsDevice.GL.DeleteFramebuffer(1);
		}
	}
}

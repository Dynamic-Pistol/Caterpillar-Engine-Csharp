using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Silk.NET.OpenGL;

namespace CaterpillarEngine.Rendering.Objects
{
	internal struct BufferObject<TElement> : IDisposable
		where TElement : unmanaged
	{
		private uint _handle;
		private BufferTargetARB _buffertype;

		public unsafe BufferObject(Span<TElement> data, BufferTargetARB buffertype)
		{
			_buffertype = buffertype;
			_handle = GraphicsDevice.GL.GenBuffer();
			GraphicsDevice.GL.BindBuffer(buffertype, _handle);
			fixed (void* i = data)
			{
				GraphicsDevice.GL.BufferData(buffertype, (nuint)(data.Length * sizeof(TElement)), i, BufferUsageARB.StaticDraw);
			}
		}

		public void Bind()
		{
			GraphicsDevice.GL.BindBuffer(_buffertype, _handle);
		}

		public void Dispose()
		{
			GraphicsDevice.GL.DeleteBuffer(_handle);
			GraphicsDevice.GL.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
		}
	}
}

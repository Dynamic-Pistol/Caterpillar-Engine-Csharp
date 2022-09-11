using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Silk.NET.OpenGL;

namespace CaterpillarEngine.Rendering.Objects
{
	internal class VertexArrayObject<TVertexType, TIndexType> : IDisposable
		where TVertexType : unmanaged
		where TIndexType : unmanaged
	{
		private uint _handle = 0;

		internal VertexArrayObject(BufferObject<TVertexType> vbo, BufferObject<TIndexType> ebo)
		{
			_handle = GraphicsDevice.GL.GenVertexArray();
			Bind();
			vbo.Bind();
			ebo.Bind();
		}

		public unsafe void VertexAttributePointer(uint index, int count, VertexAttribPointerType type, int offSet)
		{
			GraphicsDevice.GL.VertexAttribPointer(index, count, type, false,(uint)sizeof(TVertexType), (void*)(offSet * sizeof(TVertexType)));
			GraphicsDevice.GL.EnableVertexAttribArray(index);
		}

		public unsafe void VertexAttributePointer(uint index, int count, VertexAttribPointerType type, string fieldname)
		{
			GraphicsDevice.GL.VertexAttribPointer(index, count, type, true,(uint)sizeof(TVertexType), (void*)Marshal.OffsetOf<Vertex>(fieldname));
			GraphicsDevice.GL.EnableVertexAttribArray(index);
		}

		internal void Bind()
		{
			GraphicsDevice.GL.BindVertexArray(_handle);
		}

		public void Dispose()
		{
			GraphicsDevice.GL.DeleteVertexArray(_handle);
		}
	}
}

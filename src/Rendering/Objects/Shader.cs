using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Silk.NET.OpenGL;

namespace CaterpillarEngine.Rendering
{
	internal class Shader : IDisposable
	{
		internal uint _handle = 0;

		internal Shader(uint handle)
		{
			_handle = handle; 
		}

		public void Use()
		{
			GraphicsDevice.GL.UseProgram(_handle);
		}

		public void SetUniform(string name, int value)
		{
			int location = GraphicsDevice.GL.GetUniformLocation(_handle, name);
			if (location == -1)
			{
				throw new Exception($"{name} uniform not found on shader.");
			}
			GraphicsDevice.GL.Uniform1(location, value);
		}

		public unsafe void SetUniform(string name, Matrix4x4 value)
		{
			int location = GraphicsDevice.GL.GetUniformLocation(_handle, name);
			if (location == -1)
			{
				throw new Exception($"{name} uniform not found on shader.");
			}
			GraphicsDevice.GL.UniformMatrix4(location, 1, false, (float*)&value);
		}

		public unsafe void SetUniform(string name, System.Numerics.Vector3 value)
		{
			int location = GraphicsDevice.GL.GetUniformLocation(_handle, name);
			if (location == -1)
			{
				throw new Exception($"{name} uniform not found on shader.");
			}
			GraphicsDevice.GL.Uniform3(location,ref value);
		}

		public unsafe void SetUniform(string name, System.Numerics.Vector4 value)
		{
			int location = GraphicsDevice.GL.GetUniformLocation(_handle, name);
			if (location == -1)
			{
				throw new Exception($"{name} uniform not found on shader.");
			}
			GraphicsDevice.GL.Uniform4(location,ref value);
		}

		public void Dispose()
		{
			GraphicsDevice.GL.DeleteProgram(_handle);
		}
	}
}
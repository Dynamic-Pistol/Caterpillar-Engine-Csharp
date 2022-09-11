using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Silk.NET.OpenGL;
using CaterpillarEngine.Rendering.Objects;

namespace CaterpillarEngine.Rendering
{
	public static class GraphicsDevice
	{
		public static GL GL = default!;

		public static void ClearColor(System.Drawing.Color color)
		{
			GL.ClearColor(color);
		}

		public static void ClearColor(System.Numerics.Vector4 color)
		{
			GL.ClearColor(color.X,color.Y,color.Z,color.W);
		}

		internal static Shader GenShader(string filepath)
		{
			uint handle = GL.CreateProgram();
			uint LoadShader(ShaderType type, string @path)
			{
				string src = File.ReadAllText(@path);
				uint handle = GL.CreateShader(type);
				if (type == ShaderType.VertexShader)
					GL.ShaderSource(handle, "#version 330 \n #define VERTEX_PROGRAM" + src);
				else if (type == ShaderType.FragmentShader)
					GL.ShaderSource(handle, "#version 330 \n #define FRAGMENT_PROGRAM" + src);
				GL.CompileShader(handle);
				string infoLog = GL.GetShaderInfoLog(handle);
				if (!string.IsNullOrWhiteSpace(infoLog))
					throw new Exception($"Error compiling shader of type {type}, failed with error {infoLog}");
				return handle;
			}
			var vertshader = LoadShader(ShaderType.VertexShader,filepath);
			var fragshader = LoadShader(ShaderType.FragmentShader,filepath);
			GL.AttachShader(handle, vertshader);
			GL.AttachShader(handle, fragshader);
			GL.LinkProgram(handle);
			GL.GetProgram(handle, GLEnum.LinkStatus, out var status);
			if (status == 0) Console.WriteLine($"Error linking shader {GL.GetProgramInfoLog(handle)}");
			GL.DetachShader(handle, vertshader);
			GL.DetachShader(handle, fragshader);
			GL.DeleteShader(vertshader);
			GL.DeleteShader(fragshader);
			return new Shader(handle);
		}

		internal static void UseShader(Shader shader)
		{
			uint handle = shader._handle;
			GL.UseProgram(handle);
		}

		public static void ViewPort(Silk.NET.Maths.Vector2D<int> size) => GL.Viewport(size);

		public static void DisposeGL() => GL.Dispose();

	}
}

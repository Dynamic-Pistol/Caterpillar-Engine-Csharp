using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Silk.NET.OpenGL;
using CaterpillarEngine.Objects;

namespace CaterpillarEngine.Rendering.Objects
{
	public class Sprite : IDisposable
	{
		internal Shader shad = default!;
		internal VertexArrayObject<float,uint> vao = default!;
		internal BufferObject<float> vbo = default!;
		internal BufferObject<uint> ebo = default!;
		internal Texture tex = default!;
		private readonly float[] Vertices =
		{
            //X    Y      Z     U   V
             0.5f,  0.5f, 0.0f, 1f, 0f,
			 0.5f, -0.5f, 0.0f, 1f, 1f,
			-0.5f, -0.5f, 0.0f, 0f, 1f,
			-0.5f,  0.5f, 0.0f, 0f, 0f
		};
		private readonly uint[] Indices =
		{
			0, 1, 3,
			1, 2, 3
		};
		
		public unsafe Sprite(Image<Rgba32> img)
		{
			Init(GraphicsDevice.GL, img);
		}

		public unsafe Sprite(string @imagepath)
		{
			var image = RenderLoader.LoadImage(imagepath);
			Init(GraphicsDevice.GL, image);
		}

		public void Init(GL gl,Image<Rgba32> img)
		{
			ebo = new(Indices, BufferTargetARB.ElementArrayBuffer);
			vbo = new(Vertices, BufferTargetARB.ArrayBuffer);
			vao = new(vbo, ebo);
			shad = GraphicsDevice.GenShader(@"Assets\Shaders\Sprite.glsl");
			vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 0);
			vao.VertexAttributePointer(1, 2, VertexAttribPointerType.Float, 3);
			tex = new(img);
		}

		public unsafe void Render(Transform transform,Camera camera)
		{
			vao.Bind();
			tex.Bind();
			GraphicsDevice.UseShader(shad);
			var view = Matrix4x4.CreateLookAt(camera.transform.position, camera.CamDir, camera.CamUp);
			var projection = Matrix4x4.CreatePerspectiveFieldOfView(Math.Math.Deg2Rad * camera.FOV, camera.AspectRatio, camera.NearPlaneDistance, camera.FarPlaneDistance);
			shad.SetUniform("uTexture0",0);
			shad.SetUniform("uModel", transform.ViewMatrix);
			shad.SetUniform("uView", view);
			shad.SetUniform("uProjection", projection);
			GraphicsDevice.GL.DrawElements(PrimitiveType.Triangles, (uint)Indices.Length, DrawElementsType.UnsignedInt, null);
		}

		public void Dispose()
		{
			vao.Dispose();
			tex.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}

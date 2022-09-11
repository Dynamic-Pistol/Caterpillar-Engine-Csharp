using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using CaterpillarEngine.Objects;
using Silk.NET.OpenGL;
using System.Runtime.InteropServices;

namespace CaterpillarEngine.Rendering.Objects
{
	public class CatMesh : IDisposable
	{
		private Vertex[] vertices;
		private uint[] indices;
		private BufferObject<uint> ebo = default!;
		private BufferObject<Vertex> vbo = default!;
		private VertexArrayObject<Vertex, uint> vao = default!;
		private Shader shad = default!;
		private Texture tex = default!;


		public unsafe CatMesh(Assimp.Mesh mesh, string path)
		{
			indices = mesh.GetUnsignedIndices();
			vertices = new Vertex[mesh.VertexCount];
			for (int i = 0; i < vertices.Length; i++)
			{
				vertices[i].Position = new(mesh.Vertices[i].X, mesh.Vertices[i].Y, mesh.Vertices[i].Z);
				vertices[i].Texcoords = new(mesh.TextureCoordinateChannels[0][i].X, mesh.TextureCoordinateChannels[0][i].Y);
			}
			ebo = new(indices, BufferTargetARB.ElementArrayBuffer);
			vbo = new(vertices, BufferTargetARB.ArrayBuffer);
			vao = new(vbo, ebo);
			shad = GraphicsDevice.GenShader(@"Assets\Shaders\Mesh.glsl");
			vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 0);
			vao.VertexAttributePointer(1, 2, VertexAttribPointerType.Float, "Texcoords");
			var img = RenderLoader.LoadImage(path);
			tex = new Texture(img);
		}

		public unsafe CatMesh(Vertex[] vertices,uint[] indices,string path)
		{
			this.vertices = vertices;
			this.indices = indices;
			ebo = new(indices, BufferTargetARB.ElementArrayBuffer);
			vbo = new(vertices, BufferTargetARB.ArrayBuffer);
			vao = new(vbo, ebo);
			shad = GraphicsDevice.GenShader(@"Assets\Shaders\Mesh.glsl");
			vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 0);
			vao.VertexAttributePointer(1, 2, VertexAttribPointerType.Float, "Texcoords");
			var img = RenderLoader.LoadImage(path);
			tex = new Texture(img);
		}

		public unsafe void Render(Transform transform,Camera cam,Vector4 color)
		{
			vao.Bind();
			tex.Bind();
			GraphicsDevice.UseShader(shad);shad.SetUniform("uModel", transform.ViewMatrix); 
			shad.SetUniform("uTexture0", 0);
			shad.SetUniform("uView", cam.view);
			shad.SetUniform("uProjection", cam.PrespectiveFOV);
			shad.SetUniform("uColor", color);
			GraphicsDevice.GL.DrawElements(PrimitiveType.Triangles, (uint)indices.Length, DrawElementsType.UnsignedInt,null);
			//GraphicsDevice.GL.DrawArrays(PrimitiveType.Triangles, 0, (uint)vertices.Length);
		}

		public void Dispose()
		{
			vao.Dispose();
			vbo.Dispose();
			ebo.Dispose();
			shad.Dispose();
			tex.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}

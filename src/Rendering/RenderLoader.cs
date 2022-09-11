using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using ImageData = Silk.NET.Core.RawImage;
using Silk.NET.OpenGL;
using Assimp;
using CaterpillarEngine.Rendering.Objects;

namespace CaterpillarEngine.Rendering
{
	public static class RenderLoader
	{
		private static AssimpContext importer = new();

		public static ImageData LoadRawImage(string filepath)
		{
			Image<Rgba32> image = Image.Load<Rgba32>(@filepath);
			var pixels = new byte[4 * image.Width * image.Height];
			image.CopyPixelDataTo(pixels);
			ImageData imageData = new ImageData(image.Width, image.Height, pixels);
			return imageData;
		}

		public static Image<Rgba32> LoadImage(string filepath)
		{
			return Image.Load<Rgba32>(@filepath);
		}

		public static Objects.Texture LoadTextureFlipped(string filepath)
		{
			var img = Image.Load<Rgba32>(@filepath);
			img.Mutate(x => x.Flip(FlipMode.Horizontal));
			img.Mutate(x => x.Flip(FlipMode.Vertical));
			return new Objects.Texture(img);
		}

		public static Sprite LoadSprite(string filepath)
		{
			var img = LoadImage(filepath);
			return new(img);
		}

		public static Sprite LoadSprite(Image<Rgba32> img)
		{
			return new(img);
		}

		public static Mesh LoadMesh(string filepath)
		{
			Scene scene = importer.ImportFile(filepath, PostProcessSteps.Triangulate | PostProcessSteps.FlipUVs);
			return scene.Meshes[0];
		}

		public static EmbeddedTexture LoadAssimpTexture(string filepath)
		{
			Scene scene = importer.ImportFile(filepath);
			return scene.Textures[0];
		}

		public static CatMesh LoadCatMesh(string meshfilepath,string texturefilepath)
		{
			Mesh mesh = LoadMesh(meshfilepath);
			return new CatMesh(mesh,texturefilepath);
		}
	}
}

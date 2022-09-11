using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkyNvg;
using SilkyNvg.Rendering.OpenGL;

namespace CaterpillarEngine.src.Rendering
{
	public static class DrawingDevice
	{
		public static Nvg nvg = null!;

		public static void SetNvg(Silk.NET.OpenGL.GL gl)
		{
			OpenGLRenderer gLRenderer = new OpenGLRenderer(CreateFlags.Antialias | CreateFlags.StencilStrokes | CreateFlags.Debug, gl);
			nvg = Nvg.Create(gLRenderer);
		}

		public static void DrawText()
		{
			
		}

	}
}

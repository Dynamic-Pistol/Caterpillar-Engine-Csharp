using System.Text;
using System.Drawing;
using System.Numerics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Formats.Png;
using Silk.NET.Core;
using Silk.NET.Input;
using Silk.NET.Input.Extensions;
using Silk.NET.OpenGL;
using Silk.NET.OpenGL.Extensions.ImGui;
using Silk.NET.Windowing;
using ImGuizmoNET;
using SilkyNvg;
using SilkyNvg.Graphics;
using SilkyNvg.Rendering.OpenGL;
using SilkyNvg.Text;
using SilkyNvg.Paths;
using CaterpillarEngine.Rendering;
using CaterpillarEngine.Objects;
using CaterpillarEngine.Math;
using CaterpillarEngine.Inputhandling;

namespace CaterpillarEngine
{
	public abstract class MainWindow
	{
		#region Variables
		#region General		
		private static IWindow _mainWindow = default!;
		private static ImGuiController _controller = default!;
		#endregion
		#region Window Info
		/// <summary>
		/// The Width of the Window
		/// </summary>
		public static int Width => _mainWindow.Size.X;
		/// <summary>
		/// The Height of the Window
		/// </summary>
		public static int Height => _mainWindow.Size.Y;
		/// <summary>
		/// The Frames Per Second of the Window
		/// </summary>
		public static double FPS => _mainWindow.FramesPerSecond;
		/// <summary>
		/// Is Vsync Enabled?
		/// </summary>
		public static bool Vsync => _mainWindow.VSync;
		/// <summary>
		/// The Graphics Api of the Window
		/// </summary>
		public static GraphicsAPI API => _mainWindow.API;
		#endregion
		#region Editor
		protected Action<double> RenderImGuiWindows = default!;
		#endregion
		#region Testing
		protected Action<double> RenderMeshes = default!;
		private Nvg nvg = default!;
		#endregion
		#endregion

		public MainWindow(Vector2 size,string title,bool Borderless,GAPI api)
		{
			GraphicsAPI GetAPIFromEnum(GAPI gapi)
			{
				return gapi switch
				{
					GAPI.None => GraphicsAPI.None,
					GAPI.Vulkan => GraphicsAPI.DefaultVulkan,
					GAPI.OpenGl => GraphicsAPI.Default,
					_ => GraphicsAPI.Default,
				};
			}

			WindowOptions options = new()
			{
				API = GetAPIFromEnum(api),
				Size = new((int)size.X, (int)size.Y),
				Title = title,
				VideoMode = VideoMode.Default,
				IsVisible = true,
				WindowBorder = Borderless ? WindowBorder.Hidden : WindowBorder.Fixed,
				PreferredDepthBufferBits = 24,
				ShouldSwapAutomatically = true,
			};
			_mainWindow = Window.Create(options);
			_mainWindow.FileDrop += OnFileDrop;
			_mainWindow.Load += OnLoad;
			_mainWindow.Update += OnUpdate;
			_mainWindow.Render += OnRender;
			_mainWindow.Resize += (Silk.NET.Maths.Vector2D<int> obj) => GraphicsDevice.ViewPort(obj);
			_mainWindow.Closing += OnClosing;
			
		}

		protected virtual void OnFileDrop(string[] droppedfiles)
		{
			foreach (var file in droppedfiles)
			{
				if (file.EndsWith(".png"))
				{
					string filename = Path.GetFileName(file);
					Console.Write(file);
					Image<Rgba32> image = Image<Rgba32>.Load<Rgba32>(file);
					image.SaveAsPng(@$"Assets\{filename}");
				}
			}
		}

		public void Run() => _mainWindow.Run();
		public void Close() => _mainWindow.Close();

		protected virtual void OnLoad()
		{
			var icon = RenderLoader.LoadRawImage(@"Assets\Sprites\csharp.png");
			_mainWindow.SetWindowIcon(ref icon);
			_mainWindow.Center();
			_controller = new
				(
					GraphicsDevice.GL = _mainWindow.CreateOpenGL(),
					_mainWindow,
					Input.inputContext = _mainWindow.CreateInput()
				);
			OpenGLRenderer gLRenderer = new OpenGLRenderer(CreateFlags.Antialias, GraphicsDevice.GL);
			nvg = Nvg.Create(gLRenderer);
			GraphicsDevice.GL.Enable(EnableCap.DepthTest);
		}

		protected virtual void OnUpdate(double obj)
		{
			if (ImGuiNET.ImGui.IsKeyPressed(ImGuiNET.ImGuiKey.Escape)) Close();
		}

		protected virtual void OnRender(double obj)
		{
			_controller.Update((float)obj);
			GraphicsDevice.GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			RenderMeshes(obj);
			nvg.BeginFrame(Width, Height, 1);
			nvg.BeginPath();
			nvg.TextAlign(Align.Centre);
			nvg.FontSize(15);
			nvg.FontFace("sans");
			nvg.FillColour(new(1, 1, 0, 1));
			_ = nvg.Text(Width / 2, Height / 2, "Hi!");
			nvg.EndFrame();
			if (!_mainWindow.IsClosing)
			{
				_controller.Render();
			};
		}


		protected virtual void OnClosing()
		{
			GraphicsDevice.DisposeGL();
			_controller.Dispose();
			nvg.Dispose();
		}
	}
}

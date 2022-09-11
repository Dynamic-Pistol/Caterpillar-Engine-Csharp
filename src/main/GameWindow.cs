using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Silk.NET.Input;
using CaterpillarEngine;
using CaterpillarEngine.Objects;
using CaterpillarEngine.Rendering;
using CaterpillarEngine.Rendering.Objects;
using CaterpillarEngine.Inputhandling;

namespace CaterpillarEngine
{
	public class GameWindow : MainWindow
	{
		private GameObject testcam = new("Main Camera");
		private GameObject meshrendtest = new("MeshRendTest");
		private GameObject meshrendtest1 = new("MeshRendTest 2");
		private GameObject ballrendtest = new("Ball");
		private Vector3 CamMoveDir = Vector3.Zero;

		public GameWindow(Vector2 size, string title, bool Borderless, GAPI api) : base(size, title, Borderless, api)
		{RenderMeshes = new Action<double>(t => { SceneManager.OnRender(t); });
		}

		protected override void OnLoad()
		{
			base.OnLoad();
			Vertex[] vertices =
			{
				new(-1,-1,-1, 0, 0),
				new( 1,-1,-1, 1, 0),
				new( 1, 1,-1, 1, 1),
				new( 1, 1,-1, 1, 1),
				new(-1, 1,-1, 0, 1),
				new(-1,-1,-1, 0, 0),

				new(-1,-1, 1, 0, 0),
				new( 1,-1, 1, 1, 0),
				new( 1, 1, 1, 1, 1),
				new( 1, 1, 1, 1, 1),
				new(-1, 1, 1, 0, 1),
				new(-1,-1, 1, 0, 0),

				new(-1, 1, 1, 1, 0),
				new(-1, 1,-1, 1, 1),
				new(-1,-1,-1, 0, 1),
				new(-1,-1,-1, 0, 1),
				new(-1,-1, 1, 0, 0),
				new(-1, 1, 1, 1, 0),

				new( 1, 1, 1, 1, 0),
				new( 1, 1,-1, 1, 1),
				new( 1,-1,-1, 0, 1),
				new( 1,-1,-1, 0, 1),
				new( 1,-1, 1, 0, 0),
				new( 1, 1, 1, 1, 0),

				new(-1,-1,-1, 0, 1),
				new( 1,-1,-1, 1, 1),
				new( 1,-1, 1, 1, 0),
				new( 1,-1, 1, 1, 0),
				new(-1,-1, 1, 0, 0),
				new(-1,-1,-1, 0, 1),

				new(-1, 1,-1, 0, 1),
				new( 1, 1,-1, 1, 1),
				new( 1, 1, 1, 1, 0),
				new( 1, 1, 1, 1, 0),
				new(-1, 1, 1, 0, 0),
				new(-1, 1,-1, 0, 1),
			};
			SceneManager.AddScene(new CatScene("Test Scene", testcam, meshrendtest,meshrendtest1,ballrendtest));
			Camera cam = testcam.AddComponent<Camera>();
			//CatMesh catMesh = new CatMesh(vertices,Array.Empty<uint>(),@"Assets\Sprites\awesomeface.png");
			//CatMesh catMesh1 = new CatMesh(vertices,Array.Empty<uint>(),@"Assets\Sprites\spoopyawesomeface.png");
			CatMesh catMesh = RenderLoader.LoadCatMesh(@"Assets\Meshes\cube.fbx", @"Assets\Sprites\awesomeface.png");
			CatMesh catMesh1 = RenderLoader.LoadCatMesh(@"Assets\Meshes\cube.fbx", @"Assets\Sprites\awesomeface.png");
			CatMesh catMesh2 = RenderLoader.LoadCatMesh(@"Assets\Meshes\ball.fbx", @"Assets\Sprites\spoopyawesomeface.png");
			meshrendtest.AddComponent<MeshRenderer>().mesh = catMesh;
			meshrendtest.GetComponent<MeshRenderer>().cam = cam;
			meshrendtest1.AddComponent<MeshRenderer>().mesh = catMesh1;
			meshrendtest1.GetComponent<MeshRenderer>().cam = cam;
			ballrendtest.AddComponent<MeshRenderer>().mesh = catMesh2;
			ballrendtest.GetComponent<MeshRenderer>().cam = cam;
			testcam.transform.position.Z = -6;
			ballrendtest.transform.position.X = -3;
			meshrendtest1.transform.position.X = -5;
			SceneManager.OnLoad();

		}
		protected override void OnRender(double time)
		{
			base.OnRender(time);
		}

		protected override void OnUpdate(double obj)
		{
			base.OnUpdate(obj);
			if (Input.IsKeyPressed(Key.W))
			{
				CamMoveDir.Y += 3 * (float)obj;
			}
			if (Input.IsKeyPressed(Key.S))
			{
				CamMoveDir.Y -= 3 * (float)obj;
			}
			if (Input.IsKeyPressed(Key.A))
			{
				CamMoveDir.X += 3 * (float)obj;
			}
			if (Input.IsKeyPressed(Key.D))
			{
				CamMoveDir.X -= 3 * (float)obj;
			}
			CamMoveDir.Z += Input.ScrollDelta();
			CamMoveDir.Normalize();
			testcam.transform.position = CamMoveDir;
			Console.Write(testcam.transform.position + "\n");
		}

		protected override void OnClosing()
		{
			base.OnClosing();
		}
	}
}

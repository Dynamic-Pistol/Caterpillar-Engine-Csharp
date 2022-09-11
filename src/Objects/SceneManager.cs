using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterpillarEngine.Objects;

namespace CaterpillarEngine
{
	public static class SceneManager
	{
		private static CatScene activescene = default!;
		private static List<CatScene> sceneList = new List<CatScene>();

		public static ref CatScene GetActiveScene()
		{
			return ref activescene;
		}

		public static void AddScene(CatScene scene)
		{
			sceneList.Add(scene);
			if (sceneList.Count == 1)
				activescene = scene;
		}

		public static void LoadScene(int index)
		{
			ref CatScene currentactivescene = ref GetActiveScene(); 
			sceneList.ForEach(x => x.isactive = false);
			try
			{
				sceneList[index].isactive = true;
				activescene = sceneList[index];
			}
			catch (ArgumentOutOfRangeException)
			{
				Console.WriteLine("Scene Index out of Reach\n");
				currentactivescene.isactive = true;
			}
		}

		public static void LoadScene(string name)
		{
			sceneList.ForEach(x => x.isactive = false);
			try
			{
				CatScene loadedscene = sceneList.Find(x => x.name == name)!;
				loadedscene!.isactive = true;
				activescene = loadedscene;
			}
			catch (ArgumentNullException)
			{
				Console.WriteLine("Scene doesn't exist");
			}
		}

		public static void OnLoad() => activescene.OnLoad();
		public static void OnUpdate(double time) => activescene.Update(time);
		public static void OnRender(double time) => activescene.Render(time);
		public static void OnClosing() => activescene.OnClosing();
	}
}

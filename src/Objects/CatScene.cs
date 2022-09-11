using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterpillarEngine.Objects
{
	public sealed class CatScene
	{
		private List<GameObject> gameobjects = new List<GameObject>();
		public string name { get; init; }
		public bool isactive { get; set; }
		public void OnLoad() => gameobjects.ForEach(x => x.OnLoad());
		public void Update(double time) => gameobjects.ForEach(x => x.Update(time));
		public void Render(double time) => gameobjects.ForEach(x => x.Render(time));
		public void OnClosing() => gameobjects.ForEach(x => x.OnClosing());

		public CatScene(string name,params GameObject[] paramgameobjects)
		{
			this.name = name;
			foreach (GameObject go in paramgameobjects)
			{
				gameobjects.Add(go);
			}
		}

		public void AddGameObject(GameObject gameobject)
		{
			gameobjects.Add(gameobject);
		}

		public GameObject[] GetGameObjects()
		{
			return gameobjects.ToArray();
		}

		public Camera? GetCamera()
		{
			return gameobjects.Find(x => x.Name == "Main Camera")?.GetComponent<Camera>();
		}
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ImGuiNET;

namespace CaterpillarEngine.Objects
{
	public sealed class GameObject : IDisposable
	{
		#region Variables
		public string Name;
		public List<string> Tags = new List<string>() {"Default"};
		public bool IsActive { get; set; }
		public int ID { get; private init; } = new Random().Next(int.MaxValue);
		private readonly List<Component> components;
		public Transform transform { get; init; }
		private List<GameObject> children = new List<GameObject>();
		internal GameObject? parent;
		#endregion

		#region Constructors
		public GameObject(string name, List<string> tags,params Component[] componentstoadd)
		{
			Name = name;
			Tags = tags;
			components = new List<Component>();
			transform = AddComponent<Transform>();
			for (int i = 0; i < componentstoadd.Length; i++)
			{
				AddComponent(ref componentstoadd[i]);
			}
		}

		public GameObject(string name, List<string> tags)
		{
			Name = name;
			Tags = tags;
			components = new List<Component>();
			transform = AddComponent<Transform>();
		}

		public GameObject(string name)
		{
			Name = name;
			components = new List<Component>();
			transform = AddComponent<Transform>();
		}

		public GameObject()
		{
			Name = "New GameObject";
			components = new List<Component>();
			transform = AddComponent<Transform>();
		}
		#endregion

		#region Component Management

		public TComponent AddComponent<TComponent>() where TComponent : Component, new()
		{
			if (typeof(TComponent) == typeof(Transform) && components.Contains(transform))
				throw new Exception("GameObject already has a Transform component");

			Component component = new TComponent()
			{
				transform = this.transform,
				gameObject = this
			};
			components.Add(component);
			return (TComponent)component;
		}

		public void AddComponent(ref Component component)
		{
			components.Add(component);
		}

		public TComponent GetComponent<TComponent>() where TComponent : Component
		{
			var component = components.Find(x => x.GetType() == typeof(TComponent));
			if (component is not null)
				return (TComponent)component;
			else
				throw new Exception("Gameobject doesn't have this component");
		}

		public bool TryGetComponent<TComponent>(out TComponent component) where TComponent : Component
		{
			component = GetComponent<TComponent>();
			if (component is null)
				return false;
			else
				return true;
		}

		public bool HasComponent<TComponent>() where TComponent: Component
		{
			return TryGetComponent<TComponent>(out TComponent clear);
		}

		public bool HasComponent(ref Component component)
		{
			return components.Contains(component);
		}

		public void RemoveComponent<TComponent>() where TComponent : Component
		{
			if (HasComponent<TComponent>())
			{
				components.Remove(GetComponent<TComponent>());
			}
			else
			{
				Console.WriteLine("Component Doesn't Exist");
			}
		}

		public void RemoveComponent(ref Component component)
		{
			components.Remove(component);
		}

		public int ComponentsLength()
		{
			return components.Count();
		}

		#endregion

		#region Children Management

		public void AddChild(GameObject child)
		{
			child.parent = this;
			children.Add(child);
		}

		public GameObject GetChildByName(string name)
		{
			var child = children.Find(x => x.Name == name);
			if (child is not null)
				return child;
			else
				throw new Exception("Couldn't find child");
		}

		public GameObject GetChildByTag(string tag)
		{
			GameObject? child = null;
			foreach (var cd in children)
			{
				foreach (var stringtag in cd.Tags)
				{
					if (stringtag == tag) child = cd;
					break;
				}
			}
			if (child is not null)
				return child;
			else
				throw new Exception("Couldn't find child");
		}

		public bool HasChildren()
		{
			return children.Count != 0;
		}

		public void RemoveChild(ref GameObject child)
		{
			child.parent = null;
			children.Remove(child);
		}

		#endregion

		#region GameObject Logic

		public static GameObject? GetGameObject(string name)
		{
			return SceneManager.GetActiveScene().GetGameObjects().ToList().Find(x => x.Name == name);
		}

		public static GameObject? GetGameObjectTag(string tag)
		{
			foreach (GameObject go in SceneManager.GetActiveScene().GetGameObjects())
			{
				if (go.Tags.Contains(tag))
				{
					return go;
				}
			}
			return null;
		}
		#endregion

		#region Functions

		internal void OnLoad() => components.ForEach(x => x.OnLoad());

		internal void Update(double time) => components.ForEach(x => x.Update(time));

		internal void Render(double time) => components.ForEach(x => x.Render(time));

		internal void OnClosing() => components.ForEach(x => x.OnClosing());
		#endregion

		#region Misc
		public void Dispose()
		{
			Tags.Clear();
			components.Clear();
			GC.SuppressFinalize(this);
		}

		public List<Component>.Enumerator GetEnumerator()
		{
			return components.GetEnumerator();
		}

		#endregion
	}
}

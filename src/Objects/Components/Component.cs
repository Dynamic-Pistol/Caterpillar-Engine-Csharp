using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterpillarEngine.Objects
{
	public abstract class Component
	{
		public GameObject gameObject { get; init; } = default!;
		public Transform transform { get; init; } = default!;
		public int ID { get; private init; } = new Random().Next(int.MaxValue);
		private event Action OnCreation = default!;

		internal Component()
		{
			OnCreation += OnCreate;
			OnCreation?.Invoke();
		}

		~Component()
		{
			OnCreation -= OnCreate;
		}

		protected virtual void OnCreate()
		{

		}

		public virtual void OnLoad()
		{

		}

		public virtual void Update(double deltatime)
		{

		}

		public virtual void Render(double deltatime)
		{

		}

		public virtual void OnClosing()
		{

		}

		public abstract void ImGuiInspectorValues();
	}
}

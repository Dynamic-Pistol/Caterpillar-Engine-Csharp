using System.Numerics;
using ImGuiNET;
using CaterpillarEngine.Rendering;
using CaterpillarEngine.Rendering.Objects;

namespace CaterpillarEngine.Objects
{
	public sealed class SpriteRenderer : Component
	{
		public Sprite? sprite;
		private Camera? camera;

		protected override void OnCreate()
		{
			sprite = RenderLoader.LoadSprite(@"Assets\Sprites\csharp.png");
			camera = GameObject.GetGameObject("Camera")!.GetComponent<Camera>();
		}

		public override void Render(double deltatime)
		{
			if (camera is not null)
				sprite?.Render(transform, camera);
		}

		public override void OnClosing()
		{
			sprite?.Dispose();
		}

		public override void ImGuiInspectorValues()
		{
			if (ImGui.TreeNodeEx("Sprite Renderer"))
			{
				if (ImGui.BeginPopup($"Delete Component##SpriteRenderer{ID}"))
				{
					gameObject.RemoveComponent<SpriteRenderer>();
					ImGui.EndPopup();
				}
				if (ImGui.Button("Sprite"))
				{
					if (ImGui.BeginDragDropTarget())
					{
						
					}
				}
				ImGui.TreePop();
			}
			ImGui.Separator();
		}
	}
}

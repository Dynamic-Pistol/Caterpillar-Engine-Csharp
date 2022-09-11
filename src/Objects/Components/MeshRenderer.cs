using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using ImGuiNET;
using CaterpillarEngine.Rendering;
using CaterpillarEngine.Rendering.Objects;

namespace CaterpillarEngine.Objects
{
	public sealed class MeshRenderer : Component
	{
		public CatMesh? mesh;
		public Camera? cam;
		public Vector4 color = Vector4.Zero;

		public override void Render(double deltatime)
		{
			if (cam is not null)
				mesh?.Render(transform,cam,color);
		}

		public override void OnClosing()
		{
			mesh?.Dispose();
		}

		public override void ImGuiInspectorValues()
		{
			if (ImGui.TreeNodeEx("Mesh renderer"))
			{
				ImGui.Text($"Color:");
				ImGui.SameLine();
				ImGui.ColorEdit4($"##Color{ID}",ref color);
				ImGui.TreePop();
			}
			ImGui.Separator();
		}
	}
}

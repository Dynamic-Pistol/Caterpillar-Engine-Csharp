using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using ImGuiNET;
using ImGuizmoNET;
using CaterpillarEngine;

namespace CaterpillarEngine.Objects
{
	public sealed class Transform : Component
	{
		public Vector3 position = Vector3.Zero;
		public Vector3 scale = Vector3.One;
		public Vector3 eularangles = Vector3.Zero;
		public Matrix4x4 ViewMatrix => Matrix4x4.Identity * Matrix4x4.CreateFromYawPitchRoll(eularangles.X,eularangles.Y,eularangles.Z) * Matrix4x4.CreateScale(scale) * Matrix4x4.CreateTranslation(position);


		public override void ImGuiInspectorValues()
		{
			if (ImGui.TreeNodeEx("Transform"))
			{
				ImGui.Text("Position:");
				ImGui.SameLine();
				ImGui.PushItemWidth(100);
				ImGui.DragFloat3("##Position", ref position, 0.01f, float.MinValue, float.MaxValue, "%.2f");
				ImGui.SameLine();
				if (ImGui.SmallButton($"R##pos{ID}"))
					position = Vector3.Zero;
				ImGui.Text("Scale:   ");
				ImGui.SameLine();
				ImGui.DragFloat3("##Scale", ref scale, 0.01f, float.MinValue, float.MaxValue, "%.2f");
				ImGui.SameLine();
				if (ImGui.SmallButton($"R##size{ID}"))
					scale = Vector3.One;
				ImGui.Text("Rotation:");
				ImGui.SameLine();
				ImGui.DragFloat3("##Rotation", ref eularangles, 0.01f, float.MinValue, float.MaxValue, "%.2f");
				ImGui.SameLine();
				if (ImGui.SmallButton($"R##rot{ID}"))
					eularangles = Vector3.Zero;
				ImGui.PopItemWidth();
				ImGui.TreePop();
			}
			ImGui.Separator();
		}
	}
}
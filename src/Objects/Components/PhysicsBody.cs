using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using ImGuiNET;
using BepuPhysics;

namespace CaterpillarEngine.Objects
{
	public class PhysicsBody : Component
	{
		private enum BodyType
		{
			Rigid,
			Kinematic,
			Static
		}
		private bool IsTrigger;
		private float Mass;
		private Vector3 Gravity;
		private BodyType PhysicsBodyType;

		public override void ImGuiInspectorValues()
		{
			if (ImGui.TreeNodeEx($"PhysicsBody##{ID}"))
			{
				ImGui.Text("IsTrigger:");
				ImGui.SameLine();
				ImGui.Checkbox($"##Trigger{ID}", ref IsTrigger);
				ImGui.Text("Mass:");
				ImGui.SameLine();
				ImGui.DragFloat($"##Mass{ID}", ref Mass);
				ImGui.Text("Gravity:");
				ImGui.SameLine();
				ImGui.DragFloat3($"##Gravity{ID}", ref Gravity);
				ImGui.Text("BodyType:");
				ImGui.SameLine();
				if (ImGui.BeginCombo("##BodyType",$"{PhysicsBodyType}"))
				{
					for (int i = 0; i < Enum.GetValues(typeof(BodyType)).Length; i++)
					{
						bool _isselected = (PhysicsBodyType == Enum.GetValues(typeof(BodyType)).Cast<BodyType>().ToList()[i]);
						if (ImGui.Selectable($"{Enum.GetNames(typeof(BodyType))[i]}",_isselected))
							PhysicsBodyType = Enum.GetValues(typeof(BodyType)).Cast<BodyType>().ToList()[i];
						if (_isselected)
							ImGui.SetItemDefaultFocus();
					}
					ImGui.EndCombo();
				}

				ImGui.TreePop();
			}
		}
	}
}

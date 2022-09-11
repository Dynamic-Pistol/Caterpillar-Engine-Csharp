using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using ImGuiNET;
using static CaterpillarEngine.Rendering.GraphicsDevice;

namespace CaterpillarEngine.Objects
{
	public class Camera : Component
	{
		public readonly Vector3 CamUp = Vector3.UnitY;
		public Vector3 CamDir { get; private set; } = new Vector3(0,0,1);
		public float FOV = 45;
		public float AspectRatio = (float)GameWindow.Width / GameWindow.Height;
		public float NearPlaneDistance = 0.1f;
		public float FarPlaneDistance = 100f;
		private Vector4 backgroundcolor = new Vector4(0,1,1,1);
		public Matrix4x4 view => Matrix4x4.CreateTranslation(transform.position);
		public Matrix4x4 Perspective => Matrix4x4.CreatePerspective(GameWindow.Width, GameWindow.Height, NearPlaneDistance, FarPlaneDistance);
		public Matrix4x4 PrespectiveFOV => Matrix4x4.CreatePerspectiveFieldOfView(FOV * Math.Math.Deg2Rad, AspectRatio, NearPlaneDistance, FarPlaneDistance);
		

		public override void Render(double deltatime)
		{
			ClearColor(backgroundcolor);
		}

		public override void ImGuiInspectorValues()
		{
			if (ImGui.TreeNodeEx($"Camera##{ID}"))
			{
				ImGui.Text("AspectRatio");
				ImGui.SameLine();
				ImGui.DragFloat($"##AspectRation{ID}",ref AspectRatio,0.01f,1,float.MaxValue,"%.2f");
				ImGui.Text("FOV");
				ImGui.SameLine();
				ImGui.DragFloat($"##FOV{ID}",ref FOV,0.01f,1,float.MaxValue,"%.2f");
				ImGui.Text("Color");
				ImGui.SameLine();
				ImGui.ColorEdit4($"##{ID}",ref backgroundcolor);
				ImGui.TreePop();
			}
			ImGui.Separator();
		}
	}
}

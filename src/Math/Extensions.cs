using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Math;
using static System.MathF;
using CaterpillarEngine.Math;

namespace CaterpillarEngine
{
	public static class Extensions
	{

		public static Vector3 ToEulerAngles(this Quaternion quat)
		{
			Vector3 angles = new Vector3();
			float sinyaw = 2 * (quat.W * quat.X + quat.Y * quat.Z);
			float cosyaw = 1 - 2 * (quat.X * quat.X + quat.Y * quat.Y);
			angles.X = Atan2(sinyaw, cosyaw);

			float sinpitch = 2 * (quat.W * quat.Y + quat.Z * quat.X);
			if (MathF.Abs(sinpitch) >= 1)
				angles.Y = CopySign(CaterpillarEngine.Math.Math.PI / 2, sinpitch);
			else
				angles.Y = Asin(sinpitch);

			float sinroll = 2 * (quat.W * quat.Z + quat.X * quat.Y);
			float cosroll = 1 - 2 * (quat.Y * quat.Y + quat.Z * quat.Z);
			angles.Z = Atan2(sinroll, cosroll);

			return angles;
		}

		public static Vector3 Normalize(this ref Vector3 vector3)
		{
			return Vector3.Normalize(vector3);
		}

	}
}
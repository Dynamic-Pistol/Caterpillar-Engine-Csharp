using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using static System.Math;
using static System.MathF;
using Silk.NET.Maths;

namespace CaterpillarEngine.Math
{
	public static class Math
	{
		public const float PI = MathF.PI;
		public const float E = MathF.E;
		public const float Tau = MathF.Tau;
		public const float Deg2Rad = PI / 180;
		public const float Rad2Deg = 360 / (PI * 2);
		
		public static float Clamp(float value,float minium,float maxmium)
		{
			return System.Math.Clamp(value, minium, maxmium);
		}

		public static float Vector2toAngle(Vector2 origin,Vector2 target)
		{
			Vector2 lookdir = target - origin;
			float angle = Atan2(lookdir.Y, lookdir.X) * Rad2Deg;
			return angle;
		}
	}
}

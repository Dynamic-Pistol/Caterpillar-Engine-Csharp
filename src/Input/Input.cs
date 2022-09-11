using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Silk.NET.Input;

namespace CaterpillarEngine.Inputhandling
{
	public static class Input
	{
		public static IInputContext inputContext = null!;

		public static bool IsKeyPressed(Key key)
		{
			return inputContext.Keyboards[0].IsKeyPressed(key);
		}

		public static float ScrollDelta()
		{
			return inputContext.Mice[0].ScrollWheels[0].Y;
		}
	}
}

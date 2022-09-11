using System.Numerics;

namespace CaterpillarEngine.Rendering
{
	public struct Vertex
	{
		public Vector3 Position;
		public Vector2 Texcoords;

		public Vertex(float posx,float posy,float posz,float texx,float texy)
		{
			Position = new Vector3(posx, posy, posz);
			Texcoords = new Vector2(texx, texy);
		}
	}
}

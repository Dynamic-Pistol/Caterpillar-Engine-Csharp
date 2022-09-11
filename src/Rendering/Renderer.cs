using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterpillarEngine.Rendering
{
	public class Renderer
	{
		private const int Maxbatches = 10000;
		private List<RenderBatch> batches = new List<RenderBatch>();

		public void AddBatch(RenderBatch batch)
		{
			batches.Add(batch);
		}

	}
}

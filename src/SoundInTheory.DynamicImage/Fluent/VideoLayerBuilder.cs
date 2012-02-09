using System;
using SoundInTheory.DynamicImage.Layers;

namespace SoundInTheory.DynamicImage.Fluent
{
	public class VideoLayerBuilder : BaseLayerBuilder<VideoLayer, VideoLayerBuilder>
	{
		public VideoLayerBuilder SourceFile(string filename)
		{
			Layer.SourceFileName = filename;
			return this;
		}

		public VideoLayerBuilder SnapshotTime(TimeSpan time)
		{
			Layer.SnapshotTime = time;
			return this;
		}
	}
}
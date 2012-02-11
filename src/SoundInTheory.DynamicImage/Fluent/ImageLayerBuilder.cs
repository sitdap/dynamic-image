using SoundInTheory.DynamicImage.Layers;
using SoundInTheory.DynamicImage.Sources;

namespace SoundInTheory.DynamicImage.Fluent
{
	public class ImageLayerBuilder : BaseLayerBuilder<ImageLayer, ImageLayerBuilder>
	{
		public ImageSource Source
		{
			get { return Layer.Source; }
			set { Layer.Source = value; }
		}

		public ImageLayerBuilder SourceFile(string filename)
		{
			Layer.SourceFileName = filename;
			return this;
		}

		public ImageLayerBuilder SourceBytes(byte[] bytes)
		{
			Layer.Source = new BytesImageSource { Bytes = bytes };
			return this;
		}
	}
}
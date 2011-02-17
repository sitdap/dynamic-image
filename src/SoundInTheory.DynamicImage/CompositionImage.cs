using System;
using System.Windows.Media.Imaging;

namespace SoundInTheory.DynamicImage
{
	public class CompositionImage
	{
		public CompositionImage(ImageProperties properties, BitmapSource image)
		{
			Properties = properties;
			Image = image;
		}

		public ImageProperties Properties { get; private set; }
		public BitmapSource Image { get; private set; }
	}
}
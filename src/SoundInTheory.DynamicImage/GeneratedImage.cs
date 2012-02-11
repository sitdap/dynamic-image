using System.IO;
using System.Windows.Media.Imaging;

namespace SoundInTheory.DynamicImage
{
	public class GeneratedImage
	{
		public GeneratedImage(ImageProperties properties, BitmapSource image)
		{
			Properties = properties;
			Image = image;
		}

		public ImageProperties Properties { get; private set; }
		public BitmapSource Image { get; private set; }

		public void Save(string path)
		{
			using (FileStream fileStream = File.OpenWrite(path))
			{
				BitmapEncoder encoder = Properties.GetEncoder();
				encoder.Frames.Add(BitmapFrame.Create(Image));
				encoder.Save(fileStream);
			}
		}
	}
}
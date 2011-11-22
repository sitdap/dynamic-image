using System.ComponentModel;
using System.Windows.Media.Imaging;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.Sources
{
	public class ImageImageSource : ImageSource
	{
		[Category("Source"), Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
		public BitmapSource Image
		{
			get
			{
				object value = this.PropertyStore["Image"];
				if (value != null)
					return (BitmapSource)value;
				return null;
			}
			set
			{
				this.PropertyStore["Image"] = value;
			}
		}

		public override FastBitmap GetBitmap()
		{
			return new FastBitmap(Image);
		}
	}
}

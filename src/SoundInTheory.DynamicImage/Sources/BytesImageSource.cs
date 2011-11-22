using System.ComponentModel;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.Sources
{
	public class BytesImageSource : ImageSource
	{
		[Category("Source"), Browsable(false)]
		public byte[] Bytes
		{
			get
			{
				object value = this.PropertyStore["Bytes"];
				if (value != null)
					return (byte[]) value;
				return null;
			}
			set
			{
				this.PropertyStore["Bytes"] = value;
			}
		}

		public override FastBitmap GetBitmap()
		{
			byte[] bytes = this.Bytes;
			if (bytes != null && bytes.Length > 0)
				return new FastBitmap(bytes);
			return null;
		}
	}
}

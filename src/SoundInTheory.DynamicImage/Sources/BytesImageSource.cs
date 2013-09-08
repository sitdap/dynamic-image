using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.Sources
{
	public class BytesImageSource : ImageSource
	{
		public byte[] Bytes
		{
			get { return (byte[]) this["Bytes"]; }
			set { this["Bytes"] = value; }
		}

		public override FastBitmap GetBitmap(ImageGenerationContext context)
		{
			byte[] bytes = this.Bytes;
			if (bytes != null && bytes.Length > 0)
				return new FastBitmap(bytes);
			return null;
		}
	}
}

using SWM = System.Windows.Media;

namespace SoundInTheory.DynamicImage
{
	public struct Color
	{
		public static Color FromHtml(string hexref)
		{
			var temp = SWM.ColorConverter.ConvertFromString(hexref);
			if (temp != null)
			{
				var color = (SWM.Color) temp;
				return FromArgb(color.A, color.R, color.G, color.B);
			}
			return Colors.Black;
		}

		public static Color FromArgb(byte a, byte r, byte g, byte b)
		{
			return new Color
			{
				A = a,
				R = r,
				G = g,
				B = b
			};
		}

		public static Color FromRgb(byte r, byte g, byte b)
		{
			return new Color
			{
				A = 255,
				R = r,
				G = g,
				B = b
			};
		}

		internal Color(uint packedValue)
			: this()
		{
			R = (byte) (packedValue);
			G = (byte) (packedValue >> 8);
			B = (byte) (packedValue >> 16);
			A = (byte) (packedValue >> 24);
		}

		public byte A { get; set; }
		public byte R { get; set; }
		public byte G { get; set; }
		public byte B { get; set; }
	}
}
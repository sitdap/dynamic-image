using System;
using System.Windows.Media;

namespace SoundInTheory.DynamicImage
{
	internal struct ColorHsv
	{
		// All values are between 0 and 255.
		public int H;
		public int S;
		public int V;

		public static explicit operator Color(ColorHsv value)
		{
			return ToColor(value);
		}

		private static Color ToColor(ColorHsv hsv)
		{
			// HSV contains values scaled as in the color wheel:
			// that is, all from 0 to 255. 

			// For this code to work, HSV.Hue needs
			// to be scaled from 0 to 360 (it's the angle of the selected
			// point within the circle). HSV.Saturation and HSV.Value must be 
			// scaled to be between 0 and 1.

			float r = 0, g = 0, b = 0;

			// Scale Hue to be between 0 and 360. Saturation
			// and Value scale to be between 0 and 1.
			float h = (hsv.H / 255.0f * 360.0f) % 360;
			float s = hsv.S / 255.0f;
			float v = hsv.V / 255.0f;

			if (s == 0)
			{
				// If s is 0, all colors are the same.
				// This is some flavor of gray.
				r = v;
				g = v;
				b = v;
			}
			else
			{
				// The color wheel consists of 6 sectors.
				// Figure out which sector you're in.
				float sectorPos = h / 60.0f;
				int sectorNumber = (int) Math.Floor(sectorPos);

				// Get the fractional part of the sector.
				// That is, how many degrees into the sector
				// are you?
				float fractionalSector = sectorPos - sectorNumber;

				// Calculate values for the three axes
				// of the color. 
				float p = v * (1 - s);
				float q = v * (1 - (s * fractionalSector));
				float t = v * (1 - (s * (1 - fractionalSector)));

				// Assign the fractional colors to r, g, and b
				// based on the sector the angle is in.
				switch (sectorNumber)
				{
					case 0:
						r = v;
						g = t;
						b = p;
						break;
					case 1:
						r = q;
						g = v;
						b = p;
						break;
					case 2:
						r = p;
						g = v;
						b = t;
						break;
					case 3:
						r = p;
						g = q;
						b = v;
						break;
					case 4:
						r = t;
						g = p;
						b = v;
						break;
					case 5:
						r = v;
						g = p;
						b = q;
						break;
				}
			}

			return Color.FromRgb((byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
		}
	}
}

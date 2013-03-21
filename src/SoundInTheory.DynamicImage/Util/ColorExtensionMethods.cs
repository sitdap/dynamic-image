namespace SoundInTheory.DynamicImage.Util
{
	public static class ColorExtensionMethods
	{
		public static System.Windows.Media.Color ToWpfColor(this Color color)
		{
			return System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
		}

		public static float GetHue(this Color c)
		{
			if ((c.R == c.G) && (c.G == c.B))
			{
				return 0f;
			}
			float num = ((float)c.R) / 255f;
			float num2 = ((float)c.G) / 255f;
			float num3 = ((float)c.B) / 255f;
			float num7 = 0f;
			float num4 = num;
			float num5 = num;
			if (num2 > num4)
			{
				num4 = num2;
			}
			if (num3 > num4)
			{
				num4 = num3;
			}
			if (num2 < num5)
			{
				num5 = num2;
			}
			if (num3 < num5)
			{
				num5 = num3;
			}
			float num6 = num4 - num5;
			if (num == num4)
			{
				num7 = (num2 - num3) / num6;
			}
			else if (num2 == num4)
			{
				num7 = 2f + ((num3 - num) / num6);
			}
			else if (num3 == num4)
			{
				num7 = 4f + ((num - num2) / num6);
			}
			num7 *= 60f;
			if (num7 < 0f)
			{
				num7 += 360f;
			}
			return num7;
		}

		public static float GetSaturation(this Color c)
		{
			float num = ((float)c.R) / 255f;
			float num2 = ((float)c.G) / 255f;
			float num3 = ((float)c.B) / 255f;
			float num7 = 0f;
			float num4 = num;
			float num5 = num;
			if (num2 > num4)
			{
				num4 = num2;
			}
			if (num3 > num4)
			{
				num4 = num3;
			}
			if (num2 < num5)
			{
				num5 = num2;
			}
			if (num3 < num5)
			{
				num5 = num3;
			}
			if (num4 == num5)
			{
				return num7;
			}
			float num6 = (num4 + num5) / 2f;
			if (num6 <= 0.5)
			{
				return ((num4 - num5) / (num4 + num5));
			}
			return ((num4 - num5) / ((2f - num4) - num5));
		}

		public static float GetBrightness(this Color c)
		{
			float num = ((float)c.R) / 255f;
			float num2 = ((float)c.G) / 255f;
			float num3 = ((float)c.B) / 255f;
			float num4 = num;
			float num5 = num;
			if (num2 > num4)
			{
				num4 = num2;
			}
			if (num3 > num4)
			{
				num4 = num3;
			}
			if (num2 < num5)
			{
				num5 = num2;
			}
			if (num3 < num5)
			{
				num5 = num3;
			}
			return ((num4 + num5) / 2f);
		}
	}
}
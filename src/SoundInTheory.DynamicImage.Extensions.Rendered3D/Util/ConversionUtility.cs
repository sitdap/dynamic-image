using Nexus;

namespace SoundInTheory.DynamicImage.Util
{
	internal static class ConversionUtility
	{
		public static ColorRgbF ToNexusColorRgbF(Color c)
		{
			return ColorRgbF.FromRgbColor(Nexus.Color.FromArgb(c.A, c.R, c.G, c.B));
		}
	}
}
using SWMColor = System.Windows.Media.Color;

namespace SoundInTheory.DynamicImage.Util
{
	public static class ColorExtensionMethods
	{
		public static SWMColor ToWpfColor(this Color color)
		{
			return SWMColor.FromArgb(color.A, color.R, color.G, color.B);
		}
	}
}
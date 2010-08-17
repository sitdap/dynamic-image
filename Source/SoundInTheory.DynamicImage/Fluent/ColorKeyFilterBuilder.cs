using System.Windows.Media;
using SoundInTheory.DynamicImage.Filters;

namespace SoundInTheory.DynamicImage.Fluent
{
	public class ColorKeyFilterBuilder : BaseFilterBuilder<ColorKeyFilter, ColorKeyFilterBuilder>
	{
		public ColorKeyFilterBuilder Color(Color color)
		{
			Filter.Color = color;
			return this;
		}

		public ColorKeyFilterBuilder ColorTolerance(byte tolerance)
		{
			Filter.ColorTolerance = tolerance;
			return this;
		}

		public ColorKeyFilterBuilder UseFirstPixel()
		{
			Filter.UseFirstPixel = true;
			return this;
		}
	}
}
using System.Windows.Media;
using SoundInTheory.DynamicImage.Filters;

namespace SoundInTheory.DynamicImage.Fluent
{
	public class ColorTintFilterBuilder : BaseFilterBuilder<ColorTintFilter, ColorTintFilterBuilder>
	{
		public ColorTintFilterBuilder Color(Color color)
		{
			Filter.Color = color;
			return this;
		}

		public ColorTintFilterBuilder Amount(int amount)
		{
			Filter.Amount = amount;
			return this;
		}
	}
}
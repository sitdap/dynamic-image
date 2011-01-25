using System.Windows.Media;
using SoundInTheory.DynamicImage.Filters;

namespace SoundInTheory.DynamicImage.Fluent
{
	public class RoundCornersFilterBuilder : BaseFilterBuilder<RoundCornersFilter, RoundCornersFilterBuilder>
	{
		public RoundCornersFilterBuilder BorderColor(Color color)
		{
			Filter.BorderColor = color;
			return this;
		}

		public RoundCornersFilterBuilder BorderWidth(int width)
		{
			Filter.BorderWidth = width;
			return this;
		}

		public RoundCornersFilterBuilder Roundness(int roundness)
		{
			Filter.Roundness = roundness;
			return this;
		}
	}
}
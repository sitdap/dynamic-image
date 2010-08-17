using SoundInTheory.DynamicImage.Filters;

namespace SoundInTheory.DynamicImage.Fluent
{
	public class GaussianBlurFilterBuilder : BaseFilterBuilder<GaussianBlurFilter, GaussianBlurFilterBuilder>
	{
		public GaussianBlurFilterBuilder Radius(int radius)
		{
			Filter.Radius = radius;
			return this;
		}
	}
}
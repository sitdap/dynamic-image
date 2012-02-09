using SoundInTheory.DynamicImage.Filters;

namespace SoundInTheory.DynamicImage.Fluent
{
	public class FeatherFilterBuilder : BaseFilterBuilder<FeatherFilter, FeatherFilterBuilder>
	{
		public FeatherFilterBuilder Radius(int radius)
		{
			Filter.Radius = radius;
			return this;
		}

		public FeatherFilterBuilder Shape(FeatherShape shape)
		{
			Filter.Shape = shape;
			return this;
		}
	}
}
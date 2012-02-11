using SoundInTheory.DynamicImage.Filters;

namespace SoundInTheory.DynamicImage.Fluent
{
	public class RotationFilterBuilder : BaseFilterBuilder<RotationFilter, RotationFilterBuilder>
	{
		public RotationFilterBuilder To(int angle)
		{
			Filter.Angle = angle;
			return this;
		}
	}
}
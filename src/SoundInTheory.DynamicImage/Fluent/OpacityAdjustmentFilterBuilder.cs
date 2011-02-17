using SoundInTheory.DynamicImage.Filters;

namespace SoundInTheory.DynamicImage.Fluent
{
	public class OpacityAdjustmentFilterBuilder : BaseFilterBuilder<OpacityAdjustmentFilter, OpacityAdjustmentFilterBuilder>
	{
		public OpacityAdjustmentFilterBuilder Opacity(byte opacity)
		{
			Filter.Opacity = opacity;
			return this;
		}
	}
}
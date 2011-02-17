using SoundInTheory.DynamicImage.Filters;

namespace SoundInTheory.DynamicImage.Fluent
{
	public class DropShadowFilterBuilder : ShadowFilterBuilderBase<DropShadowFilter, DropShadowFilterBuilder>
	{
		public DropShadowFilterBuilder Angle(int angle)
		{
			Filter.Angle = angle;
			return this;
		}

		public DropShadowFilterBuilder Distance(int distance)
		{
			Filter.Distance = distance;
			return this;
		}
	}
}
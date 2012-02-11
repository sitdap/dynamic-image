using SoundInTheory.DynamicImage.Filters;

namespace SoundInTheory.DynamicImage.Fluent
{
	public class UnsharpMaskFilterBuilder : BaseFilterBuilder<UnsharpMaskFilter, UnsharpMaskFilterBuilder>
	{
		public UnsharpMaskFilterBuilder Amount(int amount)
		{
			Filter.Amount = amount;
			return this;
		}

		public UnsharpMaskFilterBuilder Radius(int radius)
		{
			Filter.Radius = radius;
			return this;
		}

		public UnsharpMaskFilterBuilder Threshold(int threshold)
		{
			Filter.Threshold = threshold;
			return this;
		}
	}
}
using SoundInTheory.DynamicImage.Filters;

namespace SoundInTheory.DynamicImage.Fluent
{
	public class EmbossFilterBuilder : BaseFilterBuilder<EmbossFilter, EmbossFilterBuilder>
	{
		public EmbossFilterBuilder Amount(float amount)
		{
			Filter.Amount = amount;
			return this;
		}

		public EmbossFilterBuilder Width(float width)
		{
			Filter.Width = width;
			return this;
		}
	}
}
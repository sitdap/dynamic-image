using SoundInTheory.DynamicImage.Filters;

namespace SoundInTheory.DynamicImage.Fluent
{
	public class ContrastAdjustmentFilterBuilder : BaseFilterBuilder<ContrastAdjustmentFilter, ContrastAdjustmentFilterBuilder>
	{
		/// <summary>
		/// Sets the contrast level. Values range from -100 to 100.
		/// </summary>
		/// <param name="level"></param>
		/// <returns></returns>
		public ContrastAdjustmentFilterBuilder Level(int level)
		{
			Filter.Level = level;
			return this;
		}
	}
}
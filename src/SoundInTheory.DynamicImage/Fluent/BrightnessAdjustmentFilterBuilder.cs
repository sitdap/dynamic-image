using SoundInTheory.DynamicImage.Filters;

namespace SoundInTheory.DynamicImage.Fluent
{
	public class BrightnessAdjustmentFilterBuilder : BaseFilterBuilder<BrightnessAdjustmentFilter, BrightnessAdjustmentFilterBuilder>
	{
		/// <summary>
		/// Sets the brightness level. Values range from -100 to 100.
		/// </summary>
		/// <param name="level"></param>
		/// <returns></returns>
		public BrightnessAdjustmentFilterBuilder Level(int level)
		{
			Filter.Level = level;
			return this;
		}
	}
}
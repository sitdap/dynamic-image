using SoundInTheory.DynamicImage.Filters;

namespace SoundInTheory.DynamicImage.Fluent
{
	public class ShinyFloorFilterBuilder : BaseFilterBuilder<ShinyFloorFilter, ShinyFloorFilterBuilder>
	{
		public ShinyFloorFilterBuilder ReflectionPercentage(byte percentage)
		{
			Filter.ReflectionPercentage = percentage;
			return this;
		}

		public ShinyFloorFilterBuilder ReflectionOpacity(byte opacity)
		{
			Filter.ReflectionOpacity = opacity;
			return this;
		}

		public ShinyFloorFilterBuilder ReflectionPositionY(int y)
		{
			Filter.ReflectionPositionY = y;
			return this;
		}
	}
}
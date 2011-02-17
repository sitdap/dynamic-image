using SoundInTheory.DynamicImage.Filters;

namespace SoundInTheory.DynamicImage.Fluent
{
	public class DistortCornersFilterBuilder : BaseFilterBuilder<DistortCornersFilter, DistortCornersFilterBuilder>
	{
		public DistortCornersFilterBuilder TopLeft(int x, int y)
		{
			Filter.X1 = x;
			Filter.Y1 = y;
			return this;
		}

		public DistortCornersFilterBuilder TopRight(int x, int y)
		{
			Filter.X2 = x;
			Filter.Y2 = y;
			return this;
		}

		public DistortCornersFilterBuilder BottomRight(int x, int y)
		{
			Filter.X3 = x;
			Filter.Y3 = y;
			return this;
		}

		public DistortCornersFilterBuilder BottomLeft(int x, int y)
		{
			Filter.X4 = x;
			Filter.Y4 = y;
			return this;
		}
	}
}
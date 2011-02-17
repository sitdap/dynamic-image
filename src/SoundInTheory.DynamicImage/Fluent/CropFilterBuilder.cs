using SoundInTheory.DynamicImage.Filters;

namespace SoundInTheory.DynamicImage.Fluent
{
	public class CropFilterBuilder : BaseFilterBuilder<CropFilter, CropFilterBuilder>
	{
		public CropFilterBuilder To(int width, int height)
		{
			Filter.Width = width;
			Filter.Height = height;
			return this;
		}

		public CropFilterBuilder X(int x)
		{
			Filter.X = x;
			return this;
		}

		public CropFilterBuilder Y(int y)
		{
			Filter.Y = y;
			return this;
		}
	}
}
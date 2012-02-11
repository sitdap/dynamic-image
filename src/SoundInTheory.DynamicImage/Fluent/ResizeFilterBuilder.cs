using SoundInTheory.DynamicImage.Filters;

namespace SoundInTheory.DynamicImage.Fluent
{
	public class ResizeFilterBuilder : BaseFilterBuilder<ResizeFilter, ResizeFilterBuilder>
	{
		public ResizeFilterBuilder To(int width, int height)
		{
			return To(width, height, ResizeMode.Uniform);
		}

		public ResizeFilterBuilder To(int width, int height, ResizeMode resizeMode)
		{
			Filter.Width = Unit.Pixel(width);
			Filter.Height = Unit.Pixel(height);
			Filter.Mode = resizeMode;
			return this;
		}

		public ResizeFilterBuilder To(Unit width, Unit height)
		{
			return To(width, height, ResizeMode.Uniform);
		}

		public ResizeFilterBuilder To(Unit width, Unit height, ResizeMode resizeMode)
		{
			Filter.Width = width;
			Filter.Height = height;
			Filter.Mode = resizeMode;
			return this;
		}

		public ResizeFilterBuilder ToWidth(int width)
		{
			Filter.Width = Unit.Pixel(width);
			Filter.Mode = ResizeMode.UseWidth;
			return this;
		}

		public ResizeFilterBuilder ToHeight(int height)
		{
			Filter.Height = Unit.Pixel(height);
			Filter.Mode = ResizeMode.UseHeight;
			return this;
		}

		public ResizeFilterBuilder EnlargeImage()
		{
			Filter.EnlargeImage = true;
			return this;
		}
	}
}
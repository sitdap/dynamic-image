using SoundInTheory.DynamicImage.Filters;

namespace SoundInTheory.DynamicImage.Fluent
{
	public class ContentAwareResizeFilterBuilder : BaseFilterBuilder<ContentAwareResizeFilter, ContentAwareResizeFilterBuilder>
	{
		public ContentAwareResizeFilterBuilder To(int width, int height)
		{
			Filter.Width = Unit.Pixel(width);
			Filter.Height = Unit.Pixel(height);
			return this;
		}

		public ContentAwareResizeFilterBuilder ToWidth(int width)
		{
			Filter.Width = Unit.Pixel(width);
			return this;
		}

		public ContentAwareResizeFilterBuilder ToHeight(int height)
		{
			Filter.Height = Unit.Pixel(height);
			return this;
		}

		public ContentAwareResizeFilterBuilder ConvolutionType(ContentAwareResizeFilterConvolutionType convolutionType)
		{
			Filter.ConvolutionType = convolutionType;
			return this;
		}
	}
}
using SoundInTheory.DynamicImage.Filters;
using SoundInTheory.DynamicImage.Sources;

namespace SoundInTheory.DynamicImage.Fluent
{
	public class ClippingMaskFilterBuilder : BaseFilterBuilder<ClippingMaskFilter, ClippingMaskFilterBuilder>
	{
		public ClippingMaskFilterBuilder MaskImage(ImageSource imageSource)
		{
			Filter.MaskImage = imageSource;
			return this;
		}

		public ClippingMaskFilterBuilder MaskImageFile(string file)
		{
			Filter.MaskImage = new FileImageSource { FileName = file };
			return this;
		}

		public ClippingMaskFilterBuilder MaskPositionX(int x)
		{
			Filter.MaskPositionX = x;
			return this;
		}

		public ClippingMaskFilterBuilder MaskPositionY(int y)
		{
			Filter.MaskPositionY = y;
			return this;
		}
	}
}
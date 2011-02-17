using SoundInTheory.DynamicImage.Filters;
using SoundInTheory.DynamicImage.Sources;

namespace SoundInTheory.DynamicImage.Fluent
{
	public class ClippingMaskFilterBuilder : BaseFilterBuilder<ClippingMaskFilter, ClippingMaskFilterBuilder>
	{
		public ClippingMaskFilterBuilder MaskImage(ImageSource imageSource)
		{
			Filter.MaskImage.SingleSource = imageSource;
			return this;
		}

		public ClippingMaskFilterBuilder MaskPositionX(int x)
		{
			Filter.MaskPositionX = x;
			return this;
		}

		public ClippingMaskFilterBuilder MaskPositionY(int y)
		{
			Filter.MaskPositionX = y;
			return this;
		}
	}
}
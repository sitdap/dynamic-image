using SoundInTheory.DynamicImage.Filters;

namespace SoundInTheory.DynamicImage.Fluent
{
	public abstract class FilterBuilder
	{
		public static BrightnessAdjustmentFilterBuilder AdjustBrightness
		{
			get { return new BrightnessAdjustmentFilterBuilder(); }
		}

		public static ContrastAdjustmentFilterBuilder AdjustContrast
		{
			get { return new ContrastAdjustmentFilterBuilder(); }
		}

		public static OpacityAdjustmentFilterBuilder AdjustOpacity
		{
			get { return new OpacityAdjustmentFilterBuilder(); }
		}

		public static ClippingMaskFilterBuilder ClippingMask
		{
			get { return new ClippingMaskFilterBuilder(); }
		}

		public static ColorKeyFilterBuilder ColorKey
		{
			get { return new ColorKeyFilterBuilder(); }
		}

		public static CropFilterBuilder Crop
		{
			get { return new CropFilterBuilder(); }
		}

		public static DistortCornersFilterBuilder DistortCorners
		{
			get { return new DistortCornersFilterBuilder(); }
		}

		public static DropShadowFilterBuilder DropShadow
		{
			get { return new DropShadowFilterBuilder(); }
		}

		public static EmbossFilterBuilder Emboss
		{
			get { return new EmbossFilterBuilder(); }
		}

		public static FeatherFilterBuilder Feather
		{
			get { return new FeatherFilterBuilder(); }
		}

		public static GaussianBlurFilterBuilder GaussianBlur
		{
			get { return new GaussianBlurFilterBuilder(); }
		}

		public static GrayscaleFilterBuilder Grayscale
		{
			get { return new GrayscaleFilterBuilder(); }
		}

		public static InversionFilterBuilder Inversion
		{
			get { return new InversionFilterBuilder(); }
		}

		public static OuterGlowFilterBuilder OuterGlow
		{
			get { return new OuterGlowFilterBuilder(); }
		}

		public static ResizeFilterBuilder Resize
		{
			get { return new ResizeFilterBuilder(); }
		}

		public static RotationFilterBuilder Rotate
		{
			get { return new RotationFilterBuilder(); }
		}

		public static SepiaFilterBuilder Sepia
		{
			get { return new SepiaFilterBuilder(); }
		}

		public static ShinyFloorFilterBuilder ShinyFloor
		{
			get { return new ShinyFloorFilterBuilder(); }
		}
		
		public abstract Filter ToFilter();
	}
}
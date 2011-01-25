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

		public static CurvesAdjustmentFilterBuilder AdjustCurves
		{
			get { return new CurvesAdjustmentFilterBuilder(); }
		}

		public static OpacityAdjustmentFilterBuilder AdjustOpacity
		{
			get { return new OpacityAdjustmentFilterBuilder(); }
		}

		public static BorderFilterBuilder Border
		{
			get { return new BorderFilterBuilder(); }
		}

		public static ClippingMaskFilterBuilder ClippingMask
		{
			get { return new ClippingMaskFilterBuilder(); }
		}

		public static ColorKeyFilterBuilder ColorKey
		{
			get { return new ColorKeyFilterBuilder(); }
		}

		public static ContentAwareResizeFilterBuilder ContentAwareResize
		{
			get { return new ContentAwareResizeFilterBuilder(); }
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

		public static RoundCornersFilterBuilder RoundCorners
		{
			get { return new RoundCornersFilterBuilder(); }
		}

		public static SepiaFilterBuilder Sepia
		{
			get { return new SepiaFilterBuilder(); }
		}

		public static ShinyFloorFilterBuilder ShinyFloor
		{
			get { return new ShinyFloorFilterBuilder(); }
		}

		public static SolarizeFilterBuilder Solarize
		{
			get { return new SolarizeFilterBuilder(); }
		}

		public static UnsharpMaskFilterBuilder UnsharpMask
		{
			get { return new UnsharpMaskFilterBuilder(); }
		}

		public static VignetteFilterBuilder Vignette
		{
			get { return new VignetteFilterBuilder(); }
		}
		
		public abstract Filter ToFilter();
	}
}
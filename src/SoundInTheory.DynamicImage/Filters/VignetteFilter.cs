using System.Windows;
using System.Windows.Media;
using SoundInTheory.DynamicImage.Util;
using SWMColor = System.Windows.Media.Color;
using SWMColors = System.Windows.Media.Colors;

namespace SoundInTheory.DynamicImage.Filters
{
	/// <summary>
	/// Applies a vignette to the edges of the image.
	/// </summary>
	public class VignetteFilter : ImageReplacementFilter
	{
		protected override bool GetDestinationDimensions(FastBitmap source, out int width, out int height)
		{
			width = source.Width;
			height = source.Height;
			return true;
		}

		protected override void ApplyFilter(FastBitmap source, DrawingContext dc, int width, int height)
		{
			var gradientStops = new GradientStopCollection();
			gradientStops.Add(new GradientStop(SWMColor.FromArgb(0, 0, 0, 0), 0));
			gradientStops.Add(new GradientStop(SWMColor.FromArgb(0, 0, 0, 0), 0.5));
			gradientStops.Add(new GradientStop(SWMColor.FromArgb(180, 0, 0, 0), 1.3));
			gradientStops.Add(new GradientStop(SWMColor.FromArgb(230, 0, 0, 0), 1.7));

			var brush = new RadialGradientBrush(gradientStops)
			{
				GradientOrigin = new Point(0.5, 0.5),
				Center = new Point(0.5, 0.45),
				RadiusX = 0.5,
				RadiusY = 0.5
			};

			dc.DrawImage(source.InnerBitmap, new Rect(0, 0, width, height));

			dc.PushOpacityMask(brush);
			dc.DrawRectangle(new SolidColorBrush(SWMColors.Black), null, new Rect(0, 0, width, height));
			dc.Pop();
		}
	}
}

using System.Windows;
using System.Windows.Media;
using SoundInTheory.DynamicImage.Util;

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
			GradientStopCollection gradientStops = new GradientStopCollection();
			gradientStops.Add(new GradientStop(Color.FromArgb(0, 0, 0, 0), 0));
			gradientStops.Add(new GradientStop(Color.FromArgb(0, 0, 0, 0), 0.5));
			gradientStops.Add(new GradientStop(Color.FromArgb(150, 0, 0, 0), 1.3));
			gradientStops.Add(new GradientStop(Color.FromArgb(200, 0, 0, 0), 1.7));

			RadialGradientBrush brush = new RadialGradientBrush(gradientStops)
			{
				GradientOrigin = new Point(0.5, 0.5),
				Center = new Point(0.5, 0.4),
				RadiusX = 0.5,
				RadiusY = 0.5
			};

			dc.DrawImage(source.InnerBitmap, new Rect(0, 0, width, height));

			dc.PushOpacityMask(brush);
			dc.DrawRectangle(new SolidColorBrush(Colors.Black), null, new Rect(0, 0, width, height));
			dc.Pop();
		}

		public override string ToString()
		{
			return "Vignette";
		}
	}
}

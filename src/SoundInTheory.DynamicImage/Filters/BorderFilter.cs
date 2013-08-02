using System;
using System.Windows;
using System.Windows.Media;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.Filters
{
	/// <summary>
	/// Applies a border to an image. The original image is not resized; the border is added around the edge of the image.
	/// </summary>
	public class BorderFilter : ImageReplacementFilter
	{
		#region Properties

		/// <summary>
		/// Gets or sets the width of the border. Defaults to 10.
		/// </summary>
		public int Width
		{
			get { return (int)(this["Width"] ?? 10); }
			set
			{
				if (value < 0)
					throw new ArgumentException("The border width must be greater than or equal to zero.", "value");
				this["Width"] = value;
			}
		}

		public Fill Fill
		{
			get { return (Fill)(this["Fill"] ?? (this["Fill"] = new Fill())); }
			set { this["Fill"] = value; }
		}

		#endregion

		#region Methods

		protected override bool GetDestinationDimensions(FastBitmap source, out int width, out int height)
		{
			width = source.Width + Width * 2;
			height = source.Height + Width * 2;
			return true;
		}

		protected override void ApplyFilter(FastBitmap source, DrawingContext dc, int width, int height)
		{
            // Draw image.
            dc.PushTransform(new TranslateTransform(Width, Width));
            dc.DrawImage(source.InnerBitmap, new Rect(0, 0, source.Width, source.Height));
            dc.Pop();

			// Draw border.
            dc.DrawRectangle(null, new Pen(Fill.GetBrush(), Width),
                new Rect(Width / 2.0, Width / 2.0, width - Width, height - Width));
		}

		#endregion
	}
}

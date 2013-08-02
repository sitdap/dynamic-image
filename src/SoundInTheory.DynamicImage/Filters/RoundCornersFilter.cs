using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.Filters
{
	public class RoundCornersFilter : ImageReplacementFilter
	{
		#region Properties

		/// <summary>
		/// Gets or sets the border color. Defaults to White.
		/// </summary>
		public Color BorderColor
		{
			get { return (Color)(this["BorderColor"] ?? Colors.White); }
			set { this["BorderColor"] = value; }
		}

		/// <summary>
		/// Gets or sets the border width. Defaults to 0.
		/// </summary>
		public int BorderWidth
		{
			get { return (int)(this["BorderWidth"] ?? 0); }
			set
			{
				if (value < 0)
					throw new ArgumentException("The border width must be greater than or equal to zero.", "value");
				this["BorderWidth"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the roundness of the corners.
		/// </summary>
		[DefaultValue(5), Description("Gets or sets the roundness of the corners.")]
		public int Roundness
		{
			get { return (int)(this["Roundness"] ?? 5); }
			set
			{
				if (value < 0)
					throw new ArgumentException("The roundness must be greater than or equal to zero.", "value");
				this["Roundness"] = value;
			}
		}

		#endregion

		#region Methods

		protected override bool GetDestinationDimensions(FastBitmap source, out int width, out int height)
		{
			width = source.Width + BorderWidth * 2;
			height = source.Height + BorderWidth * 2;
			return true;
		}

		protected override void ApplyFilter(FastBitmap source, DrawingContext dc, int width, int height)
		{
            // Clip to rounded rectangle shape.
            dc.PushClip(new RectangleGeometry(new Rect(new Size(width, height)), Roundness, Roundness));

			// Draw image.
			dc.PushTransform(new TranslateTransform(BorderWidth, BorderWidth));
			dc.DrawImage(source.InnerBitmap, new Rect(0, 0, source.Width, source.Height));
			dc.Pop();

            dc.Pop();

			// Draw border.
			dc.DrawRoundedRectangle(null, new Pen(new SolidColorBrush(BorderColor.ToWpfColor()), BorderWidth),
				new Rect(BorderWidth / 2.0, BorderWidth / 2.0, width - BorderWidth, height - BorderWidth),
				Roundness, Roundness);
		}

		#endregion
	}
}

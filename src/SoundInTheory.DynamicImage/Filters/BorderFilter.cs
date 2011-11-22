using System;
using System.ComponentModel;
using System.Web.UI;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SoundInTheory.DynamicImage.Util;
using Size = System.Drawing.Size;

namespace SoundInTheory.DynamicImage.Filters
{
	/// <summary>
	/// Applies a border to an image. The original image is not resized; the border is added around the edge of the image.
	/// </summary>
	public class BorderFilter : ImageReplacementFilter
	{
		#region Properties

		/// <summary>
		/// Gets or sets the width of the border.
		/// </summary>
		[DefaultValue(10), Description("Gets or sets the width of the border.")]
		public int Width
		{
			get { return (int)(PropertyStore["Width"] ?? 10); }
			set
			{
				if (value < 0)
					throw new ArgumentException("The border width must be greater than or equal to zero.", "value");

				PropertyStore["Width"] = value;
			}
		}

		public Fill Fill
		{
			get { return (Fill)(PropertyStore["Fill"] ?? (PropertyStore["Fill"] = new Fill())); }
			set { PropertyStore["Fill"] = value; }
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
			// Draw border.
			dc.DrawRectangle(Fill.GetBrush(), null, new Rect(0, 0, width, height));

			// Draw image.
			dc.PushTransform(new TranslateTransform(Width, Width));
			dc.DrawImage(source.InnerBitmap, new Rect(0, 0, source.Width, source.Height));
			dc.Pop();
		}

		public override string ToString()
		{
			return "Border";
		}

		#endregion
	}
}

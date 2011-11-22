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
	public class RoundCornersFilter : ImageReplacementFilter
	{
		#region Properties

		/// <summary>
		/// Gets or sets the border colour.
		/// </summary>
		[DefaultValue(typeof(Colors), "White"), Description("Gets or sets the border colour.")]
		public Color BorderColor
		{
			get { return (Color)(PropertyStore["BorderColor"] ?? Colors.White); }
			set { PropertyStore["BorderColor"] = value; }
		}

		/// <summary>
		/// Gets or sets the border width.
		/// </summary>
		[DefaultValue(0), Description("Gets or sets the border width.")]
		public int BorderWidth
		{
			get { return (int)(PropertyStore["BorderWidth"] ?? 0); }
			set
			{
				if (value < 0)
					throw new ArgumentException("The border width must be greater than or equal to zero.", "value");

				PropertyStore["BorderWidth"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the roundness of the corners.
		/// </summary>
		[DefaultValue(5), Description("Gets or sets the roundness of the corners.")]
		public int Roundness
		{
			get { return (int)(PropertyStore["Roundness"] ?? 5); }
			set
			{
				if (value < 0)
					throw new ArgumentException("The roundness must be greater than or equal to zero.", "value");

				PropertyStore["Roundness"] = value;
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
			// Draw image.
			dc.PushTransform(new TranslateTransform(BorderWidth, BorderWidth));
			dc.DrawImage(source.InnerBitmap, new Rect(0, 0, source.Width, source.Height));
			dc.Pop();

			// Draw border.
			dc.DrawRoundedRectangle(null, new Pen(new SolidColorBrush(BorderColor), BorderWidth),
				new Rect(BorderWidth / 2.0, BorderWidth / 2.0, width - BorderWidth, height - BorderWidth),
				Roundness, Roundness);
		}

		#endregion
	}
}

using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.Filters
{
	/// <summary>
	/// Crops an image. The rectangular section to crop is defined by X/Y coordinates and a size.
	/// </summary>
	/// <include file="../documentation.xml" path="docs/types/type[@name = 'CropFilter']/*" />
	public class CropFilter : ImageReplacementFilter
	{
		#region Properties

		/// <summary>
		/// Gets or sets the X-coordinate of the rectangular section to crop.
		/// </summary>
		[DefaultValue(0), Description("Gets or sets the X-coordinate of the rectangular section to crop.")]
		public int X
		{
			get
			{
				object value = this.PropertyStore["X"];
				if (value != null)
					return (int) value;
				return 0;
			}
			set
			{
				if (value < 0)
					throw new ArgumentException("value", "The X-coordinate of the rectangular section must be greater than or equal to zero.");

				this.PropertyStore["X"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the Y-coordinate of the rectangular section to crop.
		/// </summary>
		[DefaultValue(0), Description("Gets or sets the Y-coordinate of the rectangular section to crop.")]
		public int Y
		{
			get
			{
				object value = this.PropertyStore["Y"];
				if (value != null)
					return (int) value;
				return 0;
			}
			set
			{
				if (value < 0)
					throw new ArgumentException("value", "The Y-coordinate of the rectangular section must be greater than or equal to zero.");

				this.PropertyStore["Y"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the width of the rectangular section to crop.
		/// </summary>
		[DefaultValue(200), Description("Gets or sets the width of the rectangular section to crop.")]
		public int Width
		{
			get
			{
				object value = this.PropertyStore["Width"];
				if (value != null)
					return (int) value;
				return 200;
			}
			set
			{
				if (value < 1)
					throw new ArgumentException("value", "The width of the rectangular section must be greater than one.");

				this.PropertyStore["Width"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the height of the rectangular section to crop.
		/// </summary>
		[DefaultValue(200), Description("Gets or sets the height of the rectangular section to crop.")]
		public int Height
		{
			get
			{
				object value = this.PropertyStore["Height"];
				if (value != null)
					return (int) value;
				return 200;
			}
			set
			{
				if (value < 1)
					throw new ArgumentException("value", "The height of the rectangular section must be greater than one.");

				this.PropertyStore["Height"] = value;
			}
		}

		#endregion

		#region Methods

		protected override bool GetDestinationDimensions(FastBitmap source, out int width, out int height)
		{
			width = this.Width;
			height = this.Height;
			return true;
		}

		protected override void ApplyFilter(FastBitmap source, DrawingContext dc, int width, int height)
		{
			BitmapSource bitmapSource = new CroppedBitmap(source.InnerBitmap, new System.Windows.Int32Rect(X, Y, Width, Height));
			dc.DrawImage(bitmapSource, new System.Windows.Rect(0, 0, width, height));
		}

		#endregion
	}
}

using System;
using System.Windows;
using System.Windows.Media;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.Filters
{
	/// <summary>
	/// Adjusts the opacity of a layer. Opacity can be from 0,
	/// which is totally transparent, to 100, which is totally opaque.
	/// </summary>
	public class OpacityAdjustmentFilter : ImageReplacementFilter
	{
		#region Properties

		/// <summary>
		/// Gets or sets the opacity. Valid values range from 0 to 100. Defaults to 50.
		/// </summary>
		public byte Opacity
		{
			get { return (byte)(this["Opacity"] ?? 50); }
			set
			{
				if (value < 0 || value > 100)
					throw new ArgumentOutOfRangeException("value", "Opacity values must range from 0 to 100.");
				this["Opacity"] = value;
			}
		}

		#endregion

		protected override bool GetDestinationDimensions(FastBitmap source, out int width, out int height)
		{
			width = source.Width;
			height = source.Height;
			return true;
		}

		protected override void ApplyFilter(FastBitmap source, DrawingContext dc, int width, int height)
		{
			dc.PushOpacity(Opacity/100.0);
			dc.DrawImage(source.InnerBitmap, new Rect(0, 0, width, height));
			dc.Pop();
		}
	}
}
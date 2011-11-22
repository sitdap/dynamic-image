using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.Filters
{
	/// <summary>
	/// Adjusts the opacity of a layer. Opacity can be from 0,
	/// which is totally transparent, to 100, which is totally opaque.
	/// </summary>
	/// <include file="../documentation.xml" path="docs/types/type[@name = 'OpacityAdjustmentFilter']/*" />
	public class OpacityAdjustmentFilter : ImageReplacementFilter
	{
		#region Properties

		/// <summary>
		/// Gets or sets the opacity. Valid values range from 0 to 100.
		/// </summary>
		[DefaultValue(50), Description("Gets or sets the opacity. Values range from 0 to 100.")]
		public byte Opacity
		{
			get { return (byte)(PropertyStore["Opacity"] ?? 50); }
			set
			{
				if (value < 0 || value > 100)
					throw new ArgumentOutOfRangeException("value", "Opacity values must range from 0 to 100.");

				PropertyStore["Opacity"] = value;
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

		public override string ToString()
		{
			return "Opacity Adjustment";
		}
	}
}
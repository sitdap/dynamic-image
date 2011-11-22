using System;
using System.Windows;
using System.Windows.Media;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.Filters
{
	public abstract class ShadowFilterBase : ShaderEffectFilter
	{
		#region Properties

		/// <summary>
		/// Gets or sets the color of shadow or glow. Defaults to Black.
		/// </summary>
		public Color Color
		{
			get { return (Color)(this["Color"] ?? Colors.Black); }
			set { this["Color"] = value; }
		}

		/// <summary>
		/// Gets or sets the opacity of the shadow. Values range from 0 to 100. Defaults to 75.
		/// </summary>
		public int Opacity
		{
			get { return (int) (this["Opacity"] ?? 75); }
			set
			{
				if (value < 0 || value > 100)
					throw new ArgumentOutOfRangeException("value", "Opacity values must range from 0 to 100.");
				this["Opacity"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the size of the shadow or glow. Defaults to 5.
		/// </summary>
		public int Size
		{
			get { return (int)(this["Size"] ?? 5); }
			set
			{
				if (value < 0)
					throw new ArgumentOutOfRangeException("value", "The size of the shadow or glow must be greater than or equal to zero.");
				this["Size"] = value;
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Returns the dimensions of the output image.
		/// </summary>
		/// <param name="source">The source image.</param>
		/// <param name="width">The desired width of the output image.</param>
		/// <param name="height">The desired height of the output image.</param>
		/// <returns><c>true</c> if the destination image should be created; otherwise <c>false</c>.
		/// The <see cref="DropShadowFilter" /> always returns <c>true</c>.</returns>
		protected override bool GetDestinationDimensions(FastBitmap source, out int width, out int height)
		{
			Vector padding = GetPadding();
			width = source.Width + (int) Math.Ceiling(Math.Abs(padding.X));
			height = source.Height + (int) Math.Ceiling(Math.Abs(padding.Y));
			return true;
		}

		protected abstract Vector GetPadding();

		#endregion
	}
}

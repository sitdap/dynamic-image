using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.Filters
{
	public abstract class ShadowFilterBase : ShaderEffectFilter
	{
		#region Properties

		/// <summary>
		/// Gets or sets the color of shadow or glow.
		/// </summary>
		[DefaultValue(typeof(Colors), "Black")]
		public Color Color
		{
			get
			{
				object value = this.ViewState["Color"];
				if (value != null)
					return (Color) value;
				return Colors.Black;
			}
			set
			{
				this.ViewState["Color"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the opacity of the shadow. Values range from 0 to 100.
		/// </summary>
		[DefaultValue(75), Description("Gets or sets the opacity. Values range from 0 to 100.")]
		public int Opacity
		{
			get
			{
				object value = this.ViewState["Opacity"];
				if (value != null)
					return (int) value;
				return 75;
			}
			set
			{
				if (value < 0 || value > 100)
					throw new ArgumentOutOfRangeException("value", "Opacity values must range from 0 to 100.");

				this.ViewState["Opacity"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the size of the shadow or glow.
		/// </summary>
		[DefaultValue(5)]
		public int Size
		{
			get
			{
				object value = this.ViewState["Size"];
				if (value != null)
					return (int) value;
				return 5;
			}
			set
			{
				if (value < 0)
					throw new ArgumentOutOfRangeException("value", "The size of the shadow or glow must be greater than or equal to zero.");

				this.ViewState["Size"] = value;
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

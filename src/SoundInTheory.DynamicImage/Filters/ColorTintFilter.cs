using System;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Media.Effects;
using SoundInTheory.DynamicImage.ShaderEffects;

namespace SoundInTheory.DynamicImage.Filters
{
	/// <summary>
	/// Adjusts the colour tint of the layer.
	/// </summary>
	/// <include file="../documentation.xml" path="docs/types/type[@name = 'ColorTintFilter']/*" />
	public class ColorTintFilter : ShaderEffectFilter
	{
		#region Properties

		/// <summary>
		/// Gets or sets the colour used to tint the layer.
		/// </summary>
		[DefaultValue(typeof(Colors), "Red"), Description("Gets or sets the colour used to tint the layer.")]
		public Color Color
		{
			get { return (Color) (ViewState["Color"] ?? Colors.Red); }
			set { ViewState["Color"] = value; }
		}

		/// <summary>
		/// Gets or sets the color tint amount. Values range from 0 (image will be grayscale) to 100.
		/// </summary>
		[DefaultValue(25), Description("Gets or sets the color tint amount. Values range from 0 (image will be grayscale) to 100.")]
		public int Amount
		{
			get { return (int) (ViewState["Amount"] ?? 25); }
			set
			{
				if (value < 0 || value > 100)
					throw new ArgumentOutOfRangeException("value", "Color tint amounts must range from 0 to 100.");

				ViewState["Amount"] = value;
			}
		}

		#endregion

		#region Methods

		protected override Effect GetEffect(Util.FastBitmap source)
		{
			return new ColorTintEffect
			{
				Amount = Amount/100.0,
				RequiredColor = Color
			};
		}

		public override string ToString()
		{
			return "Color Tint";
		}

		#endregion
	}
}

using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Media.Effects;
using SoundInTheory.DynamicImage.ShaderEffects;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.Filters
{
	/// <summary>
	/// Adjusts the brightness of the layer.
	/// </summary>
	/// <include file="../documentation.xml" path="docs/types/type[@name = 'BrightnessAdjustmentFilter']/*" />
	public class BrightnessAdjustmentFilter : ShaderEffectFilter
	{
		#region Properties

		/// <summary>
		/// Gets or sets the brightness level. Values range from -100 to 100.
		/// </summary>
		[DefaultValue(0), Description("Gets or sets the brightness level. Values range from -100 to 100.")]
		public int Level
		{
			get
			{
				object value = this.PropertyStore["Level"];
				if (value != null)
					return (int) value;
				return 0;
			}
			set
			{
				if (value < -100 || value > 100)
					throw new ArgumentOutOfRangeException("value", "Brightness level values must range from -100 to 100.");

				this.PropertyStore["Level"] = value;
			}
		}

		#endregion

		#region Methods

		protected override Effect GetEffect(FastBitmap source)
		{
			return new BrightnessAdjustmentEffect
			{
				Level = this.Level/100.0
			};
		}

		public override string ToString()
		{
			return "Brightness Adjustment";
		}

		#endregion
	}
}

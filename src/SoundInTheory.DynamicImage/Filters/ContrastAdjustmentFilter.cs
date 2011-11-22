using System;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Media.Effects;
using SoundInTheory.DynamicImage.ShaderEffects;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.Filters
{
	/// <summary>
	/// Adjusts the contrast of the layer.
	/// </summary>
	/// <include file="../documentation.xml" path="docs/types/type[@name = 'ContrastAdjustmentFilter']/*" />
	public class ContrastAdjustmentFilter : ShaderEffectFilter
	{
		#region Properties

		/// <summary>
		/// Gets or sets the contrast level. Values range from -100 to 100.
		/// </summary>
		[DefaultValue(0), Description("Gets or sets the contrast level. Values range from -100 to 100.")]
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
					throw new ArgumentOutOfRangeException("value", "Contrast level values must range from -100 to 100.");

				this.PropertyStore["Level"] = value;
			}
		}

		#endregion

		#region Methods

		protected override Effect GetEffect(FastBitmap source)
		{
			return new ContrastAdjustmentEffect
			{
				Level = (Level + 100) / 40.0
			};
		}

		#endregion
	}
}

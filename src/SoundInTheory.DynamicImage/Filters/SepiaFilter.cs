using System;
using System.Windows.Media.Effects;
using SoundInTheory.DynamicImage.ShaderEffects;

namespace SoundInTheory.DynamicImage.Filters
{
	/// <summary>
	/// Converts the image to grayscale (using the correct colour ratios).
	/// </summary>
	/// <include file="../documentation.xml" path="docs/types/type[@name = 'SepiaFilter']/*" />
	public class SepiaFilter : ShaderEffectFilter
	{
		protected override Effect GetEffect(Util.FastBitmap source)
		{
			return new SepiaEffect();
		}
	}
}

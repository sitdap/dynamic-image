using System.Windows.Media.Effects;
using SoundInTheory.DynamicImage.ShaderEffects;

namespace SoundInTheory.DynamicImage.Filters
{
	/// <summary>
	/// Inverts the colours of the image.
	/// </summary>
	/// <include file="../documentation.xml" path="docs/types/type[@name = 'InversionFilter']/*" />
	public class InversionFilter : ShaderEffectFilter
	{
		protected override Effect GetEffect(Util.FastBitmap source)
		{
			return new InversionEffect();
		}
	}
}

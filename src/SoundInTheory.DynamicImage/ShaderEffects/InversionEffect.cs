using System.Windows;
using System.Windows.Media;

namespace SoundInTheory.DynamicImage.ShaderEffects
{
	internal class InversionEffect : ShaderEffectBase<InversionEffect>
	{
		public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty("Input", typeof(InversionEffect), 0);

		public Brush Input
		{
			get { return (Brush)GetValue(InputProperty); }
			set { SetValue(InputProperty, value); }
		}

		public InversionEffect()
		{
			UpdateShaderValue(InputProperty);
		}
	}
}
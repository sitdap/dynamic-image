using System.Windows;
using System.Windows.Media;

namespace SoundInTheory.DynamicImage.ShaderEffects
{
	internal class SepiaEffect : ShaderEffectBase<SepiaEffect>
	{
		public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty("Input", typeof(SepiaEffect), 0);

		public Brush Input
		{
			get { return (Brush)GetValue(InputProperty); }
			set { SetValue(InputProperty, value); }
		}

		public SepiaEffect()
		{
			UpdateShaderValue(InputProperty);
		}
	}
}
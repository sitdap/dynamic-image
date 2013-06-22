using System.Windows;
using System.Windows.Media;

namespace SoundInTheory.DynamicImage.ShaderEffects
{
	internal class FeatherEffect : ShaderEffectBase<FeatherEffect>
	{
		public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty("Input", typeof(FeatherEffect), 0);

		public Brush Input
		{
			get { return (Brush)GetValue(InputProperty); }
			set { SetValue(InputProperty, value); }
		}

		public static readonly DependencyProperty AlphaMaskProperty = RegisterPixelShaderSamplerProperty("AlphaMask", typeof(FeatherEffect), 1);

		public Brush AlphaMask
		{
			get { return (Brush)GetValue(AlphaMaskProperty); }
			set { SetValue(AlphaMaskProperty, value); }
		}

		public FeatherEffect()
		{
			UpdateShaderValue(InputProperty);
			UpdateShaderValue(AlphaMaskProperty);
		}
	}
}
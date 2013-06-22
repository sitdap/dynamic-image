using System.Windows;
using System.Windows.Media;

namespace SoundInTheory.DynamicImage.ShaderEffects
{
	internal class ContrastAdjustmentEffect : ShaderEffectBase<ContrastAdjustmentEffect>
	{
		public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty("Input", typeof(ContrastAdjustmentEffect), 0);

		public Brush Input
		{
			get { return (Brush)GetValue(InputProperty); }
			set { SetValue(InputProperty, value); }
		}

		public static readonly DependencyProperty LevelProperty = DependencyProperty.Register("Level", typeof(double), typeof(ContrastAdjustmentEffect), new UIPropertyMetadata(0.0, PixelShaderConstantCallback(0)));

		public double Level
		{
			get { return (double)GetValue(LevelProperty); }
			set { SetValue(LevelProperty, value); }
		}

		public ContrastAdjustmentEffect()
		{
			UpdateShaderValue(InputProperty);
			UpdateShaderValue(LevelProperty);
		}
	}
}
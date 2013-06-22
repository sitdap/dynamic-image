using System.Windows;
using System.Windows.Media;
using SWMColor = System.Windows.Media.Color;
using SWMColors = System.Windows.Media.Colors;

namespace SoundInTheory.DynamicImage.ShaderEffects
{
	internal class ColorTintEffect : ShaderEffectBase<ColorTintEffect>
	{
		public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty("Input", typeof(ColorTintEffect), 0);

		public Brush Input
		{
			get { return (Brush)GetValue(InputProperty); }
			set { SetValue(InputProperty, value); }
		}

		public static readonly DependencyProperty AmountProperty = DependencyProperty.Register("Amount", typeof(double), typeof(ColorTintEffect), new UIPropertyMetadata(0.0, PixelShaderConstantCallback(0)));

		public double Amount
		{
			get { return (double)GetValue(AmountProperty); }
			set { SetValue(AmountProperty, value); }
		}

		public static readonly DependencyProperty RequiredColorProperty = DependencyProperty.Register("RequiredColor", typeof(SWMColor), typeof(ColorTintEffect), new UIPropertyMetadata(SWMColors.Red, PixelShaderConstantCallback(1)));

		public SWMColor RequiredColor
		{
			get { return (SWMColor)GetValue(RequiredColorProperty); }
			set { SetValue(RequiredColorProperty, value); }
		}

		public ColorTintEffect()
		{
			UpdateShaderValue(InputProperty);
			UpdateShaderValue(AmountProperty);
			UpdateShaderValue(RequiredColorProperty);
		}
	}
}
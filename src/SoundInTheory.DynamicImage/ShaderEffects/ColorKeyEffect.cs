using System.Windows;
using System.Windows.Media;
using SWMColor = System.Windows.Media.Color;
using SWMColors = System.Windows.Media.Colors;

namespace SoundInTheory.DynamicImage.ShaderEffects
{
	internal class ColorKeyEffect : ShaderEffectBase<ColorKeyEffect>
	{
		public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty("Input", typeof(ColorKeyEffect), 0);

		public Brush Input
		{
			get { return (Brush)GetValue(InputProperty); }
			set { SetValue(InputProperty, value); }
		}

		public static readonly DependencyProperty ColorToleranceProperty = DependencyProperty.Register("ColorTolerance", typeof(double), typeof(ColorKeyEffect), new UIPropertyMetadata(0.0, PixelShaderConstantCallback(0)));

		public double ColorTolerance
		{
			get { return (double)GetValue(ColorToleranceProperty); }
			set { SetValue(ColorToleranceProperty, value); }
		}

		public static readonly DependencyProperty TransparentColorProperty = DependencyProperty.Register("TransparentColor", typeof(SWMColor), typeof(ColorKeyEffect), new UIPropertyMetadata(SWMColors.White, PixelShaderConstantCallback(1)));

		public SWMColor TransparentColor
		{
			get { return (SWMColor) GetValue(TransparentColorProperty); }
			set { SetValue(TransparentColorProperty, value); }
		}

		public ColorKeyEffect()
		{
			UpdateShaderValue(InputProperty);
			UpdateShaderValue(ColorToleranceProperty);
			UpdateShaderValue(TransparentColorProperty);
		}
	}
}
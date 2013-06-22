using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace SoundInTheory.DynamicImage.ShaderEffects
{
	internal class EmbossEffect : ShaderEffectBase<EmbossEffect>
	{
		public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty("Input", typeof(EmbossEffect), 0, SamplingMode.Bilinear);

		public Brush Input
		{
			get { return (Brush)GetValue(InputProperty); }
			set { SetValue(InputProperty, value); }
		}

		public static readonly DependencyProperty AmountProperty = DependencyProperty.Register("Amount", typeof(double), typeof(EmbossEffect), new UIPropertyMetadata(0.0, PixelShaderConstantCallback(0)));

		public double Amount
		{
			get { return (double)GetValue(AmountProperty); }
			set { SetValue(AmountProperty, value); }
		}

		public static readonly DependencyProperty WidthProperty = DependencyProperty.Register("Width", typeof(double), typeof(EmbossEffect), new UIPropertyMetadata(0.0, PixelShaderConstantCallback(1)));

		public double Width
		{
			get { return (double)GetValue(WidthProperty); }
			set { SetValue(WidthProperty, value); }
		}

		public EmbossEffect()
		{
			UpdateShaderValue(InputProperty);
			UpdateShaderValue(AmountProperty);
			UpdateShaderValue(WidthProperty);
		}
	}
}
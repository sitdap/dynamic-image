using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.ShaderEffects
{
	internal class EmbossEffect : ShaderEffect
	{
		[ThreadStatic]
		private static PixelShader _shader;

		private static PixelShader Shader
		{
			get { return (_shader ?? (_shader = ShaderEffectUtility.GetPixelShader("EmbossEffect"))); }
		}

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
			PixelShader = Shader;
			UpdateShaderValue(InputProperty);
			UpdateShaderValue(AmountProperty);
			UpdateShaderValue(WidthProperty);
		}
	}
}
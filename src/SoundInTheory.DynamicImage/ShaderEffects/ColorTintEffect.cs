using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.ShaderEffects
{
	internal class ColorTintEffect : ShaderEffect, IDisposable
	{
		[ThreadStatic]
		private static PixelShader _shader;

		private static PixelShader Shader
		{
			get { return (_shader ?? (_shader = ShaderEffectUtility.GetPixelShader("ColorTintEffect"))); }
		}

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

		public static readonly DependencyProperty RequiredColorProperty = DependencyProperty.Register("RequiredColor", typeof(Color), typeof(ColorTintEffect), new UIPropertyMetadata(Colors.Red, PixelShaderConstantCallback(1)));

		public Color RequiredColor
		{
			get { return (Color)GetValue(RequiredColorProperty); }
			set { SetValue(RequiredColorProperty, value); }
		}

		public ColorTintEffect()
		{
			PixelShader = Shader;
			UpdateShaderValue(InputProperty);
			UpdateShaderValue(AmountProperty);
			UpdateShaderValue(RequiredColorProperty);
		}

		void IDisposable.Dispose()
		{
			PixelShader = null;
		}
	}
}
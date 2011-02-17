using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.ShaderEffects
{
	internal class GrayscaleEffect : ShaderEffect
	{
		[ThreadStatic]
		private static PixelShader _shader;

		private static PixelShader Shader
		{
			get { return (_shader ?? (_shader = ShaderEffectUtility.GetPixelShader("GrayscaleEffect"))); }
		}

		public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty("Input", typeof(GrayscaleEffect), 0);

		public Brush Input
		{
			get { return (Brush)GetValue(InputProperty); }
			set { SetValue(InputProperty, value); }
		}

		public GrayscaleEffect()
		{
			PixelShader = Shader;
			UpdateShaderValue(InputProperty);
		}
	}
}
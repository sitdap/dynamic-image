using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.ShaderEffects
{
	internal class FeatherEffect : ShaderEffect
	{
		[ThreadStatic]
		private static PixelShader _shader;

		private static PixelShader Shader
		{
			get { return (_shader ?? (_shader = ShaderEffectUtility.GetPixelShader("FeatherEffect"))); }
		}

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
			PixelShader = Shader;
			UpdateShaderValue(InputProperty);
			UpdateShaderValue(AlphaMaskProperty);
		}
	}
}
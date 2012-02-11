using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.ShaderEffects
{
	internal class CurvesEffect : ShaderEffect
	{
		[ThreadStatic]
		private static PixelShader _shader;

		private static PixelShader Shader
		{
			get { return (_shader ?? (_shader = ShaderEffectUtility.GetPixelShader("CurvesEffect"))); }
		}

		public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty("Input", typeof(CurvesEffect), 0);

		public Brush Input
		{
			get { return (Brush)GetValue(InputProperty); }
			set { SetValue(InputProperty, value); }
		}

		public static readonly DependencyProperty CurvesLookupProperty = RegisterPixelShaderSamplerProperty("CurvesLookup", typeof(CurvesEffect), 1);

		public Brush CurvesLookup
		{
			get { return (Brush)GetValue(CurvesLookupProperty); }
			set { SetValue(CurvesLookupProperty, value); }
		}

		public CurvesEffect()
		{
			PixelShader = Shader;
			UpdateShaderValue(InputProperty);
			UpdateShaderValue(CurvesLookupProperty);
		}
	}
}
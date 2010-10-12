using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.ShaderEffects
{
	internal class TransferEffect : ShaderEffect
	{
		[ThreadStatic]
		private static PixelShader _shader;

		private static PixelShader Shader
		{
			get { return (_shader ?? (_shader = ShaderEffectUtility.GetPixelShader("TransferEffect"))); }
		}

		public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty("Input", typeof(TransferEffect), 0);

		public Brush Input
		{
			get { return (Brush)GetValue(InputProperty); }
			set { SetValue(InputProperty, value); }
		}

		public static readonly DependencyProperty TransferLookupProperty = RegisterPixelShaderSamplerProperty("TransferLookup", typeof(TransferEffect), 1);

		public Brush TransferLookup
		{
			get { return (Brush)GetValue(TransferLookupProperty); }
			set { SetValue(TransferLookupProperty, value); }
		}

		public TransferEffect()
		{
			PixelShader = Shader;
			UpdateShaderValue(InputProperty);
			UpdateShaderValue(TransferLookupProperty);
		}
	}
}
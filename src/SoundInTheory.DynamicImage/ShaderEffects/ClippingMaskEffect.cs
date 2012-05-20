using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.ShaderEffects
{
	internal class ClippingMaskEffect : ShaderEffect, IDisposable
	{
		[ThreadStatic]
		private static PixelShader _shader;

		private static PixelShader Shader
		{
			get { return (_shader ?? (_shader = ShaderEffectUtility.GetPixelShader("ClippingMaskEffect"))); }
		}

		public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty("Input", typeof(ClippingMaskEffect), 0, SamplingMode.Bilinear);

		public Brush Input
		{
			get { return (Brush)GetValue(InputProperty); }
			set { SetValue(InputProperty, value); }
		}

		public static readonly DependencyProperty MaskProperty = RegisterPixelShaderSamplerProperty("Mask", typeof(ClippingMaskEffect), 1);

		public Brush Mask
		{
			get { return (Brush)GetValue(MaskProperty); }
			set { SetValue(MaskProperty, value); }
		}

		public static readonly DependencyProperty InputCoordsOffsetProperty = DependencyProperty.Register("InputCoordsOffset", typeof(Vector), typeof(ClippingMaskEffect), new UIPropertyMetadata(new Vector(), PixelShaderConstantCallback(0)));

		public Vector InputCoordsOffset
		{
			get { return (Vector)GetValue(InputCoordsOffsetProperty); }
			set { SetValue(InputCoordsOffsetProperty, value); }
		}

		public static readonly DependencyProperty InputCoordsScaleProperty = DependencyProperty.Register("InputCoordsScale", typeof(Vector), typeof(ClippingMaskEffect), new UIPropertyMetadata(new Vector(), PixelShaderConstantCallback(1)));

		public Vector InputCoordsScale
		{
			get { return (Vector)GetValue(InputCoordsScaleProperty); }
			set { SetValue(InputCoordsScaleProperty, value); }
		}

		public ClippingMaskEffect()
		{
			PixelShader = Shader;
			UpdateShaderValue(InputProperty);
			UpdateShaderValue(MaskProperty);
			UpdateShaderValue(InputCoordsOffsetProperty);
			UpdateShaderValue(InputCoordsScaleProperty);
		}

		void IDisposable.Dispose()
		{
			PixelShader = null;
		}
	}
}
using System.Windows;
using System.Windows.Media;

namespace SoundInTheory.DynamicImage.ShaderEffects
{
	internal class CurvesEffect : ShaderEffectBase<CurvesEffect>
	{
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
			UpdateShaderValue(InputProperty);
			UpdateShaderValue(CurvesLookupProperty);
		}
	}
}
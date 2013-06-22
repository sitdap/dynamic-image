using System.Windows;
using System.Windows.Media;

namespace SoundInTheory.DynamicImage.ShaderEffects
{
	internal class TransferEffect : ShaderEffectBase<TransferEffect>
	{
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
			UpdateShaderValue(InputProperty);
			UpdateShaderValue(TransferLookupProperty);
		}
	}
}
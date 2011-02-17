using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.ShaderEffects.LayerBlending
{
	internal class LayerBlenderEffect : ShaderEffect
	{
		private const int RandomSize = 512;

		[ThreadStatic]
		private static Dictionary<BlendMode, PixelShader> _shaders;

		[ThreadStatic]
		private static BitmapSource _randomBitmap;

		private static Dictionary<BlendMode, PixelShader> Shaders
		{
			get
			{
				if (_shaders == null)
				{
					_shaders = new Dictionary<BlendMode, PixelShader>();

					// Add shader for each BlendMode. This loops through the values of the BlendMode enum,
					// doing something like this for each value:
					// Shaders.Add(BlendMode.Normal, ShaderEffectUtility.GetPixelShader("LayerBlenderEffectNormal"));
					foreach (BlendMode blendMode in Enum.GetValues(typeof(BlendMode)))
						_shaders.Add(blendMode, ShaderEffectUtility.GetPixelShader(@"LayerBlending\LayerBlenderEffect" + blendMode));
				}
				return _shaders;
			}
		}

		private static BitmapSource RandomBitmap
		{
			get
			{
				if (_randomBitmap == null)
				{
					FastBitmap randomImage = new FastBitmap(RandomSize, RandomSize);
					Random random = new Random();
					randomImage.Lock();
					for (int y = 0; y < RandomSize; ++y)
						for (int x = 0; x < RandomSize; ++x)
							randomImage[x, y] = Color.FromRgb((byte)(random.NextDouble() * 255), 0, 0);
					randomImage.Unlock();
					_randomBitmap = randomImage.InnerBitmap;
				}
				return _randomBitmap;
			}
		}

		public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty("Input", typeof(LayerBlenderEffect), 0);

		public Brush Input
		{
			get { return (Brush)GetValue(InputProperty); }
			set { SetValue(InputProperty, value); }
		}

		public static readonly DependencyProperty BackgroundProperty = RegisterPixelShaderSamplerProperty("Background", typeof(LayerBlenderEffect), 1);

		public Brush Background
		{
			get { return (Brush)GetValue(BackgroundProperty); }
			set { SetValue(BackgroundProperty, value); }
		}

		public static readonly DependencyProperty RandomBrushProperty = RegisterPixelShaderSamplerProperty("RandomBrush", typeof(LayerBlenderEffect), 2);

		public LayerBlenderEffect(BlendMode blendMode, int outputWidth, int outputHeight)
		{
			PixelShader = Shaders[blendMode];
			UpdateShaderValue(InputProperty);
			UpdateShaderValue(BackgroundProperty);

			if (blendMode == BlendMode.Dissolve)
			{
				SetValue(RandomBrushProperty, new ImageBrush(RandomBitmap)
				{
					TileMode = TileMode.Tile,
					Stretch = Stretch.None,
					ViewportUnits = BrushMappingMode.RelativeToBoundingBox,
					Viewport = new Rect(0, 0, RandomSize/(double) outputWidth, RandomSize/(double) outputHeight)
				});
				UpdateShaderValue(RandomBrushProperty);
			}
		}
	}
}
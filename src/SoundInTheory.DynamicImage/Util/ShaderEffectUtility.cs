using System;
using System.Reflection;
using System.Windows;
using System.Windows.Media.Effects;

namespace SoundInTheory.DynamicImage.Util
{
	internal static class ShaderEffectUtility
	{
		static ShaderEffectUtility()
		{
			// Bootstrap WPF so that pack URIs work.
			Application.ResourceAssembly = Assembly.GetExecutingAssembly();
		}

		public static PixelShader GetPixelShader(string name)
		{
			return new PixelShader
			{
				UriSource = new Uri(@"pack://application:,,,/SoundInTheory.DynamicImage;component/ShaderEffects/" + name + ".ps")
			};
		}
	}
}
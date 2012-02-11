using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SoundInTheory.DynamicImage.Util
{
	public static class RenderTargetBitmapUtility
	{
		public static RenderTargetBitmap CreateRenderTargetBitmap(int width, int height)
		{
			return new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
		}
	}
}
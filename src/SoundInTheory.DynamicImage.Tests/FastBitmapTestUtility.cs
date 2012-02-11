using SoundInTheory.DynamicImage.Util;
using NUnit.Framework;

namespace SoundInTheory.DynamicImage.Tests
{
	public static class FastBitmapTestUtility
	{
		public static void AssertEqual(FastBitmap expected, FastBitmap actual)
		{
			Assert.AreEqual(expected.Width, actual.Width);
			Assert.AreEqual(expected.Height, actual.Height);

			try
			{
				expected.Lock();
				actual.Lock();

				for (int y = 0, height = expected.Height; y < height; ++y)
					for (int x = 0, width = expected.Width; x < width; ++x)
						Assert.AreEqual(expected[x, y], actual[x, y]);
			}
			finally
			{
				actual.Unlock();
				expected.Unlock();
			}
		}
	}
}
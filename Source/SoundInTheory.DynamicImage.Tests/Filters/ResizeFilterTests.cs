using System;
using SoundInTheory.DynamicImage.Filters;
using SoundInTheory.DynamicImage.Util;
using NUnit.Framework;

namespace SoundInTheory.DynamicImage.Tests.Filters
{
	[TestFixture]
	public class ResizeFilterTests
	{
		[Test]
		public void ResizeFilter_UseWidthResizeMode_CalculatedCorrectly()
		{
			FastBitmap bitmap = new FastBitmap("Filters\\Images\\Tulips.png", UriKind.Relative);

			ResizeFilter resizeFilter = new ResizeFilter();
			resizeFilter.Mode = ResizeMode.UseWidth;
			resizeFilter.Width = Unit.Pixel(200);

			resizeFilter.ApplyFilter(bitmap);

			Assert.AreEqual(200, bitmap.Width);
			Assert.AreEqual(133, bitmap.Height);
		}

		[Test]
		public void ResizeFilter_UseHeightResizeMode_CalculatedCorrectly()
		{
			FastBitmap bitmap = new FastBitmap("Filters\\Images\\Tulips.png", UriKind.Relative);

			ResizeFilter resizeFilter = new ResizeFilter();
			resizeFilter.Mode = ResizeMode.UseHeight;
			resizeFilter.Height = Unit.Pixel(200);

			resizeFilter.ApplyFilter(bitmap);

			Assert.AreEqual(300, bitmap.Width);
			Assert.AreEqual(200, bitmap.Height);
		}

		[Test]
		public void ResizeFilter_FillResizeMode_CalculatedCorrectly()
		{
			FastBitmap bitmap = new FastBitmap("Filters\\Images\\Tulips.png", UriKind.Relative);

			ResizeFilter resizeFilter = new ResizeFilter();
			resizeFilter.Mode = ResizeMode.Fill;
			resizeFilter.Width = Unit.Pixel(250);
			resizeFilter.Height = Unit.Pixel(249);

			resizeFilter.ApplyFilter(bitmap);

			Assert.AreEqual(250, bitmap.Width);
			Assert.AreEqual(249, bitmap.Height);
		}

		[Test]
		public void ResizeFilter_UniformResizeModeWithDominantWidth_CalculatedCorrectly()
		{
			FastBitmap bitmap = new FastBitmap("Filters\\Images\\Tulips.png", UriKind.Relative);

			ResizeFilter resizeFilter = new ResizeFilter();
			resizeFilter.Mode = ResizeMode.Uniform;
			resizeFilter.Width = Unit.Pixel(200);
			resizeFilter.Height = Unit.Pixel(200);

			resizeFilter.ApplyFilter(bitmap);

			Assert.AreEqual(200, bitmap.Width);
			Assert.AreEqual(133, bitmap.Height);
		}

		[Test]
		public void ResizeFilter_UniformResizeModeWithDominantHeight_CalculatedCorrectly()
		{
			FastBitmap bitmap = new FastBitmap("Filters\\Images\\Tulips.png", UriKind.Relative);

			ResizeFilter resizeFilter = new ResizeFilter();
			resizeFilter.Mode = ResizeMode.Uniform;
			resizeFilter.Width = Unit.Pixel(200);
			resizeFilter.Height = Unit.Pixel(100);

			resizeFilter.ApplyFilter(bitmap);

			Assert.AreEqual(150, bitmap.Width);
			Assert.AreEqual(100, bitmap.Height);
		}

		[Test]
		public void ResizeFilter_UniformFillResizeMode_CalculatedCorrectly()
		{
			FastBitmap bitmap = new FastBitmap("Filters\\Images\\Tulips.png", UriKind.Relative);

			ResizeFilter resizeFilter = new ResizeFilter();
			resizeFilter.Mode = ResizeMode.Fill;
			resizeFilter.Width = Unit.Pixel(250);
			resizeFilter.Height = Unit.Pixel(250);

			resizeFilter.ApplyFilter(bitmap);

			Assert.AreEqual(250, bitmap.Width);
			Assert.AreEqual(250, bitmap.Height);
		}
	}
}
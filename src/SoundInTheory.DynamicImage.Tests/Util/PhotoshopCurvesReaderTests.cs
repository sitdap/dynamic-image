using NUnit.Framework;
using SoundInTheory.DynamicImage.Filters;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.Tests.Util
{
	[TestFixture]
	public class PhotoshopCurvesReaderTests
	{
		[Test]
		public void CanReadAcvFile()
		{
			// Act.
			CurveCollection curves = PhotoshopCurvesReader.ReadPhotoshopCurvesFile(@"Util\Cross_process___Photoshop__acv_by_LikeGravity.acv");

			// Assert.
			Assert.AreEqual(5, curves.Count);
		}
	}
}
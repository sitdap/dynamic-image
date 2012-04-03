using NUnit.Framework;
using SoundInTheory.DynamicImage;

namespace SoundInTheory.DynamicImage.Tests
{
	[TestFixture]
	public class FontFileDefaultsToVerdana
	{
		[Test]
		public void FontFileShouldDefaultToNull()
		{
			// Arrange.
			var font = new Font();

			// Act.

			// Assert.
			Assert.That(font.CustomFontFile, Is.Null);
		}

		[Test]
		public void FontFileShouldCustomString()
		{
			// Arrange.
			var font = new Font();

			// Act.
			font.CustomFontFile = "FooBar";

			// Assert.
			Assert.That(font.CustomFontFile, Is.EqualTo("FooBar"));
		}
	}
}
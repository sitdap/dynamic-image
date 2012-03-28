using NUnit.Framework;
using SoundInTheory.DynamicImage.Fluent;

namespace SoundInTheory.DynamicImage.Tests
{
	[TestFixture]
	public class CompositionTests
	{
		[Test]
		public void CacheKeysDifferWhenResizeFilterDiffersOnlyByWidth()
		{
			// Arrange.
			var compositionBuilder1 = new CompositionBuilder()
				.WithLayer(LayerBuilder.Text
					.WithFilter(FilterBuilder.Resize.ToWidth(300)));
			var compositionBuilder2 = new CompositionBuilder()
				.WithLayer(LayerBuilder.Text
					.WithFilter(FilterBuilder.Resize.ToWidth(50)));

			// Act.
			var cacheKey1 = compositionBuilder1.Composition.GetCacheKey();
			var cacheKey2 = compositionBuilder2.Composition.GetCacheKey();

			// Assert.
			Assert.That(cacheKey1, Is.Not.EqualTo(cacheKey2));
		}
	}
}
using System.Linq;
using SoundInTheory.DynamicImage.Caching;

namespace SoundInTheory.DynamicImage.Web
{
	public static class ImageUrlGenerator
	{
		public static string GetImageUrl(Composition composition)
		{
			string cacheKey = composition.GetDirtyProperties();

			if (DynamicImageCacheManager.Exists(cacheKey))
				return DynamicImageCacheManager.GetProperties(cacheKey).Url;

			CompositionImage compositionImage = composition.GetCompositionImage(cacheKey);
			if (compositionImage.Properties.IsImagePresent || DynamicImageCacheManager.StoreMissingImagesInCache)
			{
				Dependency[] dependencies = composition.GetDependencies().Distinct().ToArray();
				DynamicImageCacheManager.Add(cacheKey, compositionImage, dependencies);
			}
			return compositionImage.Properties.Url;
		}
	}
}
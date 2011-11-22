using System.Linq;
using System.Security.Cryptography;
using System.Text;
using SoundInTheory.DynamicImage.Caching;

namespace SoundInTheory.DynamicImage
{
	internal static class ImageManager
	{
		public static string GetFileName(ImageProperties imageProperties)
		{
			return Util.Util.CalculateShaHash(imageProperties.UniqueKey);
		}

		/// <summary>
		/// Gets the properties for the specified image, respecting caching settings.
		/// </summary>
		/// <param name="composition"></param>
		/// <returns></returns>
		public static ImageProperties GetImageProperties(Composition composition)
		{
			string cacheKey = composition.GetDirtyProperties();

			ImageProperties imageProperties;
			if (!DynamicImageCacheManager.Exists(cacheKey))
			{
				CompositionImage compositionImage = composition.GetCompositionImage(cacheKey);
				if (compositionImage.Properties.IsImagePresent || DynamicImageCacheManager.StoreMissingImagesInCache)
				{
					Dependency[] dependencies = composition.GetDependencies().Distinct().ToArray();
					DynamicImageCacheManager.Add(cacheKey, compositionImage, dependencies);
				}

				imageProperties = compositionImage.Properties;
			}
			else
			{
				imageProperties = DynamicImageCacheManager.GetProperties(cacheKey);
			}

			return imageProperties;
		}
	}
}
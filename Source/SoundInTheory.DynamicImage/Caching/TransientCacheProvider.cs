using System.Web;

namespace SoundInTheory.DynamicImage.Caching
{
	/// <summary>
	/// This is how the "no caching" option is implemented.
	/// </summary>
	internal class TransientCacheProvider : InProcDynamicImageCacheProvider
	{
		public override bool ExistsInCache(string cacheKey)
		{
			return false;
		}

		public override void SendImageToHttpResponse(HttpContext context, string cacheProviderKey, string fileExtension)
		{
			base.SendImageToHttpResponse(context, cacheProviderKey, fileExtension);

			InProcCacheEntry cacheEntry = GetCacheEntry(cacheProviderKey);
			Cache.Remove(cacheEntry.CompositionImage.Properties.UniqueKey);
		}
	}
}
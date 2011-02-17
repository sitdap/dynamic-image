using System;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Collections;

namespace SoundInTheory.DynamicImage.Caching
{
	internal class InProcDynamicImageCacheProvider : DynamicImageCacheProvider
	{
		private Cache _cache;

		public Cache Cache
		{
			get
			{
				if (_cache == null)
					_cache = HttpRuntime.Cache;
				return _cache;
			}
		}

		public override bool ExistsInCache(string cacheKey)
		{
			return (Cache.Get(cacheKey) != null);
		}

		public override void AddToCache(string cacheKey, CompositionImage compositionImage, Dependency[] dependencies)
		{
			InProcCacheEntry cacheEntry = new InProcCacheEntry
			{
				CompositionImage = compositionImage,
				Dependencies = dependencies
			};
			Cache.Insert(cacheKey, cacheEntry);
		}

		public override ImageProperties GetPropertiesFromCache(string cacheKey)
		{
			return ((InProcCacheEntry)Cache.Get(cacheKey)).CompositionImage.Properties;
		}

		public override void RemoveAllFromCache()
		{
			Cache.Cast<DictionaryEntry>().Where(de => de.Value is InProcCacheEntry).ToList()
				.ForEach(de => Cache.Remove((string) de.Key));
		}

		public override void RemoveFromCache(Dependency dependency)
		{
			foreach (var dictionaryEntry in Cache.Cast<DictionaryEntry>().Where(de => de.Value is InProcCacheEntry))
			{
				InProcCacheEntry cacheEntry = (InProcCacheEntry) dictionaryEntry.Value;
				if (cacheEntry.Dependencies.Contains(dependency))
					Cache.Remove((string) dictionaryEntry.Key);
			}
		}

		public override DateTime GetImageLastModifiedDate(HttpContext context, string cacheProviderKey, string fileExtension)
		{
			return DateTime.Now;
		}

		protected InProcCacheEntry GetCacheEntry(string cacheProviderKey)
		{
			foreach (var dictionaryEntry in Cache.Cast<DictionaryEntry>().Where(de => de.Value is InProcCacheEntry))
				if (((InProcCacheEntry)dictionaryEntry.Value).CompositionImage.Properties.CacheProviderKey == cacheProviderKey)
					return (InProcCacheEntry)dictionaryEntry.Value;
			throw new InvalidOperationException("Could not find image in cache");
		}

		public override void SendImageToHttpResponse(HttpContext context, string cacheProviderKey, string fileExtension)
		{
			InProcCacheEntry cacheEntry = GetCacheEntry(cacheProviderKey);
			Util.Util.SendImageToHttpResponse(context, cacheEntry.CompositionImage);
		}

		internal class InProcCacheEntry
		{
			public CompositionImage CompositionImage;
			public Dependency[] Dependencies;
		}
	}
}

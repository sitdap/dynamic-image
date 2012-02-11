using System;
using System.IO;
using System.Web;
using System.Windows.Media.Imaging;

namespace SoundInTheory.DynamicImage.Caching
{
	public abstract class DiskCacheProviderBase : DynamicImageCacheProvider
	{
		public override DateTime GetImageLastModifiedDate(HttpContext context, string cacheKey, string fileExtension)
		{
			string filePath = GetDiskCacheFilePath(context, cacheKey, fileExtension);
			return File.GetLastWriteTime(context.Server.MapPath(filePath));
		}

		public override void SendImageToHttpResponse(HttpContext context, string cacheKey, string fileExtension)
		{
			// Instead of sending image directly to the response, just call RewritePath and let IIS
			// handle the actual serving of the image.
			string filePath = GetDiskCacheFilePath(context, cacheKey, fileExtension);

			context.Items["FinalCachedFile"] = context.Server.MapPath(filePath);

			context.RewritePath(filePath, false);
		}

		private static string GetDiskCacheFilePath(HttpContext httpContext, string cacheProviderKey, string fileExtension)
		{
			if (httpContext == null)
				throw new InvalidOperationException("HttpContext.Current is null; SqlCacheProviderBase only supports being run within the context of a web request.");

			string imageCacheFolder = httpContext.Server.MapPath("~/App_Data/DynamicImage");
			if (!Directory.Exists(imageCacheFolder))
				Directory.CreateDirectory(imageCacheFolder);

			string fileName = cacheProviderKey + "." + fileExtension;
			string filePath = string.Format("~/App_Data/DynamicImage/{0}", fileName);
			return filePath;
		}

		protected void SaveImageToDiskCache(string cacheKey, GeneratedImage generatedImage)
		{
			if (!generatedImage.Properties.IsImagePresent)
				return;

			HttpContext httpContext = HttpContext.Current;
			string filePath = httpContext.Server.MapPath(GetDiskCacheFilePath(httpContext, cacheKey, generatedImage.Properties.FileExtension));

			using (FileStream fileStream = File.OpenWrite(filePath))
			{
				BitmapEncoder encoder = generatedImage.Properties.GetEncoder();
				encoder.Frames.Add(BitmapFrame.Create(generatedImage.Image));
				encoder.Save(fileStream);
			}
		}

		protected void DeleteImageFromDiskCache(string cacheKey, ImageProperties imageProperties, HttpContext httpContext)
		{
			string filePath = httpContext.Server.MapPath(GetDiskCacheFilePath(httpContext, cacheKey, imageProperties.FileExtension));
			DeleteImageFromDiskCache(imageProperties, filePath);
		}

		protected void DeleteImageFromDiskCache(ImageProperties imageProperties, string filePath)
		{
			if (!imageProperties.IsImagePresent)
				return;

			if (File.Exists(filePath))
				File.Delete(filePath);
		}
	}
}
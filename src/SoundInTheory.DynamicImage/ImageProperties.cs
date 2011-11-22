using System;
using System.Web;
using System.Windows.Media.Imaging;

namespace SoundInTheory.DynamicImage
{
	[Serializable]
	public class ImageProperties
	{
		public string CacheProviderKey;

		public string UniqueKey;

		/// <summary>
		/// Image might be null due to, for example, the source column in the database containing null
		/// </summary>
		public bool IsImagePresent;

		public DynamicImageFormat Format;
		public int? Width;
		public int? Height;
		public int ColourDepth;
		public int? JpegCompressionLevel;

		public BitmapEncoder GetEncoder()
		{
			BitmapEncoder encoder = Util.Util.GetEncoder(Format);

			if (encoder is JpegBitmapEncoder)
			{
				JpegBitmapEncoder jpegEncoder = (JpegBitmapEncoder)encoder;
				if (Format == DynamicImageFormat.Jpeg && JpegCompressionLevel != null)
					jpegEncoder.QualityLevel = JpegCompressionLevel.Value;
			}

			return encoder;
		}

		/// <summary>
		/// Gets the file extension, not including a '.'
		/// </summary>
		public string FileExtension
		{
			get
			{
				switch (Format)
				{
					case DynamicImageFormat.Bmp:
						return "bmp";
					case DynamicImageFormat.Gif:
						return "gif";
					case DynamicImageFormat.Jpeg:
						return "jpg";
					case DynamicImageFormat.Png:
						return "png";
					default:
						throw new InvalidOperationException("Unrecognised DynamicImageFormat");
				}
			}
		}

		public string MimeType
		{
			// TODO: Check that plural MimeTypes is okay.
			get { return GetEncoder().CodecInfo.MimeTypes; }
		}

		public string Url
		{
			get
			{
				const string path = "~/Assets/Images/DynamicImages/";
				string fileName = string.Format("{0}.{1}", CacheProviderKey, FileExtension);
				return VirtualPathUtility.ToAbsolute(path) + fileName;
			}
		}
	}
}
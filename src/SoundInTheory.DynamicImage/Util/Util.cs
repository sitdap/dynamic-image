using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Windows.Media.Imaging;

namespace SoundInTheory.DynamicImage.Util
{
	public static class Util
	{
		public static BitmapEncoder GetEncoder(DynamicImageFormat format)
		{
			switch (format)
			{
				case DynamicImageFormat.Bmp :
					return new BmpBitmapEncoder();
				case DynamicImageFormat.Gif :
					return new GifBitmapEncoder();
				case DynamicImageFormat.Jpeg :
					return new JpegBitmapEncoder();
				case DynamicImageFormat.Png :
					return new PngBitmapEncoder();
				default :
					throw new NotSupportedException();
			}
		}

		public static void SendImageToHttpResponse(HttpContext context, GeneratedImage generatedImage)
		{
			context.Response.ContentType = generatedImage.Properties.MimeType;
			SaveImageStream(generatedImage, context.Response.OutputStream);
			context.Response.Flush();
		}

		private static void SaveImageStream(GeneratedImage generatedImage, Stream stream)
		{
			// setup parameters
			BitmapEncoder encoder = generatedImage.Properties.GetEncoder();

			//encoderParametersTemp.Add(new EncoderParameter(Encoder.ColorDepth, (long)GeneratedImage.Properties.ColorDepth));
			// TODO: Use ColorConvertedBitmap to allow configurable colour depth in output image.

			encoder.Frames.Add(BitmapFrame.Create(generatedImage.Image));

			// Write to temporary stream first. this is because PNG must be written to a seekable stream.
			using (var tempStream = new MemoryStream())
			{
				encoder.Save(tempStream);

				// Now write temp stream to output stream.
				tempStream.WriteTo(stream);
			}
		}

		/// <summary>
		/// Calculates SHA1 hash
		/// </summary>
		/// <param name="text">input string</param>
		/// <param name="encoding">Character encoding</param>
		/// <returns>SHA1 hash</returns>
		public static string CalculateShaHash(string text, Encoding encoding)
		{
			SHA256 h = SHA256.Create();
			byte[] hash = h.ComputeHash(encoding.GetBytes(text));
			// Can't use base64 hash... filesystem has case-insensitive lookup.
			// Would use base32, but too much code to bloat the resizer. Simple base16 encoding is fine
			return Base16Encode(hash);
		}

		/// <summary>
		/// Returns a lowercase hexadecimal encoding of the specified binary data
		/// </summary>
		private static string Base16Encode(byte[] bytes)
		{
			StringBuilder sb = new StringBuilder(bytes.Length * 2);
			foreach (byte b in bytes)
				sb.Append(b.ToString("x").PadLeft(2, '0'));
			return sb.ToString();
		}

		/// <summary>
		/// Calculates SHA1 hash
		/// </summary>
		/// <param name="text">input string</param>
		/// <returns>SHA1 hash</returns>
		public static string CalculateShaHash(string text)
		{
			return CalculateShaHash(text, Encoding.UTF8);
		}
	}
}
using System;
using System.IO;

namespace SoundInTheory.DynamicImage.Util
{
	public static class FileSourceHelper
	{
		public static string ResolveFileName(ImageGenerationContext context, string filename)
		{
			string fileName = null;
			if (!Path.IsPathRooted(filename))
			{
				if (context.HttpContext != null)
					fileName = context.HttpContext.Server.MapPath(filename);

				if (fileName == null)
					throw new InvalidOperationException("Could not resolve source filename.");
			}
			else
			{
				fileName = filename;
			}

			return fileName;
		}
	}
}

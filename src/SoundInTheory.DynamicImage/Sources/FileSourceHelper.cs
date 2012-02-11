using System;
using System.Web;
using System.IO;

namespace SoundInTheory.DynamicImage.Sources
{
	public static class FileSourceHelper
	{
		public static string ResolveFileName(string filename)
		{
			string fileName = null;
			if (!Path.IsPathRooted(filename))
			{
				if (HttpContext.Current != null)
					fileName = HttpContext.Current.Server.MapPath(filename);

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

using System;
using System.IO;

namespace SoundInTheory.DynamicImage.Util
{
	internal static class GhostscriptUtil
	{
		public static void EnsureDll(ImageGenerationContext context)
		{
			string folder = (context.HttpContext == null)
				? AppDomain.CurrentDomain.BaseDirectory
				: context.HttpContext.Server.MapPath("~/bin");

			string gsDllPath = Path.Combine(folder, "gsdll.dll");

			if (!File.Exists(gsDllPath))
			{
				string gsDllFile = Environment.Is64BitProcess ? "gsdll64.dll" : "gsdll32.dll";
				using (var stream = typeof(GhostscriptUtil).Assembly.GetManifestResourceStream("SoundInTheory.DynamicImage.Resources." + gsDllFile))
				using (var fileStream = File.OpenWrite(gsDllPath))
					stream.CopyTo(fileStream);
			}
		}
	}
}
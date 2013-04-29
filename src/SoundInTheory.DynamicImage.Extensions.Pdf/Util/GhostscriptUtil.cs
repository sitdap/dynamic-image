using System;
using System.IO;
using System.Web;

namespace SoundInTheory.DynamicImage.Util
{
	internal static class GhostscriptUtil
	{
		public static void EnsureDll()
		{
			string folder = (HttpContext.Current == null)
				? AppDomain.CurrentDomain.BaseDirectory
				: HttpContext.Current.Server.MapPath("~/bin");

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
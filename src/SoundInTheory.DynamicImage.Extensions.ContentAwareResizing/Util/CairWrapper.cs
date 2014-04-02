using System;
using System.Diagnostics;
using System.IO;
using SoundInTheory.DynamicImage.Filters;

namespace SoundInTheory.DynamicImage.Util
{
	/// <summary>
	/// Thanks to http://sites.google.com/site/brainrecall/cair.
	/// </summary>
	internal class CairWrapper
	{
	    private static string _cairPath;

		private static string GetCairFolder(ImageGenerationContext context)
		{
			string folder = (context.HttpContext == null)
				? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CAIR")
                : context.HttpContext.Server.MapPath("~/App_Data/CAIR");
			if (!Directory.Exists(folder))
				Directory.CreateDirectory(folder);
			return folder;
		}

		private static string GetCairPath(ImageGenerationContext context)
		{
			return Path.Combine(GetCairFolder(context), "CAIR.exe");
		}

		public string CairDefaultArguments { get; set; }

		public CairWrapper(ImageGenerationContext context)
		{
			// Extract CAIR.exe and pthreadVSE2.dll (stored as embedded resources in this DLL)
		    _cairPath = GetCairPath(context);
			if (!File.Exists(_cairPath))
			{
				using (var stream = typeof(CairWrapper).Assembly.GetManifestResourceStream("SoundInTheory.DynamicImage.Resources.CAIR.exe"))
                using (var fileStream = File.OpenWrite(_cairPath))
					stream.CopyTo(fileStream);

				using (var stream = typeof(CairWrapper).Assembly.GetManifestResourceStream("SoundInTheory.DynamicImage.Resources.pthreadVSE2.dll"))
				using (var fileStream = File.OpenWrite(Path.Combine(GetCairFolder(context), "pthreadVSE2.dll")))
					stream.CopyTo(fileStream);
			}
		}

		public bool ProcessImage(string sourceFile, string destinationFile, int timeout, int width, int height,
			ContentAwareResizeFilterConvolutionType convolutionType)
		{
			string runArguments = " -I \"" + sourceFile + "\" -O \"" + destinationFile + "\" -T 1 -C " + convolutionType;
			runArguments += " -X " + width;
			runArguments += " -Y " + height;

            var info = new ProcessStartInfo(_cairPath, runArguments)
			{
				UseShellExecute = false,
				RedirectStandardInput = true,
				RedirectStandardError = true,
				RedirectStandardOutput = true,
				CreateNoWindow = true
			};
			using (Process scr = Process.Start(info))
			{
				bool result = scr.WaitForExit(timeout);
				if (!result)
					scr.Kill();
				return result;
			}
		}
	}
}
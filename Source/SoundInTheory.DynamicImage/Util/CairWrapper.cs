using System;
using System.Diagnostics;
using System.IO;
using System.Web;
using SoundInTheory.DynamicImage.Filters;

namespace SoundInTheory.DynamicImage.Util
{
	/// <summary>
	/// Thanks to http://sites.google.com/site/brainrecall/cair.
	/// </summary>
	internal class CairWrapper
	{
		public string CairPath { get; set; }
		public string CairDefaultArguments { get; set; }

		public CairWrapper()
		{
			CairPath = HttpContext.Current.Server.MapPath("~/App_Data/DynamicImage/CAIR.exe"); // must be within the web root
		}

		public bool ProcessImage(string sourceFile, string destinationFile, int timeout, int width, int height,
			ContentAwareResizeFilterConvolutionType convolutionType)
		{
			string runArguments = " -I \"" + sourceFile + "\" -O \"" + destinationFile + "\" -T 1 -C " + convolutionType;
			runArguments += " -X " + width;
			runArguments += " -Y " + height;

			if (!File.Exists(CairPath))
				throw new DynamicImageException("Could not find CAIR.exe. This file needs to be in ~/App_Data/DynamicImage.");

			ProcessStartInfo info = new ProcessStartInfo(CairPath, runArguments)
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
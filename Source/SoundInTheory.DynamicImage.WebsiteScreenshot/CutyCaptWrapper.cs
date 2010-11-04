using System;
using System.Diagnostics;
using System.IO;
using System.Web;

namespace SoundInTheory.DynamicImage
{
	/// <summary>
	/// Thanks to http://nolovelust.com/post/C-Website-Screenshot-Generator-AKA-Get-Screenshot-of-Webpage-With-Aspnet-C.aspx
	/// for providing the starting point for this class.
	/// </summary>
	public class CutyCaptWrapper
	{
		public string CutyCaptPath { get; set; }
		public string CutyCaptDefaultArguments { get; set; }

		public CutyCaptWrapper()
		{
			CutyCaptPath = HttpContext.Current.Server.MapPath("~/App_Data/DynamicImage/CutyCapt.exe"); // must be within the web root
			CutyCaptDefaultArguments = " --max-wait=0 --out-format=png --javascript=off --java=off --plugins=off --js-can-open-windows=off --js-can-access-clipboard=off --private-browsing=on";
		}

		public bool SaveScreenShot(string url, string destinationFile, int timeout)
		{
			if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
				return false;

			string runArguments = " --url=" + url + " --out=" + destinationFile + CutyCaptDefaultArguments;

			if (!File.Exists(CutyCaptPath))
				throw new DynamicImageException("Could not find CutyCapt.exe. This file needs to be in ~/App_Data/DynamicImage.");

			ProcessStartInfo info = new ProcessStartInfo(CutyCaptPath, runArguments)
			{
				UseShellExecute = false,
				RedirectStandardInput = true,
				RedirectStandardError = true,
				RedirectStandardOutput = true,
				CreateNoWindow = true,
				//WorkingDirectory = CutyCaptWorkingDirectory;
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
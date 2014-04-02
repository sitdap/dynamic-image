using System;
using System.Diagnostics;
using System.IO;

namespace SoundInTheory.DynamicImage.Util
{
	/// <summary>
	/// Thanks to http://nolovelust.com/post/C-Website-Screenshot-Generator-AKA-Get-Screenshot-of-Webpage-With-Aspnet-C.aspx
	/// for providing the starting point for this class.
	/// </summary>
	public class CutyCaptWrapper
	{
	    private readonly string _cutyCaptPath;

		private static string GetCutyCaptFolder(ImageGenerationContext context)
		{
            string folder = (context.HttpContext == null)
				? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CutyCapt")
                : context.HttpContext.Server.MapPath("~/App_Data/CutyCapt");
			if (!Directory.Exists(folder))
				Directory.CreateDirectory(folder);
			return folder;
		}

        private static string GetCutyCaptPath(ImageGenerationContext context)
		{
            return Path.Combine(GetCutyCaptFolder(context), "CutyCapt.exe");
		}

		public string CutyCaptDefaultArguments { get; set; }

		public CutyCaptWrapper(ImageGenerationContext context)
		{
            _cutyCaptPath = GetCutyCaptPath(context);
            if (!File.Exists(_cutyCaptPath))
			{
				using (var stream = typeof(CutyCaptWrapper).Assembly.GetManifestResourceStream("SoundInTheory.DynamicImage.Resources.CutyCapt.exe"))
                using (var fileStream = File.OpenWrite(_cutyCaptPath))
					stream.CopyTo(fileStream);
			}

            CutyCaptDefaultArguments = " --max-wait=0 --out-format=png --javascript=off --java=off --plugins=off --js-can-open-windows=off --js-can-access-clipboard=off --private-browsing=on";
		}

		public bool SaveScreenShot(string url, string destinationFile, int timeout, int width)
		{
			if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
				return false;

            string runArguments = " --url=" + url + " --out=" + destinationFile + CutyCaptDefaultArguments + " --min-width=" + width;

            var info = new ProcessStartInfo(_cutyCaptPath, runArguments)
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
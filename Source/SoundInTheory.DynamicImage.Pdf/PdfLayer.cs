using System.ComponentModel;
using System.IO;
using System.Web.UI;
using SoundInTheory.DynamicImage.Sources;

namespace SoundInTheory.DynamicImage
{
	public class PdfLayer : Layer
	{
		[Category("Source"), UrlProperty]
		public string SourceFileName
		{
			get { return ViewState["SourceFileName"] as string ?? string.Empty; }
			set { ViewState["SourceFileName"] = value; }
		}

		[DefaultValue(1)]
		public int PageNumber
		{
			get { return (int)(ViewState["PageNumber"] ?? 1); }
			set { ViewState["PageNumber"] = value; }
		}

		public override bool HasFixedSize
		{
			get { return true; }
		}

		protected override void CreateImage()
		{
			string outputFileName = Path.GetTempFileName();

			try
			{
				string filename = FileSourceHelper.ResolveFileName(SourceFileName, Site, DesignMode);
				GhostscriptSharp.GhostscriptWrapper.GeneratePageThumb(filename, outputFileName, PageNumber, 96, 96);

				Bitmap = new Util.FastBitmap(File.ReadAllBytes(outputFileName));
			}
			finally
			{
				File.Delete(outputFileName);
			}
		}
	}
}
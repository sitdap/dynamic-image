using System.ComponentModel;
using System.IO;
using System.Web.UI;

namespace SoundInTheory.DynamicImage
{
	public class WebsiteScreenshotLayer : Layer
	{
		[Category("Source"), UrlProperty]
		public string WebsiteUrl
		{
			get { return ViewState["WebsiteUrl"] as string ?? string.Empty; }
			set { ViewState["WebsiteUrl"] = value; }
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
				if (!new CutyCaptWrapper().SaveScreenShot(WebsiteUrl, outputFileName))
					return;
				Bitmap = new Util.FastBitmap(File.ReadAllBytes(outputFileName));
			}
			finally
			{
				File.Delete(outputFileName);
			}
		}
	}
}
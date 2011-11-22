using System.ComponentModel;
using System.IO;
using System.Web.UI;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.Sources
{
	public class FileImageSource : ImageSource
	{
		[Category("Source"), Browsable(true), UrlProperty]
		public string FileName
		{
			get
			{
				object value = this.ViewState["FileName"];
				if (value != null)
					return (string) value;
				return string.Empty;
			}
			set
			{
				this.ViewState["FileName"] = value;
			}
		}

		public override FastBitmap GetBitmap()
		{
			string resolvedFileName = FileSourceHelper.ResolveFileName(this.FileName);
			if (File.Exists(resolvedFileName))
				return new FastBitmap(resolvedFileName);
			else
				return null;
		}
	}
}

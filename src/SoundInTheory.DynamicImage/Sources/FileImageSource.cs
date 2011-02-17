using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web;
using System.IO;
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

		public override FastBitmap GetBitmap(ISite site, bool designMode)
		{
			string resolvedFileName = FileSourceHelper.ResolveFileName(this.FileName, site, designMode);
			if (File.Exists(resolvedFileName))
				return new FastBitmap(resolvedFileName);
			else
				return null;
		}
	}
}

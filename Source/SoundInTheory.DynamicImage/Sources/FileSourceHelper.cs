using System;
using System.ComponentModel;
using System.Web.UI.Design;
using System.Web;
using System.IO;

namespace SoundInTheory.DynamicImage.Sources
{
	public static class FileSourceHelper
	{
		public static string ResolveFileName(string filename, ISite site, bool designMode)
		{
			string fileName = null;
			if (!Path.IsPathRooted(filename))
			{
				// in design mode, resolve url using IUrlResolutionService
				if (designMode)
				{
					IWebApplication app = (IWebApplication) site.GetService(typeof(IWebApplication));
					if (app != null)
					{
						IProjectItem projectItem = app.GetProjectItemFromUrl(filename);
						if (projectItem == null)
							throw new InvalidOperationException("Could not load ProjectItem corresponding to source filename '" + filename + "'.");

						fileName = projectItem.PhysicalPath;
					}
				}
				else if (HttpContext.Current != null)
				{
					fileName = HttpContext.Current.Server.MapPath(filename);
				}

				if (fileName == null)
					throw new InvalidOperationException("Could not resolve source filename.");
			}
			else
			{
				fileName = filename;
			}

			return fileName;
		}
	}
}

using System.ComponentModel;
using System.IO;
using System.Web.UI;
using Meshellator;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage
{
	public class FileSceneSource : SceneSource
	{
		[Category("Source"), Browsable(true), UrlProperty]
		public string FileName
		{
			get { return (string) this["FileName"] ?? string.Empty; }
			set { this["FileName"] = value; }
		}

		public override Scene GetScene(ImageGenerationContext context)
		{
			string resolvedFileName = FileSourceHelper.ResolveFileName(context, FileName);
			if (File.Exists(resolvedFileName))
				return MeshellatorLoader.ImportFromFile(resolvedFileName);
			return null;
		}
	}
}
using System.Configuration;

namespace SoundInTheory.DynamicImage.Configuration
{
	public class DependentSiteElement : ConfigurationElement
	{
		[ConfigurationProperty("path")]
		public string Path
		{
			get { return this["path"] as string; }
			set { this["path"] = value; }
		}
	}
}
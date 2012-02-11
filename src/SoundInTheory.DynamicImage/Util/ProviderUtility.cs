using System;
using System.Collections.Specialized;

namespace SoundInTheory.DynamicImage.Util
{
	public static class ProviderUtility
	{
		public static void InitialiseConfiguration(NameValueCollection config, ref string name, string defaultName, string defaultDescription)
		{
			if (config == null)
				throw new ArgumentException("config");
			if (string.IsNullOrEmpty(name))
				name = defaultName;
			if (string.IsNullOrEmpty(config["description"]))
			{
				config.Remove("description");
				config.Add("description", defaultDescription);
			}
		}
	}
}

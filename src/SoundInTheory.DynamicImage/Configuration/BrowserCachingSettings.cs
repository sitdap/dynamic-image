using System;
using System.Configuration;
using SoundInTheory.DynamicImage.Caching;
using System.ComponentModel;

namespace SoundInTheory.DynamicImage.Configuration
{
	internal class BrowserCachingSettings : ConfigurationElement
	{
		[ConfigurationProperty("enabled", DefaultValue = true)]
		public bool Enabled
		{
			get { return (bool) this["enabled"]; }
			set { this["enabled"] = value; }
		}

		[ConfigurationProperty("duration", DefaultValue = "365.00:00:00")]
		public TimeSpan Duration
		{
			get { return (TimeSpan) this["duration"]; }
			set { this["duration"] = value; }
		}
	}
}

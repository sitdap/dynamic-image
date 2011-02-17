using System;
using System.Configuration;
using SoundInTheory.DynamicImage.Caching;

namespace SoundInTheory.DynamicImage.Configuration
{
	[ConfigurationCollection(typeof(FilterSettings))]
	internal class FilterSettingsCollection : ConfigurationElementCollection
	{
		protected override ConfigurationElement CreateNewElement()
		{
			return new FilterSettings();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((FilterSettings) element).Name;
		}
	}
}

using System;
using System.Configuration;
using SoundInTheory.DynamicImage.Caching;

namespace SoundInTheory.DynamicImage.Configuration
{
	[ConfigurationCollection(typeof(LayerSettings))]
	internal class LayerSettingsCollection : ConfigurationElementCollection
	{
		protected override ConfigurationElement CreateNewElement()
		{
			return new LayerSettings();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((LayerSettings) element).Name;
		}
	}
}

using System;
using System.Configuration;
using SoundInTheory.DynamicImage.Caching;

namespace SoundInTheory.DynamicImage.Configuration
{
	[ConfigurationCollection(typeof(TemplateSettings))]
	internal class TemplateSettingsCollection : ConfigurationElementCollection
	{
		public new TemplateSettings this[string key]
		{
			get { return (TemplateSettings) base.BaseGet(key); }
		}
 
		protected override ConfigurationElement CreateNewElement()
		{
			return new TemplateSettings();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((TemplateSettings) element).Name;
		}
	}
}

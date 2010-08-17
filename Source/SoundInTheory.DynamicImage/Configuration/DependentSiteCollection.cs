using System.Configuration;

namespace SoundInTheory.DynamicImage.Configuration
{
	[ConfigurationCollection(typeof(DependentSiteElement))]
	internal class DependentSiteCollection : ConfigurationElementCollection
	{
		public new DependentSiteElement this[string key]
		{
			get { return (DependentSiteElement)BaseGet(key); }
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return new DependentSiteElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((DependentSiteElement)element).Path;
		}
	}
}
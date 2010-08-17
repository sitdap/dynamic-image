using System;
using System.Configuration;
using System.Collections.Specialized;

namespace SoundInTheory.DynamicImage.Configuration
{
	internal class TemplateSettings : ConfigurationElement
	{
		private NameValueCollection _propertyNameCollection;

		[ConfigurationProperty("name")]
		public string Name
		{
			get { return this["name"] as string; }
			set { this["name"] = value; }
		}

		[ConfigurationProperty("layers")]
		public LayerSettingsCollection Layers
		{
			get
			{
				return (LayerSettingsCollection) base["layers"];
			}
		}

		public NameValueCollection Parameters
		{
			get
			{
				if (_propertyNameCollection == null)
					_propertyNameCollection = new NameValueCollection();
				return _propertyNameCollection;
			}
		}

		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			Parameters.Add(name, value);
			return true;
		}
	}
}

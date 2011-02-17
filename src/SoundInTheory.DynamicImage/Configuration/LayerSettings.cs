using System;
using System.Configuration;
using System.Collections.Specialized;

namespace SoundInTheory.DynamicImage.Configuration
{
	internal class LayerSettings : ConfigurationElement
	{
		private NameValueCollection _propertyNameCollection;

		[ConfigurationProperty("name")]
		public string Name
		{
			get { return this["name"] as string; }
			set { this["name"] = value; }
		}

		[ConfigurationProperty("type")]
		public string Type
		{
			get { return this["type"] as string; }
			set { this["type"] = value; }
		}

		[ConfigurationProperty("filters")]
		public FilterSettingsCollection Filters
		{
			get
			{
				return (FilterSettingsCollection) base["filters"];
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

using System;
using System.Configuration;
using System.Linq;
using System.Web.Compilation;
using System.Web.UI;
using SoundInTheory.DynamicImage.Configuration;
using SoundInTheory.DynamicImage.Filters;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage
{
	public static class ExtensionMethods
	{
		internal static void ApplyTemplate(this IDynamicImageControl dynamicImageControl)
		{
			DynamicImageSection config = (DynamicImageSection) ConfigurationManager.GetSection("soundInTheory/dynamicImage");
			TemplateSettings templateSettings = config.Templates[dynamicImageControl.TemplateName];
			if (templateSettings == null)
				throw new ArgumentException(string.Format("Could not find template '{0}' for DynamicImage ID '{1}'.", dynamicImageControl.TemplateName, dynamicImageControl.ID));

			// TODO: Validate that the layers of this instance are a subset of the layers in the template.

			int currentLayerIndex = 0;
			foreach (LayerSettings layerSettings in templateSettings.Layers)
			{
				Layer layer;

				// Check if layer already exists
				if (dynamicImageControl.Composition.Layers.Contains(layerSettings.Name))
				{
					layer = dynamicImageControl.Composition.Layers[layerSettings.Name];
				}
				else
				{
					// If it doesn't, create it and insert it at correct position in layer collection.
					Type layerType = BuildManager.GetType(layerSettings.Type, false);
					if (layerType == null)
						throw new ArgumentException(string.Format("Could not find type '{0}' for layer '{1}' in DynamicImage template '{2}'.", layerSettings.Type, layerSettings.Name, templateSettings.Name));

					layer = (Layer) Activator.CreateInstance(layerType);
					layer.Name = layerSettings.Name;

					dynamicImageControl.Composition.Layers.Insert(currentLayerIndex, layer);
				}

				// Use reflection to set properties on layer.
				foreach (string key in layerSettings.Parameters)
					layer.SetValue(key, layerSettings.Parameters[key], "-", true);

				// TODO: Merge filters.
				int currentFilterIndex = 0;
				foreach (FilterSettings filterSettings in layerSettings.Filters)
				{
					Filter filter;

					// Check if filter already exists
					if (layer.Filters.Contains(filterSettings.Name))
					{
						filter = layer.Filters[layerSettings.Name];
					}
					else
					{
						// If it doesn't, create it and insert it at correct position in filter collection.
						Type filterType = BuildManager.GetType(filterSettings.Type, false);
						if (filterType == null)
							throw new ArgumentException(string.Format("Could not find type '{0}' for filter '{1}' in layer '{2}' in DynamicImage template '{3}'.", filterSettings.Type, filterSettings.Name, layerSettings.Name, templateSettings.Name));

						filter = (Filter) Activator.CreateInstance(filterType);
						filter.Name = filterSettings.Name;

						if (layer.Filters.Count == 0)
							layer.Filters.Add(filter);
						else
							layer.Filters.Insert(currentFilterIndex, filter);
					}

					// Use reflection to set properties on filter.
					foreach (string key in filterSettings.Parameters)
						filter.SetValue(key, filterSettings.Parameters[key], "-", true);

					++currentFilterIndex;
				}

				++currentLayerIndex;
			}
		}

		public static object SaveAllViewState(this StateBag stateBag)
		{
			return stateBag.Keys
				.Cast<string>()
				.Select(v => new Pair(v, stateBag[v]))
				.ToArray();
		}
	}
}

using System;
using System.ComponentModel.Design;
using SoundInTheory.DynamicImage.Filters;
using SoundInTheory.DynamicImage.Sources;

namespace SoundInTheory.DynamicImage.Design
{
	public class ImageSourceCollectionEditor : CollectionEditor
	{
		public ImageSourceCollectionEditor()
			: base(typeof(FilterCollection))
		{

		}

		protected override bool CanSelectMultipleInstances()
		{
			return false;
		}

		protected override Type[] CreateNewItemTypes()
		{
			return new Type[]
			{
				typeof(BytesImageSource),
				typeof(DatabaseImageSource),
				typeof(FileImageSource),
				typeof(ImageImageSource)
			};
		}

		protected override Type CreateCollectionItemType()
		{
			// don't ask me why, but we need to return a type of filter which has
			// child properties. it doesn't matter which one, because the CollectionEditor
			// will (correctly) show each filter type's properties.
			return typeof(BytesImageSource);
		}

		protected override string GetDisplayText(object value)
		{
			return value.ToString();
		}
	}
}

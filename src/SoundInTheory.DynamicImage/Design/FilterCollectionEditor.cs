using System;
using System.ComponentModel.Design;
using SoundInTheory.DynamicImage.Filters;
using System.Collections;
using System.Linq;

namespace SoundInTheory.DynamicImage.Design
{
	public class FilterCollectionEditor : CollectionEditor
	{
		public FilterCollectionEditor()
			: base(typeof(FilterCollection))
		{

		}

		protected override bool CanSelectMultipleInstances()
		{
			return false;
		}

		protected override Type[] CreateNewItemTypes()
		{
			Type[] builtinFilterTypes = new Type[]
			{
				typeof(BrightnessAdjustmentFilter),
				typeof(ContrastAdjustmentFilter),
				//typeof(CropFilter),
				//typeof(GaussianBlurFilter),
				typeof(GrayscaleFilter),
				typeof(InversionFilter),
				typeof(OpacityAdjustmentFilter),
				//typeof(ResizeFilter),
				//typeof(RotationFilter),
				//typeof(ShinyFloorFilter)
			};

			ITypeDiscoveryService typeDiscoveryService = (ITypeDiscoveryService) this.GetService(typeof(ITypeDiscoveryService));
			ICollection types = typeDiscoveryService.GetTypes(typeof(Filter), true);

			return types.Cast<Type>().Union(builtinFilterTypes).ToArray();
		}

		protected override Type CreateCollectionItemType()
		{
			// don't ask me why, but we need to return a type of filter which has
			// child properties. it doesn't matter which one, because the CollectionEditor
			// will (correctly) show each filter type's properties.
			return typeof(BrightnessAdjustmentFilter);
		}

		protected override string GetDisplayText(object value)
		{
			return value.ToString();
		}
	}
}

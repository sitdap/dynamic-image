using System;
using System.ComponentModel.Design;
using System.Collections;
using System.Linq;

namespace SoundInTheory.DynamicImage.Design
{
	public class LayerCollectionEditor : CollectionEditor
	{
		public LayerCollectionEditor()
			: base(typeof(LayerCollection))
		{

		}

		protected override bool CanSelectMultipleInstances()
		{
			return false;
		}

		protected override Type[] CreateNewItemTypes()
		{
			Type[] builtinLayerTypes = new Type[]
			{
				typeof(ImageLayer),
				typeof(JuliaFractalLayer),
				typeof(MandelbrotFractalLayer),
				typeof(RectangleShapeLayer),
				typeof(TextLayer)
			};

			ITypeDiscoveryService typeDiscoveryService = (ITypeDiscoveryService) this.GetService(typeof(ITypeDiscoveryService));
			ICollection types = typeDiscoveryService.GetTypes(typeof(Layer), true);
			
			return types.Cast<Type>().Union(builtinLayerTypes).ToArray();
		}

		protected override string GetDisplayText(object value)
		{
			return value.ToString();
		}
	}
}

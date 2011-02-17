using System;
using System.Linq;

namespace SoundInTheory.DynamicImage
{
	public class LayerCollection : DataBoundCollection<Layer>
	{
		#region Static stuff

		static LayerCollection()
		{
			_knownTypes = new Type[]
			{
				typeof(ImageLayer),
				typeof(JuliaFractalLayer),
				typeof(MandelbrotFractalLayer),
				typeof(RectangleShapeLayer),
				typeof(TextLayer)
			};
		}

		#endregion

		#region Properties

		public Layer this[string layerName]
		{
			get { return this.Cast<Layer>().Single(l => l.Name == layerName); }
		}

		#endregion

		#region Methods

		public bool Contains(string layerName)
		{
			return this.Cast<Layer>().Any(l => l.Name == layerName);
		}

		protected override object CreateKnownType(int index)
		{
			switch (index)
			{
				case 0:
					return new ImageLayer();
				case 1:
					return new JuliaFractalLayer();
				case 2:
					return new MandelbrotFractalLayer();
				case 3:
					return new RectangleShapeLayer();
				case 4:
					return new TextLayer();
			}
			throw new ArgumentOutOfRangeException("Type index is out of bounds.");
		}

		#endregion
	}
}

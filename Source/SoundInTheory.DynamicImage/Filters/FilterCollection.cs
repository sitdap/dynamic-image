using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Collections;

namespace SoundInTheory.DynamicImage.Filters
{
	public class FilterCollection : DataBoundCollection<Filter>
	{
		#region Static stuff

		static FilterCollection()
		{
			_knownTypes = new Type[]
			{
				typeof(BrightnessAdjustmentFilter),
				typeof(ContrastAdjustmentFilter),
				//typeof(CropFilter),
				typeof(GrayscaleFilter),
				typeof(InversionFilter),
				typeof(OpacityAdjustmentFilter),
				//typeof(ResizeFilter),
				//typeof(RotationFilter),
				//typeof(ShinyFloorFilter)
			};
		}

		#endregion

		public Filter this[string filterName]
		{
			get { return this.Cast<Filter>().Single(l => l.Name == filterName); }
		}

		public bool Contains(string filterName)
		{
			return this.Cast<Filter>().Any(l => l.Name == filterName);
		}

		protected override object CreateKnownType(int index)
		{
			switch (index)
			{
				case 0:
					return new BrightnessAdjustmentFilter();
				case 1:
					return new ContrastAdjustmentFilter();
				/*case 2:
					return new CropFilter();*/
				case 3:
					return new GrayscaleFilter();
				case 4:
					return new InversionFilter();
				case 5:
					return new OpacityAdjustmentFilter();
				/*case 6:
					return new ResizeFilter();
				case 7:
					return new RotationFilter();
				case 8:
					return new ShinyFloorFilter();*/
			}
			throw new ArgumentOutOfRangeException("Type index is out of bounds.");
		}
	}
}

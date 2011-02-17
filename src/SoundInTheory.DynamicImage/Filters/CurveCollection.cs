using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Collections;

namespace SoundInTheory.DynamicImage.Filters
{
	public class CurveCollection : DataBoundCollection<Curve>
	{
		#region Static stuff

		static CurveCollection()
		{
			_knownTypes = new[]
			{
				typeof(Curve)
			};
		}

		#endregion

		#region Methods

		protected override object CreateKnownType(int index)
		{
			switch (index)
			{
				case 0:
					return new Curve();
			}
			throw new ArgumentOutOfRangeException("Type index is out of bounds.");
		}

		#endregion
	}
}

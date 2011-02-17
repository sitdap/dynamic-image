using System;

namespace SoundInTheory.DynamicImage.Filters
{
	public class CurvePointCollection : DataBoundCollection<CurvePoint>
	{
		#region Static stuff

		static CurvePointCollection()
		{
			_knownTypes = new[]
			{
				typeof(CurvePoint)
			};
		}

		#endregion

		#region Methods

		protected override object CreateKnownType(int index)
		{
			switch (index)
			{
				case 0:
					return new CurvePoint();
			}
			throw new ArgumentOutOfRangeException("Type index is out of bounds.");
		}

		#endregion
	}
}

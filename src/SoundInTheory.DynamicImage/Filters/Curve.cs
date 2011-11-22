using System;
using System.ComponentModel;
using System.Web.UI;

namespace SoundInTheory.DynamicImage.Filters
{
	public class Curve : StateManagedObject
	{
		private CurvePointCollection _points;

		[Browsable(true), PersistenceMode(PersistenceMode.InnerProperty), NotifyParentProperty(true)]
		public CurvePointCollection Points
		{
			get
			{
				if (_points == null)
				{
					_points = new CurvePointCollection();
					if (((IStateManager)this).IsTrackingViewState)
						((IStateManager)_points).TrackViewState();
				}
				return _points;
			}
			set
			{
				if (_points != null)
					throw new Exception("You can only set a new curve points collection if one does not already exist");

				_points = value;
				if (((IStateManager)this).IsTrackingViewState)
					((IStateManager)_points).TrackViewState();
			}
		}
	}
}
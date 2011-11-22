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

		#region View state implementation

		/// <summary>
		/// Loads the previously saved state of the <see cref="Composition" /> object.
		/// </summary>
		/// <param name="savedState">
		/// An object containing the saved view state values for the <see cref="Composition" /> object.
		/// </param>
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				Pair pair = (Pair)savedState;
				base.LoadViewState(pair.First);
				if (pair.Second != null)
					((IStateManager)Points).LoadViewState(pair.Second);
			}
		}

		/// <summary>
		/// Saves the current view state of the <see cref="Composition" /> object.
		/// </summary>
		/// <param name="saveAll"><c>true</c> if all values should be saved regardless
		/// of whether they are dirty; otherwise <c>false</c>.</param>
		/// <returns>An object that represents the saved state. The default is <c>null</c>.</returns>
		protected override object SaveViewState(bool saveAll)
		{
			Pair pair = new Pair();
			pair.First = base.SaveViewState(saveAll);
			if (_points != null)
				pair.Second = ((IStateManagedObject)_points).SaveViewState(saveAll);
			return (pair.First == null && pair.Second == null) ? null : pair;
		}

		/// <summary>
		/// Tracks view state changes to the <see cref="Composition" /> object.
		/// </summary>
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (_points != null)
				((IStateManager)_points).TrackViewState();
		}

		#endregion
	}
}
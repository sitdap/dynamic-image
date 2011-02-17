using System;
using System.ComponentModel;
using System.Web.UI;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SoundInTheory.DynamicImage
{
	/// <summary>
	/// An abstract base class that provides basic functionality for derived close shape classes.
	/// </summary>
	public abstract class ClosedShapeLayer : ShapeLayer
	{
		private Fill _fill;

		#region Properties

		[Category("Appearance"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true)]
		public Fill Fill
		{
			get
			{
				if (_fill == null)
				{
					_fill = new Fill();
					if (IsTrackingViewState)
						((IStateManager) _fill).TrackViewState();
				}
				return _fill;
			}
			set
			{
				if (_fill != null)
					throw new Exception("You can only set a new fill if one does not already exist");

				_fill = value;
				if (IsTrackingViewState)
					((IStateManager) _fill).TrackViewState();
			}
		}

		#endregion

		#region View state implementation

		/// <summary>
		/// Loads the previously saved state of the <see cref="ClosedShapeLayer" /> object.
		/// </summary>
		/// <param name="savedState">
		/// An object containing the saved view state values for the <see cref="ClosedShapeLayer" /> object.
		/// </param>
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				Pair pair = (Pair) savedState;
				if (pair.First != null)
					base.LoadViewState(pair.First);
				if (pair.Second != null)
					((IStateManager) this.Fill).LoadViewState(pair.Second);
			}
		}

		/// <summary>
		/// Saves the current view state of the <see cref="ClosedShapeLayer" /> object.
		/// </summary>
		/// <param name="saveAll"><c>true</c> if all values should be saved regardless
		/// of whether they are dirty; otherwise <c>false</c>.</param>
		/// <returns>An object that represents the saved state. The default is <c>null</c>.</returns>
		protected override object SaveViewState(bool saveAll)
		{
			Pair pair = new Pair();
			pair.First = base.SaveViewState(saveAll);
			if (_fill != null)
				pair.Second = ((IStateManagedObject) _fill).SaveViewState(saveAll);
			return (pair.First == null && pair.Second == null) ? null : pair;
		}

		/// <summary>
		/// Tracks view state changes to the <see cref="ClosedShapeLayer" /> object.
		/// </summary>
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (_fill != null)
				((IStateManager) _fill).TrackViewState();
		}

		#endregion
	}
}

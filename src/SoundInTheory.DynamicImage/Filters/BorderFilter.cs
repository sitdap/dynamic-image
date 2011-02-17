using System;
using System.ComponentModel;
using System.Web.UI;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SoundInTheory.DynamicImage.Util;
using Size = System.Drawing.Size;

namespace SoundInTheory.DynamicImage.Filters
{
	/// <summary>
	/// Applies a border to an image. The original image is not resized; the border is added around the edge of the image.
	/// </summary>
	public class BorderFilter : ImageReplacementFilter
	{
		private Fill _fill;

		#region Properties

		/// <summary>
		/// Gets or sets the width of the border.
		/// </summary>
		[DefaultValue(10), Description("Gets or sets the width of the border.")]
		public int Width
		{
			get { return (int)(ViewState["Width"] ?? 10); }
			set
			{
				if (value < 0)
					throw new ArgumentException("The border width must be greater than or equal to zero.", "value");

				ViewState["Width"] = value;
			}
		}

		[Category("Appearance"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true)]
		public Fill Fill
		{
			get
			{
				if (_fill == null)
				{
					_fill = new Fill();
					if (IsTrackingViewState)
						((IStateManager)_fill).TrackViewState();
				}
				return _fill;
			}
			set
			{
				if (_fill != null)
					throw new Exception("You can only set a new fill if one does not already exist");

				_fill = value;
				if (IsTrackingViewState)
					((IStateManager)_fill).TrackViewState();
			}
		}

		#endregion

		#region Methods

		protected override bool GetDestinationDimensions(FastBitmap source, out int width, out int height)
		{
			width = source.Width + Width * 2;
			height = source.Height + Width * 2;
			return true;
		}

		protected override void ApplyFilter(FastBitmap source, DrawingContext dc, int width, int height)
		{
			// Draw border.
			dc.DrawRectangle(Fill.GetBrush(), null, new Rect(0, 0, width, height));

			// Draw image.
			dc.PushTransform(new TranslateTransform(Width, Width));
			dc.DrawImage(source.InnerBitmap, new Rect(0, 0, source.Width, source.Height));
			dc.Pop();
		}

		public override string ToString()
		{
			return "Border";
		}

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
				Pair pair = (Pair)savedState;
				if (pair.First != null)
					base.LoadViewState(pair.First);
				if (pair.Second != null)
					((IStateManager)this.Fill).LoadViewState(pair.Second);
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
				pair.Second = ((IStateManagedObject)_fill).SaveViewState(saveAll);
			return (pair.First == null && pair.Second == null) ? null : pair;
		}

		/// <summary>
		/// Tracks view state changes to the <see cref="ClosedShapeLayer" /> object.
		/// </summary>
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (_fill != null)
				((IStateManager)_fill).TrackViewState();
		}

		#endregion

		#endregion
	}
}

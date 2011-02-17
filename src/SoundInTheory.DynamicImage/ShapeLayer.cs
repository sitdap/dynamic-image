using System;
using System.ComponentModel;
using System.Web.UI;
using System.Windows.Media;

namespace SoundInTheory.DynamicImage
{
	public abstract class ShapeLayer : Layer
	{
		private Fill _strokeFill;

		#region Properties

		[Browsable(true), DefaultValue(null), Category("Layout"), Description("Width of the shape layer")]
		public int? Width
		{
			get { return ViewState["Width"] as int?; }
			set { ViewState["Width"] = value; }
		}

		[Browsable(true), DefaultValue(null), Category("Layout"), Description("Height of the shape layer")]
		public int? Height
		{
			get { return ViewState["Height"] as int?; }
			set { ViewState["Height"] = value; }
		}

		[Browsable(true), DefaultValue(DashStyle.Solid), NotifyParentProperty(true)]
		public DashStyle StrokeDashStyle
		{
			get { return (DashStyle)(ViewState["StrokeDashStyle"] ?? DashStyle.Solid); }
			set { ViewState["StrokeDashStyle"] = value; }
		}

		[Category("Appearance"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true)]
		public Fill StrokeFill
		{
			get
			{
				if (_strokeFill == null)
				{
					_strokeFill = new Fill();
					if (this.IsTrackingViewState)
						((IStateManager) _strokeFill).TrackViewState();
				}
				return _strokeFill;
			}
		}

		[Browsable(true), DefaultValue(0.0f), NotifyParentProperty(true)]
		public float StrokeWidth
		{
			get { return (float) (ViewState["StrokeWidth"] ?? 0.0f); }
			set { ViewState["StrokeWidth"] = value; }
		}

		[Browsable(true), DefaultValue(0)]
		public int Roundness
		{
			get { return (int) (ViewState["Roundness"] ?? 0); }
			set { ViewState["Roundness"] = value; }
		}

		public override bool HasFixedSize
		{
			get { return (Width != null && Height != null); }
		}

		#endregion

		protected override void CreateImage()
		{
			if (this.Width != null)
				this.CalculatedWidth = this.Width.Value;

			if (this.Height != null)
				this.CalculatedHeight = this.Height.Value;
		}

		protected Pen GetPen()
		{
			Pen pen = null;
			if (StrokeWidth > 0)
				pen = new Pen(StrokeFill.GetBrush(), StrokeWidth)
				{
					DashStyle = GetDashStyle()
				};
			return pen;
		}

		private System.Windows.Media.DashStyle GetDashStyle()
		{
			switch (StrokeDashStyle)
			{
				case DashStyle.Solid :
					return DashStyles.Solid;
				case DashStyle.Dash :
					return DashStyles.Dash;
				case DashStyle.Dot :
					return DashStyles.Dot;
				case DashStyle.DashDot :
					return DashStyles.DashDot;
				case DashStyle.DashDotDot :
					return DashStyles.DashDotDot;
				default :
					throw new NotSupportedException();
			}
		}

		#region View state implementation

		/// <summary>
		/// Loads the previously saved state of the <see cref="ShapeLayer" /> object.
		/// </summary>
		/// <param name="savedState">
		/// An object containing the saved view state values for the <see cref="ShapeLayer" /> object.
		/// </param>
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				Pair pair = (Pair) savedState;
				if (pair.First != null)
					base.LoadViewState(pair.First);
				if (pair.Second != null)
					((IStateManager) this.StrokeFill).LoadViewState(pair.Second);
			}
		}

		/// <summary>
		/// Saves the current view state of the <see cref="ShapeLayer" /> object.
		/// </summary>
		/// <param name="saveAll"><c>true</c> if all values should be saved regardless
		/// of whether they are dirty; otherwise <c>false</c>.</param>
		/// <returns>An object that represents the saved state. The default is <c>null</c>.</returns>
		protected override object SaveViewState(bool saveAll)
		{
			Pair pair = new Pair();
			pair.First = base.SaveViewState(saveAll);
			if (_strokeFill != null)
				pair.Second = ((IStateManager) _strokeFill).SaveViewState();
			return (pair.First == null && pair.Second == null) ? null : pair;
		}

		/// <summary>
		/// Tracks view state changes to the <see cref="ShapeLayer" /> object.
		/// </summary>
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (_strokeFill != null)
				((IStateManager) _strokeFill).TrackViewState();
		}

		#endregion
	}
}

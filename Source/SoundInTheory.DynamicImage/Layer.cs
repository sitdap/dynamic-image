using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;
using System.Windows;
using SoundInTheory.DynamicImage.Caching;
using SoundInTheory.DynamicImage.Filters;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage
{
	public abstract class Layer : DataBoundObject
	{
		private Padding _padding;
		private FilterCollection _filters;

		#region Properties

		[Browsable(true), DefaultValue(""), NotifyParentProperty(true)]
		public string Name
		{
			get
			{
				object value = this.ViewState["Name"];
				if (value != null)
					return (string) value;
				return string.Empty;
			}
			set
			{
				this.ViewState["Name"] = value;
			}
		}

		[Browsable(true), DefaultValue(true), NotifyParentProperty(true)]
		public bool Visible
		{
			get
			{
				object value = this.ViewState["Visible"];
				if (value != null)
					return (bool) value;
				return true;
			}
			set
			{
				this.ViewState["Visible"] = value;
			}
		}

		[Browsable(true), DefaultValue(AnchorStyles.None), NotifyParentProperty(true)]
		public AnchorStyles Anchor
		{
			get
			{
				object value = this.ViewState["Anchor"];
				if (value != null)
					return (AnchorStyles) value;
				return AnchorStyles.None;
			}
			set
			{
				this.ViewState["Anchor"] = value;
			}
		}

		[Browsable(true), DefaultValue(0), NotifyParentProperty(true)]
		public int X
		{
			get
			{
				object value = this.ViewState["X"];
				if (value != null)
					return (int) value;
				return 0;
			}
			set
			{
				this.ViewState["X"] = value;
			}
		}

		[Browsable(true), DefaultValue(0), NotifyParentProperty(true)]
		public int Y
		{
			get
			{
				object value = this.ViewState["Y"];
				if (value != null)
					return (int) value;
				return 0;
			}
			set
			{
				this.ViewState["Y"] = value;
			}
		}

		[Browsable(true), DefaultValue(0), NotifyParentProperty(true)]
		public int AnchorPadding
		{
			get
			{
				object value = this.ViewState["AnchorPadding"];
				if (value != null)
					return (int) value;
				return 0;
			}
			set
			{
				this.ViewState["AnchorPadding"] = value;
			}
		}

		[Category("Appearance"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true)]
		public Padding Padding
		{
			get
			{
				if (_padding == null)
				{
					_padding = new Padding();
					if (this.IsTrackingViewState)
						((IStateManager) _padding).TrackViewState();
				}
				return _padding;
			}
		}

		[Browsable(true), PersistenceMode(PersistenceMode.InnerProperty), Editor("SoundInTheory.DynamicImage.Design.FilterCollectionEditor, SoundInTheory.DynamicImage.Design, Version=1.0.0.0, Culture=neutral, PublicKeyToken=fa44558110383067", typeof(UITypeEditor)), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true)]
		public FilterCollection Filters
		{
			get
			{
				if (_filters == null)
				{
					_filters = new FilterCollection();
					if (this.IsTrackingViewState)
						((IStateManager) _filters).TrackViewState();
				}
				return _filters;
			}
			set
			{
				if (_filters != null)
					throw new Exception("You can only set a new filters collection if one does not already exist");

				_filters = value;
				if (((IStateManager) this).IsTrackingViewState)
					((IStateManager) _filters).TrackViewState();
			}
		}

		[Category("Appearance"), DefaultValue(BlendMode.Normal), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true)]
		public BlendMode BlendMode
		{
			get { return (BlendMode) (ViewState["BlendMode"] ?? BlendMode.Normal); }
			set { this.ViewState["BlendMode"] = value; }
		}

		[Browsable(false)]
		public FastBitmap Bitmap
		{
			get;
			protected set;
		}

		[Browsable(false)]
		public Int32Rect? Bounds
		{
			get
			{
				if (this.Bitmap != null)
					return new Int32Rect(X, Y,
						Bitmap.Width + Padding.Left + Padding.Right,
						Bitmap.Height + Padding.Top + Padding.Bottom);
				else
					return null;
			}
		}

		[Browsable(false)]
		public Int32Size? Size
		{
			get
			{
				if (this.Bitmap != null)
					return new Int32Size(
						Bitmap.Width + Padding.Left + Padding.Right,
						Bitmap.Height + Padding.Top + Padding.Bottom);
				else
					return null;
			}
		}

		public abstract bool HasFixedSize
		{
			get;
		}

		internal int CalculatedWidth
		{
			get;
			set;
		}

		internal int CalculatedHeight
		{
			get;
			set;
		}

		public override Control BindingContainer
		{
			get
			{
				return base.BindingContainer;
			}
			internal set
			{
				base.BindingContainer = value;
				this.Filters.BindingContainer = value;
			}
		}

		protected internal override ISite Site
		{
			set
			{
				base.Site = value;
				foreach (Filter filter in this.Filters)
					filter.Site = value;
			}
		}

		protected internal override bool DesignMode
		{
			set
			{
				base.DesignMode = value;
				foreach (Filter filter in this.Filters)
					filter.DesignMode = value;
			}
		}

		#endregion

		public void Process()
		{
			CreateImage();

			if (this.Bitmap != null)
				foreach (Filter filter in this.Filters)
					if (filter.Enabled)
						filter.ApplyFilter(this.Bitmap);
		}

		protected abstract void CreateImage();

		public virtual void PopulateDependencies(List<Dependency> dependencies) { }

		#region View state implementation

		/// <summary>
		/// Loads the previously saved state of the <see cref="Layer" /> object.
		/// </summary>
		/// <param name="savedState">
		/// An object containing the saved view state values for the <see cref="Layer" /> object.
		/// </param>
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				Triplet triplet = (Triplet) savedState;
				if (triplet.First != null)
					base.LoadViewState(triplet.First);
				if (triplet.Second != null)
					((IStateManager) Padding).LoadViewState(triplet.Second);
				if (triplet.Third != null)
					((IStateManager) Filters).LoadViewState(triplet.Third);
			}
		}

		/// <summary>
		/// Saves the current view state of the <see cref="Layer" /> object.
		/// </summary>
		/// <param name="saveAll"><c>true</c> if all values should be saved regardless
		/// of whether they are dirty; otherwise <c>false</c>.</param>
		/// <returns>An object that represents the saved state. The default is <c>null</c>.</returns>
		protected override object SaveViewState(bool saveAll)
		{
			Triplet triplet = new Triplet();
			triplet.First = base.SaveViewState(saveAll);
			if (_padding != null)
				triplet.Second = ((IStateManagedObject) _padding).SaveViewState(saveAll);
			if (_filters != null)
				triplet.Third = ((IStateManagedObject) _filters).SaveViewState(saveAll);
			return (triplet.First == null && triplet.Second == null && triplet.Third == null) ? null : triplet;
		}

		/// <summary>
		/// Tracks view state changes to the <see cref="Layer" /> object.
		/// </summary>
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (_padding != null)
				((IStateManager) _padding).TrackViewState();
			if (_filters != null)
				((IStateManager) _filters).TrackViewState();
		}

		public override void DataBind()
		{
			base.DataBind();
			this.Filters.DataBind();
		}

		#endregion
	}
}

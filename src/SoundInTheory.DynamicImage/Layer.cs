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
	public abstract class Layer : DirtyTrackingObject
	{
		#region Properties

		[Browsable(true), DefaultValue(true), NotifyParentProperty(true)]
		public bool Visible
		{
			get
			{
				object value = this.PropertyStore["Visible"];
				if (value != null)
					return (bool) value;
				return true;
			}
			set
			{
				this.PropertyStore["Visible"] = value;
			}
		}

		[Browsable(true), DefaultValue(AnchorStyles.None), NotifyParentProperty(true)]
		public AnchorStyles Anchor
		{
			get
			{
				object value = this.PropertyStore["Anchor"];
				if (value != null)
					return (AnchorStyles) value;
				return AnchorStyles.None;
			}
			set
			{
				this.PropertyStore["Anchor"] = value;
			}
		}

		[Browsable(true), DefaultValue(0), NotifyParentProperty(true)]
		public int X
		{
			get
			{
				object value = this.PropertyStore["X"];
				if (value != null)
					return (int) value;
				return 0;
			}
			set
			{
				this.PropertyStore["X"] = value;
			}
		}

		[Browsable(true), DefaultValue(0), NotifyParentProperty(true)]
		public int Y
		{
			get
			{
				object value = this.PropertyStore["Y"];
				if (value != null)
					return (int) value;
				return 0;
			}
			set
			{
				this.PropertyStore["Y"] = value;
			}
		}

		[Browsable(true), DefaultValue(0), NotifyParentProperty(true)]
		public int AnchorPadding
		{
			get
			{
				object value = this.PropertyStore["AnchorPadding"];
				if (value != null)
					return (int) value;
				return 0;
			}
			set
			{
				this.PropertyStore["AnchorPadding"] = value;
			}
		}

		public Padding Padding
		{
			get { return (Padding)(PropertyStore["Padding"] ?? (PropertyStore["Padding"] = new Padding())); }
		}

		public FilterCollection Filters
		{
			get { return (FilterCollection)(PropertyStore["Filters"] ?? (PropertyStore["Filters"] = new FilterCollection())); }
			set { PropertyStore["Filters"] = value; }
		}

		[Category("Appearance"), DefaultValue(BlendMode.Normal), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true)]
		public BlendMode BlendMode
		{
			get { return (BlendMode) (PropertyStore["BlendMode"] ?? BlendMode.Normal); }
			set { this.PropertyStore["BlendMode"] = value; }
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
	}
}

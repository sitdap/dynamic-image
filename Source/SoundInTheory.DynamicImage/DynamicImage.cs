using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;
using System.Web;

namespace SoundInTheory.DynamicImage
{
	[Designer("SoundInTheory.DynamicImage.Design.DynamicImageDesigner, SoundInTheory.DynamicImage.Design, Version=1.0.0.0, Culture=neutral, PublicKeyToken=fa44558110383067")]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class DynamicImage : System.Web.UI.WebControls.Image, IDynamicImageControl
	{
		private Composition _composition;
		private bool _imageUrlCreated;

		#region Properties

		Composition IDynamicImageControl.Composition
		{
			get { return this.Composition; }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		internal Composition Composition
		{
			get
			{
				if (_composition == null)
				{
					_composition = new Composition();
					if (base.IsTrackingViewState)
						((IStateManager)_composition).TrackViewState();
				}
				return _composition;
			}
		}

		[Browsable(false)]
		public new System.Drawing.Color BackColor
		{
			get { return base.BackColor; }
			set { base.BackColor = value; }
		}

		[Browsable(false)]
		public new System.Web.UI.WebControls.Unit Width
		{
			get { return base.Width; }
			set { base.Width = value; }
		}

		[Browsable(false)]
		public new System.Web.UI.WebControls.Unit Height
		{
			get { return base.Height; }
			set { base.Height = value; }
		}

		[Browsable(true), Category("Layout"), DefaultValue(true), NotifyParentProperty(true), Description("Gets or sets whether the size of the output image is automatically determined by the size of the component layers")]
		public bool AutoSize
		{
			get { return Composition.AutoSize; }
			set { Composition.AutoSize = value; }
		}

		[Browsable(true), Category("Layout"), DefaultValue(null), NotifyParentProperty(true)]
		public int? OutputWidth
		{
			get { return Composition.Width; }
			set { Composition.Width = value; }
		}

		[Browsable(true), Category("Layout"), DefaultValue(null), NotifyParentProperty(true)]
		public int? OutputHeight
		{
			get { return Composition.Height; }
			set { Composition.Height = value; }
		}

		[Browsable(true), DefaultValue(DynamicImageFormat.Jpeg), NotifyParentProperty(true)]
		public DynamicImageFormat ImageFormat
		{
			get { return Composition.ImageFormat; }
			set { Composition.ImageFormat = value; }
		}

		[Browsable(true), DefaultValue(90), NotifyParentProperty(true)]
		public int? JpegCompressionLevel
		{
			get { return Composition.JpegCompressionLevel; }
			set { Composition.JpegCompressionLevel = value; }
		}

		// Not currently supported.
		/*[Browsable(true), DefaultValue(32), NotifyParentProperty(true)]
		public int ColourDepth
		{
			get { return Composition.ColourDepth; }
			set { Composition.ColourDepth = value; }
		}*/

		[Browsable(true), PersistenceMode(PersistenceMode.InnerProperty), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Fill Fill
		{
			get { return Composition.Fill; }
			set { Composition.Fill = value; }
		}

		[Browsable(true), PersistenceMode(PersistenceMode.InnerProperty), Editor("SoundInTheory.DynamicImage.Design.LayerCollectionEditor, SoundInTheory.DynamicImage.Design, Version=1.0.0.0, Culture=neutral, PublicKeyToken=fa44558110383067", typeof(UITypeEditor)), NotifyParentProperty(true)]
		public LayerCollection Layers
		{
			get { return Composition.Layers; }
			set { Composition.Layers = value; }
		}

		[Browsable(true), DefaultValue("")]
		public string TemplateName
		{
			get;
			set;
		}

		[Browsable(false)]
		internal string InternalImageUrl
		{
			get;
			set;
		}

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new string ImageUrl
		{
			get
			{
				EnsureImageUrl();
				return base.ImageUrl;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		#endregion

		public DynamicImage()
		{
			TemplateName = string.Empty;
		}

		#region Methods

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			Page.PreRenderComplete += Page_PreRenderComplete;
		}

		protected override void OnUnload(EventArgs e)
		{
			base.OnUnload(e);
			Page.PreRenderComplete -= Page_PreRenderComplete;
		}

		protected override void OnDataBinding(EventArgs e)
		{
			Layers.BindingContainer = this.BindingContainer;
			base.OnDataBinding(e);
			Layers.DataBind();
			ViewState["ImageProperties"] = null;
		}

		private void Page_PreRenderComplete(object sender, EventArgs e)
		{
			if (this.Visible)
				EnsureImageUrl();
		}

		private void EnsureImageUrl()
		{
			if (!_imageUrlCreated)
			{
				// If template name is set, apply template in-place to existing composition.
				if (TemplateName != string.Empty)
					this.ApplyTemplate();

				// Get composition. We don't actually create the image here.
				ImageProperties imageProperties = ImageManager.GetImageProperties(Composition);
				
				if (imageProperties.IsImagePresent)
				{
					this.Visible = true;
					this.Width = System.Web.UI.WebControls.Unit.Pixel(imageProperties.Width.Value);
					this.Height = System.Web.UI.WebControls.Unit.Pixel(imageProperties.Height.Value);

					const string path = "~/Assets/Images/DynamicImages/";
					string fileName = string.Format("{0}.{1}", imageProperties.CacheProviderKey, imageProperties.FileExtension);

					base.ImageUrl = VirtualPathUtility.ToAbsolute(path) + fileName;
				}
				else
				{
					Visible = false;
				}

				_imageUrlCreated = true;
			}
		}

		#region View state implementation

		protected override void LoadViewState(object savedState)
		{
			object baseSavedState = null, thisSavedState = null;
			if (savedState != null)
			{
				Pair pair = (Pair)savedState;
				baseSavedState = pair.First;
				thisSavedState = pair.Second;
			}
			base.LoadViewState(baseSavedState);
			if (thisSavedState != null)
				((IStateManager)this.Composition).LoadViewState(thisSavedState);
		}

		protected override object SaveViewState()
		{
			Pair pair = new Pair();
			pair.First = base.SaveViewState();
			if (_composition != null)
				pair.Second = ((IStateManager)this.Composition).SaveViewState();
			return (pair.First == null && pair.Second == null) ? null : pair;
		}

		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (_composition != null)
				((IStateManager)_composition).TrackViewState();
		}

		#endregion

		#endregion
	}
}
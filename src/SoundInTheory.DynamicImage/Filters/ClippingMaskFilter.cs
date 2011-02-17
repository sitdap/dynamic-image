using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using SoundInTheory.DynamicImage.ShaderEffects;
using SoundInTheory.DynamicImage.Sources;
using System.Web.UI;
using System.Drawing.Design;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.Filters
{
	/// <summary>
	/// Clips (reveals) the content of the layer based on the 
	/// non-transparent content of the specified image.
	/// </summary>
	/// <include file="../documentation.xml" path="docs/types/type[@name = 'ClippingMaskFilter']/*" />
	public class ClippingMaskFilter : ShaderEffectFilter
	{
		#region Fields

		private ImageSourceCollection _maskImage;
		private int _maskImageWidth, _maskImageHeight;

		#endregion

		#region Properties

		[PersistenceMode(PersistenceMode.InnerProperty), Editor("SoundInTheory.DynamicImage.Design.ImageSourceCollectionEditor, SoundInTheory.DynamicImage.Design, Version=1.0.0.0, Culture=neutral, PublicKeyToken=fa44558110383067", typeof(UITypeEditor)), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true)]
		public ImageSourceCollection MaskImage
		{
			get
			{
				if (_maskImage == null)
				{
					_maskImage = new ImageSourceCollection();
					if (((IStateManager) this).IsTrackingViewState)
						((IStateManager) _maskImage).TrackViewState();
				}
				return _maskImage;
			}
		}

		/// <summary>
		/// Gets or sets the x-position of the left side of the mask image.
		/// </summary>
		[DefaultValue(0), Description("Gets or sets the x-position of the left side of the mask image.")]
		public int MaskPositionX
		{
			get { return (int) (ViewState["MaskPositionX"] ?? 0); }
			set { ViewState["MaskPositionX"] = value; }
		}

		/// <summary>
		/// Gets or sets the y-position of the top side of the mask image.
		/// </summary>
		[DefaultValue(0), Description("Gets or sets the y-position of the top side of the mask image.")]
		public int MaskPositionY
		{
			get { return (int)(ViewState["MaskPositionY"] ?? 0); }
			set { ViewState["MaskPositionY"] = value; }
		}

		public override Control BindingContainer
		{
			get
			{
				return base.BindingContainer;
			}
			internal set
			{
				this.MaskImage.SingleSource.BindingContainer = value;
				base.BindingContainer = value;
			}
		}

		#endregion

		#region Methods

		public override void DataBind()
		{
			base.DataBind();
			MaskImage.SingleSource.DataBind();
		}

		protected override void ConfigureDrawingVisual(FastBitmap source, DrawingVisual drawingVisual)
		{
			base.ConfigureDrawingVisual(source, drawingVisual);
			
		}

		protected override Effect GetEffect(FastBitmap source)
		{
			FastBitmap maskBitmap = MaskImage.SingleSource.GetBitmap(Site, DesignMode);
			_maskImageWidth = maskBitmap.Width;
			_maskImageHeight = maskBitmap.Height;
			return new ClippingMaskEffect
			{
				Mask = new ImageBrush(maskBitmap.InnerBitmap),
				InputCoordsOffset = new Vector(MaskPositionX / (double) source.Width, MaskPositionY / (double) source.Height),
				InputCoordsScale = new Vector(_maskImageWidth / (double) source.Width, _maskImageHeight / (double) source.Height)
			};
		}

		protected override void ApplyFilter(FastBitmap source, DrawingContext dc, int width, int height)
		{
			dc.DrawImage(source.InnerBitmap, new Rect(MaskPositionX, MaskPositionY, _maskImageWidth, _maskImageHeight));
		}

		public override string ToString()
		{
			return "Clipping Mask";
		}

		#region View state implementation

		/// <summary>
		/// /// <summary>
		/// Loads the previously saved state of the <see cref="ClippingMaskFilter" /> object.
		/// </summary>
		/// <param name="savedState">
		/// An object containing the saved view state values for the <see cref="ClippingMaskFilter" /> object.
		/// </param>
		/// </summary>
		/// <param name="savedState"></param>
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				Pair pair = (Pair) savedState;
				if (pair.First != null)
					base.LoadViewState(pair.First);
				if (pair.Second != null)
					((IStateManager) this.MaskImage).LoadViewState(pair.Second);
			}
		}

		/// <summary>
		/// Saves the current view state of the <see cref="ClippingMaskFilter" /> object.
		/// </summary>
		/// <param name="saveAll"><c>true</c> if all values should be saved regardless
		/// of whether they are dirty; otherwise <c>false</c>.</param>
		/// <returns>An object that represents the saved state. The default is <c>null</c>.</returns>
		protected override object SaveViewState(bool saveAll)
		{
			Pair pair = new Pair();
			pair.First = base.SaveViewState(saveAll);
			if (_maskImage != null)
				pair.Second = ((IStateManagedObject) _maskImage).SaveViewState(saveAll);
			return (pair.First == null && pair.Second == null) ? null : pair;
		}

		/// <summary>
		/// Tracks view state changes to the <see cref="ClippingMaskFilter" /> object.
		/// </summary>
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (_maskImage != null)
				((IStateManager) _maskImage).TrackViewState();
		}

		#endregion

		#endregion
	}
}

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

		#endregion

		#region Methods

		protected override Effect GetEffect(FastBitmap source)
		{
			FastBitmap maskBitmap = MaskImage.SingleSource.GetBitmap();
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

		#endregion
	}
}

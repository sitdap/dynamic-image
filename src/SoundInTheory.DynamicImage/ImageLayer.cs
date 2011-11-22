using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;
using SoundInTheory.DynamicImage.Caching;
using SoundInTheory.DynamicImage.Sources;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage
{
	public class ImageLayer : Layer
	{
		private ImageSourceCollection _source, _alternateSource;

		#region Properties

		[Category("Source"), Browsable(true), PersistenceMode(PersistenceMode.InnerProperty), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true)]
		public ImageSourceCollection Source
		{
			get
			{
				if (_source == null)
				{
					_source = new ImageSourceCollection();
					if (((IStateManager) this).IsTrackingViewState)
						((IStateManager) _source).TrackViewState();
				}
				return _source;
			}
			set
			{
				if (_source != null)
					throw new Exception("You can only set a new source if one does not already exist");

				_source = value;
				if (((IStateManager) this).IsTrackingViewState)
					((IStateManager) _source).TrackViewState();
			}
		}

		/// <summary>
		/// Shortcut route to Source/FileImageSource
		/// </summary>
		[Category("Source"), Browsable(true), UrlProperty]
		public string SourceFileName
		{
			set { this.Source.SingleSource = new FileImageSource { FileName = value }; }
		}

		[Category("Source"), Browsable(true), PersistenceMode(PersistenceMode.InnerProperty), Editor("SoundInTheory.DynamicImage.Design.ImageSourceCollectionEditor, SoundInTheory.DynamicImage.Design, Version=1.0.0.0, Culture=neutral, PublicKeyToken=fa44558110383067", typeof(UITypeEditor)), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true)]
		public ImageSourceCollection AlternateSource
		{
			get
			{
				if (_alternateSource == null)
				{
					_alternateSource = new ImageSourceCollection();
					if (((IStateManager) this).IsTrackingViewState)
						((IStateManager) _alternateSource).TrackViewState();
				}
				return _alternateSource;
			}
		}

		public override bool HasFixedSize
		{
			get { return true; }
		}

		#endregion

		protected override void CreateImage()
		{
			FastBitmap sourceValue = this.Source.SingleSource.GetBitmap();
			if (sourceValue != null && sourceValue.InnerBitmap != null)
			{
				this.Bitmap = sourceValue;
			}
			else if (this.AlternateSource.Count > 0)
			{
				sourceValue = this.AlternateSource.SingleSource.GetBitmap();
				if (sourceValue != null && sourceValue.InnerBitmap != null)
					this.Bitmap = sourceValue;
			}
		}

		public override void PopulateDependencies(List<Dependency> dependencies)
		{
			base.PopulateDependencies(dependencies);
			this.Source.SingleSource.PopulateDependencies(dependencies);
			if (this.AlternateSource.Count > 0)
				this.AlternateSource.SingleSource.PopulateDependencies(dependencies);
		}

		public override string ToString()
		{
			return "Image Layer";
		}
	}
}

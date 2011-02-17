using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Collections.Generic;
using SoundInTheory.DynamicImage.Caching;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web;
using SoundInTheory.DynamicImage.Sources;
using System.Drawing.Design;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage
{
	public class ImageLayer : Layer
	{
		private ImageSourceCollection _source, _alternateSource;

		#region Properties

		[Category("Source"), Browsable(true), PersistenceMode(PersistenceMode.InnerProperty), Editor("SoundInTheory.DynamicImage.Design.ImageSourceCollectionEditor, SoundInTheory.DynamicImage.Design, Version=1.0.0.0, Culture=neutral, PublicKeyToken=fa44558110383067", typeof(UITypeEditor)), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true)]
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

		public override Control BindingContainer
		{
			get
			{
				return base.BindingContainer;
			}
			internal set
			{
				this.Source.SingleSource.BindingContainer = value;
				base.BindingContainer = value;
			}
		}

		public override bool HasFixedSize
		{
			get { return true; }
		}

		#endregion

		protected override void CreateImage()
		{
			FastBitmap sourceValue = this.Source.SingleSource.GetBitmap(this.Site, this.DesignMode);
			if (sourceValue != null && sourceValue.InnerBitmap != null)
			{
				this.Bitmap = sourceValue;
			}
			else if (this.AlternateSource.Count > 0)
			{
				sourceValue = this.AlternateSource.SingleSource.GetBitmap(this.Site, this.DesignMode);
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

		public override void DataBind()
		{
			base.DataBind();
			this.Source.SingleSource.DataBind();
		}

		public override string ToString()
		{
			return "Image Layer";
		}

		#region View state implementation

		/// <summary>
		/// /// <summary>
		/// Loads the previously saved state of the <see cref="ImageLayer" /> object.
		/// </summary>
		/// <param name="savedState">
		/// An object containing the saved view state values for the <see cref="ImageLayer" /> object.
		/// </param>
		/// </summary>
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				Triplet triplet = (Triplet) savedState;
				if (triplet.First != null)
					base.LoadViewState(triplet.First);
				if (triplet.Second != null)
					((IStateManager) this.Source).LoadViewState(triplet.Second);
				if (triplet.Third != null)
					((IStateManager) this.AlternateSource).LoadViewState(triplet.Third);
			}
		}

		/// <summary>
		/// Saves the current view state of the <see cref="ImageLayer" /> object.
		/// </summary>
		/// <param name="saveAll"><c>true</c> if all values should be saved regardless
		/// of whether they are dirty; otherwise <c>false</c>.</param>
		/// <returns>An object that represents the saved state. The default is <c>null</c>.</returns>
		protected override object SaveViewState(bool saveAll)
		{
			Triplet triplet = new Triplet();
			triplet.First = base.SaveViewState(saveAll);
			if (_source != null)
				triplet.Second = ((IStateManagedObject) _source).SaveViewState(saveAll);
			if (_alternateSource != null)
				triplet.Third = ((IStateManagedObject) _alternateSource).SaveViewState(saveAll);
			return (triplet.First == null && triplet.Second == null && triplet.Third == null) ? null : triplet;
		}

		/// <summary>
		/// Tracks view state changes to the <see cref="ImageLayer" /> object.
		/// </summary>
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (_source != null)
				((IStateManager) _source).TrackViewState();
			if (_alternateSource != null)
				((IStateManager) _alternateSource).TrackViewState();
		}

		#endregion
	}
}

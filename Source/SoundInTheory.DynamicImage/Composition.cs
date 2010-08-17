using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SoundInTheory.DynamicImage.Caching;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage
{
	public class Composition : DataBoundObject
	{
		private LayerCollection _layers;
		private Fill _fill;

		#region Properties

		[Browsable(true), Category("Layout"), DefaultValue(true)]
		public bool AutoSize
		{
			get
			{
				object value = this.ViewState["AutoSize"];
				if (value != null)
					return (bool) value;
				return true;
			}
			set
			{
				this.ViewState["AutoSize"] = value;
			}
		}

		[Browsable(true), Category("Layout")]
		public int? Width
		{
			get
			{
				object value = this.ViewState["Width"];
				if (value != null)
					return (int?) value;
				return null;
			}
			set
			{
				this.ViewState["Width"] = value;
			}
		}

		[Browsable(true), Category("Layout")]
		public int? Height
		{
			get
			{
				object value = this.ViewState["Height"];
				if (value != null)
					return (int?) value;
				return null;
			}
			set
			{
				this.ViewState["Height"] = value;
			}
		}

		[Browsable(true), DefaultValue(DynamicImageFormat.Jpeg)]
		public DynamicImageFormat ImageFormat
		{
			get
			{
				object value = this.ViewState["ImageFormat"];
				if (value != null)
					return (DynamicImageFormat) value;
				return DynamicImageFormat.Jpeg;
			}
			set
			{
				this.ViewState["ImageFormat"] = value;
			}
		}

		[Browsable(true), DefaultValue(90)]
		public int? JpegCompressionLevel
		{
			get
			{
				object value = this.ViewState["JpegCompressionLevel"];
				if (value != null)
					return (int?) value;
				return 90;
			}
			set
			{
				this.ViewState["JpegCompressionLevel"] = value;
			}
		}

		[Browsable(true), DefaultValue(32)]
		public int ColourDepth
		{
			get
			{
				object value = this.ViewState["ColourDepth"];
				if (value != null)
					return (int) value;
				return 32;
			}
			set
			{
				this.ViewState["ColourDepth"] = value;
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
					if (this.IsTrackingViewState)
						((IStateManager) _fill).TrackViewState();
				}
				return _fill;
			}
			set
			{
				if (_fill != null)
					throw new Exception("You can only set a new fill if one does not already exist");

				_fill = value;
				if (this.IsTrackingViewState)
					((IStateManager) _fill).TrackViewState();
			}
		}

		[Browsable(true), Editor("SoundInTheory.DynamicImage.Design.LayerCollectionEditor, SoundInTheory.DynamicImage.Design, Version=1.0.0.0, Culture=neutral, PublicKeyToken=fa44558110383067", typeof(UITypeEditor)), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public LayerCollection Layers
		{
			get
			{
				if (_layers == null)
				{
					_layers = new LayerCollection();
					if (((IStateManager) this).IsTrackingViewState)
						((IStateManager) _layers).TrackViewState();
				}
				return _layers;
			}
			set
			{
				if (_layers != null)
					throw new Exception("You can only set a new layers collection if one does not already exist");

				_layers = value;
				if (((IStateManager) this).IsTrackingViewState)
					((IStateManager) _layers).TrackViewState();
			}
		}

		private IEnumerable<Layer> VisibleLayers
		{
			get { return this.Layers.Cast<Layer>().Where(l => l.Visible); }
		}

		#endregion

		public static void SaveImageStream(CompositionImage compositionImage, Stream stream)
		{
			// setup parameters
			BitmapEncoder encoder = compositionImage.Properties.GetEncoder();

			//encoderParametersTemp.Add(new EncoderParameter(Encoder.ColorDepth, (long)compositionImage.Properties.ColourDepth));
			// TODO: Use ColorConvertedBitmap to allow configurable colour depth in output image.

			encoder.Frames.Add(BitmapFrame.Create(compositionImage.Image));

			// Write to temporary stream first. this is because PNG must be written to a seekable stream.
			using (MemoryStream tempStream = new MemoryStream())
			{
				encoder.Save(tempStream);

				// Now write temp stream to output stream.
				tempStream.WriteTo(stream);
			}
		}

		internal CompositionImage GetCompositionImage(string cacheKey)
		{
			// create image and add to cache
			BitmapSource image = CreateImage();

			// Freeze the bitmap - this means any thread can access it, as well
			// as perhaps providing some performance benefit.
			if (image != null)
				image.Freeze();

			ImageProperties properties = new ImageProperties
			{
				UniqueKey = cacheKey,
				CacheProviderKey = Util.Util.CalculateShaHash(cacheKey),
				ColourDepth = this.ColourDepth,
				Format = this.ImageFormat,
				JpegCompressionLevel = this.JpegCompressionLevel
			};
			if (image != null)
			{
				properties.Width = image.PixelWidth;
				properties.Height = image.PixelHeight;
			}
			properties.IsImagePresent = (image != null);
			return new CompositionImage(properties, image);
		}

		/// <summary>
		/// Used by the visual designer. In the future this could be exposed for
		/// non-web uses.
		/// </summary>
		/// <returns></returns>
		public CompositionImage GetCompositionImage()
		{
			return GetCompositionImage(GetCacheKey());
		}

		private BitmapSource CreateImage()
		{
			ValidateParameters();

			// Set design-mode properties.
			foreach (Layer layer in this.VisibleLayers)
				{
					layer.Site = this.Site;
					layer.DesignMode = this.DesignMode;
				}

			// First, we process layers which have a specific size.
			foreach (Layer layer in this.VisibleLayers)
				if (layer.HasFixedSize)
					layer.Process();

			// Second, for SizeMode = Auto, we calculate the output dimensions
			// based on the union of all layers' (which have an explicit size) dimensions.
			int outputWidth, outputHeight;
			if (this.AutoSize)
			{
				Int32Rect outputDimensions = Int32Rect.Empty;
				foreach (Layer layer in this.VisibleLayers)
				{
					// Calculate dimensions of layers; the dimensions of some layers may be omitted
					// at design time and are then derived from layers which have a fixed size.
					// Only include layer in bounds calculation if the Anchor property is None.
					if (layer.Anchor == AnchorStyles.None)
					{
						Int32Rect? bounds = layer.Bounds;
						if (bounds != null)
							outputDimensions = Int32RectUtility.Union(outputDimensions, bounds.Value);
					}
				}
				outputWidth = outputDimensions.Width;
				outputHeight = outputDimensions.Height;
			}
			else
			{
				outputWidth = Width.Value;
				outputHeight = Height.Value;
			}

			// If at this point we don't have a valid size, return - this means that
			// layers without an explicit size will never force an image to be created.
			if (outputWidth == 0 && outputHeight == 0)
				return null;

			// Second, layers which don't have explicit sizes set, we now set their sizes
			// based on the overall composition size, and process the layer.
			foreach (Layer layer in this.VisibleLayers)
				if (!layer.HasFixedSize)
				{
					layer.CalculatedWidth = outputWidth;
					layer.CalculatedHeight = outputHeight;

					layer.Process();
				}

			// If any of the layers are not present, we don't create the image
			foreach (Layer layer in this.VisibleLayers)
				if (layer.Bitmap == null)
					return null;
		
			// Calculate X and Y for layers which are anchored.
			foreach (Layer layer in this.VisibleLayers)
			{
				if (layer.Bitmap != null && layer.Anchor != AnchorStyles.None)
				{
					// Set X.
					switch (layer.Anchor)
					{
						case AnchorStyles.BottomCenter :
						case AnchorStyles.MiddleCenter :
						case AnchorStyles.TopCenter :
							layer.X = (outputWidth - layer.Size.Value.Width) / 2;
							break;
						case AnchorStyles.BottomLeft :
						case AnchorStyles.MiddleLeft :
						case AnchorStyles.TopLeft :
							layer.X = layer.AnchorPadding;
							break;
						case AnchorStyles.BottomRight:
						case AnchorStyles.MiddleRight:
						case AnchorStyles.TopRight:
							layer.X = outputWidth - layer.Size.Value.Width - layer.AnchorPadding;
							break;
					}

					// Set Y.
					switch (layer.Anchor)
					{
						case AnchorStyles.BottomCenter:
						case AnchorStyles.BottomLeft:
						case AnchorStyles.BottomRight:
							layer.Y = outputHeight - layer.Size.Value.Height - layer.AnchorPadding;
							break;
						case AnchorStyles.MiddleCenter:
						case AnchorStyles.MiddleLeft:
						case AnchorStyles.MiddleRight:
							layer.Y = (outputHeight - layer.Size.Value.Height) / 2;
							break;
						case AnchorStyles.TopCenter:
						case AnchorStyles.TopLeft:
						case AnchorStyles.TopRight:
							layer.Y = layer.AnchorPadding;
							break;
					}
				}
			}

			// Apply fill.
			DrawingVisual dv = new DrawingVisual();
			DrawingContext dc = dv.RenderOpen();

			// Apply fill.
			Fill.Apply(dc, new Rect(0, 0, outputWidth, outputHeight));

			dc.Close();

			// create output bitmap and lock data
			RenderTargetBitmap rtb = RenderTargetBitmapUtility.CreateRenderTargetBitmap(outputWidth, outputHeight);
			rtb.Render(dv);
			FastBitmap output = new FastBitmap(rtb);

			// Blend layers using specified blend mode.
			//output = LayerBlender.BlendLayers(output, VisibleLayers);
			output = LayerBlender.BlendLayers(output, VisibleLayers);

			// If image format doesn't support transparency, make all transparent pixels totally opaque.
			// Otherwise WPF wants to save them as black.
			if (ImageFormat == DynamicImageFormat.Bmp || ImageFormat == DynamicImageFormat.Jpeg)
			{
				output.Lock();
				for (int y = 0; y < output.Height; ++y)
					for (int x = 0; x < output.Width; ++x)
					{
						Color c = output[x, y];
						//if (output[x, y].A == 0 && output[x, y].R == 0 && output[x, y].G == 0 && output[x, y].B == 0)
						output[x, y] = Color.FromArgb(255, c.R, c.G, c.B);
					}
				output.Unlock();
			}

			return output.InnerBitmap;
		}

		private void ValidateParameters()
		{
			// if SizeMode is set to Manual, we must have both a Width and a Height set
			if (!this.AutoSize)
				if (this.Width == null || this.Height == null)
					throw new InvalidOperationException("If AutoSize is false then both Width and Height must be set");

			// conversely, if SizeMode is set to Auto, we must have neither Width nor Height set
			if (this.AutoSize)
				if (this.Width != null || this.Height != null)
					throw new InvalidOperationException("If AutoSize is true then neither Width nor Height can be set");

			// If we don't have any layers, then AutoSize must be false otherwise no image will be generated.
			if (this.Layers.Count == 0 && this.AutoSize)
				throw new InvalidOperationException("If no layers are specified, then AutoSize must be set to false.");
		}

		internal Dependency[] GetDependencies()
		{
			List<Dependency> dependencies = new List<Dependency>();
			foreach (Layer layer in this.Layers)
				layer.PopulateDependencies(dependencies);
			return dependencies.ToArray();
		}

		#region View state implementation

		/// <summary>
		/// Loads the previously saved state of the <see cref="Composition" /> object.
		/// </summary>
		/// <param name="savedState">
		/// An object containing the saved view state values for the <see cref="Composition" /> object.
		/// </param>
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				Triplet triplet = (Triplet)savedState;
				base.LoadViewState(triplet.First);
				if (triplet.Second != null)
					((IStateManager) Layers).LoadViewState(triplet.Second);
				if (triplet.Third != null)
					((IStateManager)Fill).LoadViewState(triplet.Third);
			}
		}

		/// <summary>
		/// Saves the current view state of the <see cref="Composition" /> object.
		/// </summary>
		/// <param name="saveAll"><c>true</c> if all values should be saved regardless
		/// of whether they are dirty; otherwise <c>false</c>.</param>
		/// <returns>An object that represents the saved state. The default is <c>null</c>.</returns>
		protected override object SaveViewState(bool saveAll)
		{
			Triplet triplet = new Triplet();
			triplet.First = base.SaveViewState(saveAll);
			if (_layers != null)
				triplet.Second = ((IStateManagedObject) _layers).SaveViewState(saveAll);
			if (_fill != null)
				triplet.Third = ((IStateManagedObject)_fill).SaveViewState(saveAll);
			return (triplet.First == null && triplet.Second == null && triplet.Third == null) ? null : triplet;
		}

		/// <summary>
		/// Tracks view state changes to the <see cref="Composition" /> object.
		/// </summary>
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (_layers != null)
				((IStateManager) _layers).TrackViewState();
			if (_fill != null)
				((IStateManager)_fill).TrackViewState();
		}

		#endregion
	}
}

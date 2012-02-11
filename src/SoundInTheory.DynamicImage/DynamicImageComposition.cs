using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using SoundInTheory.DynamicMedia.Caching;
using System.IO;
using Isis.Drawing.Imaging;
using System.ComponentModel;
using System.Drawing.Design;
using Isis.Drawing;
using System.Web.UI;
using SoundInTheory.DynamicImage.Design;
using SoundInTheory.DynamicMedia;

namespace SoundInTheory.DynamicImage
{
	public class DynamicImageComposition : Composition
	{
		#region Properties

		[Browsable(true), Editor(typeof(LayerCollectionEditor), typeof(UITypeEditor)), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public LayerCollection Layers
		{
			get;
			private set;
		}

		#endregion

		public DynamicImageComposition()
			: base()
		{
			this.Layers = new LayerCollection();
		}

		protected override Image CreateImage()
		{
			// set design-mode properties
			foreach (Layer layer in this.Layers)
				if (layer is ImageLayer)
				{
					ImageLayer imageLayer = (ImageLayer) layer;
					imageLayer._site = this.Site;
					imageLayer._designMode = this.DesignMode;
				}

			// process layers
			foreach (Layer layer in this.Layers)
				layer.Process();

			// for SizeMode = Auto, we need to calculate the output dimensions
			// based on the combination of all layers' dimensions
			int outputWidth, outputHeight;
			if (this.AutoSize)
			{
				Rectangle outputDimensions = Rectangle.Empty;
				foreach (Layer layer in this.Layers)
				{
					Rectangle? bounds = layer.Bounds;
					if (bounds != null)
						outputDimensions = Rectangle.Union(outputDimensions, bounds.Value);
				}
				outputWidth = outputDimensions.Width;
				outputHeight = outputDimensions.Height;
			}
			else
			{
				outputWidth = this.Width.Value;
				outputHeight = this.Height.Value;
			}

			if (outputWidth == 0 && outputHeight == 0)
				return null;

			// create output bitmap and lock data
			FastBitmap output = new FastBitmap(outputWidth, outputHeight, PixelFormat.Format24bppRgb);

			// lock layer bitmaps and output bitmap
			output.Lock();
			foreach (Layer layer in this.Layers)
				layer.Bitmap.Lock();

			// calculate colors of flattened image
			// 1. take offsetx, offsety into consideration
			// 2. calculate alpha of color (alpha, opacity, mask)
			// 3. mix colors of current layer and layer below
			for (int y = 0; y < outputHeight; ++y)
			{
				for (int x = 0; x < outputWidth; ++x)
				{
					Color c0 = Color.Transparent;
					foreach (Layer layer in this.Layers)
					{
						Color c1 = Color.Transparent;

						if (x >= layer.X &&
							x <= layer.X + layer.Bitmap.Width - 1 &&
							y >= layer.Y &&
							y <= layer.Y + layer.Bitmap.Height - 1)
						{
							c1 = layer.Bitmap[x - layer.X, y - layer.Y];
						}

						if (c1.A == 255)
						{
							c0 = c1;
						}
						else
						{
							double a = c1.A / 255.0;
							double tr = c1.R * a + c0.R * (1.0 - a);
							double tg = c1.G * a + c0.G * (1.0 - a);
							double tb = c1.B * a + c0.B * (1.0 - a);
							tr = Math.Round(tr);
							tg = Math.Round(tg);
							tb = Math.Round(tb);
							tr = Math.Min(tr, 255);
							tg = Math.Min(tg, 255);
							tb = Math.Min(tb, 255);
							c0 = Color.FromArgb((byte) tr, (byte) tg, (byte) tb);
						}
					}
					output[x, y] = c0;
				}
			}

			// unlock layer bitmaps and output bitmap
			foreach (Layer layer in this.Layers)
				layer.Bitmap.Unlock();
			output.Unlock();

			return output.InnerBitmap;
		}

		protected override void ValidateParameters()
		{
			base.ValidateParameters();

			// sanity check for at least one layer
			if (this.Layers.Count == 0)
				throw new InvalidOperationException("At least one Layer must be added to the DynamicImage");

			foreach (Layer layer in Layers)
				layer.Validate();
		}

		protected override void PopulateDependencies(List<DatabaseDependency> dependencies)
		{
			foreach (Layer layer in this.Layers)
				layer.PopulateDependencies(dependencies);
		}

		protected override string GetCacheKey()
		{
			return string.Format("W:{0};H:{1};AS:{2};L:{{{3}}}",
				this.Width, this.Height, this.AutoSize,
				this.Layers.GetCacheKey());
		}
	}
}

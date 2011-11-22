using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.Filters
{
	/// <summary>
	/// Creates the effect of an image placed on a shiny floor. This filter
	/// adds a reflection of the image just below it.
	/// </summary>
	/// <see cref="http://www.jhlabs.com/java/java2d/reflections/index.html"/>
	/// <include file="../documentation.xml" path="docs/types/type[@name = 'ShinyFloorFilter']/*" />
	public class ShinyFloorFilter : ImageReplacementFilter
	{
		#region Properties

		/// <summary>
		/// Gets or sets the percentage of the image height that will be reflected
		/// </summary>
		[Browsable(true), Category("Appearance"), DefaultValue(50), Description("Gets or sets the percentage of the image height that will be reflected.")]
		public byte ReflectionPercentage
		{
			get { return (byte)(PropertyStore["ReflectionPercentage"] ?? (byte) 50); }
			set
			{
				if (value < 0 || value > 100)
					throw new ArgumentOutOfRangeException("value", "The percentage of reflection must range from 0 to 100");

				PropertyStore["ReflectionPercentage"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the opacity of the start of the reflected image.
		/// The reflection will fade out to transparent.
		/// </summary>
		[Browsable(true), Category("Appearance"), DefaultValue(75), Description("Gets or sets the opacity of the reflected image. The reflection will fade out to an opaque white.")]
		public byte ReflectionOpacity
		{
			get { return (byte)(PropertyStore["ReflectionOpacity"] ?? (byte) 75); }
			set
			{
				if (value < 0 || value > 100)
					throw new ArgumentOutOfRangeException("value", "The reflection opacity must range from 0 to 100.");

				PropertyStore["ReflectionOpacity"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the y-position of the reflected image
		/// </summary>
		[Browsable(true), DefaultValue(null), Description("Gets or sets the y-position of the reflected image.")]
		public int? ReflectionPositionY
		{
			get { return PropertyStore["ReflectionPositionY"] as int?; }
			set { PropertyStore["ReflectionPositionY"] = value; }
		}

		#endregion

		#region Methods

		/// <summary>
		/// Returns the dimensions of the output image.
		/// </summary>
		/// <param name="source">The source image.</param>
		/// <param name="width">The desired width of the output image.</param>
		/// <param name="height">The desired height of the output image.</param>
		/// <returns><c>true</c> if the destination image should be created; otherwise <c>false</c>.
		/// The <see cref="ShinyFloorFilter" /> will only create the destination image
		/// if the <see cref="ShinyFloorFilter.ReflectionPercentage" /> property is not 0, 
		/// and the <see cref="ShinyFloorFilter.ReflectionOpacity" /> property is not 100.</returns>
		protected override bool GetDestinationDimensions(FastBitmap source, out int width, out int height)
		{
			width = source.Width;
			height = GetReflectionOffsetY(source) + (int)(source.Height * (ReflectionPercentage / 100.0f));

			return ReflectionPercentage != 0;
		}

		/// <summary>
		/// Applies the <see cref="ShinyFloorFilter" /> to the specified <paramref name="source"/>.
		/// </summary>
		/// <param name="source">The source image.</param>
		/// <param name="dc"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		protected override void ApplyFilter(FastBitmap source, DrawingContext dc, int width, int height)
		{
			// First, draw reflected image with an opacity mask.
			int reflectionHeight = (int)(source.Height * (ReflectionPercentage / 100.0f));
			dc.PushTransform(new TransformGroup
			{
				Children = new TransformCollection
				{
					new ScaleTransform {ScaleY = -1},
					new TranslateTransform {Y = GetReflectionOffsetY(source) + reflectionHeight}
				}
			});
			dc.PushOpacityMask(new LinearGradientBrush(
				Colors.Transparent,
				Color.FromArgb((byte)(255.0f * (ReflectionOpacity / 100.0f)), 0, 0, 0),
				new Point(0, 0),
				new Point(0, 1)));

			dc.DrawImage(new CroppedBitmap(source.InnerBitmap, new Int32Rect(0, source.Height - reflectionHeight, source.Width, reflectionHeight)),
				new Rect(0, 0, source.Width, reflectionHeight));


			dc.Pop();
			dc.Pop();

			// Draw original image.
			dc.DrawImage(source.InnerBitmap, new Rect(0, 0, source.Width, source.Height));
		}

		private int GetReflectionOffsetY(FastBitmap source)
		{
			return (ReflectionPositionY ?? source.Height);
		}

		#endregion
	}
}
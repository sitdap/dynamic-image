using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.Filters
{
	/// <summary>
	/// <para>Changes the size of a <see cref="Layer" />. This filter can be used in several ways
	/// depending on the effect you wish to achieve.</para>
	/// <para>There are five modes available for the <see cref="ResizeFilter" /> filter.</para>
	/// </summary>
	/// <remarks>By default, images will not be enlarged if they are smaller than 
	/// the target size. However, you can set the <see cref="ResizeFilter.EnlargeImage" /> 
	/// property to true to allow enlargement.</remarks>
	public class ResizeFilter : ImageReplacementFilter
	{
		#region Fields

		private int _xOffset, _yOffset, _sourceWidth, _sourceHeight;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets a value that describes how the image should be stretched 
		/// to fill the specified width and height. Defaults to Uniform.
		/// </summary>
		public ResizeMode Mode
		{
			get { return (ResizeMode)(this["Mode"] ?? ResizeMode.Uniform); }
			set { this["Mode"] = value; }
		}

		/// <summary>
		/// Gets or sets the desired image width. Defaults to 200.
		/// </summary>
		public Unit Width
		{
			get { return (Unit)(this["Width"] ?? Unit.Pixel(200)); }
			set { this["Width"] = value; }
		}

		/// <summary>
		/// Gets or sets the desired image height. Defaults to 200.
		/// </summary>
		public Unit Height
		{
			get { return (Unit)(this["Height"] ?? Unit.Pixel(200)); }
			set { this["Height"] = value; }
		}

		/// <summary>
		/// Gets or sets how the image is interpolated when it is resized. Usually this can
		/// be left as the default value. Defaults to HighQuality.
		/// </summary>
		public BitmapScalingMode BitmapScalingMode
		{
			get { return (BitmapScalingMode)(this["BitmapScalingMode"] ?? BitmapScalingMode.HighQuality); }
			set { this["BitmapScalingMode"] = value; }
		}

		/// <summary>
		/// Gets or sets a value that indicates whether the image will be enlarged 
		/// if it is smaller than the requested dimensions. Defaults to false.
		/// </summary>
		public bool EnlargeImage
		{
			get { return (bool)(this["EnlargeImage"] ?? false); }
			set { this["EnlargeImage"] = value; }
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
		/// The <see cref="ResizeFilter" /> will only create the destination image
		/// if the requested dimensions are different from the current dimensions.</returns>
		protected override bool GetDestinationDimensions(FastBitmap source, out int width, out int height)
		{
			width = height = _xOffset = _yOffset = 0;
			_sourceWidth = source.Width;
			_sourceHeight = source.Height;

			int calculatedWidth = Unit.GetCalculatedValue(Width, source.Width);
			int calculatedHeight = Unit.GetCalculatedValue(Height, source.Height);
			int? requestedWidth = null, requestedHeight = null;
			switch (Mode)
			{
				case ResizeMode.UseWidth:
					if (EnlargeImage || calculatedWidth < source.Width)
						requestedWidth = calculatedWidth;
					break;
				case ResizeMode.UseHeight:
					if (EnlargeImage || calculatedHeight < source.Height)
						requestedHeight = calculatedHeight;
					break;
				case ResizeMode.Fill:
					requestedWidth = calculatedWidth;
					requestedHeight = calculatedHeight;
					break;
				case ResizeMode.Uniform:
					// If both requested dimensions are greater than source image, we don't need to do any resizing.
					if (this.EnlargeImage || (calculatedWidth < source.Width || calculatedHeight < source.Height))
					{
						// Calculate requested width and height so as not to squash image.
						int tempWidth, tempHeight;

						// First, resize based on max width and check whether resized height will be more than max height.
						CalculateOutputDimensions(source.Width, source.Height, calculatedWidth, null, out tempWidth, out tempHeight);
						if (tempHeight > calculatedHeight)
						{
							// If so, we need to resize based on max height instead.
							requestedHeight = calculatedHeight;
						}
						else
						{
							// If not, we have our max dimension.
							requestedWidth = calculatedWidth;
						}
					}
					break;
				case ResizeMode.UniformFill:
					// Resize based on width first. If this means that height is less than target height, we resize based on height.
					if (this.EnlargeImage || (calculatedWidth < source.Width || calculatedHeight < source.Height))
					{
						// Calculate requested width and height so as not to squash image.
						int tempWidth, tempHeight;

						// First, resize based on width and check whether resized height will be more than max height.
						CalculateOutputDimensions(source.Width, source.Height, calculatedWidth, null, out tempWidth, out tempHeight);
						if (tempHeight < calculatedHeight)
						{
							// If so, we need to resize based on max height instead.
							requestedHeight = calculatedHeight;

							CalculateOutputDimensions(source.Width, source.Height, null, calculatedHeight, out tempWidth, out tempHeight);

							// Then crop width and calculate offset.
							requestedWidth = calculatedWidth;
							_sourceWidth = (int)((calculatedWidth / (float)tempWidth) * source.Width);
							_xOffset = (source.Width - _sourceWidth) / 2;
						}
						else
						{
							// If not, we have our max dimension.
							requestedWidth = calculatedWidth;

							// Then crop height and calculate offset.
							requestedHeight = calculatedHeight;
							_sourceHeight = (int)((calculatedHeight / (float)tempHeight) * source.Height);
							_yOffset = (source.Height - _sourceHeight) / 2;
						}
					}
					break;
			}

			if (requestedWidth == null && requestedHeight == null)
				return false;

			CalculateOutputDimensions(source.Width, source.Height, requestedWidth, requestedHeight, out width, out height);
			return true;
		}

		private static void CalculateOutputDimensions(
			int nInputWidth, int nInputHeight,
			int? nRequestedWidth, int? nRequestedHeight,
			out int nOutputWidth, out int nOutputHeight)
		{
			// both width and height are specified - squash image
			if (nRequestedWidth != null && nRequestedHeight != null)
			{
				nOutputWidth = nRequestedWidth.Value;
				nOutputHeight = nRequestedHeight.Value;
			}
			else if (nRequestedWidth != null) // calculate height to keep aspect ratio
			{
				nOutputWidth = nRequestedWidth.Value;
				double dAspectRatio = (double)nInputWidth / (double)nInputHeight;
				nOutputHeight = (int)(nOutputWidth / dAspectRatio);
			}
			else if (nRequestedHeight != null) // calculate width to keep aspect ratio
			{
				nOutputHeight = nRequestedHeight.Value;
				double dAspectRatio = (double)nInputHeight / (double)nInputWidth;
				nOutputWidth = (int)(nOutputHeight / dAspectRatio);
			}
			else
			{
				throw new Exception("Width or height, or both, must be specified");
			}
		}

		/// <summary>
		/// Applies the <see cref="ResizeFilter" /> to the specified <paramref name="source"/>.
		/// </summary>
		/// <param name="source">The source image.</param>
		/// <param name="destination">The destination image.</param>
		/// <param name="g">A <see cref="System.Drawing.Graphics" /> object, created from <paramref name="destination"/>.</param>
		protected override void ApplyFilter(FastBitmap source, DrawingContext dc, int destinationWidth, int destinationHeight)
		{
			ImageSource imageSource = source.InnerBitmap;
			if (_xOffset != 0 || _yOffset != 0)
				imageSource = new CroppedBitmap(source.InnerBitmap, new Int32Rect(_xOffset, _yOffset, _sourceWidth, _sourceHeight));

			DrawingGroup dg = new DrawingGroup();
			RenderOptions.SetBitmapScalingMode(dg, BitmapScalingMode);
			dg.Children.Add(new ImageDrawing(imageSource, new Rect(0, 0, destinationWidth, destinationHeight)));

			dc.DrawDrawing(dg);
		}

		#endregion
	}

	/// <summary>
	/// Specifies how the image should be stretched.
	/// </summary>
	public enum ResizeMode
	{
		/// <summary>
		/// The content is resized based on the new width value 
		/// while it preserves its native aspect ratio.
		/// </summary>
		UseWidth,

		/// <summary>
		/// The content is resized based on the new height value 
		/// while it preserves its native aspect ratio.
		/// </summary>
		UseHeight,

		/// <summary>
		/// The content is resized to fill the destination dimensions. 
		/// The aspect ratio is not preserved.
		/// </summary>
		Fill,

		/// <summary>
		/// The content is resized to fit in the destination dimensions while 
		/// it preserves its native aspect ratio.
		/// </summary>
		Uniform,

		/// <summary>
		/// The content is resized to fill the destination dimensions while it preserves its native aspect ratio.
		/// If the aspect ratio of the destination rectangle differs from the source,
		/// the source content is clipped to fit in the destination dimensions.
		/// The source content is centred in the clipped rectangle.
		/// </summary>
		UniformFill
	}
}

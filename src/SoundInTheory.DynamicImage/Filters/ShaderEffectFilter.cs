using System.Windows.Media;
using System.Windows.Media.Effects;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.Filters
{
	/// <summary>
	/// The base class for filters that apply an HLSL shader to the image.
	/// </summary>
	public abstract class ShaderEffectFilter : ImageReplacementFilter
	{
		/// <summary>
		/// Returns the dimensions of the output image.
		/// </summary>
		/// <param name="source">The source image.</param>
		/// <param name="width">The desired width of the output image.</param>
		/// <param name="height">The desired height of the output image.</param>
		/// <returns><c>true</c> if the destination image should be created; otherwise <c>false</c>.
		/// <see cref="ColourMatrixFilter" /> always returns <c>true</c> and sets the destination
		/// dimensions to the same as the source dimensions.</returns>
		protected override bool GetDestinationDimensions(FastBitmap source, out int width, out int height)
		{
			width = source.Width;
			height = source.Height;
			return true;
		}

		protected override void ConfigureDrawingVisual(FastBitmap source, DrawingVisual drawingVisual)
		{
			Effect shaderEffect = GetEffect(source);
			drawingVisual.Effect = shaderEffect;
		}

		protected abstract Effect GetEffect(FastBitmap source);

		/// <summary>
		/// Applies the filter to the specified source.
		/// </summary>
		/// <param name="source">The source image.</param>
		/// <param name="destination">The destination image.</param>
		/// <param name="g">A <see cref="System.Drawing.Graphics" /> object, created from <paramref name="destination"/>.</param>
		protected override void ApplyFilter(FastBitmap source, DrawingContext dc, int width, int height)
		{
			dc.DrawImage(source.InnerBitmap, new System.Windows.Rect(0, 0, source.Width, source.Height));
		}
	}
}
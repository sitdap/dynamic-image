namespace SoundInTheory.DynamicImage.Filters
{
	public enum EdgeAction
	{
		/// <summary>
		/// Treat pixels off the edge as zero.
		/// </summary>
		Zero,

		/// <summary>
		/// Clamp pixels to the image edges.
		/// </summary>
		Clamp,
		
		/// <summary>
		/// Wrap pixels off the edge onto the opposite edge.
		/// </summary>
		Wrap,

		/// <summary>
		/// Clamp pixels RGB to the image edges, but zero the alpha. This prevents gray borders on your image.
		/// </summary>
		RgbClamp
	}
}
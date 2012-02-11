using System;

namespace SoundInTheory.DynamicImage.Caching
{
	/// <summary>
	/// Specifies how generated images will be cached.
	/// </summary>
	public enum DynamicImageCachingMode
	{
		/// <summary>
		/// Caching is disabled.
		/// </summary>
		Off,

		/// <summary>
		/// Caching is in-process with an ASP.NET worker process.
		/// </summary>
		InProc,

		/// <summary>
		/// Caching is using a custom data store to store generated images.
		/// </summary>
		Custom
	}
}

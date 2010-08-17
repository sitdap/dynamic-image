using System;

namespace SoundInTheory.DynamicImage
{
	public class DynamicImageException : ApplicationException
	{
		public DynamicImageException(string message)
			: base(message)
		{
			
		}

		public DynamicImageException(string message, Exception innerException)
			: base(message, innerException)
		{

		}
	}
}
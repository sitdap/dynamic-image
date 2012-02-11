using System;

namespace SoundInTheory.DynamicImage.Util
{
	internal static class MathUtility
	{
		public static float ToRadians(int degrees)
		{
			return degrees * (float)Math.PI / 180.0f;
		}
	}
}
using System;
using System.Windows;

namespace SoundInTheory.DynamicImage.Util
{
	internal static class Int32RectUtility
	{
		public static Int32Rect Union(Int32Rect rect1, Int32Rect rect2)
		{
			Int32Rect result = new Int32Rect();

			int minLeft = Math.Min(rect1.X, rect2.X);
			int minTop = Math.Min(rect1.Y, rect2.Y);
			int maxRight = Math.Max(rect1.X + rect1.Width, rect2.X + rect2.Width);
			result.Width = Math.Max(maxRight - minLeft, 0);

			int maxBottom = Math.Max(rect1.Y + rect1.Height, rect2.Y + rect2.Height);
			result.Height = Math.Max(maxBottom - minTop, 0);

			result.X = minLeft;
			result.Y = minTop;

			return result;
		}
	}
}
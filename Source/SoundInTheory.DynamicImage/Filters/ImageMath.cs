using System.Windows;
using System.Windows.Media;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.Filters
{
	public static class ImageMath
	{
		/// <summary>
		/// Bilinear interpolation of ARGB values.
		/// </summary>
		/// <param name="x">The X interpolation parameter 0..1</param>
		/// <param name="y">The y interpolation parameter 0..1</param>
		/// <param name="nw"></param>
		/// <param name="ne"></param>
		/// <param name="sw"></param>
		/// <param name="se"></param>
		/// <returns>The interpolated value.</returns>
		public static int BilinearInterpolate(float x, float y, int nw, int ne, int sw, int se)
		{
			int a0 = (nw >> 24) & 0xff;
			int r0 = (nw >> 16) & 0xff;
			int g0 = (nw >> 8) & 0xff;
			int b0 = nw & 0xff;
			int a1 = (ne >> 24) & 0xff;
			int r1 = (ne >> 16) & 0xff;
			int g1 = (ne >> 8) & 0xff;
			int b1 = ne & 0xff;
			int a2 = (sw >> 24) & 0xff;
			int r2 = (sw >> 16) & 0xff;
			int g2 = (sw >> 8) & 0xff;
			int b2 = sw & 0xff;
			int a3 = (se >> 24) & 0xff;
			int r3 = (se >> 16) & 0xff;
			int g3 = (se >> 8) & 0xff;
			int b3 = se & 0xff;

			float cx = 1.0f - x;
			float cy = 1.0f - y;

			float m0 = cx * a0 + x * a1;
			float m1 = cx * a2 + x * a3;
			int a = (int)(cy * m0 + y * m1);

			m0 = cx * r0 + x * r1;
			m1 = cx * r2 + x * r3;
			int r = (int)(cy * m0 + y * m1);

			m0 = cx * g0 + x * g1;
			m1 = cx * g2 + x * g3;
			int g = (int)(cy * m0 + y * m1);

			m0 = cx * b0 + x * b1;
			m1 = cx * b2 + x * b3;
			int b = (int)(cy * m0 + y * m1);

			return (a << 24) | (r << 16) | (g << 8) | b;
		}

		/// <summary>
		/// Bilinear interpolation of ARGB values.
		/// </summary>
		/// <param name="x">The X interpolation parameter 0..1</param>
		/// <param name="y">The y interpolation parameter 0..1</param>
		/// <param name="nw"></param>
		/// <param name="ne"></param>
		/// <param name="sw"></param>
		/// <param name="se"></param>
		/// <returns>The interpolated value.</returns>
		public static Color BilinearInterpolate(double x, double y, Color nw, Color ne, Color sw, Color se)
		{
			double cx = 1.0 - x;
			double cy = 1.0 - y;

			//nw = ColorUtility.PreMultiplyAlpha(nw);
			//ne = ColorUtility.PreMultiplyAlpha(ne);
			//sw = ColorUtility.PreMultiplyAlpha(sw);
			//se = ColorUtility.PreMultiplyAlpha(se);

			double m0 = cx * nw.A + x * ne.A;
			double m1 = cx * sw.A + x * se.A;
			byte a = (byte)(cy * m0 + y * m1);

			m0 = cx * nw.R + x * ne.R;
			m1 = cx * sw.R + x * se.R;
			byte r = (byte)(cy * m0 + y * m1);

			m0 = cx * nw.G + x * ne.G;
			m1 = cx * sw.G + x * se.G;
			byte g = (byte)(cy * m0 + y * m1);

			m0 = cx * nw.B + x * ne.B;
			m1 = cx * sw.B + x * se.B;
			byte b = (byte)(cy * m0 + y * m1);

			Color result = Color.FromArgb(a, r, g, b);
			//result = ColorUtility.UnPreMultiplyAlpha(result);
			return result;
		}

		/// <summary>
		/// Clamps a value to an interval.
		/// </summary>
		/// <param name="x">The input parameter.</param>
		/// <param name="a">The lower clamp threshold.</param>
		/// <param name="b">The upper clamp threshold.</param>
		/// <returns>The clamped value.</returns>
		public static int Clamp(int x, int a, int b)
		{
			return (x < a) ? a : (x > b) ? b : x;
		}

		/// <summary>
		/// Returns a mod b. This differs from the % operator with respect to negative numbers.
		/// </summary>
		/// <param name="a">The dividend.</param>
		/// <param name="b">The divisor.</param>
		/// <returns>a mod b</returns>
		public static double Mod(double a, double b)
		{
			int n = (int) (a / b);

			a -= n * b;
			if (a < 0)
				return a + b;
			return a;
		}

		/// <summary>
		/// Returns a mod b. This differs from the % operator with respect to negative numbers.
		/// </summary>
		/// <param name="a">The dividend.</param>
		/// <param name="b">The divisor.</param>
		/// <returns>a mod b</returns>
		public static int Mod(int a, int b)
		{
			int n = a / b;

			a -= n * b;
			if (a < 0)
				return a + b;
			return a;
		}
	}
}
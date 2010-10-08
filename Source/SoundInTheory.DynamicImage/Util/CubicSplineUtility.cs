using System;

namespace SoundInTheory.DynamicImage.Util
{
	internal static class CubicSplineUtility
	{
		/// <summary>
		/// With a little help from Numerical Recipes...
		/// </summary>
		/// <returns></returns>
		public static float[] CalculateSpline(float[] x, float[] y)
		{
			int n = x.Length;

			// Use for temporary storage.
			float[] u = new float[n];

			// Initialise the spline with "natural" lower boundary condition.
			float[] result = new float[n];
			result[0] = u[0] = 0.0f;

			// This is the decomposition loop of the tridiagonal algorithm.
			for (int i = 1; i < n - 1; ++i)
			{
				float sig = (x[i] - x[i - 1]) / (x[i + 1] - x[i - 1]);
				float p = sig * result[i - 1] + 2.0f;
				result[i] = (sig - 1.0f) / p;

				u[i] = (y[i + 1] - y[i]) / (x[i + 1] - x[i]) - (y[i] - y[i - 1]) / (x[i] - x[i - 1]);
				u[i] = (6.0f * u[i] / (x[i + 1] - x[i - 1]) - sig * u[i - 1]) / p;
			}

			float qn = 0.0f, un = 0.0f;
			result[n - 1] = (un - qn * u[n - 1]) / (qn * result[n - 1] + 1.0f);

			// This is the backsubstitution loop of the tridiagonal algorithm.
			for (int k = n - 2; k >= 0; --k)
				result[k] = result[k] * result[k + 1] + u[k];

			return result;
		}

		public static float InterpolateSpline(float[] x, float[] y, float[] derivatives, float value)
		{
			int n = x.Length;

			// We will find the right place in the table by means of bisection.
			int klo = 0;
			int khi = n - 1;
			while (khi - klo > 1)
			{
				int k = (khi + klo) >> 1;
				if (x[k] > value)
					khi = k;
				else
					klo = k;
			}

			// klo and khi now bracket the input value of x.
			float h = x[khi] - x[klo];

			if (h == 0.0f)
				throw new Exception("Bad x input. The x values must be distinct.");

			float a = (x[khi] - value) / h;
			float b = (value - x[klo]) / h;
			
			// Cubic spline polynomial is now evaluated.
			return a * y[klo] + b * y[khi] + ((a * a * a - a) * derivatives[klo] + (b * b * b - b) * derivatives[khi]) * (h * h) / 6.0f;
		}
	}
}
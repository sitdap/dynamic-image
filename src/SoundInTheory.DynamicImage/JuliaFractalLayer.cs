using System.ComponentModel;

namespace SoundInTheory.DynamicImage
{
	public class JuliaFractalLayer : FractalLayer
	{
		[DefaultValue(-0.7f)]
		public float ConstantReal
		{
			get { return (int)(ViewState["ConstantReal"] ?? -0.7f); }
			set { ViewState["ConstantReal"] = value; }
		}

		[DefaultValue(0.27015f)]
		public float ConstantImaginary
		{
			get { return (int)(ViewState["ConstantImaginary"] ?? 0.27015f); }
			set { ViewState["ConstantImaginary"] = value; }
		}

		internal override ColorHsv CalculateFractalColour(int x, int y)
		{
			// Calculate the real and imaginary part of z, based on the pixel location
			// and zoom and position values.
			float newRe = 1.5f * (x - this.Width / 2.0f) / (0.5f * this.Zoom * this.Width) + this.OffsetX;
			float newIm = (y - this.Height / 2.0f) / (0.5f * this.Zoom * this.Height) + this.OffsetY;

			// Start the iteration process.
			int i;
			for (i = 0; i < MaxIterations; i++)
			{
				// Remember value of previous iteration.
				float oldRe = newRe;
				float oldIm = newIm;

				// Calculate real and imaginary parts.
				newRe = oldRe * oldRe - oldIm * oldIm + this.ConstantReal;
				newIm = 2.0f * oldRe * oldIm + this.ConstantImaginary;

				// If the point is outside the circle with radius 2, stop.
				if ((newRe * newRe + newIm * newIm) > 4)
					break;
			}

			return new ColorHsv { H = i % 256, S = 255, V = 255 * ((i < this.MaxIterations) ? 1 : 0) };
		}
	}
}

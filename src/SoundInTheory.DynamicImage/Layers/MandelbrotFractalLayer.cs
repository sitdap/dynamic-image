namespace SoundInTheory.DynamicImage.Layers
{
	public class MandelbrotFractalLayer : FractalLayer
	{
		public override float OffsetX
		{
			get { return (float)(this["OffsetX"] ?? -0.5f); }
			set { this["OffsetX"] = value; }
		}

		internal override ColorHsv CalculateFractalColour(int x, int y)
		{
			// Calculate the real and imaginary part of z, based on the pixel location
			// and zoom and position values.
			float pr = 1.5f * (x - this.Width / 2.0f) / (0.5f * this.Zoom * this.Width) + this.OffsetX;
			float pi = (y - this.Height / 2.0f) / (0.5f * this.Zoom * this.Height) + this.OffsetY;
			float newRe = 0, newIm = 0;

			// Start the iteration process.
			int i;
			for (i = 0; i < MaxIterations; i++)
			{
				// Remember value of previous iteration.
				float oldRe = newRe;
				float oldIm = newIm;

				// Calculate real and imaginary parts.
				newRe = oldRe * oldRe - oldIm * oldIm + pr;
				newIm = 2.0f * oldRe * oldIm + pi;

				// If the point is outside the circle with radius 2, stop.
				if ((newRe * newRe + newIm * newIm) > 4)
					break;
			}

			return new ColorHsv { H = i % 256, S = 255, V = 255 * ((i < this.MaxIterations) ? 1 : 0) };
		}
	}
}

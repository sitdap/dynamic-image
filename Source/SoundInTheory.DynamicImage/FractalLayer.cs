using System.ComponentModel;
using System.Windows.Media;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage
{
	/// <summary>
	/// http://student.kuleuven.be/~m0216922/CG/juliamandelbrot.html
	/// </summary>
	public abstract class FractalLayer : Layer
	{
		#region Properties

		[Browsable(true), DefaultValue(400), Category("Layout"), Description("Width of the layer")]
		public int Width
		{
			get { return (int)(ViewState["Width"] ?? 400); }
			set { ViewState["Width"] = value; }
		}

		[Browsable(true), DefaultValue(300), Category("Layout"), Description("Height of the layer")]
		public int Height
		{
			get { return (int)(ViewState["Height"] ?? 300); }
			set { ViewState["Height"] = value; }
		}

		[DefaultValue(300)]
		public int MaxIterations
		{
			get { return (int)(ViewState["MaxIterations"] ?? 300); }
			set { ViewState["MaxIterations"] = value; }
		}

		[DefaultValue(1.0f)]
		public float Zoom
		{
			get { return (int)(ViewState["Zoom"] ?? 1.0f); }
			set { ViewState["Zoom"] = value; }
		}

		[DefaultValue(0.0f)]
		public virtual float OffsetX
		{
			get { return (int)(ViewState["OffsetX"] ?? 0.0f); }
			set { ViewState["OffsetX"] = value; }
		}

		[DefaultValue(0.0f)]
		public virtual float OffsetY
		{
			get { return (int)(ViewState["OffsetY"] ?? 0.0f); }
			set { ViewState["OffsetY"] = value; }
		}

		public override bool HasFixedSize
		{
			get { return true; }
		}

		#endregion

		protected override void CreateImage()
		{
			Bitmap = new FastBitmap(Width, Height);
			Bitmap.Lock();

			for (int y = 0; y < Height; y++)
				for (int x = 0; x < Width; x++)
				{
					ColorHsv colourHsv = CalculateFractalColour(x, y);
					Color colour = (Color) colourHsv;
					Bitmap[x, y] = colour;
				}

			Bitmap.Unlock();
		}

		internal abstract ColorHsv CalculateFractalColour(int x, int y);

		public override string ToString()
		{
			return string.Format("Fractal Layer");
		}
	}
}

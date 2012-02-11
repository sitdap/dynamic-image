using System.Windows.Media.Effects;
using SoundInTheory.DynamicImage.ShaderEffects;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.Filters
{
	/// <summary>
	/// Embosses an image.
	/// </summary>
	public class EmbossFilter : ShaderEffectFilter
	{
		#region Fields

		//private const float PIXEL_SCALE = 255.9f;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the bump height for embossing. Defaults to 3.
		/// </summary>
		public float Amount
		{
			get { return (float)(this["Amount"] ?? 3.0f); }
			set
			{
				//if (value < 2 || value > 50)
				//    throw new ArgumentException("The bump height must be between 2 and 50.", "value");
				this["Amount"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the bump height for embossing. Defaults to 3.
		/// </summary>
		public float Width
		{
			get { return (float)(this["Width"] ?? 3.0f); }
			set
			{
				//if (value < 2 || value > 50)
				//	throw new ArgumentException("The bump height must be between 2 and 50.", "value");
				this["Width"] = value;
			}
		}

		/*/// <summary>
		/// Gets or sets the bump height for embossing.
		/// </summary>
		[DefaultValue(3.0f), Description("Gets or sets the bump height for embossing.")]
		public float BumpHeight
		{
			get { return (float) (PropertyStore["BumpHeight"] ?? 3.0f); }
			set
			{
				if (value < 2 || value > 50)
					throw new ArgumentException("The bump height must be between 2 and 50.", "value");

				PropertyStore["BumpHeight"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the light elevation in degrees.
		/// </summary>
		[DefaultValue(30), Description("Gets or sets the light elevation in degrees.")]
		public int LightElevation
		{
			get { return (int)(PropertyStore["LightElevation"] ?? 30); }
			set
			{
				if (value < 0 || value > 90)
					throw new ArgumentException("The light elevation must be between 0 and 90 degrees.", "value");

				PropertyStore["LightElevation"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the light elevation in degrees.
		/// </summary>
		[DefaultValue(0), Description("Gets or sets the light angle in degrees.")]
		public int LightAngle
		{
			get { return (int)(PropertyStore["LightAngle"] ?? 0); }
			set
			{
				if (value < 0 || value > 360)
					throw new ArgumentException("The light angle must be between 0 and 360 degrees.", "value");

				PropertyStore["LightAngle"] = value;
			}
		}*/

		#endregion

		#region Methods

		protected override Effect GetEffect(FastBitmap source)
		{
			return new EmbossEffect
			{
				Amount = Amount,
				Width = Width/(double) source.Width
			};
		}

		/*protected override bool GetDestinationDimensions(FastBitmap source, out int width, out int height)
		{
			width = source.Width;
			height = source.Height;
			return true;
		}

		protected override void ApplyFilter(FastBitmap source, FastBitmap destination, Graphics g)
		{
			int[,] bumpPixels = new int[source.Width,source.Height];
			try
			{
				source.Lock();

				for (int y = 0; y < source.Height; ++y)
					for (int x = 0; x < source.Width; ++x)
					{
						Color sourceColour = source[x, y];
						bumpPixels[x, y] = (sourceColour.R + sourceColour.G + sourceColour.B)/3;
					}
			}
			finally
			{
				source.Unlock();
			}

			int Lx = (int)(Math.Cos(MathUtility.ToRadians(LightAngle)) * Math.Cos(MathUtility.ToRadians(LightElevation)) * PIXEL_SCALE);
			int Ly = (int)(Math.Sin(MathUtility.ToRadians(LightAngle)) * Math.Cos(MathUtility.ToRadians(LightElevation)) * PIXEL_SCALE);
			int Lz = (int)(Math.Sin(MathUtility.ToRadians(LightElevation)) * PIXEL_SCALE);

			int Nz = (int)(6 * 255 / BumpHeight);
			int Nz2 = Nz * Nz;
			int NzLz = Nz * Lz;

			int background = Lz;

			try
			{
				destination.Lock();
				for (int y = 0; y < source.Height; ++y)
					for (int x = 0; x < source.Width; ++x)
					{
						int shade;
						if (y != 0 && y < source.Height - 2 && x != 0 && x < source.Width - 2)
						{
							int Nx = bumpPixels[x - 1, y] + bumpPixels[x - 1, y + 1] + bumpPixels[x - 1, y + 2] - bumpPixels[x + 1, y] - bumpPixels[x + 1, y + 1] - bumpPixels[x + 1, y + 2];
							int Ny = bumpPixels[x - 1, y + 2] + bumpPixels[x, y + 2] + bumpPixels[x + 1, y + 2] - bumpPixels[x - 1, y] - bumpPixels[x, y] - bumpPixels[x + 1, y];

							int NdotL;
							if (Nx == 0 && Ny == 0)
								shade = background;
							else if ((NdotL = Nx*Lx + Ny*Ly + NzLz) < 0)
								shade = 0;
							else
								shade = (int) (NdotL/Math.Sqrt(Nx*Nx + Ny*Ny + Nz2));
						}
						else
							shade = background;

						destination[x, y] = Color.FromArgb(shade, shade, shade);
					}
			}
			finally
			{
				destination.Unlock();
			}
		}*/

		#endregion
	}
}

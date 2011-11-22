using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Media3D;
using SoundInTheory.DynamicImage.ShaderEffects;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.Filters
{
	/// <summary>
	/// Makes a specified color transparent in the output image. Can also use the
	/// top-left pixel in the source image as the transparent color.
	/// </summary>
	/// <include file="../documentation.xml" path="docs/types/type[@name = 'ColorKeyFilter']/*" />
	public class ColorKeyFilter : ShaderEffectFilter
	{
		#region Properties

		/// <summary>
		/// Gets or sets the transparent color.
		/// </summary>
		[DefaultValue(typeof(Colors), "White"), Description("Gets or sets the transparent color.")]
		public Color Color
		{
			get { return (Color)(PropertyStore["Color"] ?? Colors.White); }
			set { PropertyStore["Color"] = value; }
		}

		/// <summary>
		/// Gets or sets the color tolerance for transparency. This value is used to create a range around the Color parameter, within which colors will be considered transparent. Ranges from 0 to 255.
		/// </summary>
		[DefaultValue(0), Description("Gets or sets the color tolerance for transparency. This value is used to create a range around the Color parameter, within which colors will be considered transparent. Ranges from 0 to 255.")]
		public byte ColorTolerance
		{
			get { return (byte)(PropertyStore["ColorTolerance"] ?? 0); }
			set { PropertyStore["ColorTolerance"] = value; }
		}

		/// <summary>
		/// Gets or sets whether the first (top left) pixel of an image will be used as the transparent color.
		/// </summary>
		[DefaultValue(false), Description("Gets or sets whether the first (top left) pixel of an image will be used as the transparent color.")]
		public bool UseFirstPixel
		{
			get { return (bool)(PropertyStore["UseFirstPixel"] ?? false); }
			set { PropertyStore["UseFirstPixel"] = value; }
		}

		#endregion

		protected override Effect GetEffect(FastBitmap source)
		{
			Color transparentColor;
			if (UseFirstPixel)
			{
				source.Lock();
				transparentColor = source[0, 0];
				source.Unlock();
			}
			else
			{
				transparentColor = Color;
			}

			return new ColorKeyEffect
			{
				ColorTolerance = ColorTolerance / 255.0,
				TransparentColor = transparentColor
			};
		}

		public override string ToString()
		{
			return "Color Key";
		}
	}
}
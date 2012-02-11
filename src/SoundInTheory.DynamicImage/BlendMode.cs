namespace SoundInTheory.DynamicImage
{
	/// <summary>
	/// Used to affect how layers are blended together.
	/// </summary>
	public enum BlendMode
	{
		/// <summary>
		/// Selects the source color, ignoring the backdrop.
		/// </summary>
		Normal = 0,

		/// <summary>
		/// Edits or paints each pixel to make it the result color. However, the result color is a random replacement 
		/// of the pixels with the base color or the blend color, depending on the opacity at any pixel location. 
		/// </summary>
		Dissolve = 1,

		/// <summary>
		/// Multiplies the backdrop and source color values.
		/// </summary>
		/// <remarks>
		/// The result color is always at least as dark as either of the two constituent colors. Multiplying
		/// any color with black produces black; multiplying with white leaves the original color
		/// unchanged. Painting successive overlapping objects with a color other than black or white
		/// produces progressively darker colors.
		/// </remarks>
		Multiply = 2,

		/// <summary>
		/// Multiplies the complements of the backdrop and source color values, then complements
		/// the result.
		/// </summary>
		/// <remarks>
		/// The result color is always at least as light as either of the two constituent colors. Screening
		/// any color with white produces white; screening with black leaves the original color unchanged.
		/// The effect is similar to projecting multiple photographic slides simultaneously
		/// onto a single screen.
		/// </remarks>
		Screen = 3,

		/// <summary>
		/// Multiplies or screens the colors, depending on the backdrop color value.
		/// </summary>
		/// <remarks>
		/// Source colors
		/// overlay the backdrop while preserving its highlights and shadows. The backdrop color is
		/// not replaced but is mixed with the source color to reflect the lightness or darkness of the
		/// backdrop.
		/// </remarks>
		Overlay = 4,

		/// <summary>
		/// Selects the darker of the backdrop and source colors.
		/// </summary>
		/// <remarks>
		///	The backdrop is replaced with the source where the source is darker; otherwise, it is left
		/// unchanged.
		/// </remarks>
		Darken = 5,

		/// <summary>
		/// Selects the lighter of the backdrop and source colors.
		/// </summary>
		/// <remarks>
		/// The backdrop is replaced with the source where the source is lighter; otherwise, it is left
		/// unchanged.
		/// </remarks>
		Lighten = 6,

		/// <summary>
		/// Brightens the backdrop color to reflect the source color. 
		/// </summary>
		/// <remarks>
		/// Painting with black produces no changes.
		/// </remarks>
		ColorDodge = 7,

		/// <summary>
		/// Darkens the backdrop color to reflect the source color. 
		/// </summary>
		/// <remarks>
		/// Painting with white produces no change.
		/// </remarks>
		ColorBurn = 8,

		/// <summary>
		/// Adds the tonal values of backdrop and source colours.
		/// </summary>
		LinearDodge = 9,

		/// <summary>
		/// Looks at the color information in each channel and darkens the base color to 
		/// reflect the blend color by decreasing the brightness.
		/// </summary>
		/// <remarks>
		/// Blending with white produces no change.
		/// </remarks>
		LinearBurn = 10,

		/// <summary>
		/// Compares the total of all channel values for the blend and base color and displays the higher value color.
		/// </summary>
		/// <remarks>
		/// Lighter Color does not produce a third color, which can result from the Lighten blend, because it chooses 
		/// the highest channel values from both the base and blend color to create the result color. 
		/// </remarks>
		LighterColor = 11,

		/// <summary>
		/// Compares the total of all channel values for the blend and base color and displays the lower value color.
		/// </summary>
		/// <remarks>
		/// Darker Color does not produce a third color, which can result from the Darken blend, because it chooses 
		/// the lowest channel values from both the base and the blend color to create the result color.
		/// </remarks>
		DarkerColor = 12,

		/// <summary>
		/// Multiplies or screens the colors, depending on the source color value.
		/// </summary>
		/// <remarks>
		/// The effect is similar to shining a harsh spotlight on the backdrop.
		/// </remarks>
		HardLight = 13,

		/// <summary>
		/// Darkens or lightens the colors, depending on the source color value.
		/// </summary>
		/// <remarks>
		///	The effect is similar to shining a diffused spotlight on the backdrop.
		/// </remarks>
		SoftLight = 14,

		/// <summary>
		/// Increases contrast very strongly, especially in highlights and shadows.
		/// </summary>
		/// <remarks>
		/// You can imagine its effect as a combination of Color Burn (in the shadows) 
		/// and Color Dodge (in the highlights).
		/// </remarks>
		//VividLight = 15, // Commented out because the shader results in too many instructions for a ps_2_0 shader.

		LinearLight = 16,

		PinLight = 17,

		// HardMix = 18, // Commented out because the shader results in too many instructions for a ps_2_0 shader.

		/// <summary>
		/// Subtracts the darker of the two constituent colors from the lighter color.
		/// </summary>
		/// <remarks>
		/// Painting with white inverts the backdrop color; painting with black produces no change.
		/// </remarks>
		Difference = 19,

		/// <summary>
		/// Produces an effect similar to that of the Difference mode but lower in contrast.
		/// </summary>
		/// <remarks>
		/// Painting with white inverts the backdrop color; painting with black produces no change.
		/// </remarks>
		Exclusion = 20,

		/// <summary>
		/// Creates a color with the hue of the source color and the saturation and luminosity of the
		/// backdrop color.
		/// </summary>
		// Hue = 21, // Commented out because the shader results in too many instructions for a ps_2_0 shader.

		/// <summary>
		/// Creates a color with the saturation of the source color and the hue and luminosity of the
		/// backdrop color.
		/// </summary>
		/// <remarks>
		/// Painting with this mode in an area of the backdrop that is a pure gray (no
		/// saturation) produces no change.
		/// </remarks>
		// Saturation = 22, // Commented out because the shader results in too many instructions for a ps_2_0 shader.

		/// <summary>
		/// Creates a color with the hue and saturation of the source color and the luminosity of the
		/// backdrop color.
		/// </summary>
		/// <remarks>
		/// This preserves the gray levels of the backdrop and is useful for coloring
		/// monochrome images or tinting color images.
		/// </remarks>
		Color = 23,

		/// <summary>
		/// Creates a color with the luminosity of the source color and the hue and saturation of the
		/// backdrop color.
		/// </summary>
		/// <remarks>
		/// This produces an inverse effect to that of the Color mode.
		/// </remarks>
		Luminosity = 24
	}
}
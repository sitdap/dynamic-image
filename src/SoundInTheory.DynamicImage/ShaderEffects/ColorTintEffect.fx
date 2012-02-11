#include "RgbHsvConversion.fxh"

sampler2D input : register(s0);
float amount : register(c0);
float4 requiredColor : register(c1);

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 color = tex2D(input, uv);

	// TODO: Move this next line to .NET since it's the same for every pixel.
	float3 requiredColorHsl = rgb_to_hsv_no_clip(requiredColor.rgb);
	float3 colorHsl = rgb_to_hsv_no_clip(color.rgb);
	colorHsl.x = requiredColorHsl.x;
	colorHsl.y = amount;

	return float4(hsv_to_rgb(colorHsl), color.a);
}
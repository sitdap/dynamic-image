#include "..\RgbHsvConversion.fxh"

sampler2D source : register(s0);
sampler2D background : register(s1);
sampler2D random : register(s2);

#define Blend(b, s, func) float4(float3(func(b.r, s.r), func(b.g, s.g), func(b.b, s.b)) + (b.rgb * (1 - s.a)), func(b.a, s.a))

// Separable blend modes

float Normal(float b, float s)
{
	return s;
}

float Multiply(float b, float s)
{
	return b * s;
}

float Screen(float b, float s)
{
	return b + s - (b * s);
}

float Darken(float b, float s)
{
	return min(b, s);
}

float Lighten(float b, float s)
{
	return max(b, s);
}

float ColorDodge(float b, float s)
{
	return (s < 1.0f) ? min(1.0f, b / (1.0f - s)) : 1.0f;
}

float ColorBurn(float b, float s)
{
	return (s > 0.0f) ? 1.0f - max(0.0f, (1.0f - b) / s) : 0.0f;
}

float LinearDodge(float b, float s)
{
	return b + s;
}

float LinearBurn(float b, float s)
{
	return b + s - 1.0f;
}

float HardLight(float b, float s)
{
	float result;
	if (s <= 0.5f)
		result = Multiply(b, 2.0f * s);
	else
		result = Screen(b, (2.0f * s) - 1.0f);
	return result;
}

float Overlay(float b, float s)
{
	return HardLight(s, b);
}

float SoftLight(float b, float s)
{
	float result;
	if (s <= 0.5f)
	{
		result = 2.0 * b * s + b * b * (1.0 - 2.0 * s);
		//result = b - ((1.0f - (2.0f * s)) * b * (1.0f - b));
	}
	else
	{
		result = sqrt(b) * (2.0 * s - 1.0) + 2.0 * b * (1.0 - s);
		//float d = (b <= 0.25f) ? (((((16.0f * b) - 12.0f) * b) + 4) * b) : sqrt(b);
		//result = b + ((2.0f * s) - 1.0f) * (d - b);
	}
	return result;
}

float VividLight(float b, float s)
{
	float result;
	if (s <= 0.5f)
		result = ColorBurn(b, 2.0f * s);
	else
		result = ColorDodge(b, (2.0f * s) - 1.0f);
	return result;
}

float LinearLight(float b, float s)
{
	float result;
	if (s <= 0.5f)
		result = LinearBurn(b, 2.0f * s);
	else
		result = LinearDodge(b, (2.0f * s) - 1.0f);
	return result;
}

float PinLight(float b, float s)
{
	float result;
	if (s <= 0.5f)
		result = Darken(b, 2.0f * s);
	else
		result = Lighten(b, (2.0f * s) - 1.0f);
	return result;
}

float HardMix(float b, float s)
{
	return (VividLight(b, s) <= 0.5f) ? 0 : 1.0f;
}

float Difference(float b, float s)
{
	return abs(b - s);
}

float Exclusion(float b, float s)
{
	return b + s - (2.0f * b * s);
}

// Non-separable blend modes

float4 Dissolve(float random, float4 b, float4 s)
{
	float4 result;
	result = (random > s.a) ? s : b;
	return result;
}

float4 DarkerColor(float4 b, float4 s)
{
	float4 result;
	result = (b.r + b.g + b.b > s.r + s.g + s.b) ? s : b;
	return result;
}

float4 LighterColor(float4 b, float4 s)
{
	float4 result;
	result = (b.r + b.g + b.b > s.r + s.g + s.b) ? b : s;
	return result;
}

float4 Hue(float4 b, float4 s)
{
	float3 baseHSL = rgb_to_hsv_no_clip(b.rgb);
	return float4(hsv_to_rgb(float3(rgb_to_hsv_no_clip(s).r, baseHSL.g, baseHSL.b)), 1);
}

float4 Saturation(float4 b, float4 s)
{
	float3 baseHSL = rgb_to_hsv_no_clip(b.rgb);
	return float4(hsv_to_rgb(float3(baseHSL.r, rgb_to_hsv_no_clip(s).g, baseHSL.b)), 1);
}

float4 Color(float4 b, float4 s)
{
	float3 blendHSL = rgb_to_hsv_no_clip(s.rgb);
	return float4(hsv_to_rgb(float3(blendHSL.r, blendHSL.g, rgb_to_hsv_no_clip(b).b)), 1);
}

float4 Luminosity(float4 b, float4 s)
{
	float3 baseHSL = rgb_to_hsv_no_clip(b.rgb);
	return float4(hsv_to_rgb(float3(baseHSL.r, baseHSL.g, rgb_to_hsv_no_clip(s).b)), 1);
}
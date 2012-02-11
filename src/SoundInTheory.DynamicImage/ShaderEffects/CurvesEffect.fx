sampler2D input : register(s0);
sampler2D curvesLookup : register(s1);

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 color = tex2D(input, uv);

	// Ignore composite for now.

	float r = tex2D(curvesLookup, float2(0.25, color.r)).r;
	float g = tex2D(curvesLookup, float2(0.5, color.g)).r;
	float b = tex2D(curvesLookup, float2(0.75, color.b)).r;

	return float4(r, g, b, color.a);
}
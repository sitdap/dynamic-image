sampler2D input : register(s0);
sampler2D transferLookup : register(s1);

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 color = tex2D(input, uv);

	float r = tex2D(transferLookup, float2(0, color.r)).r;
	float g = tex2D(transferLookup, float2(0, color.g)).g;
	float b = tex2D(transferLookup, float2(0, color.b)).b;

	return float4(r, g, b, color.a);
}
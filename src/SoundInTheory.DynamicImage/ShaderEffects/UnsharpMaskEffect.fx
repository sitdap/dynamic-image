sampler2D input : register(s0);
sampler2D blurMask : register(s1);

float amount : register(c0);
float threshold : register(c1);

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 color = tex2D(input, uv); 
	float4 blurred = tex2D(blurMask, uv);

	// Apply difference of gaussian blur filter
	float3 rgb = color.rgb + (color.rgb - blurred.rgb) * amount;
	return float4(rgb, color.a);
}
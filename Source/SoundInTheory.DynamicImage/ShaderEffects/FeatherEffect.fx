sampler2D input : register(s0);
sampler2D blurredInput : register(s1);

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 color = tex2D(input, uv); 
	float4 blurred = tex2D(blurredInput, uv);

	return float4(color.rgb * blurred.a, blurred.a);
}
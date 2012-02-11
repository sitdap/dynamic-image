sampler2D input : register(s0);

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 color = tex2D(input, uv); 
	color.rgb = dot(float3(0.299f, 0.587f, 0.114f), color.rgb);
	return color;
}
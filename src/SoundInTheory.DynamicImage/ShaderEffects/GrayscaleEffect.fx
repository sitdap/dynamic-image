sampler2D input : register(s0);

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 color = tex2D(input, uv); 
	float gray = dot(float3(0.299f, 0.587f, 0.114f), color);
	return gray;
}
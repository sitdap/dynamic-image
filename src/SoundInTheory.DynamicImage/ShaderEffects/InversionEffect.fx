sampler2D input : register(s0);

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 color = tex2D(input, uv); 
	float4 invertedColor = float4(color.a - color.rgb, color.a);
	return invertedColor;
}
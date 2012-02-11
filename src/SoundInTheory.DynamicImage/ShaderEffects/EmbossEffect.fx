sampler2D input : register(s0);
float amount : register(c0);
float width : register(c1);

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float alpha = tex2D(input, uv).a;
	float3 color = float3(0.5, 0.5, 0.5);

	color -= tex2D(input, uv - width).rgb * amount;
	color += tex2D(input, uv + width).rgb * amount;
	color.rgb = (color.r + color.g + color.b) / 3.0f;

	return float4(color, alpha);
}
sampler2D input : register(s0);
float colorTolerance : register(c0);
float4 transparentColor : register(c1);

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 color = tex2D(input, uv);

	float4 result;
	if (abs(color.r - transparentColor.r) <= colorTolerance
		&& abs(color.g - transparentColor.g) <= colorTolerance
		&& abs(color.b - transparentColor.b) <= colorTolerance)
		result = float4(0, 0, 0, 0);
	else
		result = color;
	return result;
}
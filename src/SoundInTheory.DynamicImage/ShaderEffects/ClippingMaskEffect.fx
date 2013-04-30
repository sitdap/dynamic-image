sampler2D input : register(s0);
sampler2D mask : register(s1);
float2 inputCoordsOffset : register(c0);
float2 inputCoordsScale : register(c1);

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 maskColor = tex2D(mask, uv);

	uv = (uv * inputCoordsScale) + inputCoordsOffset;
	float4 color = tex2D(input, uv); 

	// Use alpha from mask image, and RGB from source image.
	return float4(color.r * maskColor.a, color.g * maskColor.a, color.b * maskColor.a, maskColor.a);
}
sampler2D input : register(s0);

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 color = tex2D(input, uv); 

	// These factors were taken from http://msdn.microsoft.com/en-us/magazine/cc163866.aspx.
	float r = dot(float3(0.393f, 0.769f, 0.189f), color);
	float g = dot(float3(0.349f, 0.686f, 0.168f), color);
	float b = dot(float3(0.272f, 0.534f, 0.131f), color);

	return float4(r, g, b, color.a);
}
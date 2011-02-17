sampler2D input : register(s0);
float level : register(c0);

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 color = tex2D(input, uv);

	// Centre on 0 instead of 0.5.
	color -= 0.5f;

	// Adjust by contrast level. We want to end up with a range of [-0.5, 0.5].
	color *= level;

	// Re-add .5 (un-center over 0).
	color += 0.5f;

	return color;
}		
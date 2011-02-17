#include "LayerBlending.fxh"

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 b = tex2D(background, uv);
	float4 s = tex2D(source, uv);

	//return s;
	return Blend(b, s, Normal);
}
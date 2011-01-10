float4x4 WVP_Matrix;
Texture fx_Texture;
float fx_Alpha;
float fx_Rotation;
float2 fx_Pos;	

sampler texture_sampler = sampler_state {
	Texture = <fx_Texture>;
	minfilter = LINEAR;
	magfilter = LINEAR;
};

struct VS_INPUT {
	float3 pos : POSITION;
	float4 color : COLOR;
	float2 texcoord : TEXCOORD0;
};

struct VS_OUTPUT {
	float4 pos : POSITION;
	float4 color : COLOR;
	float2 texcoord : TEXCOORD0;
};

void vertex_shader( in VS_INPUT IN, out VS_OUTPUT OUT )
{	
	OUT.pos = mul(float4(IN.pos.xyz,1.0f),WVP_Matrix);
	OUT.color = IN.color;
	OUT.texcoord = IN.texcoord;
}

float4 pixel_shader( in VS_OUTPUT IN ) : COLOR
{
	float4 texture_color = tex2D( texture_sampler, IN.texcoord );
	return texture_color * fx_Alpha;
}

technique texture_technique {
	pass p0 {
		sampler[0] = (texture_sampler);
	
		AlphaBlendEnable = true;
		SrcBlend = SrcAlpha;
		DestBlend = InvSrcAlpha;
		vertexshader = compile vs_2_0 vertex_shader();
		pixelshader = compile ps_2_0 pixel_shader();
	}	
}
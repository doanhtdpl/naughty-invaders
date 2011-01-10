float4x4 WVP_Matrix;
Texture fx_Texture;

sampler texture_sampler = sampler_state {
	Texture = <fx_Texture>;
	minfilter = POINT;
	magfilter = POINT;
};

struct VS_INPUT {
	float3 pos : POSITION;
	float2 texcoord : TEXCOORD0;
};

struct VS_OUTPUT {
	float4 pos : POSITION;
	float4 color : COLOR;
	float2 texcoord : TEXCOORD0;
};

void vertex_shader( in VS_INPUT IN, out VS_OUTPUT OUT ){
	OUT.pos = mul(float4(IN.pos.xyz,1.0f), WVP_Matrix);
	OUT.color = float4(1,1,1,1);
	OUT.texcoord = IN.texcoord;
}

float4 pixel_shader( in VS_OUTPUT IN ) : COLOR
{
	return tex2D( texture_sampler, IN.texcoord );
}

technique texture_technique {
	pass p0 {
		sampler[0] = (texture_sampler);
	
		vertexshader = compile vs_2_0 vertex_shader();
		pixelshader = compile ps_2_0 pixel_shader();
	}	
}
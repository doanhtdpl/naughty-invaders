// This texture contains an overlay sketch pattern, used to create the hatched
// pencil drawing effect.
float4x4 World;
float4x4 View;
float4x4 Projection;
// determina el tamaño de la textura a la hora de pintarla
int TEX_SIZE = 60;

sampler BallTexture : register(s0);
sampler PatternTexture : register(s1);

struct VS_INPUT {
	float3 pos : POSITION;
	float2 texcoord : TEXCOORD0;
};

struct VS_OUTPUT {
	float4 pos : POSITION;
	float2 texCoord : TEXCOORD0;
	float2 pattern_texcoord : TEXCOORD1;
};

void VS( in VS_OUTPUT IN, out VS_OUTPUT OUT )
{
	OUT.pos = mul(mul(mul(IN.pos, World), View), Projection);
	OUT.texCoord = IN.texCoord;
	OUT.pattern_texcoord = mul(IN.pos, World).xy;
}

float4 PS(in VS_OUTPUT IN) : COLOR0
{
	float alpha = 0;
    float4 spy = tex2D(BallTexture, IN.texCoord);
    float3 colorPattern = float3(0,0,0);
    if (spy.a > 0)
    {
		// sumamos un valor para evitar hacer módulo de números negativos o menores que el número a operar
		float2 coord = ((IN.pattern_texcoord + 32768) % TEX_SIZE) / TEX_SIZE;
		coord.y = 1-coord.y;
		
	    
		// las coordenadas de textura van de 0 a 1, y la textura en el mundo no,
		// así que convertimos la coordenada en un número válido
		colorPattern = tex2D(PatternTexture, coord);
		alpha = 1;
    }
    return float4(colorPattern*0.5 + spy*0.5, alpha);
}

technique TexturePolygon
{
    pass P0
    {
		VertexShader = compile vs_1_1 VS();
        PixelShader = compile ps_2_0 PS();
    }
}

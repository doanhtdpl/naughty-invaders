// This texture contains an overlay sketch pattern, used to create the hatched
// pencil drawing effect.
uniform extern texture PatternTexture;
float4x4 World;
float4x4 View;
float4x4 Projection;
float GameTime;
// determina el tama�o de la textura a la hora de pintarla
int TEX_SIZE = 60;

sampler pattern : register(s0) = sampler_state
{
    Texture = (PatternTexture);
    AddressU = Wrap;
	AddressV = Wrap;
};

struct VS_INPUT {
	float3 pos : POSITION;
	float2 texcoord : TEXCOORD0;
};

struct VS_OUTPUT {
	float4 pos : POSITION;
	float2 texCoord : TEXCOORD0;
};

void VS( in VS_OUTPUT IN, out VS_OUTPUT OUT )
{
	OUT.pos = mul(mul(mul(IN.pos, World), View), Projection);
	OUT.texCoord = mul(IN.pos, World).xy;
}

float4 PS(in VS_OUTPUT IN) : COLOR0
{   
    // sumamos un valor para evitar hacer m�dulo de n�meros negativos o menores que el n�mero a operar
    float2 coord = ((IN.texCoord + 32768) % TEX_SIZE) / TEX_SIZE;
    coord.y = 1-coord.y;
    
    // las coordenadas de textura van de 0 a 1, y la textura en el mundo no,
    // as� que convertimos la coordenada en un n�mero v�lido
    float3 colorPattern = tex2D(pattern, coord);
    return float4(colorPattern, 1);
}

float4 PSdistorsion(float2 worldPosition : TEXCOORD0) : COLOR0
{
    // sumamos un valor para evitar hacer m�dulo de n�meros negativos o menores que el n�mero a operar
    float2 coord = ((worldPosition + 32768) % TEX_SIZE) / TEX_SIZE;
    coord.y = 1-coord.y;
    
    // las coordenadas de textura van de 0 a 1, y la textura en el mundo no,
    // as� que convertimos la coordenada en un n�mero v�lido
    coord.x += sin(worldPosition.x*0.5 + GameTime*6.28) * 0.01;
    coord.y += sin(worldPosition.y*0.5 + GameTime*6.28) * 0.01;
    float3 colorPattern = tex2D(pattern, coord);
    return float4(colorPattern, 1);
}

technique TexturePolygon
{
    pass P0
    {
		VertexShader = compile vs_1_1 VS();
        PixelShader = compile ps_2_0 PS();
    }
}

technique TexturePolygonDistorsion
{
    pass P0
    {
		VertexShader = compile vs_1_1 VS();
        PixelShader = compile ps_2_0 PSdistorsion();
    }
}
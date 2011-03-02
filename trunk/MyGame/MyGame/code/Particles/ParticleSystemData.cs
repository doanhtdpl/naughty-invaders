using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyGame
{
    struct ParticleSystemData
    {
        public enum tParticleSystem { Fountain, Burst };

	    public string			    name;
	    public tParticleSystem		type;
	    public Texture         	    texture;
	    public int					nParticles;
	    public float				systemLife;
	    public Vector3			    position;
	    public Vector3			    direction;
	    public Vector3			    acceleration;
	    public Vector3			    positionVarianceMax;
	    public Vector3			    directionVarianceMax;
	    public Vector3			    accelerationVarianceMax;
	    public Vector3			    positionVarianceMin;
	    public Vector3			    directionVarianceMin;
	    public Vector3			    accelerationVarianceMin;
	    public Color				color;
        public Color                colorVarianceMin;
        public Color                colorVarianceMax;
	    public float				particlesRotation;
	    public float				particlesRotationSpeed;
	    public float				particlesRotationVariance;
	    public float				particlesRotationSpeedVariance;
	    public float				size;
	    public float				sizeIni;
	    public float				sizeEnd;
	    public float				fadeIn;
	    public float				fadeOut;
	    public float				particlesLife;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyGame
{
    // align the types of BaseParticleData with the shader
    public class BaseParticleData : SortableEntity
    {
	    public Vector3     position;
	    public float       size;
	    public Color       color;
	    public float       rotation;
	    public float       nframe;
	    public float       dummy1;
	    public float       dummy2;
    };

    // base particle + cpu data needed to update the behavior of the particle
    public class Particle : BaseParticleData
    {
	    public Vector3      direction;
	    public Vector3      acceleration;
	    public float		rotationSpeed;
	    public float		life;
	    public bool		    isDead;

        // for render
        public Texture texture;
        public override void render()
        {
            texture.render(SB.getWorldMatrix(position, rotation, size), color);
        }
    };
}

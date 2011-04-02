using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    class ParticleSystem
    {
        ParticleSystemData data;
        Vector3 position;
        Vector3 direction;

	    public bool isDead;
	    List<Particle> particles = new List<Particle>();

	    public void initialize( string name, Vector3 position, Vector3 direction, Color color, float scaleModifier = 1.0f, int nParticlesModifier = 0, float lifetimeModifier = 0.0f)
        {
            particles.Clear();
            data = ParticleManager.Instance.getBaseParticleSystemData(name);
            this.position = position;
            this.direction = direction;

            // modify the base data with the parameters
            if (nParticlesModifier != 0)
            {
                data.nParticles = nParticlesModifier;
            }
            if (lifetimeModifier != 0.0f)
            {
                data.systemLife = lifetimeModifier;
                data.particlesLife = lifetimeModifier;
            }

            data.size *= scaleModifier;
            data.sizeIni *= scaleModifier;
            data.sizeEnd *= scaleModifier;
            data.positionVarianceMin *= scaleModifier;
            data.positionVarianceMax *= scaleModifier;
            data.directionVarianceMin *= scaleModifier;
            data.directionVarianceMax *= scaleModifier;
            data.accelerationVarianceMin *= scaleModifier;
            data.accelerationVarianceMax *= scaleModifier;
            data.color = new Color(data.color.ToVector4() * color.ToVector4());

	        // get an aproximate number of the simultaneous particles that will have the system
	        float spawnRatio = data.particlesLife / (float)data.nParticles;
            particles.Capacity = data.nParticles;

	        switch(data.type)
	        {
                case ParticleSystemData.tParticleSystem.Burst:
	                for(int i=0; i<data.nParticles; i++)
	                {
                        Particle p = new Particle();
		                p.isDead = true;
		                initializeParticle(p);
                        particles.Add(p);
	                }
	                break;
                case ParticleSystemData.tParticleSystem.Fountain:
	                for(int i=0; i<data.nParticles; i++)
	                {
                        Particle p = new Particle();
		                // we want particles prepared to be spawned with the spawnRatio ratio, so we set'em all alive but invisible
		                p.life = 1.3f + spawnRatio * i;
		                p.isDead = false;
		                p.color.A = 0;
                        particles.Add(p);
	                }
	                break;
                default:
	                break;
	        }
        }
	    void initializeParticle(Particle particle)
        {
	        particle.color = data.color;
            particle.size = data.sizeIni;
	        Vector3 v = Calc.randomVector3(data.positionVarianceMin, data.positionVarianceMax);
	        particle.position = position + data.position + v;
	        v = Calc.randomVector3(data.directionVarianceMin, data.directionVarianceMax);
	        particle.direction = direction + data.direction + v;
	        v = Calc.randomVector3(data.accelerationVarianceMin, data.accelerationVarianceMax);
	        particle.acceleration = data.acceleration + v;
	        particle.rotation = data.particlesRotation + Calc.randomScalar(-data.particlesRotationVariance, data.particlesRotationVariance);
            particle.rotationSpeed = data.particlesRotationSpeed + Calc.randomScalar(-data.particlesRotationSpeedVariance, data.particlesRotationSpeedVariance);
            particle.color = data.color;
            particle.color.R += (byte)Calc.randomNatural(data.colorVarianceMin.R, data.colorVarianceMax.R);
            particle.color.G += (byte)Calc.randomNatural(data.colorVarianceMin.G, data.colorVarianceMax.G);
            particle.color.B += (byte)Calc.randomNatural(data.colorVarianceMin.B, data.colorVarianceMax.B);
            particle.color.A += (byte)Calc.randomNatural(data.colorVarianceMin.A, data.colorVarianceMax.A);
            particle.life = data.particlesLife;
        }
	    public void update( )
        {
	        data.systemLife -= SB.dt;

	        // update each particle
	        float aux;
	        isDead = true;
	        for(int i=0; i<particles.Count; ++i)
	        {
		        if (particles[i].life < 0)
		        {
			        if (data.type == ParticleSystemData.tParticleSystem.Fountain && data.systemLife > 0)
			        {
				        initializeParticle(particles[i]);
			        }
			        else
			        {
				        continue;
			        }
		        }
		        isDead = false;
		        particles[i].direction += particles[i].acceleration * SB.dt;
		        particles[i].position += particles[i].direction * SB.dt;
                particles[i].position += direction * SB.dt;
		        particles[i].rotation += particles[i].rotationSpeed * SB.dt;
		        particles[i].life -= SB.dt;

		        // get the alpha related to fade in or fade out
		        aux = data.particlesLife - particles[i].life;
                float alpha = 0.0f;
		        if (aux < data.fadeIn)
		        {
			        alpha = aux/data.fadeIn;
		        }
		        else
		        {
                    aux = data.particlesLife - data.fadeIn;
			        alpha = particles[i].life/aux;
		        }
                // multiply by the real alpha of the color
                alpha *= ((float)data.color.A / 255.0f);
                // transform it to a byte
                particles[i].color.A = (byte)(alpha * 255);

                if (particles[i].life > data.particlesLife - data.fadeIn)
		        {
                    particles[i].size = data.sizeIni + ((aux / data.fadeIn) * (data.size - data.sizeIni));
		        }
                else if (particles[i].life < data.fadeOut)
		        {
                    particles[i].size = data.sizeEnd + ((particles[i].life / data.fadeOut) * (data.size - data.sizeEnd));
		        }
		        else
		        {
                    particles[i].size = data.size;
		        }
	        }
        }

        public void render()
        {
            for (int i = 0; i < particles.Count; ++i)
            {
                data.texture.render(SB.getWorldMatrix(particles[i].position, particles[i].rotation, particles[i].size), particles[i].color);
            }
        }

        public void clear()
        {
            particles.Clear();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Xml;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Linq;

namespace MyGame
{
    class ParticleManager
    {
        static ParticleManager instance = null;
        ParticleManager()
        {
        }
        public static ParticleManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ParticleManager();
                }
                return instance;
            }
        }

        Dictionary<string, ParticleSystemData> baseParticleSystems = new Dictionary<string,ParticleSystemData>();
        List<ParticleSystem> particleSystems = new List<ParticleSystem>();

        //std::map<std::string, ParticleSystemInfo> baseParticleSystems;
	    //std::vector<ParticleSystem> particleSystems;

        public List<string> getBaseParticleSystemNames()
        {
            return baseParticleSystems.Keys.ToList<string>();
        }

        public ParticleSystemData getBaseParticleSystemData(string name)
        {
            return baseParticleSystems[name];
        }

        public void loadXML()
        {
            baseParticleSystems.Clear();

            XDocument xml = XDocument.Load(SB.content.RootDirectory + "/xml/particles/particleSystems.xml");

            IEnumerable<XElement> baseParticleSystemList = xml.Descendants("particleSystem");

            foreach (XElement bps in baseParticleSystemList)
            {
                ParticleSystemData data = new ParticleSystemData();
                data.name = bps.Attribute("name").Value;
                string type = bps.Attribute("type").Value;
                switch(type)
                {
                    case "burst":
                        data.type = ParticleSystemData.tParticleSystem.Burst;
                    break;
                    case "fountain":
                        data.type = ParticleSystemData.tParticleSystem.Fountain;
                    break;
                }
                string render = bps.Attribute("render").Value;
                bool additive = render == "additive";

                string path = bps.Attribute("texturePath").Value;
                data.texture = TextureManager.Instance.getTexture("particles/" + path);
                data.nParticles = bps.Attribute("nParticles").Value.toInt();
                data.systemLife = bps.Attribute("systemLife").Value.toFloat();
                data.position = bps.Attribute("position").Value.toVector3();
                data.positionVarianceMin = bps.Attribute("positionVarianceMin").Value.toVector3();
                data.positionVarianceMax = bps.Attribute("positionVarianceMax").Value.toVector3();
                data.direction = bps.Attribute("direction").Value.toVector3();
                data.directionVarianceMin = bps.Attribute("directionVarianceMin").Value.toVector3();
                data.directionVarianceMax = bps.Attribute("directionVarianceMax").Value.toVector3();
                data.acceleration = bps.Attribute("acceleration").Value.toVector3();
                data.accelerationVarianceMin = bps.Attribute("accelerationVarianceMin").Value.toVector3();
                data.accelerationVarianceMax = bps.Attribute("accelerationVarianceMax").Value.toVector3();
                data.color = bps.Attribute("color").Value.toColor();
                data.colorVarianceMin = bps.Attribute("colorVarianceMin").Value.toColor();
                data.colorVarianceMax = bps.Attribute("colorVarianceMax").Value.toColor();
                data.particlesRotation = bps.Attribute("particlesRotation").Value.toFloat();
                data.particlesRotationVariance = bps.Attribute("particlesRotationVariance").Value.toFloat();
                data.particlesRotationSpeed = bps.Attribute("particlesRotationSpeed").Value.toFloat();
                data.particlesRotationSpeedVariance = bps.Attribute("particlesRotationSpeedVariance").Value.toFloat();
                data.size = bps.Attribute("size").Value.toFloat();
                data.sizeIni = bps.Attribute("sizeIni").Value.toFloat();
                data.sizeEnd = bps.Attribute("sizeEnd").Value.toFloat();
                data.fadeIn = bps.Attribute("fadeIn").Value.toFloat();
                data.fadeOut = bps.Attribute("fadeOut").Value.toFloat();
                data.particlesLife = bps.Attribute("particlesLife").Value.toFloat();

                // we are using premultiplied alpha so in order to render those particles in additive mode we need to set alpha to 0
                if (additive)
                {
                    data.color.A = 0;
                    data.colorVarianceMin.A = 0;
                    data.colorVarianceMax.A = 0;
                }

                //SB::ownAssert(info.fadeIn + info.fadeOut <= info.particlesLife);
                //SB::ownAssert(info.positionVarianceMin.x <= info.positionVarianceMax.x);
                //SB::ownAssert(info.positionVarianceMin.y <= info.positionVarianceMax.y);
                //SB::ownAssert(info.positionVarianceMin.z <= info.positionVarianceMax.z);
                //SB::ownAssert(info.directionVarianceMin.x <= info.directionVarianceMax.x);
                //SB::ownAssert(info.directionVarianceMin.y <= info.directionVarianceMax.y);
                //SB::ownAssert(info.directionVarianceMin.z <= info.directionVarianceMax.z);
                //SB::ownAssert(info.accelerationVarianceMin.x <= info.accelerationVarianceMax.x);
                //SB::ownAssert(info.accelerationVarianceMin.y <= info.accelerationVarianceMax.y);
                //SB::ownAssert(info.accelerationVarianceMin.z <= info.accelerationVarianceMax.z);

                baseParticleSystems.Add(data.name, data);
            }
        }

        public void update()
        {  
            for(int i=0; i<particleSystems.Count;i++)
	        {
		        if (particleSystems[i].isDead)
		        {
                    particleSystems[i].clear();
                    particleSystems.RemoveAt(i);
			        i--;
		        }
		        else
		        {
                    particleSystems[i].update();
		        }
	        }
        }

        public ParticleSystem addParticles(string name, Vector3 position, Vector3 direction, Color color,
            float scaleModifier = 1.0f, int nParticlesModifier = 0, float lifetimeModifier = 0.0f)
        {
            ParticleSystem ps = new ParticleSystem();
            ps.initialize(name, position, direction, color, scaleModifier, nParticlesModifier, lifetimeModifier);
            particleSystems.Add(ps);
            return ps;
        }

        public List<ParticleSystem> getParticles()
        {
            return particleSystems;
        }

        public void render()
        {
            for (int i = 0; i < particleSystems.Count; ++i)
            {
                particleSystems[i].render();
            }
        }

        public void clean()
        {
            particleSystems.Clear();
            baseParticleSystems.Clear();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Xml;
using Microsoft.Xna.Framework.Graphics;

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

        public ParticleSystemData getBaseParticleSystemData(string name)
        {
            return baseParticleSystems[name];
        }

        public void loadXML()
        {
            baseParticleSystems.Clear();

            XmlDocument xml = new XmlDocument();
            xml.Load(SB.content.RootDirectory + "/xml/particles/particleSystems.xml");

            XmlNodeList baseParticleSystemList = xml.GetElementsByTagName("particleSystem");

            foreach (XmlElement bps in baseParticleSystemList)
            {
                ParticleSystemData data = new ParticleSystemData();
                data.name = bps.GetAttribute("name");
                string type = bps.GetAttribute("type");
                switch(type)
                {
                    case "burst":
                        data.type = ParticleSystemData.tParticleSystem.Burst;
                    break;
                    case "fountain":
                        data.type = ParticleSystemData.tParticleSystem.Fountain;
                    break;
                }
                string path = bps.GetAttribute("texturePath");
                data.texture = TextureManager.Instance.getTexture("particles/" + path);
                data.nParticles = bps.GetAttribute("nParticles").toInt();
                data.systemLife = bps.GetAttribute("systemLife").toInt();
                data.position = bps.GetAttribute("position").toVector3();
                data.positionVarianceMin = bps.GetAttribute("positionVarianceMin").toVector3();
                data.positionVarianceMax = bps.GetAttribute("positionVarianceMax").toVector3();
                data.direction = bps.GetAttribute("direction").toVector3();
                data.directionVarianceMin = bps.GetAttribute("directionVarianceMin").toVector3();
                data.directionVarianceMax = bps.GetAttribute("directionVarianceMax").toVector3();
                data.acceleration = bps.GetAttribute("acceleration").toVector3();
                data.accelerationVarianceMin = bps.GetAttribute("accelerationVarianceMin").toVector3();
                data.accelerationVarianceMax = bps.GetAttribute("accelerationVarianceMax").toVector3();
                data.color = bps.GetAttribute("color").toColor();
                data.colorVarianceMin = bps.GetAttribute("colorVarianceMin").toColor();
                data.colorVarianceMax = bps.GetAttribute("colorVarianceMax").toColor();
                data.particlesRotation = bps.GetAttribute("particlesRotation").toFloat();
                data.particlesRotationVariance = bps.GetAttribute("particlesRotationVariance").toFloat();
                data.particlesRotationSpeed = bps.GetAttribute("particlesRotationSpeed").toFloat();
                data.particlesRotationSpeedVariance = bps.GetAttribute("particlesRotationSpeedVariance").toFloat();
                data.size = bps.GetAttribute("size").toFloat();
                data.sizeIni = bps.GetAttribute("sizeIni").toFloat();
                data.sizeEnd = bps.GetAttribute("sizeEnd").toFloat();
                data.fadeIn = bps.GetAttribute("fadeIn").toFloat();
                data.fadeOut = bps.GetAttribute("fadeOut").toFloat();
                data.particlesLife = bps.GetAttribute("particlesLife").toFloat();

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

        public ParticleSystem addParticles(string name, Vector3 position, Vector3 direction)
        {
	        ParticleSystem ps = new ParticleSystem();
	        ps.initialize(name, position, direction);
	        particleSystems.Add(ps);
	        return ps;
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

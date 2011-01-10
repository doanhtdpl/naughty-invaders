using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;

namespace MyGame
{
    class AnimatedEntity2D : MovingEntity2D
    {
        protected string entityName;

        // animations
        protected string newActionState = "";
        string actionState = "idle";
        float actionTimer = 0.0f;
        int currentTextureId = 0;
        int currentFrame = 0;

        Dictionary<string, AnimationAction> actions = new Dictionary<string, AnimationAction>();
        List<AnimatedTexture> animatedTextures = new List<AnimatedTexture>();

        public virtual void initialize(string name)
        {
            this.entityName = name;
        }

        public virtual void loadContent()
        {
            readXML();
        }

        public void readXML()
        {
            XmlTextReader textReader = new XmlTextReader(SB.content.RootDirectory + "/xml/characters/" + entityName);
            XmlDocument xml = new XmlDocument();
            xml.Load(SB.content.RootDirectory + "/xml/characters/" + entityName + ".xml");

            // read all the animatedTextures and actions of the character
            XmlNodeList animatedTextureList = xml.GetElementsByTagName("animatedTexture");
            int textureNumber = 0;

            foreach (XmlElement animatedTextureNode in animatedTextureList)
            {
                AnimatedTexture animatedTexture = new AnimatedTexture();
                animatedTexture.id = textureNumber;
                string textureName = animatedTextureNode.GetAttribute("name");
                animatedTexture.texture = TextureManager.Instance.getTexture(textureName);
                animatedTexture.width = int.Parse(animatedTextureNode.GetAttribute("width"));
                animatedTexture.height = int.Parse(animatedTextureNode.GetAttribute("height"));
                animatedTexture.columns = int.Parse(animatedTextureNode.GetAttribute("columns"));
                animatedTexture.rows = int.Parse(animatedTextureNode.GetAttribute("rows"));

                XmlNodeList actionList = animatedTextureNode.GetElementsByTagName("action");
                foreach (XmlElement actionNode in actionList)
                {
                    AnimationAction action = new AnimationAction();
                    action.textureId = textureNumber;
                    action.name = actionNode.GetAttribute("name");
                    action.initialFrame = int.Parse(actionNode.GetAttribute("initialFrame"));
                    action.endFrame = int.Parse(actionNode.GetAttribute("endFrame"));
                    action.speed = float.Parse(actionNode.GetAttribute("speed"));
                    action.loops = bool.Parse(actionNode.GetAttribute("loops"));
                    // add each action to the list
                    actions[action.name] = action;
                }
                // add each animated texture with all its actions to the list
                animatedTextures.Add(animatedTexture);

                ++textureNumber;
            }
        }

        const float FRAME_TIME = 0.2f;
        public virtual void update()
        {
            // if there is a new action
            if (newActionState != "")
            {
                actionState = newActionState;
                newActionState = "";
                actionTimer = 0.0f;
            }

            actionTimer += SB.dt;

            AnimationAction action = actions[actionState];
            currentTextureId = action.textureId;
            AnimatedTexture animatedTexture = animatedTextures[currentTextureId];
            float realFrameTime = FRAME_TIME / action.speed;

            int totalFrames = action.endFrame - action.initialFrame;
            currentFrame = (int)(actionTimer / realFrameTime);
            if (currentFrame > totalFrames)
            {
                if (action.loops)
                {
                    actionTimer -= totalFrames * realFrameTime;
                }
                else
                {
                    actionState = "idle";
                    action = actions[actionState];
                    currentTextureId = action.textureId;
                }
                currentFrame = 0;
            }

            currentFrame += action.initialFrame;
        }

        public virtual void render()
        {
            int columns = animatedTextures[currentTextureId].columns;
            int rows = animatedTextures[currentTextureId].rows;

            int x = currentFrame % columns;
            int y = (int)(currentFrame / columns);
            float frameWidth = 1.0f / columns;
            float frameHeight = 1.0f / rows;

            Vector2 initialUVs = new Vector2( frameWidth * x, frameHeight * y);
            Vector2 endingUVs = new Vector2( (frameWidth) * (x + 1), (frameHeight) * (y + 1));

            animatedTextures[currentTextureId].texture.renderWithUVs(position, orientation, size, initialUVs, endingUVs);
        }
    }
}

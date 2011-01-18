using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using System.Globalization;

namespace MyGame
{
    class RenderableEntity2D : MovingEntity2D
    {
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
                    action.frameTime = float.Parse(actionNode.GetAttribute("frameTime"), CultureInfo.InvariantCulture.NumberFormat);
                    action.loops = bool.Parse(actionNode.GetAttribute("loops"));
                    // add each action to the list
                    actions[action.name] = action;
                    action.initialize();
                }
                // add each animated texture with all its actions to the list
                animatedTextures.Add(animatedTexture);

                ++textureNumber;
            }
        }

        const float FRAME_TIME = 0.2f;
        public virtual void update()
        {
            // if there is a new action, change it and reset variables
            if (newActionState != "")
            {
                actionState = newActionState;
                newActionState = "";
                actionTimer = 0.0f;
            }

            actionTimer += SB.dt;

            AnimationAction action = actions[actionState];
 
            // get which would have to be the current frame
            currentFrame = (int)(actionTimer / action.frameTime);
            // at the end of the animation...
            if (currentFrame >= action.totalFrames)
            {
                // we want to take the time passed since the animation ended (like an error) and keep it, then update the current frame (possibly will be 0 again)
                actionTimer -= action.totalFrames * action.frameTime;
                currentFrame = (int)(actionTimer / action.frameTime);
                // if the action doesnt loop, go to idle
                if (!action.loops)
                {
                    actionState = "idle";
                    action = actions[actionState];
                    currentTextureId = action.textureId;
                }
            }

            // calculated the current frame relative to 0, we need to transpose to the real animations initial frame
            currentFrame += action.initialFrame;
        }

        public virtual void render()
        {
            currentTextureId = actions[actionState].textureId;

            int columns = animatedTextures[currentTextureId].columns;
            int rows = animatedTextures[currentTextureId].rows;

            int x = currentFrame % columns;
            int y = (int)(currentFrame / columns);
            float frameWidth = 1.0f / columns;
            float frameHeight = 1.0f / rows;

            Vector2 initialUVs = new Vector2( frameWidth * x, frameHeight * y);
            Vector2 endingUVs = new Vector2( (frameWidth) * (x + 1), (frameHeight) * (y + 1));

            animatedTextures[currentTextureId].texture.renderWithUVs(worldMatrix, initialUVs, endingUVs);
        }
    }
}

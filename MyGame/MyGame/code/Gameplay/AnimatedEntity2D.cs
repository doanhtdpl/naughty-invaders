using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using System.Globalization;

namespace MyGame
{
    class AnimatedEntityData
    {
        public Dictionary<string, AnimationAction> actions = new Dictionary<string, AnimationAction>();
        public List<AnimatedTexture> animatedTextures = new List<AnimatedTexture>();
    }

    public class AnimatedEntity2D : MovingEntity2D
    {
        // animations
        protected string newActionState = "";
        string actionState = "idle";
        float actionTimer = 0.0f;
        int currentTextureId = 0;
        int currentFrame = 0;

        // to avoid loading every time for the same animated entities the same actions and textures, use a common pool
        static Dictionary<string, AnimatedEntityData> datas = new Dictionary<string, AnimatedEntityData>();
        // ...but keep an instance pointer for the actions and animated textures for easing the use
        Dictionary<string, AnimationAction> actions;
        List<AnimatedTexture> animatedTextures;

        public AnimatedEntity2D(string entityName, Vector3 position, Vector2 scale, float orientation)
            : base(entityName, position, scale, orientation)
        {
            // load actions and textures if they havent been readen yet
            if (!datas.ContainsKey(entityName))
            {
                readXML(entityName);
            }
            // assign the pointers for this instance
            actions = datas[entityName].actions;
            animatedTextures = datas[entityName].animatedTextures;
            update();
        }

        public Vector2 getFrameSize()
        {
            return new Vector2(animatedTextures[0].frameWidth, animatedTextures[0].frameHeight);
        }

        // reads xml and loads textures and actions for this animated entity. Can be called from outside this class
        public void readXML(string entityName)
        {
            //XmlTextReader textReader = new XmlTextReader(SB.content.RootDirectory + "/xml/characters/" + entityName);
            XmlDocument xml = new XmlDocument();
            xml.Load(SB.content.RootDirectory + "/xml/characters/" + entityName + ".xml");

            // read all the animatedTextures and actions of the character
            XmlNodeList animatedTextureList = xml.GetElementsByTagName("animatedTexture");
            int textureNumber = 0;

            AnimatedEntityData data = new AnimatedEntityData();

            foreach (XmlElement animatedTextureNode in animatedTextureList)
            {
                AnimatedTexture animatedTexture = new AnimatedTexture();
                animatedTexture.id = textureNumber;
                string textureName = animatedTextureNode.GetAttribute("name");
                animatedTexture.texture = TextureManager.Instance.getTexture(textureName);
                animatedTexture.frameWidth = int.Parse(animatedTextureNode.GetAttribute("frameWidth"));
                animatedTexture.frameHeight = int.Parse(animatedTextureNode.GetAttribute("frameHeight"));
                animatedTexture.columns = int.Parse(animatedTextureNode.GetAttribute("columns"));
                animatedTexture.rows = int.Parse(animatedTextureNode.GetAttribute("rows"));

                float width = animatedTexture.columns * animatedTexture.frameWidth;
                float height = animatedTexture.rows * animatedTexture.frameHeight;
                animatedTexture.frameWidthUV = (width / animatedTexture.columns) / width;
                animatedTexture.frameHeightUV = (height / animatedTexture.rows) / height;

                XmlNodeList actionList = animatedTextureNode.GetElementsByTagName("action");
                foreach (XmlElement actionNode in actionList)
                {
                    AnimationAction action = new AnimationAction();
                    action.textureId = textureNumber;
                    action.name = actionNode.GetAttribute("name");
                    action.initialFrame = int.Parse(actionNode.GetAttribute("initialFrame")) - 1;
                    action.endFrame = int.Parse(actionNode.GetAttribute("endFrame")) - 1;

                    if (actionNode.HasAttribute("FPS"))
                    {
                        action.FPS = actionNode.GetAttribute("FPS").toFloat(); ;
                    }
                    else
                    {
                        action.FPS = 30;
                    }
                    action.loops = bool.Parse(actionNode.GetAttribute("loops"));
                    // add each action to the list
                    data.actions[action.name] = action;
                    action.initialize();
                }
                // add each animated texture with all its actions to the list
                data.animatedTextures.Add(animatedTexture);

                ++textureNumber;
            }
            datas[entityName] = data;
        }

        const float FRAME_TIME = 0.2f;
        public override void update()
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
            float frameTime = 1.0f / action.FPS;
            currentFrame = (int)(actionTimer / frameTime);
            // at the end of the animation...
            if (currentFrame >= action.totalFrames)
            {
                // we want to take the time passed since the animation ended (like an error) and keep it, then update the current frame (possibly will be 0 again)
                actionTimer -= action.totalFrames * frameTime;
                currentFrame = (int)(actionTimer / frameTime);
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

        public override void render()
        {
            currentTextureId = actions[actionState].textureId;

            int columns = animatedTextures[currentTextureId].columns;
            int x = currentFrame % columns;
            int y = (int)(currentFrame / columns);

            float frameWidthUV = animatedTextures[currentTextureId].frameWidthUV;
            float frameHeightUV = animatedTextures[currentTextureId].frameHeightUV;

            Vector2 initialUVs = new Vector2( frameWidthUV * x, frameHeightUV * y);
            Vector2 endingUVs = new Vector2( (frameWidthUV) * (x + 1), (frameHeightUV) * (y + 1));

            animatedTextures[currentTextureId].texture.renderWithUVs(worldMatrix, initialUVs, endingUVs);
        }
    }
}

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

        public AnimatedEntity2D(Vector3 position, Vector2 scale, float orientation, string entityName)
            : base(position, scale, orientation, entityName)
        {
            // load actions and textures if they havent been readen yet
            if (!datas.ContainsKey(entityName))
            {
                readXML(entityName);
            }
            // assign the pointers for this instance
            actions = datas[entityName].actions;
            animatedTextures = datas[entityName].animatedTextures;
        }

        // reads xml and loads textures and actions for this animated entity. Can be called from outside this class
        public static void readXML(string entityName)
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

        public override void render()
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

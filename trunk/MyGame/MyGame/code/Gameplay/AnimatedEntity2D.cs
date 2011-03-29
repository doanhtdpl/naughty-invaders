using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using System.Globalization;
using System.Xml.Linq;

namespace MyGame
{
    class AnimatedEntityData
    {
        public Dictionary<string, AnimationAction> actions = new Dictionary<string, AnimationAction>();
        public List<AnimatedTexture> animatedTextures = new List<AnimatedTexture>();
    }

    public class AnimatedEntity2D : RenderableEntity2D
    {
        // animations
        string newActionState = "idle";
        string actionState = "idle";
        float actionTimer = 0.0f;
        int currentTextureId = 0;
        int currentFrame = 0;

        // to avoid loading every time for the same animated entities the same actions and textures, use a common pool
        static Dictionary<string, AnimatedEntityData> datas = new Dictionary<string, AnimatedEntityData>();
        // ...but keep an instance pointer for the actions and animated textures for easing the use
        Dictionary<string, AnimationAction> actions;
        List<AnimatedTexture> animatedTextures;

        public AnimatedEntity2D(string entityFolder, string entityName, Vector3 position, float orientation, Color color, int id = -1)
            : base("animated", entityName, position, orientation, color, id)
        {
            // load actions and textures if they havent been readen yet
            if (!datas.ContainsKey(entityName))
            {
                readXML(entityFolder, entityName);
            }

            // assign the pointers for this instance
            actions = datas[entityName].actions;
            animatedTextures = datas[entityName].animatedTextures;

            currentFrame = actions["idle"].initialFrame; ;
            //update();

            scale2D = getFrameSize();
        }

        public Vector2 getFrameSize()
        {
            return new Vector2(animatedTextures[0].frameWidth, animatedTextures[0].frameHeight);
        }

        public void playAction(string newAction)
        {
            if (actionState == "die") return;

            newActionState = newAction;
        }

        // reads xml and loads textures and actions for this animated entity. Can be called from outside this class
        public void readXML(string entityFolder, string entityName)
        {
            //XmlTextReader textReader = new XmlTextReader(SB.content.RootDirectory + "/xml/characters/" + entityName);
            XDocument xml = XDocument.Load(SB.content.RootDirectory + "/xml/" + entityFolder + "/" + entityName + ".xml");

            // read all the animatedTextures and actions of the character
            IEnumerable<XElement> animatedTextureList = xml.Descendants("animatedTexture");
            int textureNumber = 0;

            AnimatedEntityData data = new AnimatedEntityData();

            foreach (XElement animatedTextureNode in animatedTextureList)
            {
                AnimatedTexture animatedTexture = new AnimatedTexture();
                animatedTexture.id = textureNumber;
                string textureName = animatedTextureNode.Attribute("name").Value;
                animatedTexture.texture = TextureManager.Instance.getTexture(entityFolder, textureName);
                animatedTexture.frameWidth = animatedTextureNode.Attribute("frameWidth").Value.toFloat();
                animatedTexture.frameHeight = animatedTextureNode.Attribute("frameHeight").Value.toFloat();
                animatedTexture.columns = animatedTextureNode.Attribute("columns").Value.toInt();
                animatedTexture.rows = animatedTextureNode.Attribute("rows").Value.toInt();

                float width = animatedTexture.texture.Width;
                float height = animatedTexture.texture.Height;
                animatedTexture.frameWidthUV = animatedTexture.frameWidth / width;
                animatedTexture.frameHeightUV = animatedTexture.frameHeight / height;

                IEnumerable<XElement> actionList = animatedTextureNode.Descendants("action");
                foreach (XElement actionNode in actionList)
                {
                    AnimationAction action = new AnimationAction();
                    action.textureId = textureNumber;
                    action.name = actionNode.Attribute("name").Value;
                    action.initialFrame = actionNode.Attribute("initialFrame").Value.toInt() - 1;
                    action.endFrame = actionNode.Attribute("endFrame").Value.toInt() - 1;

                    if (actionNode.Attributes("FPS").Count() > 0)
                    {
                        action.FPS = actionNode.Attribute("FPS").Value.toFloat(); ;
                    }
                    else
                    {
                        action.FPS = 30;
                    }
                    action.loops = actionNode.Attribute("loops").Value.toBool();
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

        public virtual void die()
        {
            playAction("die");
            entityState = tEntityState.Dying;
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
                    // if the animation played was die, delete the entity
                    if (actionState == "die")
                    {
                        requestDelete();
                    }
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
            if (renderState == tRenderState.NoRender) return;

            currentTextureId = actions[actionState].textureId;

            int columns = animatedTextures[currentTextureId].columns;
            int x = currentFrame % columns;
            int y = (int)(currentFrame / columns);

            float frameWidthUV = animatedTextures[currentTextureId].frameWidthUV;
            float frameHeightUV = animatedTextures[currentTextureId].frameHeightUV;

            Vector2 initialUVs = new Vector2( frameWidthUV * x, frameHeightUV * y);
            Vector2 endingUVs = new Vector2( (frameWidthUV) * (x + 1), (frameHeightUV) * (y + 1));

            animatedTextures[currentTextureId].texture.renderWithUVs(worldMatrix, initialUVs, endingUVs, color);
        }

        public override void delete()
        {
            base.delete();
        }
        public virtual void requestDelete()
        {
            entityState = tEntityState.ToDelete;
        }
    }
}

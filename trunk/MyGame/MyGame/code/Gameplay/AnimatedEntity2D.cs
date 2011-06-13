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
        public bool idleOffset = false;
        public Dictionary<string, AnimationAction> actions = new Dictionary<string, AnimationAction>();
        public List<AnimatedTexture> animatedTextures = new List<AnimatedTexture>();
    }

    public class AnimatedEntity2D : RenderableEntity2D
    {
        // animations
        string actionState = "idle";
        float actionTimer = 0.0f;
        int currentTextureId = 0;
        int currentFrame = 0;
        public bool avoidDelete { get; set; }

        public Vector2 paintMask = Vector2.One;

        // random action
        float nextRandomActionTime;

        // to avoid loading every time for the same animated entities the same actions and textures, use a common pool
        static Dictionary<string, AnimatedEntityData> datas = new Dictionary<string, AnimatedEntityData>();
        // ...but keep an instance pointer for the actions and animated textures for easing the use
        public Dictionary<string, AnimationAction> actions { get; set; }
        List<AnimatedTexture> animatedTextures;

        float animationSpeed = 1.0f;

        public static void unload()
        {
            datas.Clear();
        }

        public AnimatedEntity2D(string entityFolder, string entityName, Vector3 position, float orientation, Color color, bool register = true, int id = -1)
            : base("animated", entityName, position, orientation, color, register, id)
        {
            // load actions and textures if they havent been readen yet
            if (!datas.ContainsKey(entityName))
            {
                readXML(entityFolder, entityName);
            }

            // assign the pointers for this instance
            actions = datas[entityName].actions;
            animatedTextures = datas[entityName].animatedTextures;

            currentFrame = actions["idle"].initialFrame;
            if (datas[entityName].idleOffset)
            {
                currentFrame = Calc.randomNatural(actions["idle"].initialFrame, actions["idle"].endFrame);
                float frameTime = 1.0f / actions["idle"].FPS;
                actionTimer = frameTime * (currentFrame - actions["idle"].initialFrame);
            }

            scale2D = getFrameSize();
            avoidDelete = false;

            playAction("idle");
        }

        public Vector2 getFrameSize()
        {
            return new Vector2(animatedTextures[0].frameWidth, animatedTextures[0].frameHeight);
        }

        public string getCurrentAction()
        {
            return actionState;
        }

        public void playAction(string newAction, bool forcePlay = false, float animationSpeed = 1.0f)
        {
            if ((actionState == "die" || actionState == newAction) && !forcePlay) return;

            this.animationSpeed = animationSpeed;

            if (actions[newAction].playRandom)
            {
                nextRandomActionTime = Calc.randomScalar(actions[newAction].playRandomMin, actions[newAction].playRandomMax);
            }

            // see if a transition exists
            foreach (AnimationAction action in actions.Values)
            {
                if (action.from == actionState && action.to == newAction)
                {
                    action.playAtEnd = newAction;
                    newAction = action.name;
                    break;
                }
            }

            actionState = newAction;
            actionTimer = 0.0f;
        }

        // reads xml and loads textures and actions for this animated entity. Can be called from outside this class
        public void readXML(string entityFolder, string entityName)
        {
            //XmlTextReader textReader = new XmlTextReader(SB.content.RootDirectory + "/xml/characters/" + entityName);
            XDocument xml = XDocument.Load(SB.content.RootDirectory + "/xml/" + entityFolder + "/" + entityName + ".xml");

            AnimatedEntityData data = new AnimatedEntityData();

            // read
            IEnumerable<XElement> animatedEntityList = xml.Descendants("animatedEntity");
            foreach (XElement animatedEntityNode in animatedEntityList)
            {
                if (animatedEntityNode.Attribute("idleOffset") != null)
                {
                    data.idleOffset = animatedEntityNode.Attribute("idleOffset").Value.toBool();
                }
            }

            // read all the animatedTextures and actions of the character
            IEnumerable<XElement> animatedTextureList = xml.Descendants("animatedTexture");
            int textureNumber = 0;

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
                    if (actionNode.Attribute("from") != null)
                    {
                        action.from = actionNode.Attribute("from").Value;
                    }
                    else
                    {
                        action.from = null;
                    }
                    if (actionNode.Attribute("to") != null)
                    {
                        action.to = actionNode.Attribute("to").Value;
                    }
                    else
                    {
                        action.to = null;
                    }
                    action.initialFrame = actionNode.Attribute("initialFrame").Value.toInt() - 1;
                    action.endFrame = actionNode.Attribute("endFrame").Value.toInt() - 1;

                    if (actionNode.Attribute("FPS") != null)
                    {
                        action.FPS = actionNode.Attribute("FPS").Value.toFloat();
                    }
                    else
                    {
                        action.FPS = 30;
                    }
                    if (actionNode.Attribute("loops") != null)
                    {
                        action.loops = actionNode.Attribute("loops").Value.toBool();
                    }
                    else
                    {
                        action.loops = false;
                    }
                    if (actionNode.Attribute("playAtEnd") != null)
                    {
                        action.playAtEnd = actionNode.Attribute("playAtEnd").Value;
                    }
                    else
                    {
                        action.playAtEnd = null;
                    }

                    // if there is playRandom attribute, read all the random actions
                    if (actionNode.Attribute("playRandom") != null)
                    {
                        action.playRandom = actionNode.Attribute("playRandom").Value.toBool();
                        if (action.playRandom)
                        {
                            action.playRandomMin = actionNode.Attribute("playRandomMin").Value.toFloat();
                            action.playRandomMax = actionNode.Attribute("playRandomMax").Value.toFloat();
                            action.randomActions = new List<RandomAction>();
                            IEnumerable<XElement> randomActionsList = actionNode.Descendants("playRandom");
                            foreach (XElement randomActionNode in randomActionsList)
                            {
                                RandomAction randomAction = new RandomAction();
                                randomAction.name = randomActionNode.Attribute("name").Value;
                                if (randomActionNode.Attribute("probability") != null)
                                {
                                    randomAction.probability = randomActionNode.Attribute("probability").Value.toFloat();
                                }
                                else
                                {
                                    randomAction.probability = 1.0f;
                                }
                                action.randomActions.Add(randomAction);
                            }
                        }
                    }

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
            base.update();

            actionTimer += SB.dt * animationSpeed;

            AnimationAction action = actions[actionState];
            currentTextureId = action.textureId;

            // play a the random action if necessary
            if (action.playRandom)
            {
                nextRandomActionTime -= SB.dt;
                if (nextRandomActionTime < 0.0f)
                {
                    float random = Calc.randomScalar();
                    float accumulatedProbability = 0.0f;
                    for (int i = 0; i < action.randomActions.Count; ++i)
                    {
                        accumulatedProbability += action.randomActions[i].probability;
                        if (random < accumulatedProbability)
                        {
                            playAction(action.randomActions[i].name);
                            action = actions[actionState];
                            break;
                        }
                    }
                }
            }
 
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
                        if (avoidDelete)
                        {
                            renderState = tRenderState.NoRender;
                        }
                        else
                        {
                            requestDelete();
                        }
                    }
                    else if (action.playAtEnd != null)
                    {
                        playAction(action.playAtEnd);
                    }
                    else
                    {
                        playAction("idle");
                    }
                }
            }

            // calculated the current frame relative to 0, we need to transpose to the real animations initial frame
            // we dont use the local variable action because maybe the action state has changed, and the action initial frame is another
            currentFrame += actions[actionState].initialFrame;
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
            Vector2 endingUVs = new Vector2((frameWidthUV) * (x + paintMask.X), (frameHeightUV) * (y + paintMask.Y));

            if (flipHorizontal)
            {
                float temp = endingUVs.X;
                endingUVs.X = initialUVs.X;
                initialUVs.X = temp;
            }

            if (flipVertical)
            {
                float temp = endingUVs.Y;
                endingUVs.Y = initialUVs.Y;
                initialUVs.Y = temp;
            }

            Matrix mat = Matrix.Multiply(Matrix.CreateScale(paintMask.X, paintMask.Y, 1.0f), worldMatrix);
            animatedTextures[currentTextureId].texture.renderWithUVs(mat, initialUVs, endingUVs, color);
        }

        public override void delete()
        {
            base.delete();
        }
        public virtual void requestDelete(bool force = false)
        {
            if (avoidDelete && !force) return;

            entityState = tEntityState.ToDelete;
        }
    }
}

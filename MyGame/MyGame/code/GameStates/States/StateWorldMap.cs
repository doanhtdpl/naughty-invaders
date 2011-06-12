using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    struct WorldMapLocation
    {
        // if level is "", then the node is a follow next node
        public enum tLocationType { Arcade = 0, Skippable, KingTomato, EpilepticMacedonia }
        tLocationType type;
        public string level;

        public WorldMapLocation(string level, tLocationType type)
        {
            this.level = level;
            this.type = type;
        }

        public void enter()
        {
            GamerManager.getSessionOwner().data.lastLevelPlayed = level;
            TransitionManager.Instance.loadLevelWithFade(level, type, 1.0f, Color.Black);
        }
    }

    class StateWorldMap : GameState
    {
        WorldMapPlayer player;
        public NetworkNode<WorldMapLocation> lastLocation { get; set; }
        public NetworkNode<WorldMapLocation> currentLocation { get; set; }
        public Network<WorldMapLocation> locations { get; set; }

        public void initializeNetwork()
        {
            WorldMapLocation ml1 = new WorldMapLocation("fruitownA", WorldMapLocation.tLocationType.Arcade);
            NetworkNode<WorldMapLocation> nn1 = locations.addNode(ml1, new Vector3(-1100.0f, 110.0f, 200.0f));
            WorldMapLocation ml2 = new WorldMapLocation("fruitownB", WorldMapLocation.tLocationType.Arcade);
            NetworkNode<WorldMapLocation> nn2 = locations.addNode(ml2, new Vector3(-900.0f, 110.0f, 200.0f));
            WorldMapLocation ml3 = new WorldMapLocation("onionVillage", WorldMapLocation.tLocationType.KingTomato);
            NetworkNode<WorldMapLocation> nn3 = locations.addNode(ml3, new Vector3(-330.0f, 170.0f, 200.0f));
            WorldMapLocation ml4 = new WorldMapLocation("macedonia", WorldMapLocation.tLocationType.EpilepticMacedonia);
            NetworkNode<WorldMapLocation> nn4 = locations.addNode(ml4, new Vector3(-600.0f, -160.0f, 200.0f));
            WorldMapLocation ml5 = new WorldMapLocation("verducity", WorldMapLocation.tLocationType.Arcade);
            NetworkNode<WorldMapLocation> nn5 = locations.addNode(ml5, new Vector3(175.0f, -100.0f, 200.0f));

            locations.addDoubleLink(nn1, nn2);
            locations.addDoubleLink(nn2, nn3);
            locations.addDoubleLink(nn3, nn4);
            locations.addDoubleLink(nn3, nn5);

            if (!GamerManager.getSessionOwner().data.levelsPassed["fruitownA"])
            {
                EntityManager.Instance.registerEntity(new RenderableEntity2D("staticProps", "lock-34", nn2.position, 0, Color.White));
            }

            if (!GamerManager.getSessionOwner().data.levelsPassed["fruitownB"])
            {
                EntityManager.Instance.registerEntity(new RenderableEntity2D("staticProps", "lock-34", nn3.position, 0, Color.White));
            }

            if (!GamerManager.getSessionOwner().data.levelsPassed["onionVillage"])
            {
                EntityManager.Instance.registerEntity(new RenderableEntity2D("staticProps", "lock-34", nn4.position, 0, Color.White));
                EntityManager.Instance.registerEntity(new RenderableEntity2D("staticProps", "lock-34", nn5.position, 0, Color.White));
            }

            currentLocation = nn1;
            string lastLevel = GamerManager.getSessionOwner().data.lastLevelPlayed;
            if (lastLevel != null)
            {
                for(int i=0; i<locations.getNodes().Count; ++i)
                {
                    if (lastLevel == locations.getNodeAt(i).value.level)
                    {
                        currentLocation = locations.getNodeAt(i);
                    }
                }
            }
        }

        public override void initialize()
        {
            gameState = true;
            longLoad = true;
            DebugManager.Instance.initialize();
            ParticleManager.Instance.loadXML();
            SoundManager.Instance.loadXML();
            EditorHelper.Instance.loadNewLevelFromGame("mapa");
            CameraManager.Instance.cameraMode = CameraManager.tCameraMode.WorldMap;
            CameraManager.Instance.setWorldMapParams(15, 20, 0.75f, 1.2f);

            locations = new Network<WorldMapLocation>();
            initializeNetwork();

            player = new WorldMapPlayer(currentLocation.position);
            player.positionZ += 200.0f;

            TransitionManager.Instance.fadeOut();

            //SoundManager.Instance.playSong("song");
        }

        public override void loadContent()
        {

        }

        public override void render()
        {
            EntityManager.Instance.render();
            LevelManager.Instance.render();
            DebugManager.Instance.render();
            GUIManager.Instance.render();

            GraphicsManager.Instance.spriteBatchBegin();
            "::LS select level".renderNI(Screen.getXYfromCenter(380, 320), 0.8f, StringManager.tStyle.Shadowed);
            "::A enter level".renderNI(Screen.getXYfromCenter(380, 280), 0.8f, StringManager.tStyle.Shadowed);
            "::B back to menu".renderNI(Screen.getXYfromCenter(380, 240), 0.8f, StringManager.tStyle.Shadowed);
            "::BACK skills menu".renderNI(Screen.getXYfromCenter(380, 200), 0.8f, StringManager.tStyle.Shadowed);
            GraphicsManager.Instance.spriteBatchEnd();
        }

        const float WORLDMAP_SPEED = 500.0f;
        public override void update()
        {
            base.update();

            bool canMove = !TransitionManager.Instance.isFading();

            ControlPad cp = GamerManager.getMainControls();

            // if arrives to a new node
            if ((currentLocation.position - player.position).LengthSquared() < 20.0f)
            {
                // if its a transition node (without level), get the next node
                if(currentLocation.value.level == "")
                {
                    NetworkNode<WorldMapLocation> next = currentLocation.getNext(lastLocation);
                    if (next != null)
                    {
                        lastLocation = currentLocation;
                        currentLocation = next;
                    }
                }
                else // if current node has a level...
                {
                    Dictionary<string, bool> levelsPassed = GamerManager.getSessionOwner().data.levelsPassed;
                    NetworkNode<WorldMapLocation> next = currentLocation.getNext(cp.getLS());
                    if (next != null && canMove && 
                        (levelsPassed[currentLocation.value.level]
                        || levelsPassed.ContainsKey(next.value.level) && (levelsPassed[next.value.level])))
                    {
                        lastLocation = currentLocation;
                        currentLocation = next;
                    }
                    if (cp.A_firstPressed() && canMove)
                    {
                        currentLocation.value.enter();
                    }
                }
            }
            else
            {
                Vector3 direction = currentLocation.position - player.position;
                direction.Normalize();
                player.position += direction * WORLDMAP_SPEED * SB.dt;
                player.positionZ = 200.0f;
            }

            if (cp.B_firstPressed() && canMove)
            {
                TransitionManager.Instance.changeStateWithFade(StateManager.tGameState.Menu, 99, null, 0.5f, Color.Black);
            }
            if (cp.Back_firstPressed() && canMove)
            {
                StateManager.addState(StateManager.tGameState.SkillsMenu);
            }


            LevelManager.Instance.update();
            ProjectileManager.Instance.update();
            ParticleManager.Instance.update();
            CameraManager.Instance.worldMapPosition = player.position;
            CameraManager.Instance.update();
            GUIManager.Instance.update();
            player.update();

            SB.cam.update();
        }
        
        public override void dispose()
        {
        }
    }
}
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
            switch (type)
            {
                case tLocationType.Arcade:
                    StateManager.dequeueState(1);
                    StateManager.gameStates.Add(new StateGame(level));
                    break;
                case tLocationType.KingTomato:
                    StateManager.dequeueState(1);
                    StateManager.gameStates.Add(new MinigameKingTomato(level));
                    break;
                case tLocationType.EpilepticMacedonia:
                    StateManager.dequeueState(1);
                    StateManager.gameStates.Add(new MinigameEpilepticMacedonia(level));
                    break;
            }
        }
    }

    class StateWorldMap : GameState
    {
        WorldMapPlayer player;
        public NetworkNode<WorldMapLocation> lastNode { get; set; }
        public NetworkNode<WorldMapLocation> currentNode { get; set; }
        public Network<WorldMapLocation> nodes { get; set; }

        public void initializeNetwork()
        {
            WorldMapLocation ml1 = new WorldMapLocation("final_Level01", WorldMapLocation.tLocationType.Arcade);
            NetworkNode<WorldMapLocation> nn1 = nodes.addNode(ml1, new Vector3(-835.0f, 110.0f, 200.0f));
            WorldMapLocation ml2 = new WorldMapLocation("onionVillage", WorldMapLocation.tLocationType.KingTomato);
            NetworkNode<WorldMapLocation> nn2 = nodes.addNode(ml2, new Vector3(-330.0f, 170.0f, 200.0f));
            WorldMapLocation ml3 = new WorldMapLocation("epilepticMacedonia", WorldMapLocation.tLocationType.EpilepticMacedonia);
            NetworkNode<WorldMapLocation> nn3 = nodes.addNode(ml2, new Vector3(-600.0f, -160.0f, 200.0f));
            WorldMapLocation ml4 = new WorldMapLocation("level2", WorldMapLocation.tLocationType.Arcade);
            NetworkNode<WorldMapLocation> nn4 = nodes.addNode(ml2, new Vector3(0.0f, -100.0f, 200.0f));
            currentNode = nn1;
            nodes.addDoubleLink(nn1, nn2);
            nodes.addDoubleLink(nn2, nn3);
            nodes.addDoubleLink(nn2, nn4);
        }

        public override void initialize()
        {
            type = StateManager.tGS.WorldMap;
            gameState = true;
            longLoad = true;
            DebugManager.Instance.initialize();
            ParticleManager.Instance.loadXML();
            SoundManager.Instance.loadXML();
            EditorHelper.Instance.loadNewLevelFromGame("mapa");
            CameraManager.Instance.cameraMode = CameraManager.tCameraMode.WorldMap;

            nodes = new Network<WorldMapLocation>();
            initializeNetwork();

            player = new WorldMapPlayer(currentNode.position);
            player.positionZ += 200.0f;

            SoundManager.Instance.playSong("song");
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
        }

        const float WORLDMAP_SPEED = 500.0f;
        public override void update()
        {
            base.update();

            // if arrives to a new node
            if ((currentNode.position - player.position).LengthSquared() < 20.0f)
            {
                // if its a transition node (without level), get the next node
                if(currentNode.value.level == "")
                {
                    NetworkNode<WorldMapLocation> next = currentNode.getNext(lastNode);
                    if (next != null)
                    {
                        lastNode = currentNode;
                        currentNode = next;
                    }
                }
                else // if current node has a level...
                {
                    Dictionary<string, bool> levelsPassed = GamerManager.getSessionOwner().data.levelsPassed;
                    ControlPad cp = GamerManager.getMainControls();
                    NetworkNode<WorldMapLocation> next = currentNode.getNext(cp.getLS());
                    if (next != null &&
                        (levelsPassed[currentNode.value.level]
                        || levelsPassed.ContainsKey(next.value.level) && (levelsPassed[next.value.level])))
                    {
                        lastNode = currentNode;
                        currentNode = next;
                    }
                    if (cp.A_firstPressed())
                    {
                        StateManager.clearStates();
                        currentNode.value.enter();
                    }
                }
            }
            else
            {
                Vector3 direction = currentNode.position - player.position;
                direction.Normalize();
                player.position += direction * WORLDMAP_SPEED * SB.dt;
                player.positionZ = 200.0f;
            }


            LevelManager.Instance.update();
            ProjectileManager.Instance.update();
            ParticleManager.Instance.update();
            CameraManager.Instance.worldMapPosition = player.position;
            CameraManager.Instance.update();
            GUIManager.Instance.update();

            SB.cam.update();
        }
        
        public override void dispose()
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    public struct CameraData
    {
        public static int NEXT_ID = 0;

        public Vector3 target;
        public int id;
        public int next;
        public bool isFirst;
        public float speed;

        public CameraData(Vector3 target, int id = -1, bool isFirst = false, float speed = 50.0f)
        {
            this.target = target;
            if (id == -1)
            {
                this.id = NEXT_ID;
                NEXT_ID++;
            }
            else
                this.id = id;

            NEXT_ID = Math.Max(NEXT_ID, id + 1);
            next = -1;
            this.isFirst = isFirst;
            this.speed = speed;
        }
    }

    class CameraManager
    {
        static CameraManager instance = null;
        CameraManager()
        {
            CameraData cd = new CameraData(Vector3.Zero);
        }
        public static CameraManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CameraManager();
                }
                return instance;
            }
        }

        public enum tCameraMode { None, FollowPlayer, WorldMap, Nodes }
        public tCameraMode cameraMode { set; get; }

        // world map mode data
        public Vector3 worldMapPosition { get; set; }

        // node mode stuff
        NetworkNode<CameraData> currentNode;
        NetworkNode<CameraData> nextNode;
        Network<CameraData> cameraNodes = new Network<CameraData>();

        Vector3 lastPosition = Vector3.Zero;
        Vector3 currentPosition = Vector3.Zero;

        public float speedMultiplier = 1.0f;

        // returns the vector in the XY plane with origin in the last frmae camera position and ending in the current frame camera position
        public Vector3 getCameraVelocityXY()
        {
            Vector3 velocity = currentPosition - lastPosition;
            velocity.Z = 0.0f;
            return velocity;
        }
        public Vector3 getCameraPosition()
        {
            return Camera2D.position;
        }
        public Vector2 getCameraPositionXY()
        {
            return Camera2D.position.toVector2();
        }
        public void loadXMLfake()
        {
            if (cameraNodes.getNodes().Count > 0)
                return;

            NetworkNode<CameraData> first = new NetworkNode<CameraData>(new CameraData(new Vector3(0, 0, 0), 0, true), new Vector3(0, 0, 1400));
            NetworkNode<CameraData> second = new NetworkNode<CameraData>(new CameraData(new Vector3(0, 20000, 0), 1), new Vector3(0, 20000, 1400));
            NetworkNode<CameraData> third = new NetworkNode<CameraData>(new CameraData(new Vector3(10000, 20000, 0), 2), new Vector3(10000, 20000, 1400));

            first.addLinkedNode(second);
            second.addLinkedNode(third);

            cameraNodes.addNode(first);
            cameraNodes.addNode(second);
            cameraNodes.addNode(third);

            currentNode = first;
            nextNode = second;

            Camera2D.position = cameraNodes.getNodes()[0].position;
        }

        public Network<CameraData> getNodes()
        {
            return cameraNodes;
        }

        public NetworkNode<CameraData> getNode(int id)
        {
            for (int i = 0; i < cameraNodes.getNodes().Count; i++)
            {
                if (cameraNodes.getNodes()[i].value.id == id)
                    return cameraNodes.getNodes()[i];
            }
            return null;
        }

        public void addNode(NetworkNode<CameraData> node)
        {
            cameraNodes.addNode(node);
        }

        public void setupCamera()
        {
            for (int i = 0; i < cameraNodes.getNodes().Count; i++)
            {
                if (cameraNodes.getNodes()[i].value.isFirst)
                {
                    setCurrentNode(cameraNodes.getNodes()[i]);
                    Camera2D.position = cameraNodes.getNodes()[i].position;
                }
            }

            switch (cameraMode)
            {
                case tCameraMode.None:
                    break;
                case tCameraMode.FollowPlayer:
                    break;
                case tCameraMode.WorldMap:
                    break;
                case tCameraMode.Nodes:
                    lastPosition = cameraNodes.getNodes()[0].position;
                    currentPosition = lastPosition;
                    break;
            }
        }

        public void setCurrentNode(NetworkNode<CameraData> node)
        {
            currentNode = node;
            nextNode = node.getNext();
        }

        void updateFollowPlayerMode()
        {
            //Vector2 playerPosition = GamerManager.getGamerEntity(PlayerIndex.One).Player.position2D;
        }
        const float WORLDMAP_IDLE_X = 20.0f;
        const float WORLDMAP_IDLE_Y = 30.0f;
        void updateWorldMapMode()
        {
            Vector3 cameraPosition = worldMapPosition;
            float time = (float)SB.gameTime.TotalGameTime.TotalMilliseconds * 0.001f;
            cameraPosition.X += (float)Math.Sin(time * 0.75f) * WORLDMAP_IDLE_X;
            cameraPosition.Y += (float)Math.Sin(time * 2.0f) * WORLDMAP_IDLE_Y;
            cameraPosition.Z += 1400.0f;
            Camera2D.position = cameraPosition;
        }
        void updateNodesMode()
        {
            if (currentNode == null || nextNode == null) return;

            Vector3 targetPosition = nextNode.position;
            Vector3 direction = targetPosition - Camera2D.position;
            float distance = direction.Length();
            direction.Normalize();
            float distanceToAdvance = currentNode.value.speed * SB.dt * speedMultiplier;

            // always check if the camera arrives to the next node in this frame
            if (distanceToAdvance > distance)
            {
                // camera arrives to the new node
                Camera2D.position = nextNode.position;
                // after that, how much time remains to keep moving to the new next node?
                currentNode = nextNode;
                nextNode = currentNode.getNext();
                if (nextNode == null) return;
                float timeRemaining = SB.dt - ((SB.dt * distance) / distanceToAdvance);
                // get the new values to move the camera to the new next node
                targetPosition = nextNode.position;
                direction = targetPosition - Camera2D.position;
                distance = direction.Length();
                direction.Normalize();
                distanceToAdvance = currentNode.value.speed * timeRemaining;
            }
            Camera2D.position += direction * distanceToAdvance;
        }

        public void update()
        {
            // update last frame position
            lastPosition = currentPosition;

            // update the current used camera mode
            switch(cameraMode)
            {
                case tCameraMode.None:
                    break;
                case tCameraMode.FollowPlayer:
                    updateFollowPlayerMode();
                    break;
                case tCameraMode.WorldMap:
                    updateWorldMapMode();
                    break;
                case tCameraMode.Nodes:
                    updateNodesMode();
                    break;
            }

            // update current frame position
            currentPosition = Camera2D.position;
        }

        public void renderDebug()
        {
            switch (cameraMode)
            {
                case tCameraMode.None:
                    break;
                case tCameraMode.FollowPlayer:
                    break;
                case tCameraMode.WorldMap:
                    break;
                case tCameraMode.Nodes:
                    foreach (NetworkNode<CameraData> cameraNode in cameraNodes.getNodes())
                    {
                        Vector3 position = cameraNode.value.target;
                        DebugManager.Instance.addRectangle(position - new Vector3(50, 50, 0), position + new Vector3(50, 50, 0), Color.Yellow, 1.0f);

                        if (cameraNode.getNext() != null)
                        {
                            Vector3 target = cameraNode.getNext().value.target;
                            DebugManager.Instance.addLine(position, target, Color.Yellow);
                        }
                    }
                    break;
            }
        }

        public void clean()
        {
            cameraNodes.Clear();
            currentNode = null;
            nextNode = null;
        }
    }
}

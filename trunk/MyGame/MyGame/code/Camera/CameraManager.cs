﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    public struct CameraData
    {
        public static int NEXT_ID = 0;

        public Vector3 position;
        public Vector3 target;
        public int id;
        public int next;
        public bool isFirst;
        public float speed;

        public CameraData(Vector3 position, Vector3 target, int id = -1, bool isFirst = false, float speed = 50.0f)
        {
            this.position = position;
            this.target = target;
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

        public enum tCameraMode { None, FollowPlayer, Nodes }
        public tCameraMode cameraMode { set; get; }

        // node mode stuff
        NetworkNode<CameraData> currentNode;
        NetworkNode<CameraData> nextNode;
        Network<CameraData> cameraNodes = new Network<CameraData>();

        CameraData lastFrameData;
        CameraData currentFrameData;

        // returns the vector in the XY plane with origin in the last frmae camera position and ending in the current frame camera position
        public Vector3 getCameraVelocityXY()
        {
            Vector3 velocity = currentFrameData.position - lastFrameData.position;
            velocity.Z = 0.0f;
            return velocity;
        }
        public Vector2 getCameraPositionXY()
        {
            return new Vector2(Camera2D.position.X, Camera2D.position.Y);
        }
        public void loadXMLfake()
        {
            NetworkNode<CameraData> first = new NetworkNode<CameraData>(new CameraData(new Vector3(0,0,1400), new Vector3(0,0,0), 0, true));
            NetworkNode<CameraData> second = new NetworkNode<CameraData>(new CameraData(new Vector3(0, 20000, 1400), new Vector3(0, 20000, 0), 1));
            NetworkNode<CameraData> third = new NetworkNode<CameraData>(new CameraData(new Vector3(10000, 20000, 1400), new Vector3(10000, 20000, 0), 2));

            first.addLinkedNode(second);
            second.addLinkedNode(third);

            cameraNodes.addNode(first);
            cameraNodes.addNode(second);
            cameraNodes.addNode(third);

            currentNode = first;
            nextNode = second;

            Camera2D.position = cameraNodes.getNodes()[0].value.position;
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
                    Camera2D.position = cameraNodes.getNodes()[i].value.position;
                }
            }
        }

        public void setCurrentNode(NetworkNode<CameraData> node)
        {
            currentNode = node;
            nextNode = node.getNext();
        }

        void updateFollowPlayerMode()
        {
            Vector2 playerPosition = GamerManager.getGamerEntity(PlayerIndex.One).Player.position2D;
        }
        void updateNodesMode()
        {
            if (currentNode == null || nextNode == null) return;

            Vector3 targetPosition = nextNode.value.position;
            Vector3 direction = targetPosition - Camera2D.position;
            float distance = direction.Length();
            direction.Normalize();
            float distanceToAdvance = currentNode.value.speed * SB.dt;

            // always check if the camera arrives to the next node in this frame
            if (distanceToAdvance > distance)
            {
                // camera arrives to the new node
                Camera2D.position = nextNode.value.position;
                // after that, how much time remains to keep moving to the new next node?
                currentNode = nextNode;
                nextNode = currentNode.getNext();
                if (nextNode == null) return;
                float timeRemaining = SB.dt - ((SB.dt * distance) / distanceToAdvance);
                // get the new values to move the camera to the new next node
                targetPosition = nextNode.value.position;
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
            lastFrameData = currentFrameData;

            // update the current used camera mode
            switch(cameraMode)
            {
                case tCameraMode.None:
                    break;
                case tCameraMode.FollowPlayer:
                    updateFollowPlayerMode();
                    break;
                case tCameraMode.Nodes:
                    updateNodesMode();
                    break;
            }

            // update current frame position
            currentFrameData.position = Camera2D.position;
        }

        public void renderDebug()
        {
            switch (cameraMode)
            {
                case tCameraMode.None:
                    break;
                case tCameraMode.FollowPlayer:
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

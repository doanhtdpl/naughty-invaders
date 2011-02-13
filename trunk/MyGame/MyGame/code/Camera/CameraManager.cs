using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    public struct CameraData
    {
        public Vector3 position;
        public Vector3 target;

        public CameraData(Vector3 position, Vector3 target)
        {
            this.position = position;
            this.target = target;
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

        public void loadXMLfake()
        {
            NetworkNode<CameraData> first = new NetworkNode<CameraData>(new CameraData(new Vector3(0,0,1400), new Vector3(0,0,0)));
            NetworkNode<CameraData> second = new NetworkNode<CameraData>(new CameraData(new Vector3(0, 20000, 1400), new Vector3(0, 20000, 0)));
            NetworkNode<CameraData> third = new NetworkNode<CameraData>(new CameraData(new Vector3(10000, 20000, 1400), new Vector3(10000, 20000, 0)));
            first.addLinkedNode(second);
            second.addLinkedNode(third);
            cameraNodes.addNode(first);
            cameraNodes.addNode(second);
            cameraNodes.addNode(third);
            currentNode = first;
            nextNode = second;

            initialize();
        }

        public void initialize()
        {
            Camera2D.position = cameraNodes.getNodes()[0].value.position;
        }

        public void update()
        {
            if (currentNode == null || nextNode == null) return;

            // update last frame position
            lastFrameData = currentFrameData;

            float cameraSpeed = 100.0f;
            Vector3 targetPosition = nextNode.value.position;
            Vector3 direction = targetPosition - Camera2D.position;
            float distance = direction.Length();
            direction.Normalize();
            float distanceToAdvance = cameraSpeed * SB.dt;

            // always check if the camera arrives to the next node in this frame
            if (distanceToAdvance > distance)
            {
                // camera arrives to the new node
                Camera2D.position = nextNode.value.position;
                // after that, how much time remains to keep moving to the new next node?
                currentNode = nextNode;
                nextNode = currentNode.getNext();
                if (nextNode == null) return;
                float timeRemaining = SB.dt - ((SB.dt * distance)/distanceToAdvance);
                // get the new values to move the camera to the new next node
                targetPosition = nextNode.value.position;
                direction = targetPosition - Camera2D.position;
                distance = direction.Length();
                direction.Normalize();
                distanceToAdvance = cameraSpeed * timeRemaining;
            }
            Camera2D.position += direction * distanceToAdvance;

            // update current frame position
            currentFrameData.position = Camera2D.position;
        }

        public void clean()
        {
            cameraNodes.Clear();
        }
    }
}

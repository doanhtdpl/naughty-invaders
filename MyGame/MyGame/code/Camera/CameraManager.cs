using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
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

        NetworkNode<Vector3> currentNode;
        NetworkNode<Vector3> nextNode;
        Network<Vector3> cameraNodes = new Network<Vector3>();

        Vector3 lastFramePosition;
        Vector3 currentFramePosition;

        // returns the vector in the XY plane with origin in the last frmae camera position and ending in the current frame camera position
        public Vector3 getCameraVelocityXY()
        {
            Vector3 velocity = currentFramePosition - lastFramePosition;
            velocity.Z = 0.0f;
            return velocity;
        }

        public void loadXMLfake()
        {
            NetworkNode<Vector3> first = new NetworkNode<Vector3>(Vector3.Zero);
            NetworkNode<Vector3> second = new NetworkNode<Vector3>(new Vector3(0, 20000, 0));
            NetworkNode<Vector3> third = new NetworkNode<Vector3>(new Vector3(10000, 20000, 0));
            first.addLinkedNode(second);
            second.addLinkedNode(third);
            cameraNodes.addNode(first);
            cameraNodes.addNode(second);
            cameraNodes.addNode(third);
            currentNode = first;
            nextNode = second;
        }

        public void update()
        {
            if (currentNode == null || nextNode == null) return;

            // update last frame position
            lastFramePosition = currentFramePosition;

            float cameraSpeed = 100.0f;
            Vector3 targetPosition = nextNode.value;
            targetPosition.Z = Camera2D.position.Z;
            Vector3 direction = targetPosition - Camera2D.position;
            float distance = direction.Length();
            direction.Normalize();
            float distanceToAdvance = cameraSpeed * SB.dt;

            // always check if the camera arrives to the next node in this frame
            if (distanceToAdvance > distance)
            {
                // camera arrives to the new node
                float z = Camera2D.position.Z;
                Camera2D.position = nextNode.value;
                Camera2D.position.Z = z;
                // after that, how much time remains to keep moving to the new next node?
                currentNode = nextNode;
                nextNode = currentNode.getNext();
                if (nextNode == null) return;
                float timeRemaining = SB.dt - ((SB.dt * distance)/distanceToAdvance);
                // get the new values to move the camera to the new next node
                targetPosition = nextNode.value;
                targetPosition.Z = Camera2D.position.Z;
                direction = targetPosition - Camera2D.position;
                distance = direction.Length();
                direction.Normalize();
                distanceToAdvance = cameraSpeed * timeRemaining;
                Camera2D.position += direction * distanceToAdvance;
            }
            else
            {
                Camera2D.position += direction * distanceToAdvance;
            }

            // update current frame position
            currentFramePosition = Camera2D.position;
        }

        public void clean()
        {
            cameraNodes.Clear();
        }
    }
}

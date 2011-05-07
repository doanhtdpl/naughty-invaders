using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    // a network node has the value of the object it contains and a list of the nodes that points this node to
    public class NetworkNode<T>
    {
        public T value;
        public Vector3 position;
        List<NetworkNode<T>> linkedNodes = new List<NetworkNode<T>>();

        public NetworkNode(T value, Vector3 position)
        {
            this.value = value;
            this.position = position;
        }
        public void addLinkedNode(NetworkNode<T> nodeToLink)
        {
            linkedNodes.Add(nodeToLink);
        }
        public void setLinkedNode(NetworkNode<T> nodeToLink)
        {
            linkedNodes.Clear();
            linkedNodes.Add(nodeToLink);
        }
        public NetworkNode<T> getNext()
        {
            if (linkedNodes.Count > 0)
            {
                return linkedNodes[0];
            }
            return null;
        }
        const float ANGLE_CHOICE_THRESHOLD = 1.3f;
        public NetworkNode<T> getNext(Vector2 direction)
        {
            NetworkNode<T> bestChoice = null;
            float angle = Calc.directionToAngle(direction);
            float lowerDistance = Calc.TwoPi;
            for (int i = 0; i < linkedNodes.Count; ++i)
            {
                float distance = Calc.getDeltaOfAngles(angle, Calc.directionToAngle(linkedNodes[i].position.toVector2() - position.toVector2()));
                if (distance < lowerDistance)
                {
                    lowerDistance = distance;
                    bestChoice = linkedNodes[i];
                }
            }
            return bestChoice;
        }
    }

    public class Network<T>// where T:IComparable
    {
        private class compareNodes : IComparer<object>
        {
            int IComparer<object>.Compare(object a, object b)
            {
                Type t = a.GetType();
                if (a.GetType() == Type.GetType("MyGame.MenuButton"))
                {
                    if (((MenuElement)a).text == ((MenuElement)b).text)
                        return 0;
                }
                return 1;
            }
        }

        List<NetworkNode<T>> nodes = new List<NetworkNode<T>>();

        public List<NetworkNode<T>> getNodes()
        {
            return nodes;
        }
        public void addNode(T newValue, Vector3 position)
        {
            nodes.Add(new NetworkNode<T>(newValue, position));
        }
        public void addNode(NetworkNode<T> newValue)
        {
            nodes.Add(newValue);
        }
        public bool addLink(T from, T to)
        {
            NetworkNode<T> fromNode = getNode(from);
            NetworkNode<T> toNode = getNode(to);
            if (fromNode != null && toNode != null)
            {
                fromNode.addLinkedNode(toNode);
                return true;
            }
            return false;
        }
        public void addLink(NetworkNode<T> from, NetworkNode<T> to)
        {
            from.addLinkedNode(to);
        }
        public NetworkNode<T> getNode(T value)
        {
            for (int i = 0; i < nodes.Count; ++i)
            {
                if (compareNodes.Equals(value, nodes[i].value))
                {
                    return nodes[i];
                }
            }
            return null;
        }

        public void Clear()
        {
            nodes.Clear();
        }
    }
}

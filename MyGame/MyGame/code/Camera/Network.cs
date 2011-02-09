using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame
{
    // a network node has the value of the object it contains and a list of the nodes that points this node to
    public class NetworkNode<T>
    {
        public T value;
        List<NetworkNode<T>> linkedNodes = new List<NetworkNode<T>>();

        public NetworkNode(T value)
        {
            this.value = value;
        }
        public void addLinkedNode(NetworkNode<T> nodeToLink)
        {
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
    }

    public class Network<T>
    {
        List<NetworkNode<T>> nodes = new List<NetworkNode<T>>();

        public List<NetworkNode<T>> getNodes()
        {
            return nodes;
        }
        public void addNode(T newValue)
        {
            nodes.Add(new NetworkNode<T>(newValue));
        }
        public void addNode(NetworkNode<T> newValue)
        {
            nodes.Add(newValue);
        }
        public void addLink(NetworkNode<T> from, NetworkNode<T> to)
        {
            from.addLinkedNode(to);
        }

        public void Clear()
        {
            nodes.Clear();
        }
    }
}

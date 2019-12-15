using System.Collections.Generic;

namespace app.web.Models.Structs
{
    public class Graph
    {
        internal IDictionary<string, Node> Nodes { get; private set; }

        public Graph()
        {
            Nodes = new Dictionary<string, Node>();
        }

        public void AddNode(string name)
        {
            var node = new Node(name);
            Nodes.Add(name, node);
        }

        public void AddConnection(string fromNode, string toNode, double distance, bool twoWay) =>
            Nodes[fromNode].AddConnection(Nodes[toNode], distance, twoWay);
    }
}
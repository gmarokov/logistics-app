using System;
using System.Collections.Generic;
using System.Linq;
using app.web.Models.Structs;

namespace app.web.Infrastructure.Services
{
    public class DistanceCalculator
    {
        public KeyValuePair<string, double> FindClosestByNode(Graph graph, string  startingNode)
        {
            var distances = CalculateDistances(graph, startingNode);
            var closest = distances.OrderBy(k => k.Value).FirstOrDefault();
            
            if (distances.Count > 1)
            {
                distances.Remove(closest);
                closest = distances.FirstOrDefault(x => !double.IsPositiveInfinity(x.Value));
            }

            return closest;
        }

        public KeyValuePair<string, double> FindFurthest(Graph graph)
        {
            var averagesNodes = new Dictionary<string, double>();

            foreach (var node in graph.Nodes)
            {
                var distances = this.CalculateDistances(graph, node.Value.Name);

                var averageDistance = this.CalculateAverageDistance(distances);
                averagesNodes.Add(node.Value.Name, averageDistance);
            }

            var logisticsCenter = averagesNodes.FirstOrDefault(x => x.Value == averagesNodes.Values.Max());

            return logisticsCenter;
        }

        private IDictionary<string, double> CalculateDistances(Graph graph, string startingNode)
        {
            if (!graph.Nodes.Any(n => n.Key == startingNode))
                throw new ArgumentException("Starting node must be in graph.");

            InitializeGraph(graph, startingNode);
            ProcessGraph(graph, startingNode);
            return ExtractDistances(graph);
        }

        private double CalculateAverageDistance(IDictionary<string, double> distances)
        {
            var sumDistance = distances.Sum(x => x.Value);
            var averageDistance = sumDistance / (distances.Count - 1);
            return Math.Round(averageDistance, 2);
        }

        private void InitializeGraph(Graph graph, string startingNode)
        {
            foreach (Node node in graph.Nodes.Values)
                node.DistanceFromStart = double.PositiveInfinity;
            graph.Nodes[startingNode].DistanceFromStart = 0;
        }

        private void ProcessGraph(Graph graph, string startingNode)
        {
            bool finished = false;
            var queue = graph.Nodes.Values.ToList();
            while (!finished)
            {
                Node nextNode = queue.OrderBy(n => n.DistanceFromStart).FirstOrDefault(
                    n => !double.IsPositiveInfinity(n.DistanceFromStart));

                if (nextNode != null)
                {
                    ProcessNode(nextNode, queue);
                    queue.Remove(nextNode);
                }
                else
                {
                    finished = true;
                }
            }
        }

        private void ProcessNode(Node node, List<Node> queue)
        {
            var connections = node.Connections.Where(c => queue.Contains(c.Target));
            foreach (var connection in connections)
            {
                double distance = node.DistanceFromStart + connection.Distance;
                if (distance < connection.Target.DistanceFromStart)
                    connection.Target.DistanceFromStart = distance;
            }
        }

        private IDictionary<string, double> ExtractDistances(Graph graph) =>
            graph.Nodes.ToDictionary(n => n.Key, n => n.Value.DistanceFromStart);
    }
}
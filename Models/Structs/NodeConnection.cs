namespace app.web.Models.Structs
{
    internal class NodeConnection
    {
        internal Node Target { get; private set; }
        internal double Distance { get; private set; }

        internal NodeConnection(Node target, double distance)
        {
            Target = target;
            Distance = distance;
        }
    }
}
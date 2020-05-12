namespace DN.Puzzle.Color
{
    public class LineData
    {
        public Line Owner { get; private set; }
        public LineColor LineColor { get; private set; }
        public NodeData StartingNode { get; private set; }
        public NodeData EndNode { get; private set; }
        public bool CanTraverseBothWays { get; private set; }

        public LineData(
            LineColor lineColor = Color.LineColor.Blue, 
            NodeData startingNode = null,
            NodeData endNode = null,
            bool traverseBothWays = false)
        {
            LineColor = lineColor;
            StartingNode = startingNode;
            EndNode = endNode;
            CanTraverseBothWays = traverseBothWays;
        }

        public void SetOwner(Line owner)
        {
            if (!Owner)
            {
                Owner = owner;
            }
        }

#if UNITY_EDITOR
        public void ConnectNode(NodeData node, bool startingNode)
        {
            if (startingNode)
                this.StartingNode = node;
            else
                this.EndNode = node;
        }

        public void DisconnectNode(bool startingNode)
        {
            if (startingNode)
                this.StartingNode = null;
            else
                this.EndNode = null;
        }

        public void SetTraverseBothWays(bool canTraverseBothWays)
        {
            this.CanTraverseBothWays = canTraverseBothWays;
        }

        public void SetColor(LineColor lineColor)
        {
            LineColor = lineColor;
        }
#endif
    }
}
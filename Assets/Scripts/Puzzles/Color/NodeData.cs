using System.Collections.Generic;

namespace DN.Puzzle.Color
{
    public class NodeData
    {
        public Node Owner { get; private set; }
        public bool IsFinish { get; private set; }
        public IEnumerable<LineData> ConnectedLines => connectedLines;
		
        private List<LineData> connectedLines;

        public void SetOwner(Node instance)
        {
            if (!Owner)
            {
                Owner = instance;
            }
        }
		
#if UNITY_EDITOR
        public void ConnectLine(LineData line)
        {
            connectedLines.Add(line);
        }

        public void DisconnectLine(LineData line)
        {
            connectedLines.Remove(line);
        }
#endif
    }
}
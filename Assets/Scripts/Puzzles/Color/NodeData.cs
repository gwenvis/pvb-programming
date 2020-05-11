using System.Collections.Generic;

namespace DN.Puzzle.Color
{
    public class NodeData
    {
        public bool IsFinish { get; private set; }
        public IEnumerable<LineData> ConnectedLines => connectedLines;
		
        private List<LineData> connectedLines;
		
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
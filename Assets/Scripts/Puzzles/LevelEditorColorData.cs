using System.Collections.Generic;

namespace DN.Puzzle.Color
{
    /// <summary>
    /// Holds temporary data for the level editor that should later be applied to the actual
    /// level data upon saving.
    /// </summary>
    public class LevelEditorColorData
    {
        public IEnumerable<NodeData> Nodes => NodeData;
        public IEnumerable<LineData> Lines => LineData;

        private List<NodeData> NodeData { get; set; }
        private List<LineData> LineData { get; set; }
        
        public LevelEditorColorData(IEnumerable<NodeData> nodeData, IEnumerable<LineData> lineData)
        {
            LineData = new List<LineData>();
            NodeData = new List<NodeData>();

            if (nodeData != null)
                NodeData.AddRange(nodeData);

            if (lineData != null)
                LineData.AddRange(lineData);
        }

        public LineData CreateLine(string startingNodeId, string endingNodeId)
        {
            var lineData = new LineData(startingNodeId: startingNodeId, endNodeId: endingNodeId);
            return AddLine(lineData);
        }

        public LineData AddLine(LineData lineData)
        {
            LineData.Add(lineData);
            return lineData;
        }

        public NodeData CreateNode()
        {
            var nodeData = new NodeData();
            return AddNode(nodeData);
        }

        public NodeData AddNode(NodeData data)
        {
            NodeData.Add(data);
            return data;
        }

        public bool RemoveNode(NodeData data) => NodeData.Remove(data);
        public bool RemoveLine(LineData data) => LineData.Remove(data);
    }
}
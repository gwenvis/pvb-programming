using System.Collections.Generic;
using System.Linq;
using DN.Puzzle.Color;
using DN.Puzzle.Color.Editor;
using UnityEngine;

namespace DN.Puzzle.Color
{
    /// <summary>
    /// Holds the data for a specific Color Puzzle level that is
    /// then in turn loaded in by either the level editor or the color puzzle itself.
    /// </summary>
    public class ColorLevelData : LevelData
    {
        public IEnumerable<LineData> Lines => lines;
        public IEnumerable<NodeData> Nodes => nodes;
        public IEnumerable<LineColor> DraggableNodes => draggableNodes;
        [field: SerializeField, HideInInspector] public Vector2 SavedSize { get; private set; }
        
        [SerializeField] private LineData[] lines;
        [SerializeField] private NodeData[] nodes;
        [SerializeField] private List<LineColor> draggableNodes;

        #if UNITY_EDITOR
        public bool SetData(object sender, IEnumerable<NodeData> nodeData, IEnumerable<LineData> lineData, Vector2 savedSize)
        {
            if (sender.GetType() != typeof(PuzzleEditor))
            {
                return false;
            }

            lines = lineData.ToArray();
            nodes = nodeData.ToArray();
            SavedSize = savedSize;
            return true;
        }
        #endif
    }
}
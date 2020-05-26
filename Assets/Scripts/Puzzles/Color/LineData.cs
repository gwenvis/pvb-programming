using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace DN.Puzzle.Color
{
    [Serializable]
    public class LineData
    {
        public string Id => id;
        [field: NonSerialized] public Line Owner { get; private set; }
        [field: SerializeField, HideInInspector] public LineColor LineColor { get; private set; }
        [field: SerializeField, HideInInspector] public string StartingNodeId { get; private set; }
        [field: SerializeField, HideInInspector] public string EndNodeId { get; private set; }
        [field: NonSerialized] public NodeData StartingNode { get; private set; }
        [field: NonSerialized] public NodeData EndNode { get; private set; }
        [field: SerializeField, HideInInspector] public bool CanTraverseBothWays { get; private set; }

        [SerializeField, HideInInspector] private string id;
        

        public LineData(
            LineColor lineColor = Color.LineColor.Blue, 
            string startingNodeId = null,
            string endNodeId = null,
            bool traverseBothWays = false)
        {
            LineColor = lineColor;
            StartingNodeId = startingNodeId;
            EndNodeId = endNodeId;
            CanTraverseBothWays = traverseBothWays;
            
            if(string.IsNullOrWhiteSpace(id))
                id = Guid.NewGuid().ToString(); 
        }

        public void SetOwner(Line owner, IEnumerable<NodeData> nodes)
        {
            if (!Owner)
            {
                Owner = owner;
                StartingNode = nodes.FirstOrDefault(x => x.Id.Equals(StartingNodeId));
                EndNode = nodes.FirstOrDefault(x => x.Id.Equals(EndNodeId));
            }
        }

#if UNITY_EDITOR
        public void ConnectNode(string nodeId, bool startingNode)
        {
            if (startingNode)
                this.StartingNodeId = nodeId;
            else
                this.EndNodeId = nodeId;
        }

        public void DisconnectNode(bool startingNode)
        {
            if (startingNode)
                this.StartingNodeId = null;
            else
                this.EndNodeId = null;
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
        public void Clean()
        {
            StartingNode = null;
            EndNode = null;
            Owner = null;
        }
    }
}
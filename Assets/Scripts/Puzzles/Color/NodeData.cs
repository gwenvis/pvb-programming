using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace DN.Puzzle.Color
{
    [Serializable]
    public class NodeData
    {
        public string Id => id; 
        [field: NonSerialized] public Node Owner { get; private set; }
        [field: SerializeField, HideInInspector] public bool IsFinish { get; private set; }
        [field: SerializeField, HideInInspector] public bool IsStart { get; private set; }
        [field: SerializeField, HideInInspector] public Vector3 Position { get; private set; }
        public IEnumerable<LineData> ConnectedLines => connectedLines;
		
        [NonSerialized] private List<LineData> connectedLines = new List<LineData>();
        [SerializeField, HideInInspector] public string id;

        public NodeData()
        {
            if(string.IsNullOrWhiteSpace(Id))
                id = Guid.NewGuid().ToString();
        }

        public void SetOwner(Node instance)
        {
            if (!Owner)
            {
                Owner = instance;
            }
        }

        public void AddLineData(LineData line)
        {
            connectedLines.Add(line);
        }
		
#if UNITY_EDITOR
        public void SetPosition(Vector3 position)
        {
            Position = position;
        }

        public void SetIsFinish(bool isFinish)
        {
            IsFinish = isFinish;
        }

        public void SetIsStart(bool isStart)
        {
            IsStart = isStart;
        }
#endif
        public void Clean()
        {
            connectedLines.Clear();
            Owner = null;
        }
    }
}
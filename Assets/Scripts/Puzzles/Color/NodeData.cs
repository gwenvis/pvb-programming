using System;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace DN.Puzzle.Color
{
    [Serializable]
    public class NodeData
    {
        [field: SerializeField, HideInInspector] public Node Owner { get; private set; }
        [field: SerializeField, HideInInspector] public bool IsFinish { get; private set; }
        [field: SerializeField, HideInInspector] public Vector3 Position { get; private set; }
        public IEnumerable<LineData> ConnectedLines => connectedLines;
		
        [SerializeField, HideInInspector]
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

        public void SetPosition(Vector3 position)
        {
            Position = position;
        }

        public void SetIsFinish(bool isFinish)
        {
            IsFinish = isFinish;
        }
#endif
    }
}
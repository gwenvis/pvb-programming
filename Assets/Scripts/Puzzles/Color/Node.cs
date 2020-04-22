using System.Collections.Generic;
using UnityEngine;

namespace DN.Puzzle.Color
{
	/// <summary>
	/// 
	/// </summary>
	public class Node : MonoBehaviour
	{
		public IEnumerable<Line> ConnectedLines => connectedLines;

		[SerializeField] private List<Line> connectedLines;

#if UNITY_EDITOR
		public void ConnectLine(Line line)
		{
			connectedLines.Add(line);
		}

		public void DisconnectLine(Line line)
		{
			connectedLines.Remove(line);
		}
#endif

	}
}

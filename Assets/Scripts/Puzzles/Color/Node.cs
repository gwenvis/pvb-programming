using System.Collections.Generic;
using UnityEngine;

namespace DN.Puzzle.Color
{
	/// <summary>
	/// Data for which node is the finished, and what lines are connected
	/// to be used by the player.
	/// </summary>
	public class Node : MonoBehaviour
	{
		public IEnumerable<Line> ConnectedLines => connectedLines;
		public bool IsFinish => isFinish;

		[SerializeField] private List<Line> connectedLines;
		[SerializeField] private bool isFinish;

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

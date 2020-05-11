using UnityEngine;

namespace DN.Puzzle.Color
{
	/// <summary>
	/// Data for which node is the finished, and what lines are connected
	/// to be used by the player.
	/// </summary>
	public class Node : MonoBehaviour
	{
		public NodeData Data { get; private set; }

		public void InitializeNode(NodeData data)
		{
			if (Data is null)
			{
				Data = data;
			}
		}
	}
}

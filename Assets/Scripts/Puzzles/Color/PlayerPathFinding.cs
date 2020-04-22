using UnityEngine;

namespace DN.Puzzle.Color
{
	public class PlayerPathFinding : MonoBehaviour
	{ 
		/// <summary>
		/// Find the line that is requested by the color command.
		/// </summary>
		/// <param name="colorCommand">The color command requested by the player.</param>
		/// <param name="node">The node that it has to search from</param>
		/// <returns>A line if it has been found, null if there is no line.</returns>
		public Line FindLine(ColorCommand colorCommand, Node node)
		{
			foreach(Line line in node.ConnectedLines)
			{
				if ((line.StartingNode == node || line.CanTraverseBothWays) && line.LineColor == colorCommand.Color)
					return line;
			}

			return null;
		}
	}
}

using UnityEngine;

namespace DN.Puzzle.Maze.UI
{
	/// <summary>
	/// Class for the MoveForward block
	/// </summary>
	public class MoveForwardBlock : MazeDraggableItem, IMovePlayerBlock
	{
		MazeFunctions function = MazeFunctions.Forward;

		public MazeFunctions GetMazeFunction()
		{
			return function;
		}
	}
}

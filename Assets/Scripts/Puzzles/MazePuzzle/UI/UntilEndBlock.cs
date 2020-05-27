using DN.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using System.Collections.Generic;

namespace DN.Puzzle.Maze.UI
{
	/// <summary>
	/// Class for UntilEnd Block
	/// </summary>
	public class UntilEndBlock : LoopBlockDraggableItem, IMovePlayerBlock
	{
		MazeFunctions function = MazeFunctions.UntilEnd;

		public MazeFunctions GetMazeFunction()
		{
			return function;
		}
	}
}

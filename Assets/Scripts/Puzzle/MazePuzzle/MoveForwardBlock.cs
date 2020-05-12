using UnityEngine;

namespace DN.UI
{
	/// <summary>
	/// ADD CLASS SUMMARY!
	/// </summary>
	public class MoveForwardBlock : MazeDraggableItem, IMovePlayerBlock
	{
		MazeFunctions function = MazeFunctions.Forward;

		public MazeFunctions GetMazeFunctions()
		{
			return function;
		}
	}
}

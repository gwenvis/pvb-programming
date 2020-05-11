using UnityEngine;
using TMPro;

namespace DN.UI
{
	/// <summary>
	/// ADD CLASS SUMMARY!
	/// </summary>
	public class TurnBlock : MazeDraggableItem, IMovePlayerBlock
	{
		[SerializeField] private TMP_Dropdown dropdown;
		public void MovePlayer(MazePlayerMovement mazePlayerMovement)
		{
			if(dropdown.value == 0)
			{
				mazePlayerMovement.Turn(90);
			}
			else
			{
				mazePlayerMovement.Turn(-90);
			}
		}
	}
}

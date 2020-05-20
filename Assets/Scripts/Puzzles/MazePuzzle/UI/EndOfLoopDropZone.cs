using DN.UI;
using UnityEngine;

namespace DN.Puzzle.Maze.UI
{
	/// <summary>
	/// DropZone for the end of a loop block
	/// </summary>
	public class EndOfLoopDropZone : BlockDropZone
	{
		protected override void SetParent(DraggableItem droppedObject)
		{
			droppedObject.GetComponent<MazeDraggableItem>().SetParent(gameObject, gameObject.GetComponent<RectTransform>().rect.height*1.5f, gameObject.GetComponent<RectTransform>().rect.width/2);
		}
	}
}

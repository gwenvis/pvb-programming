using DN.UI;
using UnityEngine;

namespace DN.Puzzle.Maze.UI
{
	/// <summary>
	/// DropZone for the blocks
	/// </summary>
	public class BlockDropZone : MonoBehaviour, IDroppable
	{
		public DraggableItem CurrentObj { get; private set; }

		public void Drop(DraggableItem droppedObject)
		{
			if (CurrentObj == null)
			{
				droppedObject.GetComponent<MazeDraggableItem>().SetParent(gameObject, GetComponent<RectTransform>().rect.height / 2);
				CurrentObj = droppedObject;
				droppedObject.PickedUpItemEvent += OnPickedUpItemEvent;
				return;
			}
		}

		private void OnPickedUpItemEvent(DraggableItem obj)
		{
			CurrentObj.PickedUpItemEvent -= OnPickedUpItemEvent;
			CurrentObj.GetComponent<MazeDraggableItem>().SetParent(null, 0);
			CurrentObj = null;
		}
	}
}

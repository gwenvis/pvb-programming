using DN.UI;
using UnityEngine;

namespace DN.Puzzle.Maze.UI
{
	/// <summary>
	/// DropZone for the blocks
	/// </summary>
	public class BlockDropZone : MonoBehaviour, IDroppable
	{
		public int Layer { get; private set; } = 0;
		public DraggableItem CurrentObj { get; private set; }

		public void Drop(DraggableItem droppedObject)
		{
			if (CurrentObj == null)
			{
				SetParent(droppedObject);
				CurrentObj = droppedObject;
				droppedObject.PickedUpItemEvent += OnPickedUpItemEvent;
				return;
			}
		}

		protected virtual void SetParent(DraggableItem droppedObject)
		{
			droppedObject.GetComponent<MazeDraggableItem>().SetParent(gameObject, GetComponent<RectTransform>().rect.height);
		}

		private void OnPickedUpItemEvent(DraggableItem obj)
		{
			CurrentObj.PickedUpItemEvent -= OnPickedUpItemEvent;
			CurrentObj.GetComponent<MazeDraggableItem>().SetParent(null, 0);
			CurrentObj = null;
		}

		public void SetLayer(int layer)
		{
			Layer = layer;
		}

		private void OnDestroy()
		{
			if(CurrentObj != null)
				CurrentObj.PickedUpItemEvent -= OnPickedUpItemEvent;
		}
	}
}

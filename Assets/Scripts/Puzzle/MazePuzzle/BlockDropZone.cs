using UnityEngine;

namespace DN.UI
{
	/// <summary>
	/// DropZone for the blocks
	/// </summary>
	public class BlockDropZone : MonoBehaviour, IDroppable
	{
		public DraggableItem currentObj;

		public void Drop(DraggableItem droppedObject)
		{
			if (currentObj == null && droppedObject.gameObject != GetComponent<MazeDraggableItem>().parentObject)
			{
				droppedObject.GetComponent<MazeDraggableItem>().SetParent(gameObject, GetComponent<RectTransform>().rect.height / 2);
				currentObj = droppedObject;
				droppedObject.PickedUpItemEvent += OnPickedUpItemEvent;
				return;
			}
		}

		private void OnPickedUpItemEvent()
		{
			currentObj.PickedUpItemEvent -= OnPickedUpItemEvent;
			currentObj.GetComponent<MazeDraggableItem>().SetParent(null, 0);
			currentObj = null;
		}
	}
}

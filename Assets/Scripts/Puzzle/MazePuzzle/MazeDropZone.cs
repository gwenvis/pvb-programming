using System.Collections.Generic;
using UnityEngine;

namespace DN.UI
{
	/// <summary>
	/// DropZone for the maze puzzle
	/// </summary>
	public class MazeDropZone : MonoBehaviour, IDroppable
	{
		private List<DraggableItem> currentObjects = new List<DraggableItem>();

		public void Drop(DraggableItem droppedObject)
		{
			droppedObject.PickedUpItemEvent += OnPickedUpItemEvent;
			if (!currentObjects.Contains(droppedObject))
			{
				currentObjects.Add(droppedObject);
			}

			RectTransform blockTransform = droppedObject.GetComponent<RectTransform>();

			if (blockTransform.localPosition.x - blockTransform.rect.width/2 < transform.localPosition.x)
			{
				blockTransform.localPosition = new Vector2(transform.localPosition.x+ blockTransform.rect.width / 2, blockTransform.localPosition.y);
			}
		}

		private void OnPickedUpItemEvent()
		{
			//currentObj.PickedUpItemEvent -= OnPickedUpItemEvent;
			//currentObj = null;
		}
	}
}

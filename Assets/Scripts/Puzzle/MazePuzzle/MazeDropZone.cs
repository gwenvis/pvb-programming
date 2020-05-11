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

		public (int, MazeDraggableItem) GetGameObjectWithMostChilds()
		{
			if (currentObjects.Count == 0)
				return (0, null);

			int highestChildCount = -1;
			DraggableItem mostChildsObject = currentObjects[0];
			foreach (DraggableItem draggableItem in currentObjects)
			{
				bool FoundLastChild = false;
				DraggableItem currentDraggableItem = draggableItem;

				for(int i = 0; !FoundLastChild || i >= 100; i++)
				{
					if(currentDraggableItem.GetComponent<BlockDropZone>().CurrentObj != null)
					{
						currentDraggableItem = currentDraggableItem.GetComponent<BlockDropZone>().CurrentObj;
					}
					else
					{
						if(highestChildCount < i)
						{
							highestChildCount = i;
							mostChildsObject = currentDraggableItem;
							FoundLastChild = true;
						}
					}
				}
			}

			return (highestChildCount, mostChildsObject as MazeDraggableItem);
		}
	}
}

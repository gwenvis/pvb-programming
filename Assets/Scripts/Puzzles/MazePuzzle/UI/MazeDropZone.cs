using System.Collections.Generic;
using UnityEngine;
using DN.UI;

namespace DN.Puzzle.Maze.UI
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
				(droppedObject as MazeDraggableItem).IsDestroyedEvent += OnDestroyedEvent;
			}

			RectTransform blockTransform = droppedObject.GetComponent<RectTransform>();

			if (blockTransform.localPosition.x - blockTransform.rect.width/2 < transform.localPosition.x)
			{
				blockTransform.localPosition = new Vector2(transform.localPosition.x+ blockTransform.rect.width / 2, blockTransform.localPosition.y);
			}
		}

		private void OnDestroyedEvent(MazeDraggableItem item)
		{
			CheckCurrentObjects();
			item.IsDestroyedEvent -= OnDestroyedEvent;
		}

		public void DestroyAllBlocks()
		{
			CheckCurrentObjects();
			for (int i = 0; i < currentObjects.Count; i++)
			{
				Destroy(currentObjects[i].gameObject);
				SpawnBlock.DeleteBlock();
			}
			currentObjects = new List<DraggableItem>();
		}

		public void CheckCurrentObjects()
		{
			currentObjects.RemoveAll(x => x == null);
		}

		public (int, MazeDraggableItem) GetGameObjectWithMostChilds()
		{
			CheckCurrentObjects();
			if (currentObjects.Count == 0)
				return (0, null);

			int highestChildCount = 0;
			DraggableItem mostChildsObject = currentObjects[0];
			foreach (DraggableItem draggableItem in currentObjects)
			{
				bool FoundLastChild = false;
				DraggableItem currentDraggableItem = draggableItem;

				for(int i = 1; !FoundLastChild || i >= 100; i++)
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
							mostChildsObject = draggableItem;
							FoundLastChild = true;
						}
						FoundLastChild = true;
					}
				}
			}

			return (highestChildCount, mostChildsObject as MazeDraggableItem);
		}

		private void OnDestroy()
		{
			foreach(DraggableItem item in currentObjects)
				(item as MazeDraggableItem).IsDestroyedEvent -= OnDestroyedEvent;
		}
	}
}

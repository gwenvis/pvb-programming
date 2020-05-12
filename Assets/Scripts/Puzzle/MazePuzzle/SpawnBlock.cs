using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DN.UI
{
	/// <summary>
	/// Spawns Block when clicked.
	/// </summary>
	public class SpawnBlock : MonoBehaviour, IDragHandler
	{
		public static int SpawnedBlocks { get; private set; } = 0;
		public static event Action<int> ChangedBlocksEvent;
		[SerializeField]private GameObject draggableObject;
		private DraggableItem draggableItem;
		private Canvas canvas;

		public void SetDraggableItem(GameObject draggableItem)
		{
			draggableObject = draggableItem;
		}

		public void SetCanvas(Canvas newCanvas)
		{
			canvas = newCanvas;
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (SpawnedBlocks >= LoadLevel.MaxBlocks)
				return;
			GameObject block = Instantiate(draggableObject, transform.position, transform.rotation, canvas.transform);
			draggableItem = block.GetComponent<DraggableItem>();
			draggableItem.SetCanvas(canvas);
			eventData.pointerDrag = block;
			SpawnedBlocks++;
			ChangedBlocksEvent?.Invoke(SpawnedBlocks);
		}

		public static void DeleteBlock()
		{
			SpawnedBlocks--;
			ChangedBlocksEvent?.Invoke(SpawnedBlocks);
		}
	}
}

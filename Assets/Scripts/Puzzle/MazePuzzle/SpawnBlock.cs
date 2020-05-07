using UnityEngine;
using UnityEngine.EventSystems;

namespace DN.UI
{
	/// <summary>
	/// Spawns Block when clicked.
	/// </summary>
	public class SpawnBlock : MonoBehaviour, IDragHandler
	{
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
			GameObject block = Instantiate(draggableObject, transform.position, transform.rotation, canvas.transform);
			draggableItem = block.GetComponent<DraggableItem>();
			draggableItem.SetCanvas(canvas);
			eventData.pointerDrag = block;
		}
	}
}
